const { Router } = require('express');
const bcrypt = require('bcryptjs');
const { pool } = require('../config/db');
const { crearToken } = require('../utilidades/autenticacion');

const router = Router();

const TABLAS = [
  { rol: 'preceptor', tabla: 'preceptor', id: 'id_preceptor', nombre: 'nombre_preceptor', apellido: 'apellido_preceptor' },
  { rol: 'profesor', tabla: 'profesor', id: 'id_profesor', nombre: 'nombre_profesor', apellido: 'apellido_profesor' },
  { rol: 'alumno', tabla: 'alumno', id: 'id_alumno', nombre: 'nombre_alumno', apellido: 'apellido_alumno' }
];

router.post('/login', async (req, res) => {
  const { dni, contrasena } = req.body || {};

  if (typeof dni !== 'string' || !dni.trim() ||
      typeof contrasena !== 'string' || !contrasena) {
    return res.status(400).json({ error: 'Debe enviar dni y contrasena.' });
  }

  try {
    for (const t of TABLAS) {
      const [filas] = await pool.query(
        `SELECT ${t.id}, ${t.nombre}, ${t.apellido}, contrasena
         FROM ${t.tabla} WHERE dni = ? AND activo = 1 LIMIT 1`,
        [dni.trim()]
      );
      if (filas.length === 0) continue;

      if (!(await bcrypt.compare(contrasena, filas[0].contrasena))) break;

      const usuario = {
        rol: t.rol,
        dni: dni.trim(),
        [t.id]: filas[0][t.id],
        nombre: filas[0][t.nombre],
        apellido: filas[0][t.apellido]
      };

      return res.json({
        ok: true,
        token: crearToken(usuario),
        usuario
      });
    }
    res.status(401).json({ error: 'DNI o contraseña incorrectos.' });
  } catch (error) {
    res.status(500).json({ error: 'Error en el login', detalle: error.message });
  }
});

module.exports = router;