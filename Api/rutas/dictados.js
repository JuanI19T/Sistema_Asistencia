const { Router } = require('express');
const { pool } = require('../config/db');
const { requiereRol } = require('../utilidades/autenticacion');

const router = Router();

const DIAS = ['DOMINGO', 'LUNES', 'MARTES', 'MIERCOLES', 'JUEVES', 'VIERNES', 'SABADO'];

// Offset fijo de Argentina respecto de UTC (sin horario de verano).
const OFFSET_ARGENTINA_MINUTOS = -180;

function sinAcentos(texto) {
  return (texto || '')
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '')
    .toUpperCase()
    .trim();
}

// Devuelve la fecha/hora en Argentina calculada a partir del instante UTC,
// sin depender de la zona horaria configurada en el servidor.
function ahoraEnArgentina() {
  return new Date(Date.now() + OFFSET_ARGENTINA_MINUTOS * 60 * 1000);
}

function fechaDeHoy() {
  const ahora = ahoraEnArgentina();
  const mes = String(ahora.getUTCMonth() + 1).padStart(2, '0');
  const dia = String(ahora.getUTCDate()).padStart(2, '0');
  return `${ahora.getUTCFullYear()}-${mes}-${dia}`;
}

function minutosDeHora(hora) {
  if (!hora || typeof hora !== 'string') return null;
  const [h, m] = hora.split(':').map(Number);
  if (Number.isNaN(h) || Number.isNaN(m)) return null;
  return h * 60 + m;
}

function minutosAhora() {
  const ahora = ahoraEnArgentina();
  return ahora.getUTCHours() * 60 + ahora.getUTCMinutes();
}

function diaDeHoy() {
  return DIAS[ahoraEnArgentina().getUTCDay()];
}

// Calcula el estado de la clase de hoy para el alumno.
// Prioridad: asistencia registrada > clase en curso > vencida > pendiente.
function calcularEstado(fila, minutosActual) {
  if (fila.id_asistencia && fila.presente === 1) return 'asistida';
  if (fila.estado_clase === 'en_curso') return 'en_curso';

  const inicio = minutosDeHora(fila.horario);
  const fin = minutosDeHora(fila.horario_fin);

  if (inicio !== null && minutosActual < inicio) return 'pendiente';
  if (fin !== null && minutosActual >= fin) return 'vencida';
  if (inicio !== null && minutosActual >= inicio && (fin === null || minutosActual < fin)) {
    return 'en_curso';
  }
  return 'pendiente';
}

// GET /api/dictados - lista los dictados (materia + profesor + grupo)
router.get('/', async (req, res) => {
  try {
    const [filas] = await pool.query(
      `SELECT d.id_dictado, d.dia, d.horario, d.horario_fin, d.grupo, d.division, d.anio_lectivo,
               m.id_materia, m.nombre_materia, m.anio_materia,
              p.id_profesor, p.apellido_profesor, p.nombre_profesor
       FROM dictado d
       JOIN materia m ON m.id_materia = d.id_materia
       LEFT JOIN profesor p ON p.id_profesor = d.id_profesor
       ORDER BY d.anio_lectivo, d.grupo, m.nombre_materia`
    );
    res.json(filas);
  } catch (error) {
    res.status(500).json({ error: 'Error consultando dictados', detalle: error.message });
  }
});

// GET /api/dictados/alumno/hoy - clases de hoy del alumno autenticado con su estado
router.get('/alumno/hoy', requiereRol(['alumno']), async (req, res) => {
  const idAlumno = req.usuario.id_alumno;

  if (!Number.isInteger(idAlumno) || idAlumno <= 0) {
    return res.status(401).json({ error: 'Sesión de alumno inválida.' });
  }

  try {
    const hoy = fechaDeHoy();
    const diaHoy = diaDeHoy();
    const minutosActual = minutosAhora();

    const [filas] = await pool.query(
       `SELECT d.id_dictado, d.dia, d.horario, d.horario_fin, d.grupo, d.division, d.anio_lectivo,
              m.nombre_materia, p.apellido_profesor,
              c.id_clase, c.fecha_clase, c.estado AS estado_clase,
              a.id_asistencia, a.presente, a.verificada
       FROM inscribe i
       JOIN dictado d ON d.id_dictado = i.id_dictado
       JOIN materia m ON m.id_materia = d.id_materia
       LEFT JOIN profesor p ON p.id_profesor = d.id_profesor
       LEFT JOIN clase c ON c.id_dictado = d.id_dictado AND c.fecha_clase = ?
       LEFT JOIN asistencia a ON a.id_clase = c.id_clase AND a.id_alumno = ?
       WHERE i.id_alumno = ?
       ORDER BY d.horario`,
      [hoy, idAlumno, idAlumno]
    );

    const clases = filas
      .filter((fila) => sinAcentos(fila.dia) === diaHoy)
      .map((fila) => ({
        id_dictado: fila.id_dictado,
        id_clase: fila.id_clase,
        dia: fila.dia,
        horario: fila.horario,
        horario_fin: fila.horario_fin,
        grupo: fila.grupo,
        anio_lectivo: fila.anio_lectivo,
        nombre_materia: fila.nombre_materia,
        apellido_profesor: fila.apellido_profesor,
        estado: calcularEstado(fila, minutosActual)
      }));

    res.json({ fecha: hoy, dia: diaHoy, clases });
  } catch (error) {
    res.status(500).json({ error: 'Error consultando las clases de hoy', detalle: error.message });
  }
});

// GET /api/dictados/profesor/hoy - clases de hoy del profesor con su estado
router.get('/profesor/hoy', requiereRol(['profesor']), async (req, res) => {
  try {
    const hoy = fechaDeHoy();
    const diaHoy = diaDeHoy();
    const minutosActual = minutosAhora();

    const [filas] = await pool.query(
      `SELECT d.id_dictado, d.dia, d.horario, d.horario_fin, d.grupo, d.division, d.anio_lectivo,
              m.nombre_materia, p.apellido_profesor,
               c.id_clase, c.estado AS estado_clase, c.abierta_en,
              EXISTS (SELECT 1 FROM asistencia a WHERE a.id_clase = c.id_clase) AS tiene_asistencia
       FROM dictado d
       JOIN materia m ON m.id_materia = d.id_materia
       JOIN especialidad e ON e.id_especialidad = m.id_especialidad
       LEFT JOIN profesor p ON p.id_profesor = d.id_profesor
       LEFT JOIN clase c ON c.id_dictado = d.id_dictado AND c.fecha_clase = ?
       WHERE d.id_profesor = ?
       ORDER BY d.horario`,
      [hoy, req.usuario.id_profesor]
    );

    const clases = filas
      .filter((fila) => sinAcentos(fila.dia) === diaHoy)
      .map((fila) => {
        const inicio = minutosDeHora(fila.horario);
        const fin = minutosDeHora(fila.horario_fin);
        let estado = 'pendiente';

        if (fila.tiene_asistencia && fin !== null && minutosActual >= fin) estado = 'asistida';
        else if (inicio !== null && fin !== null && minutosActual >= inicio && minutosActual < fin) estado = 'en_curso';
        else if (fin !== null && minutosActual >= fin) estado = 'vencida';

        return {
          id_dictado: fila.id_dictado,
          id_clase: fila.id_clase,
          abierta_en: fila.abierta_en,
          dia: fila.dia,
          horario: fila.horario,
          horario_fin: fila.horario_fin,
          grupo: fila.grupo,
          anio_lectivo: fila.anio_lectivo,
          nombre_materia: fila.nombre_materia,
          apellido_profesor: fila.apellido_profesor,
          estado
        };
      });

    res.json({ fecha: hoy, dia: diaHoy, clases });
  } catch (error) {
    res.status(500).json({ error: 'Error consultando las clases de hoy', detalle: error.message });
  }
});

// GET /api/dictados/preceptor/hoy - clases del preceptor con estado de asistencia
router.get('/preceptor/hoy', requiereRol(['preceptor']), async (req, res) => {
  try {
    const hoy = fechaDeHoy();
    const diaHoy = diaDeHoy();
    const minutosActual = minutosAhora();

    const [filas] = await pool.query(
       `SELECT d.id_dictado, d.dia, d.horario, d.horario_fin, d.grupo, d.division, d.anio_lectivo,
                 m.nombre_materia, m.anio_materia, p.apellido_profesor,
               c.id_clase, c.estado AS estado_clase, c.abierta_en,
              COUNT(DISTINCT i.id_alumno) AS total_alumnos,
              COUNT(DISTINCT CASE WHEN a.verificada = 1 THEN a.id_alumno END) AS total_verificados
       FROM dictado d
       JOIN materia m ON m.id_materia = d.id_materia
       JOIN especialidad e ON e.id_especialidad = m.id_especialidad
       LEFT JOIN profesor p ON p.id_profesor = d.id_profesor
       LEFT JOIN inscribe i ON i.id_dictado = d.id_dictado
       LEFT JOIN clase c ON c.id_dictado = d.id_dictado AND c.fecha_clase = ?
       LEFT JOIN asistencia a ON a.id_clase = c.id_clase AND a.id_alumno = i.id_alumno
       WHERE d.id_preceptor = ?
       GROUP BY d.id_dictado, d.dia, d.horario, d.horario_fin, d.grupo, d.division, d.anio_lectivo,
                   m.nombre_materia, m.anio_materia, p.apellido_profesor, c.id_clase, c.estado, c.abierta_en
       ORDER BY d.horario`,
      [hoy, req.usuario.id_preceptor]
    );

    const clases = filas
      .filter((fila) => sinAcentos(fila.dia) === diaHoy)
      .map((fila) => {
        const inicio = minutosDeHora(fila.horario);
        const fin = minutosDeHora(fila.horario_fin);
        const asistenciaCompleta = Number(fila.total_alumnos) > 0 &&
          Number(fila.total_verificados) >= Number(fila.total_alumnos);
        let estado = 'pendiente';

        if (asistenciaCompleta) estado = 'asistida';
        else if (inicio !== null && fin !== null && minutosActual >= inicio && minutosActual < fin) {
          estado = 'en_curso';
        } else if (fin !== null && minutosActual >= fin) {
          estado = 'vencida';
        }

        return {
          id_dictado: fila.id_dictado,
          id_clase: fila.id_clase,
          abierta_en: fila.abierta_en,
          dia: fila.dia,
          horario: fila.horario,
          horario_fin: fila.horario_fin,
          grupo: fila.grupo,
          division: fila.division,
          anio_materia: fila.anio_materia,
          anio_lectivo: fila.anio_lectivo,
          nombre_materia: fila.nombre_materia,
          apellido_profesor: fila.apellido_profesor,
          total_alumnos: Number(fila.total_alumnos),
          total_verificados: Number(fila.total_verificados),
          estado
        };
      });

    res.json({ fecha: hoy, dia: diaHoy, clases });
  } catch (error) {
    res.status(500).json({ error: 'Error consultando las clases del preceptor', detalle: error.message });
  }
});

// GET /api/dictados/alumno/:idAlumno - dictados donde está inscripto el alumno
router.get('/alumno/:idAlumno', async (req, res) => {
  const idAlumno = Number(req.params.idAlumno);

  if (!Number.isInteger(idAlumno) || idAlumno <= 0) {
    return res.status(400).json({ error: 'id_alumno inválido.' });
  }

  try {
    const [filas] = await pool.query(
      `SELECT d.id_dictado, d.dia, d.horario, d.horario_fin, d.grupo, d.anio_lectivo,
              m.nombre_materia, p.apellido_profesor
       FROM inscribe i
       JOIN dictado d ON d.id_dictado = i.id_dictado
       JOIN materia m ON m.id_materia = d.id_materia
       LEFT JOIN profesor p ON p.id_profesor = d.id_profesor
       WHERE i.id_alumno = ?
       ORDER BY d.grupo, m.nombre_materia`,
      [idAlumno]
    );
    res.json(filas);
  } catch (error) {
    res.status(500).json({ error: 'Error consultando dictados del alumno', detalle: error.message });
  }
});

// GET /api/dictados/:id/alumnos - alumnos inscriptos en un dictado (tabla inscribe)
router.get('/:id/alumnos', async (req, res) => {
  try {
    const id = Number(req.params.id);

    if (!Number.isInteger(id) || id <= 0) {
      return res.status(400).json({ error: 'id_dictado inválido.' });
    }

    const [filas] = await pool.query(
      `SELECT al.id_alumno, al.nombre_alumno, al.apellido_alumno, al.legajo_alumno
       FROM inscribe i
       JOIN alumno al ON al.id_alumno = i.id_alumno
       WHERE i.id_dictado = ?
       ORDER BY al.apellido_alumno, al.nombre_alumno`,
      [id]
    );

    res.json(filas);
  } catch (error) {
    res.status(500).json({ error: 'Error consultando inscriptos', detalle: error.message });
  }
});

module.exports = router;
