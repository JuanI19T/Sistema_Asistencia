-- ------------------------------------------------------------
-- clase.codigo: código corto de 6 dígitos para alumnos sin cámara.
-- Se genera junto al token QR (apertura y rotación) y vence con él
-- (token_valido_hasta). Solo lo ve el profesor de la clase.
-- No es UNIQUE global a propósito: los códigos se reciclan en el
-- tiempo; la unicidad se garantiza solo entre clases 'en_curso'
-- al momento de generar (ver generarCodigoUnico en rutas/clases.js).
-- ------------------------------------------------------------
ALTER TABLE `clase`
  ADD COLUMN `codigo` varchar(10) DEFAULT NULL,
  ADD KEY `IX_CLASE_CODIGO` (`codigo`);
