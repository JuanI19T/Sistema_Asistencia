// Validaciones del módulo de asistencia (v6.2 - por clase).

function validarRegistro({ id_alumno, id_clase, presente, verificada }) {
  if (!Number.isInteger(id_alumno) || id_alumno <= 0) {
    return 'id_alumno debe ser un entero positivo.';
  }

  if (!Number.isInteger(id_clase) || id_clase <= 0) {
    return 'id_clase debe ser un entero positivo.';
  }

  if (presente !== 0 && presente !== 1) {
    return 'presente debe ser 0 (ausente) o 1 (presente).';
  }

  if (verificada !== undefined && verificada !== 0 && verificada !== 1) {
    return 'verificada debe ser 0 o 1.';
  }

  return null;
}

module.exports = { validarRegistro };