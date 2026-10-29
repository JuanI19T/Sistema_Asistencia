-- Datos de apertura manual por parte del preceptor.
ALTER TABLE clase
  ADD COLUMN abierta_por_rol VARCHAR(15) NULL AFTER abierta_por,
  ADD COLUMN motivo_apertura VARCHAR(255) NULL AFTER abierta_por_rol,
  ADD COLUMN profesor_llego_en DATETIME NULL AFTER motivo_apertura;
