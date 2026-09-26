const express = require('express');
const cors = require('cors');
const { pool } = require('./config/db');

const asistencias = require('./rutas/asistencias');
const dictados = require('./rutas/dictados');
const eventos = require('./rutas/eventos');
const login = require('./rutas/login');
const clases = require('./rutas/clases');

const app = express();
app.use(cors({ exposedHeaders: ['Content-Type'] }));
app.use(express.json());

// Rutas del módulo de asistencia
app.use('/api/asistencia', asistencias);

app.use('/api/eventos', eventos);

// Rutas del módulo de dictados
app.use('/api/dictados', dictados);

// Rutas de autenticación
app.use('/api', login);

// Rutas del módulo de clases (abrir/cerrar clase, token QR, escaneo)
app.use('/api/clases', clases);

// GET / - pagina indice con los endpoints disponibles
app.get('/', (req, res) => {
  res.set('Content-Type', 'text/html; charset=utf-8');
  res.send(`
    <h1>ApiAsistencia</h1>
    <p>API corriendo correctamente.</p>
    <ul>
      <li><a href="/api/health">/api/health</a></li>
      <li><a href="/api/alumnos">/api/alumnos</a> (MySQL)</li>
      <li><a href="/api/asistencia">/api/asistencia</a> (consulta)</li>
      <li><a href="/api/dictados">/api/dictados</a> (lista)</li>
      <li>GET /api/dictados/:id/alumnos (inscriptos)</li>
      <li>GET /api/dictados/alumno/:idAlumno (dictados de un alumno)</li>
      <li>GET /api/eventos (SSE autenticado)</li>
      <li>GET /api/clases?fecha= (lista de clases)</li>
      <li>POST /api/clases (abrir/crear clase)</li>
      <li>PATCH /api/clases/:id (cambiar estado de la clase)</li>
      <li>POST /api/clases/:id/token (rotar token QR)</li>
      <li>POST /api/clases/:id/escanear (alumno escanea QR)</li>
<li>POST /api/clases/ingresar (alumno ingresa código corto)</li>
      <li>POST /api/login (autenticación por rol: preceptor, profesor o alumno)</li>
      <li>POST /api/asistencia (alta individual)</li>
      <li>POST /api/asistencia/lote (alta en lote)</li>
    </ul>
  `);
});

// GET /api/health - verifica que el servidor responde
app.get('/api/health', (req, res) => {
  res.json({
    ok: true,
    servicio: 'ApiAsistencia',
    hora: new Date().toISOString()
  });
});

// GET /api/alumnos - lee la tabla ALUMNO en MySQL
app.get('/api/alumnos', async (req, res) => {
  try {
    const [filas] = await pool.query(
      `SELECT id_alumno, nombre_alumno, apellido_alumno, legajo_alumno,
              dni, correo_alumno, telefono_alumno
       FROM ALUMNO
       ORDER BY apellido_alumno, nombre_alumno`
    );
    res.json(filas);
  } catch (error) {
    res.status(500).json({ error: 'Error consultando MySQL', detalle: error.message });
  }
});

const PUERTO = Number(process.env.PORT) || 3000;

app.listen(PUERTO, () => {
  console.log(`API corriendo en http://localhost:${PUERTO}`);
});