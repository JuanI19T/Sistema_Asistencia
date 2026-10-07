-- ============================================================
-- Reconstrucción completa y DEFINITIVA (v8 - reglas EEST)
--
-- 1) ELIMINA la base entera si existe (¡descartás todos los datos!).
-- 2) Crea la base y TODAS las tablas desde cero.
-- 3) Deja el catálogo de especialidades ya cargado (6 filas).
-- 4) Deja datos básicos de prueba ya cargados (ver sección final).
--    No requiere seed.js (retirado: los datos vienen incluidos acá).
--
-- Reglas aplicadas (decididas con la escuela):
--  a) ALUMNO.legajo_alumno: lo genera la escuela, numérico de hasta
--     10 dígitos, único y obligatorio. DNI obligatorio, único, 7-8 dígitos.
--  b) Personal (PROFESOR, PRECEPTOR): el legajo EXISTE pero se
--     autocompleta con el DNI (legajo = dni). Ambas columnas NOT NULL,
--     únicas, 7-8 dígitos; la app lo rellena solo y lo valida.
--  c) ESPECIALIDAD: catálogo fijo de 6 (Ciclo Básico + 5 tecnicaturas),
--     nombre único. Las materias de 1-3 van con 'Ciclo Básico';
--     las de 4-7 con su tecnicatura (regla validada en la app).
--  d) MATERIA.anio_materia: 1-7 (EEST: 1-3 Ciclo Básico, 4-7 Superior).
--  e) Teléfono único por persona (se eliminan telefono_emergencia/
--     telefono_padre/telefono_madre de ALUMNO: la app ya no los usa).
--  f) DICTADO.division: obligatoria (NOT NULL). Junto con `grupo`
--     define a quién va el dictado.
--
--     Alcance de un dictado:
--       division  X   | grupo NULL -> toda la división X
--       division  X   | grupo Y    -> división X, grupo Y
--  g) Ningún profesor toma dos dictados superpuestos: mismo día,
--     mismo año lectivo, ambos activos (triggers TRG_DICTADO_SIN_SOLAPE_*).
--     Pegados (fin == inicio ajeno) sí se permiten.
--
-- Requiere MySQL 8.0.16+ (CHECK + REGEXP).
-- Ejecutar como SCRIPT COMPLETO (todo el archivo de una vez), con
-- "stop on error" activado: si se corta en el medio queda la base a medias.
-- ============================================================

DROP DATABASE IF EXISTS gestion_asistencia_eest;

CREATE DATABASE gestion_asistencia_eest
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_spanish_ci;

USE gestion_asistencia_eest;

-- ------------------------------------------------------------
-- especialidad (catálogo fijo de 6)
-- ------------------------------------------------------------
CREATE TABLE `especialidad` (
  `id_especialidad` int NOT NULL AUTO_INCREMENT,
  `nombre_especialidad` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  `division` int DEFAULT NULL,
  PRIMARY KEY (`id_especialidad`),
  UNIQUE KEY `UQ_ESPECIALIDAD_NOMBRE` (`nombre_especialidad`),
  CONSTRAINT `CHK_ESPECIALIDAD_DIVISION` CHECK (`division` IS NULL OR (`division` BETWEEN 1 AND 6)),
  UNIQUE KEY `UQ_ESPECIALIDAD_DIVISION` (`division`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;
  

INSERT INTO `especialidad` (`nombre_especialidad`, `division`, `activo`) VALUES
  ('Ciclo Básico', NULL, 1),
  ('Programación', 6, 1),
  ('Química', 1, 1),
  ('Electrónica', 3, 1),
  ('Electromecánica', 2, 1),
  ('Electricidad', 5, 1);

-- ------------------------------------------------------------
-- materia
-- ------------------------------------------------------------
CREATE TABLE `materia` (
  `id_materia` int NOT NULL AUTO_INCREMENT,
  `id_especialidad` int NOT NULL,
  `nombre_materia` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `carga_horaria` int NOT NULL,
  `anio_materia` int NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_materia`),
  KEY `FK_MATERIA_ESPECIALIDAD` (`id_especialidad`),
  CONSTRAINT `FK_MATERIA_ESPECIALIDAD` FOREIGN KEY (`id_especialidad`) REFERENCES `especialidad` (`id_especialidad`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `CHK_MATERIA_ANIO` CHECK (`anio_materia` BETWEEN 1 AND 7),
  CONSTRAINT `CHK_MATERIA_CARGA` CHECK (`carga_horaria` > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- profesor (legajo = dni, autocompletado por la app)
-- ------------------------------------------------------------
CREATE TABLE `profesor` (
  `id_profesor` int NOT NULL AUTO_INCREMENT,
  `nombre_profesor` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `apellido_profesor` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `legajo_profesor` varchar(20) COLLATE utf8mb4_spanish_ci NOT NULL,
  `dni` varchar(20) COLLATE utf8mb4_spanish_ci NOT NULL,
  `correo_profesor` varchar(100) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `telefono_profesor` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `contrasena` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_profesor`),
  UNIQUE KEY `UQ_LEGAJO_PROFESOR` (`legajo_profesor`),
  UNIQUE KEY `UQ_DNI_PROFESOR` (`dni`),
  CONSTRAINT `CHK_DNI_PROFESOR` CHECK (`dni` REGEXP '^[0-9]{7,8}$'),
  CONSTRAINT `CHK_LEGAJO_PROFESOR` CHECK (`legajo_profesor` REGEXP '^[0-9]{7,8}$')
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- preceptor (legajo = dni, autocompletado por la app)
-- ------------------------------------------------------------
CREATE TABLE `preceptor` (
  `id_preceptor` int NOT NULL AUTO_INCREMENT,
  `nombre_preceptor` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `apellido_preceptor` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `legajo_preceptor` varchar(20) COLLATE utf8mb4_spanish_ci NOT NULL,
  `dni` varchar(20) COLLATE utf8mb4_spanish_ci NOT NULL,
  `correo_preceptor` varchar(100) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `telefono_preceptor` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `contrasena` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_preceptor`),
  UNIQUE KEY `UQ_LEGAJO_PRECEPTOR` (`legajo_preceptor`),
  UNIQUE KEY `UQ_DNI_PRECEPTOR` (`dni`),
  CONSTRAINT `CHK_DNI_PRECEPTOR` CHECK (`dni` REGEXP '^[0-9]{7,8}$'),
  CONSTRAINT `CHK_LEGAJO_PRECEPTOR` CHECK (`legajo_preceptor` REGEXP '^[0-9]{7,8}$')
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- alumno (legajo numérico de la escuela + DNI obligatorio)
-- ------------------------------------------------------------
CREATE TABLE `alumno` (
  `id_alumno` int NOT NULL AUTO_INCREMENT,
  `nombre_alumno` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `apellido_alumno` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `legajo_alumno` varchar(10) COLLATE utf8mb4_spanish_ci NOT NULL,
  `dni` varchar(20) COLLATE utf8mb4_spanish_ci NOT NULL,
  `correo_alumno` varchar(100) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `telefono_alumno` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `contrasena` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_alumno`),
  UNIQUE KEY `UQ_LEGAJO_ALUMNO` (`legajo_alumno`),
  UNIQUE KEY `UQ_DNI_ALUMNO` (`dni`),
  CONSTRAINT `CHK_LEGAJO_ALUMNO` CHECK (`legajo_alumno` REGEXP '^[0-9]{1,10}$'),
  CONSTRAINT `CHK_DNI_ALUMNO` CHECK (`dni` REGEXP '^[0-9]{7,8}$')
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- dictado
--   division es NOT NULL (mínimo 1). grupo NULL = toda la división.
-- ------------------------------------------------------------
CREATE TABLE `dictado` (
  `id_dictado` int NOT NULL AUTO_INCREMENT,
  `id_materia` int NOT NULL,
  `id_profesor` int NOT NULL,
  `id_preceptor` int DEFAULT NULL,
  `dia` varchar(15) COLLATE utf8mb4_spanish_ci NOT NULL,
  `horario` time NOT NULL,
  `horario_fin` time DEFAULT NULL,
  `division` int NOT NULL,
  `grupo` int DEFAULT NULL,
  `anio_lectivo` int NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_dictado`),
  KEY `FK_DICTADO_MATERIA` (`id_materia`),
  KEY `FK_DICTADO_PROFESOR` (`id_profesor`),
  KEY `FK_DICTADO_PRECEPTOR` (`id_preceptor`),
  KEY `IX_DICTADO_ALCANCE` (`anio_lectivo`,`division`,`grupo`),
  CONSTRAINT `FK_DICTADO_MATERIA` FOREIGN KEY (`id_materia`) REFERENCES `materia` (`id_materia`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `FK_DICTADO_PRECEPTOR` FOREIGN KEY (`id_preceptor`) REFERENCES `preceptor` (`id_preceptor`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `FK_DICTADO_PROFESOR` FOREIGN KEY (`id_profesor`) REFERENCES `profesor` (`id_profesor`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `CHK_DICTADO_GRUPO` CHECK (`grupo` IS NULL OR `grupo` BETWEEN 1 AND 2),
  CONSTRAINT `CHK_DICTADO_DIVISION` CHECK (`division` BETWEEN 1 AND 7),
  CONSTRAINT `CHK_DICTADO_HORARIO` CHECK (`horario_fin` IS NULL OR `horario_fin` > `horario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- Sin solapamiento de horarios por profesor: mismo día, mismo
-- año lectivo, ambos activos. Pegados (fin == inicio ajeno) sí
-- se permiten. El escritorio también lo valida (DictadoController);
-- el trigger cubre altas directas por SQL u otros clientes.
-- ------------------------------------------------------------
DELIMITER $$

CREATE TRIGGER `TRG_DICTADO_SIN_SOLAPE_INS`
BEFORE INSERT ON `dictado`
FOR EACH ROW
BEGIN
    IF NEW.activo = 1 AND NEW.horario_fin IS NOT NULL THEN
        IF EXISTS (
            SELECT 1 FROM `dictado` d
            WHERE d.id_profesor = NEW.id_profesor
              AND d.dia = NEW.dia
              AND d.anio_lectivo = NEW.anio_lectivo
              AND d.activo = 1
              AND TIME(d.horario) < TIME(NEW.horario_fin)
              AND TIME(d.horario_fin) > TIME(NEW.horario)
        ) THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'El profesor ya tiene un dictado que se superpone en ese día y horario.';
        END IF;
    END IF;
END$$

CREATE TRIGGER `TRG_DICTADO_SIN_SOLAPE_UPD`
BEFORE UPDATE ON `dictado`
FOR EACH ROW
BEGIN
    IF NEW.activo = 1 AND NEW.horario_fin IS NOT NULL THEN
        IF EXISTS (
            SELECT 1 FROM `dictado` d
            WHERE d.id_profesor = NEW.id_profesor
              AND d.dia = NEW.dia
              AND d.anio_lectivo = NEW.anio_lectivo
              AND d.activo = 1
              AND d.id_dictado <> NEW.id_dictado
              AND TIME(d.horario) < TIME(NEW.horario_fin)
              AND TIME(d.horario_fin) > TIME(NEW.horario)
        ) THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'El profesor ya tiene un dictado que se superpone en ese día y horario.';
        END IF;
    END IF;
END$$

DELIMITER ;

-- ------------------------------------------------------------
-- inscribe (alumno <-> dictado)
-- ------------------------------------------------------------
CREATE TABLE `inscribe` (
  `id_inscripcion` int NOT NULL AUTO_INCREMENT,
  `id_alumno` int NOT NULL,
  `id_dictado` int NOT NULL,
  `nota_final` decimal(4,2) DEFAULT NULL,
  `anio_inicio` int NOT NULL,
  `condicion` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `situacion` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  PRIMARY KEY (`id_inscripcion`),
  UNIQUE KEY `UQ_ALUMNO_DICTADO_INSCRIPCION` (`id_alumno`,`id_dictado`),
  KEY `FK_INSCRIBE_DICTADO` (`id_dictado`),
  CONSTRAINT `FK_INSCRIBE_ALUMNO` FOREIGN KEY (`id_alumno`) REFERENCES `alumno` (`id_alumno`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `FK_INSCRIBE_DICTADO` FOREIGN KEY (`id_dictado`) REFERENCES `dictado` (`id_dictado`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- clase (1 fila = 1 sesión real del dictado en una fecha)
-- ------------------------------------------------------------
CREATE TABLE `clase` (
  `id_clase` int NOT NULL AUTO_INCREMENT,
  `id_dictado` int NOT NULL,
  `fecha_clase` date NOT NULL,
  `estado` varchar(20) NOT NULL DEFAULT 'programada',
  `token` varchar(64) DEFAULT NULL,
  `token_valido_hasta` datetime DEFAULT NULL,
  `codigo` varchar(10) DEFAULT NULL,
  `abierta_por` int DEFAULT NULL,
  PRIMARY KEY (`id_clase`),
  UNIQUE KEY `UQ_CLASE_DICTADO_FECHA` (`id_dictado`,`fecha_clase`),
  KEY `IX_CLASE_CODIGO` (`codigo`),
  CONSTRAINT `FK_CLASE_DICTADO` FOREIGN KEY (`id_dictado`) REFERENCES `dictado` (`id_dictado`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- asistencia (detalle: qué hizo cada alumno en la clase)
-- ------------------------------------------------------------
CREATE TABLE `asistencia` (
  `id_asistencia` int NOT NULL AUTO_INCREMENT,
  `id_clase` int NOT NULL,
  `id_alumno` int NOT NULL,
  `presente` tinyint(1) NOT NULL DEFAULT '0',
  `verificada` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id_asistencia`),
  UNIQUE KEY `UQ_ALUMNO_CLASE` (`id_clase`,`id_alumno`),
  KEY `FK_ASISTENCIA_ALUMNO` (`id_alumno`),
  CONSTRAINT `FK_ASISTENCIA_ALUMNO` FOREIGN KEY (`id_alumno`) REFERENCES `alumno` (`id_alumno`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `FK_ASISTENCIA_CLASE` FOREIGN KEY (`id_clase`) REFERENCES `clase` (`id_clase`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- usuario (tabla administrativa)
-- ------------------------------------------------------------
CREATE TABLE `usuario` (
  `id_usuario` int NOT NULL AUTO_INCREMENT,
  `usuario` varchar(30) COLLATE utf8mb4_spanish_ci NOT NULL,
  `contrasena` varchar(255) COLLATE utf8mb4_spanish_ci NOT NULL,
  `rol` enum('Administrador','Directivo','Preceptor','Docente') COLLATE utf8mb4_spanish_ci NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_usuario`),
  UNIQUE KEY `UQ_USUARIO` (`usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ============================================================
-- Datos básicos de prueba (respetan todas las reglas)
-- ============================================================

INSERT INTO `materia` (`id_especialidad`, `nombre_materia`, `carga_horaria`, `anio_materia`) VALUES
  (1, 'Matemática', 5, 1),
  (1, 'Lengua', 5, 2),
  (2, 'Programación', 6, 4),
  (3, 'Química General', 4, 4);

INSERT INTO `profesor`
  (`nombre_profesor`, `apellido_profesor`, `legajo_profesor`, `dni`,
   `correo_profesor`, `telefono_profesor`, `contrasena`) VALUES
  ('Carlos', 'Gutierrez', '30111222', '30111222',
   'carlos.gutierrez@escuela.edu.ar', '54 11 12345678',
   '$2b$10$A88T9xCB.W71lGjqXX6gaOlcjHnYXFC5U/kUkQGBGYIm1liirhhWu'),
  ('María', 'Fernández', '31222333', '31222333',
   'maria.fernandez@escuela.edu.ar', '54 11 12345679',
   '$2b$10$x4IFTN3RaYsvbSFjtPpReeyfcbZBZGfSssLCI74peLiqS7EG67Cia');

INSERT INTO `preceptor`
  (`nombre_preceptor`, `apellido_preceptor`, `legajo_preceptor`, `dni`,
   `correo_preceptor`, `telefono_preceptor`, `contrasena`) VALUES
  ('Laura', 'Martínez', '34555666', '34555666',
   'laura.martinez@escuela.edu.ar', '54 11 98765432',
   '$2b$10$MMy34ne4JmWPHELlGjhskOjQ0wR3VG5YXsqZkJX9oL5ZaC6MgmKXS');

INSERT INTO `alumno`
  (`nombre_alumno`, `apellido_alumno`, `legajo_alumno`, `dni`,
   `correo_alumno`, `telefono_alumno`, `contrasena`) VALUES
  ('Juan', 'Pérez', '1001', '45222001',
   'juan.perez@escuela.edu.ar', '54 115 5550001',
   '$2b$10$kWggglHoOXsLa6hKW/H1PO35susms8iIoy.lEVltE1NsrNvao7kUy'),
  ('Ana', 'Gómez', '1002', '45222002',
   'ana.gomez@escuela.edu.ar', '54 115 5550011',
   '$2b$10$ORMPCKSZY2sld8.wXL4aO.OBdyyvQE1RmX81dbOceY9DNcYlacNWW'),
  ('Luis', 'Díaz', '1003', '45222003',
   'luis.diaz@escuela.edu.ar', '54 115 5550021',
   '$2b$10$RAu0I.Iz4nb.dQ2DE06PRun9BaWLG3gAj8x3QrZFLsONrl0XAL5au');

-- Dictados de prueba (todos tienen una división asignada >= 1):
INSERT INTO `dictado`
  (`id_materia`, `id_profesor`, `id_preceptor`, `dia`, `horario`, `horario_fin`,
   `division`, `grupo`, `anio_lectivo`) VALUES
  (3, 1, 1, 'LUNES',    '08:00:00', '10:00:00', 1, 2, 2026),
  (1, 2, 1, 'MARTES',   '10:00:00', '12:00:00', 3, 1, 2026),
  (2, 1, 1, 'MIÉRCOLES','09:00:00', '11:00:00', 1, NULL, 2026);

INSERT INTO `inscribe` (`id_alumno`, `id_dictado`, `anio_inicio`) VALUES
  (1, 1, 2026),
  (2, 1, 2026),
  (3, 1, 2026),
  (1, 2, 2026),
  (1, 3, 2026),
  (2, 3, 2026),
  (3, 3, 2026);

INSERT INTO `clase` (`id_dictado`, `fecha_clase`, `estado`) VALUES
  (1, CURDATE(), 'programada');

INSERT INTO `asistencia` (`id_clase`, `id_alumno`, `presente`, `verificada`) VALUES
  (1, 1, 0, 0),
  (1, 2, 0, 0),
  (1, 3, 0, 0);