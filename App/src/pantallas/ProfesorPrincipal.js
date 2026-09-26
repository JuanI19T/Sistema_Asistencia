import { useCallback, useEffect, useState } from 'react';
import {
  ActivityIndicator,
  AppState,
  ScrollView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View
} from 'react-native';
import QRCode from 'react-native-qrcode-svg';

const INTERVALO_RESPALDO = 5000;
const INTERVALO_RECONCILIACION = 30000;
import {
  abrirClase,
  fechaDeHoy,
  obtenerClase,
  obtenerDictados,
  rotarToken
} from '../api';

function segundosRestantes(fecha) {
  if (!fecha) return 0;
  return Math.max(0, Math.floor((new Date(fecha).getTime() - Date.now()) / 1000));
}

export default function ProfesorPrincipal({ sesion, evento, eventosConectados }) {
  const [dictados, setDictados] = useState([]);
  const [clase, setClase] = useState(null);
  const [restante, setRestante] = useState(0);
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState(null);

  const cargarDictados = useCallback(async () => {
    setCargando(true);
    setMensaje(null);
    try {
      const todos = await obtenerDictados();
      setDictados(todos.filter((d) => d.id_profesor === sesion.id_profesor));
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  }, [sesion.id_profesor]);

  const cargarClaseActualizada = useCallback(async (idClase) => {
    if (!idClase) return;

    try {
      setClase(await obtenerClase(idClase));
    } catch (error) {
      setMensaje(error.message);
    }
  }, []);

  const sincronizar = useCallback(async () => {
    try {
      if (clase?.id_clase) {
        setClase(await obtenerClase(clase.id_clase));
        return;
      }

      const todos = await obtenerDictados();
      setDictados(todos.filter((d) => d.id_profesor === sesion.id_profesor));
    } catch (error) {
      if (!eventosConectados) setMensaje(error.message);
    }
  }, [clase?.id_clase, eventosConectados, sesion.id_profesor]);

  useEffect(() => {
    cargarDictados();
  }, [cargarDictados]);

  useEffect(() => {
    if (!evento) return;

    if (evento.tipo === 'conectado') {
      void cargarClaseActualizada(clase?.id_clase);
      return;
    }

    if (!['clase.abierta', 'clase.cerrada', 'qr.rotado'].includes(evento.tipo)) return;
    if (clase?.id_clase === evento.id_clase) void cargarClaseActualizada(evento.id_clase);
  }, [cargarClaseActualizada, clase?.id_clase, evento]);

  useEffect(() => {
    const intervalo = setInterval(() => {
      if (AppState.currentState === 'active') void sincronizar();
    }, eventosConectados ? INTERVALO_RECONCILIACION : INTERVALO_RESPALDO);

    return () => clearInterval(intervalo);
  }, [eventosConectados, sincronizar]);

  useEffect(() => {
    setRestante(segundosRestantes(clase?.token_valido_hasta));
    const id = setInterval(() => {
      setRestante(segundosRestantes(clase?.token_valido_hasta));
    }, 1000);
    return () => clearInterval(id);
  }, [clase?.token_valido_hasta]);

  const abrir = async (idDictado) => {
    setCargando(true);
    setMensaje(null);
    try {
      setClase(await abrirClase(idDictado, fechaDeHoy()));
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  };

  const rotar = async () => {
    setCargando(true);
    setMensaje(null);
    try {
      setClase(await rotarToken(clase.id_clase));
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  };

  const enCurso = clase && clase.estado === 'en_curso';
  const codigoQR = enCurso && clase.token
    ? JSON.stringify({ id_clase: clase.id_clase, token: clase.token })
    : '';

  return (
    <ScrollView style={styles.pantalla}>
      <View style={styles.contenedor}>
        <Text style={styles.titulo}>Mis clases</Text>

        {clase ? (
          <View style={styles.tarjeta}>
            <Text style={styles.textoPrincipal}>
              {clase.nombre_materia} - {clase.grupo}
            </Text>
            <Text style={styles.textoSecundario}>
              {clase.fecha_clase} · {clase.apellido_profesor} · {clase.estado}
            </Text>

            {enCurso ? (
              <>
                <Text style={styles.etiqueta}>QR de la clase (ESCANEAR)</Text>
                <View style={styles.qrContenedor}>
                  <QRCode value={codigoQR} size={220} />
                </View>
                <Text style={styles.textoSecundario}>
                  El QR vence en {restante}s · los alumnos lo escanean para registrarse
                </Text>
                <Text style={styles.etiqueta}>Código para alumnos sin cámara</Text>
                {clase.codigo ? (
                  <>
                    <View style={styles.codigoContenedor}>
                      <Text style={styles.codigoClase}>{clase.codigo}</Text>
                    </View>
                    <Text style={styles.textoSecundario}>
                      Dictá este código en voz alta · vence junto con el QR ({restante}s)
                    </Text>
                  </>
                ) : (
                  <Text style={styles.textoSecundario}>
                    Rotá el QR para generar el código de esta clase.
                  </Text>
                )}
              </>
            ) : (
              <Text style={styles.textoSecundario}>La clase está cerrada.</Text>
            )}

            {enCurso ? (
              <TouchableOpacity
                style={[styles.boton, cargando && styles.botonDeshabilitado]}
                onPress={cargando ? undefined : rotar}
                accessibilityState={{ disabled: cargando }}
              >
                <Text style={styles.textoBoton}>Rotar QR (nuevo token)</Text>
              </TouchableOpacity>
            ) : null}
          </View>
        ) : null}

        {!clase && cargando ? (
          <ActivityIndicator size="large" color="#1f6feb" style={styles.cargando} />
        ) : null}

        {!clase ? dictados.map((dictado) => (
          <View key={dictado.id_dictado} style={styles.tarjeta}>
            <Text style={styles.textoPrincipal}>
              {dictado.nombre_materia} - {dictado.grupo}
            </Text>
            <Text style={styles.textoSecundario}>
              {dictado.dia} {dictado.horario} ({dictado.anio_lectivo})
            </Text>
            <TouchableOpacity style={styles.boton} onPress={() => abrir(dictado.id_dictado)}>
              <Text style={styles.textoBoton}>Abrir clase de hoy</Text>
            </TouchableOpacity>
          </View>
        )) : null}

        {clase ? (
          <TouchableOpacity
            style={[styles.boton, styles.botonVolver]}
            onPress={() => setClase(null)}
          >
            <Text style={styles.textoBoton}>Volver a la lista</Text>
          </TouchableOpacity>
        ) : null}

        {!clase && dictados.length === 0 && !cargando ? (
          <Text style={styles.mensaje}>No tenés dictados asignados.</Text>
        ) : null}

        {mensaje ? <Text style={styles.mensaje}>{mensaje}</Text> : null}
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  pantalla: {
    flex: 1,
    backgroundColor: '#f6f8fa'
  },
  contenedor: {
    padding: 20,
    maxWidth: 640,
    width: '100%',
    alignSelf: 'center'
  },
  titulo: {
    fontSize: 26,
    fontWeight: 'bold',
    color: '#24292f',
    marginBottom: 16
  },
  etiqueta: {
    fontSize: 14,
    color: '#57606a',
    marginTop: 16,
    marginBottom: 6,
    fontWeight: '600'
  },
  tarjeta: {
    borderWidth: 1,
    borderColor: '#d0d7de',
    borderRadius: 8,
    padding: 12,
    marginTop: 8,
    backgroundColor: '#ffffff'
  },
  qrContenedor: {
    alignItems: 'center',
    backgroundColor: '#ffffff',
    borderWidth: 1,
    borderColor: '#d0d7de',
    borderRadius: 8,
    padding: 16
  },
  textoPrincipal: {
    fontSize: 16,
    color: '#24292f'
  },
  textoSecundario: {
    fontSize: 13,
    color: '#57606a'
  },
  boton: {
    backgroundColor: '#1f6feb',
    borderRadius: 8,
    padding: 14,
    alignItems: 'center',
    marginTop: 16
  },
  botonVolver: {
    backgroundColor: '#6e7781'
  },
  botonDeshabilitado: {
    opacity: 0.6
  },
  textoBoton: {
    color: '#ffffff',
    fontSize: 16,
    fontWeight: '600'
  },
  cargando: {
    marginTop: 20
  },
  mensaje: {
    marginTop: 16,
    color: '#cf222e',
    fontSize: 14
  }
});