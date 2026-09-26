// Sembrado de datos de prueba para el módulo de asistencia (v6.1).
// Idempotente: solo inserta si las tablas están vacías.
// Ejecutar: node scripts/seed.js

require('dotenv').config();
const bcrypt = require('bcryptjs');
const { pool } = require('../config/db');

(async () => {
  const c = await pool.getConnection();

  const vacia = async (tabla) => {
    const [filas] = await c.query(`SELECT COUNT(*) AS n FROM ${tabla}`);
    return filas[0].n === 0;
  };

  // En v6.1 la contraseña inicial de cada persona es su DNI.
  const hashDni = async (dni) => await bcrypt.hash(String(dni), 10);

  if (await vacia('especialidad')) {
    const [r] = await c.query(
      'INSERT INTO especialidad (nombre_especialidad) VALUES (?)',
      ['Técnico en Programación']
    );
    console.log('especialidad creada (id ' + r.insertId + ')');
  } else {
    console.log('especialidad ya tiene datos, se omite');
  }

  const [[esp]] = await c.query('SELECT id_especialidad FROM especialidad LIMIT 1');

  if (await vacia('materia')) {
    const [r] = await c.query(
      `INSERT INTO materia (id_especialidad, nombre_materia, carga_horaria, anio_materia)
       VALUES (?, ?, ?, ?)`,
      [esp.id_especialidad, 'Programación', 6, 4]
    );
    console.log('materia creada (id ' + r.insertId + ')');
  } else {
    console.log('materia ya tiene datos, se omite');
  }

  // --- Profesores (dni + contrasena en v6.1) ---
  if (await vacia('profesor')) {
    const dni1 = '30111222';
    const dni2 = '31222333';
    const [r] = await c.query(
      `INSERT INTO profesor
        (nombre_profesor, apellido_profesor, legajo_profesor, dni,
         correo_profesor, telefono_profesor, contrasena)
       VALUES (?, ?, ?, ?, ?, ?, ?),
              (?, ?, ?, ?, ?, ?, ?)`,
      ['Carlos', 'Gutierrez', 'LEG-PROF-1', dni1,
       'carlos.gutierrez@escuela.edu.ar', '1123456789', await hashDni(dni1),
       'María', 'Fernández', 'LEG-PROF-2', dni2,
       'maria.fernandez@escuela.edu.ar', '1123456790', await hashDni(dni2)]
    );
    console.log('profesores creados (' + r.affectedRows + ')');
  } else {
    console.log('profesor ya tiene datos, se omite');
  }

  // --- Preceptor (tabla nueva v6.1) ---
  if (await vacia('preceptor')) {
    const dni = '34555666';
    const [r] = await c.query(
      `INSERT INTO preceptor
        (nombre_preceptor, apellido_preceptor, legajo_preceptor, dni,
         correo_preceptor, telefono_preceptor, contrasena)
       VALUES (?, ?, ?, ?, ?, ?, ?)`,
      ['Laura', 'Martínez', 'LEG-PREC-1', dni,
       'laura.martinez@escuela.edu.ar', '1198765432', await hashDni(dni)]
    );
    console.log('preceptor creado (id ' + r.insertId + ')');
  } else {
    console.log('preceptor ya tiene datos, se omite');
  }

  // --- Alumnos (dni + contrasena en v6.1) ---
  if (await vacia('alumno')) {
    const alumnos = [
      ['Juan', 'Pérez', 'LEG-AL-001', '45222001',
       'juan.perez@escuela.edu.ar', '1155550001', '1155550002', '1155550003', '1155550004'],
      ['Ana', 'Gómez', 'LEG-AL-002', '45222002',
       'ana.gomez@escuela.edu.ar', '1155550011', '1155550012', '1155550013', '1155550014'],
      ['Luis', 'Díaz', 'LEG-AL-003', '45222003',
       'luis.diaz@escuela.edu.ar', '1155550021', '1155550022', '1155550023', '1155550024']
    ];
    const contrasenas = await Promise.all(alumnos.map(a => hashDni(a[3])));
    const placeholders = alumnos.map(() => '(?, ?, ?, ?, ?, ?, ?, ?, ?, ?)').join(',\n');
    const params = alumnos.flatMap((a, i) => [...a, contrasenas[i]]);
    const [r] = await c.query(
      `INSERT INTO alumno
        (nombre_alumno, apellido_alumno, legajo_alumno, dni, correo_alumno,
         telefono_alumno, telefono_emergencia, telefono_padre, telefono_madre, contrasena)
       VALUES ${placeholders}`,
      params
    );
    console.log('alumnos creados (' + r.affectedRows + ')');
  } else {
    console.log('alumno ya tiene datos, se omite');
  }

  // --- Dictado (incluye id_preceptor en v6.1) ---
  if (await vacia('dictado')) {
    const [[mat]] = await c.query('SELECT id_materia FROM materia LIMIT 1');
    const [[prof]] = await c.query('SELECT id_profesor FROM profesor LIMIT 1');
    const [[prec]] = await c.query('SELECT id_preceptor FROM preceptor LIMIT 1');

    const [r] = await c.query(
      `INSERT INTO dictado (id_materia, id_profesor, id_preceptor, dia, horario, grupo, anio_lectivo)
       VALUES (?, ?, ?, ?, ?, ?, ?)`,
      [mat.id_materia, prof.id_profesor, prec.id_preceptor, 'LUNES', '08:00:00', '4to B', 2026]
    );
    console.log('dictado creado (id ' + r.insertId + ')');
  } else {
    console.log('dictado ya tiene datos, se omite');
  }

  // --- Inscripciones (alumnos -> dictado) ---
  if (await vacia('inscribe')) {
    const [[d]] = await c.query('SELECT id_dictado FROM dictado LIMIT 1');
    const [alumnos] = await c.query('SELECT id_alumno FROM alumno ORDER BY id_alumno');
    for (const a of alumnos) {
      await c.query(
        'INSERT INTO inscribe (id_alumno, id_dictado, anio_inicio) VALUES (?, ?, ?)',
        [a.id_alumno, d.id_dictado, 2026]
      );
    }
    console.log('inscripciones creadas: ' + alumnos.length);
  } else {
    console.log('inscribe ya tiene datos, se omite');
  }

  const [[d]] = await c.query('SELECT id_dictado, grupo FROM dictado LIMIT 1');
  const [alumnos] = await c.query(
    'SELECT id_alumno, apellido_alumno, nombre_alumno, dni FROM alumno ORDER BY id_alumno'
  );

  console.log('\nResumen:');
  console.log('  dictado: id ' + d.id_dictado + ' (' + d.grupo + ')');
  console.log('  alumnos: ' + JSON.stringify(alumnos));
  console.log('\nCredenciales de prueba (contrasena = DNI):');
  console.log('  Profesor  Carlos Gutierrez: dni ' + '30111222');
  console.log('  Profesor  María Fernández:  dni ' + '31222333');
  console.log('  Preceptor Laura Martínez:   dni ' + '34555666');
  console.log('  Alumno    Juan Pérez:       dni ' + '45222001');
  console.log('  Alumno    Ana Gómez:        dni ' + '45222002');
  console.log('  Alumno    Luis Díaz:        dni ' + '45222003');

  await c.end();
})().catch(e => { console.error('ERROR:', e.message); process.exit(1); });