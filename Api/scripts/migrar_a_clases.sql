-- ============================================================
-- Reconstrucción completa de la base (v6.2 - asistencia por CLASE)
--
-- 1) ELIMINA la base entera si existe (¡descartás todos los datos!).
-- 2) Crea la base y TODAS las tablas desde cero.
-- 3) Después ejecutá la carga de datos de prueba:
--      cd ApiAsistencia && node scripts/seed.js
--
-- Si tu .env usa otro nombre de base, ajustá la línea del CREATE.
-- ============================================================

DROP DATABASE IF EXISTS gestion_asistencia_eest;

CREATE DATABASE gestion_asistencia_eest
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_spanish_ci;

USE gestion_asistencia_eest;

-- ------------------------------------------------------------
-- especialidad
-- ------------------------------------------------------------
CREATE TABLE `especialidad` (
  `id_especialidad` int NOT NULL AUTO_INCREMENT,
  `nombre_especialidad` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_especialidad`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

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
  CONSTRAINT `FK_MATERIA_ESPECIALIDAD` FOREIGN KEY (`id_especialidad`) REFERENCES `especialidad` (`id_especialidad`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- profesor
-- ------------------------------------------------------------
CREATE TABLE `profesor` (
  `id_profesor` int NOT NULL AUTO_INCREMENT,
  `nombre_profesor` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `apellido_profesor` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `legajo_profesor` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `dni` varchar(20) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `correo_profesor` varchar(100) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `telefono_profesor` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `contrasena` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_profesor`),
  UNIQUE KEY `UQ_LEGAJO_PROFESOR` (`legajo_profesor`),
  UNIQUE KEY `UQ_DNI_PROFESOR` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- preceptor
-- ------------------------------------------------------------
CREATE TABLE `preceptor` (
  `id_preceptor` int NOT NULL AUTO_INCREMENT,
  `nombre_preceptor` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `apellido_preceptor` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `legajo_preceptor` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `dni` varchar(20) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `correo_preceptor` varchar(100) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `telefono_preceptor` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `contrasena` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_preceptor`),
  UNIQUE KEY `UQ_LEGAJO_PRECEPTOR` (`legajo_preceptor`),
  UNIQUE KEY `UQ_DNI_PRECEPTOR` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- alumno
-- ------------------------------------------------------------
CREATE TABLE `alumno` (
  `id_alumno` int NOT NULL AUTO_INCREMENT,
  `nombre_alumno` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `apellido_alumno` varchar(100) COLLATE utf8mb4_spanish_ci NOT NULL,
  `legajo_alumno` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `dni` varchar(20) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `correo_alumno` varchar(100) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `telefono_alumno` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `telefono_emergencia` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `telefono_padre` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `telefono_madre` varchar(50) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `contrasena` varchar(255) COLLATE utf8mb4_spanish_ci DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_alumno`),
  UNIQUE KEY `UQ_LEGAJO_ALUMNO` (`legajo_alumno`),
  UNIQUE KEY `UQ_DNI_ALUMNO` (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

-- ------------------------------------------------------------
-- dictado
-- ------------------------------------------------------------
CREATE TABLE `dictado` (
  `id_dictado` int NOT NULL AUTO_INCREMENT,
  `id_materia` int NOT NULL,
  `id_profesor` int NOT NULL,
  `id_preceptor` int DEFAULT NULL,
  `dia` varchar(15) COLLATE utf8mb4_spanish_ci NOT NULL,
  `horario` time NOT NULL,
  `grupo` varchar(50) COLLATE utf8mb4_spanish_ci NOT NULL,
  `anio_lectivo` int NOT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_dictado`),
  KEY `FK_DICTADO_MATERIA` (`id_materia`),
  KEY `FK_DICTADO_PROFESOR` (`id_profesor`),
  KEY `FK_DICTADO_PRECEPTOR` (`id_preceptor`),
  CONSTRAINT `FK_DICTADO_MATERIA` FOREIGN KEY (`id_materia`) REFERENCES `materia` (`id_materia`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `FK_DICTADO_PRECEPTOR` FOREIGN KEY (`id_preceptor`) REFERENCES `preceptor` (`id_preceptor`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `FK_DICTADO_PROFESOR` FOREIGN KEY (`id_profesor`) REFERENCES `profesor` (`id_profesor`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_spanish_ci;

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
-- estado: programada | en_curso | cerrada
-- token: token QR de la sesión (se rota; vence en token_valido_hasta)
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
-- presente: marcado por QR (o manual) · verificada: preceptor confirma en persona
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
-- usuario (tabla administrativa que ya existía en la base)
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
-- Listo. Ejecutar después:
--   cd ApiAsistencia && node scripts/seed.js
-- (crea especialidad, materia, profesor, preceptor, alumnos,
--  dictado e inscribe con contraseña inicial = DNI)
-- ============================================================