const { Router } = require('express');
const { pool } = require('../config/db');
const { validarRegistro } = require('../utilidades/asistencia');
const { requiereRol } = require('../utilidades/autenticacion');
const { publicarEvento } = require('../utilidades/eventos');

const router = Router();

async function errorAccesoPreceptor(idClase, idPreceptor) {
  const [[clase]] = await pool.query(
    `SELECT c.id_clase, c.estado, d.id_preceptor
     FROM clase c
     JOIN dictado d ON d.id_dictado = c.id_dictado
     WHERE c.id_clase = ?`,
    [idClase]
  );

  if (!clase) return { status: 404, error: 'La clase no existe.' };
  if (clase.id_preceptor !== idPreceptor) {
    return { status: 403, error: 'No estás asignado a esta clase.' };
  }
  if (clase.estado !== 'en_curso') {
    return { status: 409, error: 'La clase está cerrada y su asistencia ya no puede modificarse.' };
  }

  return null;
}

// GET /api/asistencia?id_clase=N
// Consulta de asistencia de una clase, con datos del alumno y del dictado.
// Si no se pasa id_clase pero sí fecha + id_dictado, resuelve la clase.
router.get('/', async (req, res) => {
  try {
    let idClase = Number(req.query.id_clase);

    if (!idClase && req.query.fecha && req.query.id_dictado) {
      const [[clase]] = await pool.query(
        'SELECT id_clase FROM clase WHERE fecha_clase = ? AND id_dictado = ?',
        [req.query.fecha, Number(req.query.id_dictado)]
      );
      idClase = clase ? clase.id_clase : -1;
    }

    if (!Number.isInteger(idClase) || idClase <= 0) {
      return res.status(400).json({ error: 'Se requiere id_clase válido.' });
    }

    const [filas] = await pool.query(
      `SELECT a.id_asistencia, a.presente, a.verificada,
              al.id_alumno, al.apellido_alumno, al.nombre_alumno, al.legajo_alumno,
              c.id_clase, c.fecha_clase, c.estado,
              d.id_dictado, m.nombre_materia, d.grupo, d.dia, d.horario,
              p.apellido_profesor
       FROM asistencia a
       JOIN alumno al ON al.id_alumno = a.id_alumno
       JOIN clase c ON c.id_clase = a.id_clase
       JOIN dictado d ON d.id_dictado = c.id_dictado
       LEFT JOIN materia m ON m.id_materia = d.id_materia
       LEFT JOIN profesor p ON p.id_profesor = d.id_profesor
       WHERE a.id_clase = ?
       ORDER BY al.apellido_alumno, al.nombre_alumno`,
      [idClase]
    );

    res.json(filas);
  } catch (error) {
    res.status(500).json({ error: 'Error consultando asistencia', detalle: error.message });
  }
});

// POST /api/asistencia  body: { id_clase, id_alumno, presente, verificada? }
router.post('/', requiereRol(['preceptor']), async (req, res) => {
  const errores = validarRegistro(req.body);
  if (errores) return res.status(400).json({ error: errores });

  const { id_clase, id_alumno, presente, verificada } = req.body;

  try {
    const errorAcceso = await errorAccesoPreceptor(id_clase, req.usuario.id_preceptor);
    if (errorAcceso) return res.status(errorAcceso.status).json({ error: errorAcceso.error });

    const resultado = await registrarAsistencia({
      id_clase, id_alumno, presente,
      verificada: verificada === undefined ? 0 : verificada
    });

    publicarEvento({
      tipo: 'asistencia.actualizada',
      roles: ['profesor', 'preceptor', 'alumno'],
      id_clase,
      id_alumno
    });

    res.status(201).json(resultado);
  } catch (error) {
    res.status(500).json({ error: 'Error registrando asistencia', detalle: error.message });
  }
});

// POST /api/asistencia/lote
// Marca la asistencia de varios alumnos en la misma clase.
// Body: { id_clase, registros: [{ id_alumno, presente, verificada? }, ...] }
router.post('/lote', requiereRol(['preceptor']), async (req, res) => {
  const { id_clase, registros } = req.body || {};

  if (!Number.isInteger(id_clase) || id_clase <= 0) {
    return res.status(400).json({ error: 'Se requiere id_clase válido.' });
  }

  if (!Array.isArray(registros) || registros.length === 0) {
    return res.status(400).json({ error: 'Se requiere una lista no vacía de registros.' });
  }

  try {
    const errorAcceso = await errorAccesoPreceptor(id_clase, req.usuario.id_preceptor);
    if (errorAcceso) return res.status(errorAcceso.status).json({ error: errorAcceso.error });
  } catch (error) {
    return res.status(500).json({ error: 'Error verificando la clase', detalle: error.message });
  }

  const procesados = [];
  const errores = [];

  for (const registro of registros) {
    const validacion = validarRegistro({
      id_alumno: registro.id_alumno,
      id_clase,
      presente: registro.presente,
      verificada: registro.verificada
    });

    if (validacion) {
      errores.push({ id_alumno: registro.id_alumno, error: validacion });
      continue;
    }

    try {
      const resultado = await registrarAsistencia({
        id_clase,
        id_alumno: registro.id_alumno,
        presente: registro.presente,
        verificada: registro.verificada === undefined ? 0 : registro.verificada
      });
      procesados.push(resultado);
    } catch (error) {
      errores.push({ id_alumno: registro.id_alumno, error: error.message });
    }
  }

  if (procesados.length > 0) {
    publicarEvento({
      tipo: 'asistencia.actualizada',
      roles: ['profesor', 'preceptor', 'alumno'],
      id_clase,
      id_alumnos: procesados.map((registro) => registro.id_alumno)
    });
  }

  res.status(200).json({ procesados, errores });
});

// Inserta o actualiza (upsert) una asistencia por alumno + clase.
async function registrarAsistencia({ id_clase, id_alumno, presente, verificada }) {
  const [existentes] = await pool.query(
    'SELECT id_asistencia FROM asistencia WHERE id_clase = ? AND id_alumno = ?',
    [id_clase, id_alumno]
  );

  if (existentes.length > 0) {
    await pool.query(
      'UPDATE asistencia SET presente = ?, verificada = ? WHERE id_asistencia = ?',
      [presente, verificada, existentes[0].id_asistencia]
    );
    return { id_asistencia: existentes[0].id_asistencia, id_alumno, actualizado: true };
  }

  const [resultado] = await pool.query(
    'INSERT INTO asistencia (id_clase, id_alumno, presente, verificada) VALUES (?, ?, ?, ?)',
    [id_clase, id_alumno, presente, verificada]
  );

  return { id_asistencia: resultado.insertId, id_alumno, actualizado: false };
}

module.exports = router;