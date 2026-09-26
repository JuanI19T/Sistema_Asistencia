-- =============================================================
--  SistemaAsistencia
--  Migración: baja lógica (columna activo)
--  Fecha....: 2026-09-24
--  Uso.....: ejecutar en la base de datos (gestion_asistencia_eest)
--            en CADA PC donde esté instalada la app.
--  Nota....: es idempotente si la columna ya existe se omite;
--            si ya se ejecutó, mostrará error de columna duplicada
--            y se puede continuar.
-- =============================================================

USE gestion_asistencia_eest;

ALTER TABLE ALUMNO
    ADD COLUMN activo TINYINT(1) NOT NULL DEFAULT 1;

ALTER TABLE PROFESOR
    ADD COLUMN activo TINYINT(1) NOT NULL DEFAULT 1;

ALTER TABLE MATERIA
    ADD COLUMN activo TINYINT(1) NOT NULL DEFAULT 1;

ALTER TABLE ESPECIALIDAD
    ADD COLUMN activo TINYINT(1) NOT NULL DEFAULT 1;

ALTER TABLE DICTADO
    ADD COLUMN activo TINYINT(1) NOT NULL DEFAULT 1;