const crypto = require('crypto');

const clave = process.env.SESSION_SECRET || crypto.randomBytes(32);
const vigenciaMs = 8 * 60 * 60 * 1000;

function crearToken(usuario) {
  const payload = Buffer.from(JSON.stringify({
    ...usuario,
    exp: Date.now() + vigenciaMs
  })).toString('base64url');
  const firma = crypto.createHmac('sha256', clave).update(payload).digest('base64url');
  return `${payload}.${firma}`;
}

function verificarToken(token) {
  if (typeof token !== 'string') return null;

  const partes = token.split('.');
  if (partes.length !== 2) return null;

  const firmaEsperada = crypto.createHmac('sha256', clave)
    .update(partes[0])
    .digest();
  const firmaRecibida = Buffer.from(partes[1], 'base64url');

  if (
    firmaRecibida.length !== firmaEsperada.length ||
    !crypto.timingSafeEqual(firmaRecibida, firmaEsperada)
  ) {
    return null;
  }

  try {
    const payload = JSON.parse(Buffer.from(partes[0], 'base64url').toString('utf8'));
    if (!payload.exp || payload.exp <= Date.now()) return null;
    return payload;
  } catch {
    return null;
  }
}

function requiereRol(roles) {
  return (req, res, next) => {
    const encabezado = req.get('authorization') || '';
    const [esquema, token] = encabezado.split(' ');

    if (esquema !== 'Bearer' || !token) {
      return res.status(401).json({ error: 'Sesión requerida.' });
    }

    const usuario = verificarToken(token);
    if (!usuario) {
      return res.status(401).json({ error: 'Sesión inválida o vencida.' });
    }

    if (!roles.includes(usuario.rol)) {
      return res.status(403).json({ error: 'No tenés permisos para realizar esta acción.' });
    }

    req.usuario = usuario;
    return next();
  };
}

module.exports = { crearToken, requiereRol, verificarToken };
