const { Router } = require('express');
const crypto = require('crypto');
const { pool } = require('../config/db');
const { requiereRol } = require('../utilidades/autenticacion');
const { publicarEvento } = require('../utilidades/eventos');

const router = Router();

const VIGENCIA_TOKEN_MS = 60 * 1000;

function generarToken() {
  return {
    token: crypto.randomBytes(24).toString('base64url'),
    token_valido_hasta: new Date(Date.now() + VIGENCIA_TOKEN_MS)
  };
}

// Código corto de 6 dígitos para alumnos sin cámara. Se genera junto
// al token QR y vence con él. La unicidad se garantiza solo entre
// clases 'en_curso' (los códigos se reciclan en el tiempo).
async function generarCodigoUnico() {
  for (let intento = 0; intento < 10; intento++) {
    const codigo = String(crypto.randomInt(0, 1000000)).padStart(6, '0');
    const [[existe]] = await pool.query(
      "SELECT 1 AS ok FROM clase WHERE codigo = ? AND estado = 'en_curso' LIMIT 1",
      [codigo]
    );
    if (!existe) return codigo;
  }
  throw new Error('No se pudo generar un código único para la clase.');
}

const SELECT_BASE = `
  SELECT c.id_clase, c.id_dictado, c.fecha_clase, c.estado,
         NULL AS token, NULL AS token_valido_hasta, c.abierta_por,
         m.nombre_materia, p.apellido_profesor,
         d.grupo, d.dia, d.horario, d.horario_fin, d.anio_lectivo
  FROM clase c
  JOIN dictado d ON d.id_dictado = c.id_dictado
  JOIN materia m ON m.id_materia = d.id_materia
  LEFT JOIN profesor p ON p.id_profesor = d.id_profesor`;

async function claseCompleta(idClase, conToken) {
  const columnas = conToken
    ? 'token, token_valido_hasta, codigo,'
    : 'NULL AS token, NULL AS token_valido_hasta, NULL AS codigo,';
  const [filas] = await pool.query(
    `SELECT c.id_clase, c.id_dictado, c.fecha_clase, c.estado,
            ${columnas}
            c.abierta_por,
            m.nombre_materia, p.apellido_profesor,
            d.grupo, d.dia, d.horario, d.horario_fin, d.anio_lectivo
     FROM clase c
     JOIN dictado d ON d.id_dictado = c.id_dictado
     JOIN materia m ON m.id_materia = d.id_materia
     LEFT JOIN profesor p ON p.id_profesor = d.id_profesor
     WHERE c.id_clase = ?`,
    [idClase]
  );
  return filas[0] || null;
}

// GET /api/clases?fecha=YYYY-MM-DD  (lista; opcional filtro por fecha)
router.get('/', async (req, res) => {
  try {
    const condiciones = [];
    const parametros = [];

    if (req.query.fecha) {
      condiciones.push('c.fecha_clase = ?');
      parametros.push(req.query.fecha);
    }

    if (req.query.id_dictado) {
      condiciones.push('c.id_dictado = ?');
      parametros.push(Number(req.query.id_dictado));
    }

    const where = condiciones.length ? 'WHERE ' + condiciones.join(' AND ') : '';

    const [filas] = await pool.query(
      `${SELECT_BASE}
       ${where}
       ORDER BY c.fecha_clase DESC, d.grupo, m.nombre_materia`,
      parametros
    );

    res.json(filas);
  } catch (error) {
    res.status(500).json({ error: 'Error consultando clases', detalle: error.message });
  }
});

router.get('/:id', requiereRol(['profesor', 'preceptor', 'alumno']), async (req, res) => {
  const idClase = Number(req.params.id);

  if (!Number.isInteger(idClase) || idClase <= 0) {
    return res.status(400).json({ error: 'id_clase inválido.' });
  }

  try {
    const [[info]] = await pool.query(
      `SELECT c.id_clase, c.id_dictado, d.id_profesor, d.id_preceptor
       FROM clase c
       JOIN dictado d ON d.id_dictado = c.id_dictado
       WHERE c.id_clase = ?`,
      [idClase]
    );

    if (!info) return res.status(404).json({ error: 'La clase no existe.' });

    if (req.usuario.rol === 'profesor' && info.id_profesor !== req.usuario.id_profesor) {
      return res.status(403).json({ error: 'La clase no pertenece a tu usuario.' });
    }

    if (req.usuario.rol === 'preceptor' && info.id_preceptor !== req.usuario.id_preceptor) {
      return res.status(403).json({ error: 'No estás asignado a esta clase.' });
    }

    if (req.usuario.rol === 'alumno') {
      const [[inscripto]] = await pool.query(
        'SELECT 1 AS ok FROM inscribe WHERE id_alumno = ? AND id_dictado = ? LIMIT 1',
        [req.usuario.id_alumno, info.id_dictado]
      );
      if (!inscripto) return res.status(403).json({ error: 'No estás inscripto en esta clase.' });
    }

    res.json(await claseCompleta(idClase, req.usuario.rol === 'profesor'));
  } catch (error) {
    res.status(500).json({ error: 'Error consultando la clase', detalle: error.message });
  }
});

// POST /api/clases  body: { id_dictado, fecha }
// Abre la clase del dictado para esa fecha (la crea si no existe).
router.post('/', requiereRol(['profesor']), async (req, res) => {
  const { id_dictado, fecha } = req.body || {};

  if (!Number.isInteger(id_dictado) || id_dictado <= 0) {
    return res.status(400).json({ error: 'id_dictado debe ser un entero positivo.' });
  }

  if (typeof fecha !== 'string' || !/^\d{4}-\d{2}-\d{2}$/.test(fecha)) {
    return res.status(400).json({ error: 'fecha debe tener formato YYYY-MM-DD.' });
  }

  try {
    const [[dictado]] = await pool.query(
      'SELECT id_profesor FROM dictado WHERE id_dictado = ?',
      [id_dictado]
    );

    if (!dictado) return res.status(404).json({ error: 'El dictado no existe.' });
    if (dictado.id_profesor !== req.usuario.id_profesor) {
      return res.status(403).json({ error: 'El dictado no pertenece a tu usuario.' });
    }

    const abiertaPor = req.usuario.id_profesor;
    const tokenNuevo = generarToken();
    const codigoNuevo = await generarCodigoUnico();

    const [existentes] = await pool.query(
      'SELECT id_clase FROM clase WHERE id_dictado = ? AND fecha_clase = ?',
      [id_dictado, fecha]
    );

    let idClase;
    if (existentes.length > 0) {
      idClase = existentes[0].id_clase;
      await pool.query(
        `UPDATE clase
         SET estado = 'en_curso', token = ?, token_valido_hasta = ?,
             codigo = ?, abierta_por = ?
         WHERE id_clase = ?`,
        [tokenNuevo.token, tokenNuevo.token_valido_hasta, codigoNuevo, abiertaPor, idClase]
      );
    } else {
      const [resultado] = await pool.query(
        `INSERT INTO clase (id_dictado, fecha_clase, estado, token, token_valido_hasta, codigo, abierta_por)
         VALUES (?, ?, 'en_curso', ?, ?, ?, ?)`,
        [id_dictado, fecha, tokenNuevo.token, tokenNuevo.token_valido_hasta, codigoNuevo, abiertaPor]
      );
      idClase = resultado.insertId;
    }

    const claseActualizada = await claseCompleta(idClase, true);
    publicarEvento({
      tipo: 'clase.abierta',
      roles: ['profesor', 'preceptor', 'alumno'],
      id_clase: idClase,
      id_dictado
    });

    res.status(existentes.length > 0 ? 200 : 201).json(claseActualizada);
  } catch (error) {
    res.status(500).json({ error: 'Error abriendo la clase', detalle: error.message });
  }
});

// PATCH /api/clases/:id  body: { estado }
router.patch('/:id', requiereRol(['preceptor']), async (req, res) => {
  const idClase = Number(req.params.id);
  const { estado } = req.body || {};

  if (!Number.isInteger(idClase) || idClase <= 0) {
    return res.status(400).json({ error: 'id_clase inválido.' });
  }

  if (estado !== 'cerrada') {
    return res.status(403).json({ error: 'Solo se permite cerrar una clase.' });
  }

  try {
    const [[clase]] = await pool.query(
      `SELECT c.id_clase, d.id_preceptor
       FROM clase c
       JOIN dictado d ON d.id_dictado = c.id_dictado
       WHERE c.id_clase = ?`,
      [idClase]
    );

    if (!clase) return res.status(404).json({ error: 'La clase no existe.' });
    if (clase.id_preceptor !== req.usuario.id_preceptor) {
      return res.status(403).json({ error: 'No estás asignado a esta clase.' });
    }

    const [resultado] = await pool.query(
      `UPDATE clase SET estado = ?
       WHERE id_clase = ?`,
      [estado, idClase]
    );

    if (resultado.affectedRows === 0) {
      return res.status(404).json({ error: 'La clase no existe.' });
    }

    const claseActualizada = await claseCompleta(idClase, false);
    publicarEvento({
      tipo: 'clase.cerrada',
      roles: ['profesor', 'preceptor', 'alumno'],
      id_clase: idClase
    });

    res.json(claseActualizada);
  } catch (error) {
    res.status(500).json({ error: 'Error actualizando la clase', detalle: error.message });
  }
});

// POST /api/clases/:id/token  rota el token QR (visto y expiración)
router.post('/:id/token', requiereRol(['profesor']), async (req, res) => {
  const idClase = Number(req.params.id);

  if (!Number.isInteger(idClase) || idClase <= 0) {
    return res.status(400).json({ error: 'id_clase inválido.' });
  }

  try {
    const [[clase]] = await pool.query(
      `SELECT c.estado, d.id_profesor
       FROM clase c
       JOIN dictado d ON d.id_dictado = c.id_dictado
       WHERE c.id_clase = ?`,
      [idClase]
    );

    if (!clase) return res.status(404).json({ error: 'La clase no existe.' });
    if (clase.id_profesor !== req.usuario.id_profesor) {
      return res.status(403).json({ error: 'La clase no pertenece a tu usuario.' });
    }
    if (clase.estado !== 'en_curso') {
      return res.status(409).json({ error: 'La clase no está en curso.' });
    }

    const tokenNuevo = generarToken();
    const codigoNuevo = await generarCodigoUnico();
    await pool.query(
      'UPDATE clase SET token = ?, token_valido_hasta = ?, codigo = ? WHERE id_clase = ?',
      [tokenNuevo.token, tokenNuevo.token_valido_hasta, codigoNuevo, idClase]
    );

    const claseActualizada = await claseCompleta(idClase, true);
    publicarEvento({
      tipo: 'qr.rotado',
      roles: ['profesor', 'preceptor', 'alumno'],
      id_clase: idClase
    });

    res.json(claseActualizada);
  } catch (error) {
    res.status(500).json({ error: 'Error rotando el token', detalle: error.message });
  }
});

// Núcleo compartido: marca presente al alumno en la clase.
// Lo usan tanto el escaneo QR como el ingreso con código corto.
async function registrarPresencia(idClase, idDictado, idAlumno) {
  const [[inscripto]] = await pool.query(
    'SELECT 1 AS ok FROM inscribe WHERE id_alumno = ? AND id_dictado = ? LIMIT 1',
    [idAlumno, idDictado]
  );

  if (!inscripto) {
    const error = new Error('El alumno no está inscripto en este dictado.');
    error.status = 403;
    throw error;
  }

  const [existentes] = await pool.query(
    'SELECT id_asistencia, presente FROM asistencia WHERE id_clase = ? AND id_alumno = ?',
    [idClase, idAlumno]
  );

  if (existentes.length > 0) {
    if (existentes[0].presente === 1) {
      return {
        ok: true,
        id_asistencia: existentes[0].id_asistencia,
        ya_registrado: true
      };
    }

    await pool.query(
      'UPDATE asistencia SET presente = 1, verificada = 0 WHERE id_asistencia = ?',
      [existentes[0].id_asistencia]
    );
    publicarEvento({
      tipo: 'asistencia.registrada',
      roles: ['preceptor', 'alumno'],
      id_clase: idClase,
      id_alumno: idAlumno,
      actualizado: true
    });
    return {
      ok: true,
      id_asistencia: existentes[0].id_asistencia,
      ya_registrado: false,
      actualizado: true
    };
  }

  const [resultado] = await pool.query(
    'INSERT INTO asistencia (id_clase, id_alumno, presente, verificada) VALUES (?, ?, 1, 0)',
    [idClase, idAlumno]
  );

  publicarEvento({
    tipo: 'asistencia.registrada',
    roles: ['preceptor', 'alumno'],
    id_clase: idClase,
    id_alumno: idAlumno,
    actualizado: false
  });

  return { ok: true, id_asistencia: resultado.insertId, ya_registrado: false };
}

// POST /api/clases/ingresar  body: { codigo }
// El alumno ingresa el código corto de 6 dígitos que muestra el profesor
// (alternativa al QR para quienes no pueden escanear).
router.post('/ingresar', requiereRol(['alumno']), async (req, res) => {
  const { codigo } = req.body || {};
  const idAlumno = req.usuario.id_alumno;

  if (typeof codigo !== 'string' || !/^\d{6}$/.test(codigo.trim())) {
    return res.status(400).json({ error: 'Código inválido. Ingresá los 6 dígitos que muestra el profesor.' });
  }

  if (!Number.isInteger(idAlumno) || idAlumno <= 0) {
    return res.status(401).json({ error: 'Sesión de alumno inválida.' });
  }

  try {
    const [[clase]] = await pool.query(
      `SELECT c.id_clase, c.estado, c.token_valido_hasta, c.id_dictado
       FROM clase c WHERE c.codigo = ? AND c.estado = 'en_curso' LIMIT 1`,
      [codigo.trim()]
    );

    if (!clase) {
      return res.status(403).json({ error: 'Código inválido o vencido. Pedile al profesor el código actual.' });
    }

    if (!clase.token_valido_hasta || new Date(clase.token_valido_hasta) < new Date()) {
      return res.status(403).json({ error: 'Código vencido. Pedile al profesor que genere uno nuevo.' });
    }

    res.json(await registrarPresencia(clase.id_clase, clase.id_dictado, idAlumno));
  } catch (error) {
    if (error.status) return res.status(error.status).json({ error: error.message });
    res.status(500).json({ error: 'Error registrando asistencia', detalle: error.message });
  }
});

// POST /api/clases/:id/escanear  body: { token }
// El alumno escanea el QR de la clase y queda marcado presente (sin verificar).
router.post('/:id/escanear', requiereRol(['alumno']), async (req, res) => {
  const idClase = Number(req.params.id);
  const { token } = req.body || {};
  const idAlumno = req.usuario.id_alumno;

  if (!Number.isInteger(idClase) || idClase <= 0) {
    return res.status(400).json({ error: 'id_clase inválido.' });
  }

  if (typeof token !== 'string' || !token) {
    return res.status(400).json({ error: 'token requerido.' });
  }

  if (!Number.isInteger(idAlumno) || idAlumno <= 0) {
    return res.status(401).json({ error: 'Sesión de alumno inválida.' });
  }

  try {
    const [[clase]] = await pool.query(
      `SELECT c.id_clase, c.estado, c.token, c.token_valido_hasta, c.id_dictado
       FROM clase c WHERE c.id_clase = ?`,
      [idClase]
    );

    if (!clase) return res.status(404).json({ error: 'La clase no existe.' });
    if (clase.estado !== 'en_curso') {
      return res.status(409).json({ error: 'La clase no está en curso.' });
    }

    if (clase.token !== token) {
      return res.status(403).json({ error: 'Token inválido o vencido. Rotá el QR y reintentá.' });
    }

    if (!clase.token_valido_hasta || new Date(clase.token_valido_hasta) < new Date()) {
      return res.status(403).json({ error: 'Token vencido. Rotá el QR y reintentá.' });
    }

    res.json(await registrarPresencia(clase.id_clase, clase.id_dictado, idAlumno));
  } catch (error) {
    if (error.status) return res.status(error.status).json({ error: error.message });
    res.status(500).json({ error: 'Error registrando asistencia', detalle: error.message });
  }
});

module.exports = router;