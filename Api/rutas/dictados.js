const { Router } = require('express');
const { pool } = require('../config/db');

const router = Router();

// GET /api/dictados - lista los dictados (materia + profesor + grupo)
router.get('/', async (req, res) => {
  try {
    const [filas] = await pool.query(
      `SELECT d.id_dictado, d.dia, d.horario, d.grupo, d.anio_lectivo,
              m.id_materia, m.nombre_materia,
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

// GET /api/dictados/alumno/:idAlumno - dictados donde está inscripto el alumno
router.get('/alumno/:idAlumno', async (req, res) => {
  const idAlumno = Number(req.params.idAlumno);

  if (!Number.isInteger(idAlumno) || idAlumno <= 0) {
    return res.status(400).json({ error: 'id_alumno inválido.' });
  }

  try {
    const [filas] = await pool.query(
      `SELECT d.id_dictado, d.dia, d.horario, d.grupo, d.anio_lectivo,
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