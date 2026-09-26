const clientes = new Set();
const historial = [];
const MAX_HISTORIAL = 100;
let secuencia = 0;

function cerrarCliente(cliente) {
  if (cliente.cerrado) return;
  cliente.cerrado = true;
  clearInterval(cliente.heartbeat);
  clientes.delete(cliente);
  if (!cliente.res.writableEnded) cliente.res.end();
}

function escribirEvento(cliente, evento) {
  if (cliente.cerrado || cliente.res.writableEnded) return;

  const payload = `id: ${evento.id}\nevent: ${evento.tipo}\ndata: ${JSON.stringify(evento)}\n\n`;
  try {
    cliente.res.write(payload);
  } catch {
    cerrarCliente(cliente);
  }
}

function puedeRecibir(cliente, evento, roles) {
  if (roles && !roles.includes(cliente.usuario.rol)) return false;

  if (cliente.usuario.rol === 'alumno') {
    if (evento.id_alumno !== undefined && evento.id_alumno !== cliente.usuario.id_alumno) {
      return false;
    }
    if (
      Array.isArray(evento.id_alumnos) &&
      !evento.id_alumnos.includes(cliente.usuario.id_alumno)
    ) {
      return false;
    }
  }

  return true;
}

function publicarEvento({ tipo, roles, id_clase, id_alumno, id_alumnos, ...datos }) {
  const evento = {
    id: String(++secuencia),
    tipo,
    fecha: new Date().toISOString(),
    ...datos
  };

  if (id_clase !== undefined) evento.id_clase = id_clase;
  if (id_alumno !== undefined) evento.id_alumno = id_alumno;
  if (id_alumnos !== undefined) evento.id_alumnos = id_alumnos;

  const registro = { evento, roles };
  historial.push(registro);
  if (historial.length > MAX_HISTORIAL) historial.shift();

  for (const cliente of clientes) {
    if (puedeRecibir(cliente, evento, roles)) escribirEvento(cliente, evento);
  }

  return evento;
}

function conectarEventos(req, res, usuario) {
  res.status(200);
  res.set({
    'Content-Type': 'text/event-stream',
    'Cache-Control': 'no-cache, no-transform',
    Connection: 'keep-alive',
    'X-Accel-Buffering': 'no'
  });
  res.flushHeaders();

  const cliente = { res, usuario, cerrado: false, heartbeat: null };
  clientes.add(cliente);

  res.write('retry: 3000\n\n');
  res.write(`event: conectado\ndata: ${JSON.stringify({
    tipo: 'conectado',
    fecha: new Date().toISOString()
  })}\n\n`);

  const ultimoId = Number(req.get('last-event-id') || 0);
  if (Number.isInteger(ultimoId) && ultimoId > 0) {
    for (const registro of historial) {
      if (Number(registro.evento.id) > ultimoId && puedeRecibir(cliente, registro.evento, registro.roles)) {
        escribirEvento(cliente, registro.evento);
      }
    }
  }

  cliente.heartbeat = setInterval(() => {
    if (cliente.cerrado || res.writableEnded) return;
    try {
      res.write(': heartbeat\n\n');
    } catch {
      cerrarCliente(cliente);
    }
  }, 25000);
  cliente.heartbeat.unref?.();

  res.on('close', () => cerrarCliente(cliente));
  res.on('error', () => cerrarCliente(cliente));
  req.on('aborted', () => cerrarCliente(cliente));
}

module.exports = { conectarEventos, publicarEvento };
