import Constants from 'expo-constants';
import { EventStreamContentType, fetchEventSource } from 'react-native-fetch-event-source';

function obtenerHost() {
  const hostUri = Constants.expoConfig?.hostUri;
  return hostUri ? hostUri.split(':')[0] : 'localhost';
}

const API_URL = `http://${obtenerHost()}:3000`;
let tokenSesion = null;

function cabeceras(conToken = true) {
  return conToken && tokenSesion
    ? { Authorization: `Bearer ${tokenSesion}` }
    : {};
}

export function fechaDeHoy() {
  const ahora = new Date();
  const mes = String(ahora.getMonth() + 1).padStart(2, '0');
  const dia = String(ahora.getDate()).padStart(2, '0');
  return `${ahora.getFullYear()}-${mes}-${dia}`;
}

async function pedir(url, opciones = {}) {
  const respuesta = await fetch(url, {
    ...opciones,
    headers: {
      ...cabeceras(),
      ...(opciones.headers || {})
    }
  });
  const datos = await respuesta.json().catch(() => ({}));
  if (!respuesta.ok) throw new Error(datos.error || 'Error de comunicación con la API.');
  return datos;
}

async function post(url, cuerpo, conToken = true) {
  return pedir(url, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json', ...cabeceras(conToken) },
    body: JSON.stringify(cuerpo)
  });
}

async function patch(url, cuerpo) {
  return pedir(url, {
    method: 'PATCH',
    headers: { 'Content-Type': 'application/json', ...cabeceras() },
    body: JSON.stringify(cuerpo)
  });
}

export function conectarEventos(alEvento, alCambiarEstado = () => {}) {
  const controlador = new AbortController();
  let activo = true;

  const cambiarEstado = (conectado) => {
    if (activo) alCambiarEstado(conectado);
  };

  void fetchEventSource(`${API_URL}/api/eventos`, {
    method: 'GET',
    headers: {
      Accept: EventStreamContentType,
      ...cabeceras()
    },
    signal: controlador.signal,
    onopen: (response) => {
      if (!response.ok) {
        if (__DEV__) console.warn('SSE onopen: HTTP', response.status);
        const error = new Error('No se pudo abrir el canal de eventos.');
        error.fatal = response.status >= 400 && response.status < 500 && response.status !== 429;
        throw error;
      }

      const contentType = response.headers.get('content-type');
      if (!contentType?.startsWith(EventStreamContentType)) {
        if (__DEV__) console.warn('SSE onopen: Content-Type inesperado:', contentType);
        throw new Error('La API no devolvió un canal SSE válido.');
      }

      cambiarEstado(true);
    },
    onmessage: (evento) => {
      if (!activo) return;
      if (!evento.data) return;

      try {
        alEvento(JSON.parse(evento.data));
      } catch {
        if (__DEV__) console.warn('SSE mensaje inválido.');
        cambiarEstado(false);
      }
    },
    onclose: () => {
      cambiarEstado(false);
      throw new Error('El canal de eventos se cerró.');
    },
    onerror: (error) => {
      cambiarEstado(false);
      if (__DEV__) console.warn('SSE error:', error?.message);
      if (error?.fatal) throw error;
      return 3000;
    }
  }).catch((error) => {
    cambiarEstado(false);
    if (__DEV__) console.warn('SSE finalizado:', error?.message);
  });

  return () => {
    activo = false;
    controlador.abort();
  };
}

export function limpiarToken() {
  tokenSesion = null;
}

export async function iniciarSesion(dni, contrasena) {
  const resultado = await post(`${API_URL}/api/login`, { dni, contrasena }, false);
  tokenSesion = resultado.token;
  return resultado;
}

export async function obtenerDictados() {
  return pedir(`${API_URL}/api/dictados`);
}

export async function obtenerDictadosAlumno(idAlumno) {
  return pedir(`${API_URL}/api/dictados/alumno/${idAlumno}`);
}

export async function obtenerAlumnosDictado(idDictado) {
  return pedir(`${API_URL}/api/dictados/${idDictado}/alumnos`);
}

export async function obtenerClases(fecha) {
  const parametros = new URLSearchParams();
  if (fecha) parametros.set('fecha', fecha);
  const qs = parametros.toString();
  return pedir(`${API_URL}/api/clases${qs ? `?${qs}` : ''}`);
}

export async function obtenerClase(claseId) {
  return pedir(`${API_URL}/api/clases/${claseId}`);
}

export async function abrirClase(idDictado, fecha) {
  return post(`${API_URL}/api/clases`, { id_dictado: idDictado, fecha });
}

export async function rotarToken(claseId) {
  return post(`${API_URL}/api/clases/${claseId}/token`, {});
}

export async function cambiarEstadoClase(claseId, estado) {
  return patch(`${API_URL}/api/clases/${claseId}`, { estado });
}

export async function escanearClase(claseId, token) {
  return post(`${API_URL}/api/clases/${claseId}/escanear`, { token });
}

export async function ingresarConCodigo(codigo) {
  return post(`${API_URL}/api/clases/ingresar`, { codigo });
}

export async function obtenerAsistencia(claseId) {
  return pedir(`${API_URL}/api/asistencia?id_clase=${claseId}`);
}

export async function guardarAsistencia(claseId, registros) {
  return post(`${API_URL}/api/asistencia/lote`, { id_clase: claseId, registros });
}