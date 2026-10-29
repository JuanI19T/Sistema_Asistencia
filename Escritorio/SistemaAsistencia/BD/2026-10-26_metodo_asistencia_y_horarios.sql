-- Registra cuándo se abrió una clase y cómo/cuándo registró asistencia el alumno.
ALTER TABLE clase
  ADD COLUMN abierta_en DATETIME NULL AFTER abierta_por;

ALTER TABLE asistencia
  ADD COLUMN metodo_registro VARCHAR(10) NOT NULL DEFAULT 'manual' AFTER verificada,
  ADD COLUMN registrado_en DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP AFTER metodo_registro;
