import { useCallback, useEffect, useState } from 'react';
import {
  ActivityIndicator,
  AppState,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View
} from 'react-native';
import { CameraView, useCameraPermissions } from 'expo-camera';
import { escanearClase, obtenerDictadosAlumno } from '../api';

const INTERVALO_RESPALDO = 5000;
const INTERVALO_RECONCILIACION = 30000;

export default function AlumnoPrincipal({ sesion, evento, eventosConectados }) {
  const [dictados, setDictados] = useState([]);
  const [escaneando, setEscaneando] = useState(false);
  const [codigo, setCodigo] = useState('');
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState(null);
  const [permission, requestPermission] = useCameraPermissions();

  const cargarDictados = useCallback(async () => {
    setCargando(true);
    setMensaje(null);
    try {
      setDictados(await obtenerDictadosAlumno(sesion.id_alumno));
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  }, [sesion.id_alumno]);

  const sincronizar = useCallback(async () => {
    try {
      setDictados(await obtenerDictadosAlumno(sesion.id_alumno));
    } catch (error) {
      if (!eventosConectados) setMensaje(error.message);
    }
  }, [eventosConectados, sesion.id_alumno]);

  useEffect(() => {
    cargarDictados();
  }, [cargarDictados]);

  useEffect(() => {
    const intervalo = setInterval(() => {
      if (AppState.currentState === 'active') void sincronizar();
    }, eventosConectados ? INTERVALO_RECONCILIACION : INTERVALO_RESPALDO);

    return () => clearInterval(intervalo);
  }, [eventosConectados, sincronizar]);

  useEffect(() => {
    if (!evento) return;

    if (evento.tipo === 'conectado' || evento.tipo === 'clase.abierta') {
      void cargarDictados();
    } else if (evento.tipo === 'asistencia.registrada') {
      setMensaje('Tu asistencia fue actualizada.');
    } else if (evento.tipo === 'qr.rotado') {
      setMensaje('El QR de la clase cambió. Escaneá el código nuevo.');
    } else if (evento.tipo === 'clase.cerrada') {
      setMensaje('Una clase se cerró.');
    }
  }, [cargarDictados, evento]);

  const registrarConCodigo = async (texto) => {
    setCargando(true);
    setMensaje(null);
    try {
      const { id_clase, token } = JSON.parse(texto);
      const resultado = await escanearClase(id_clase, token);
      setMensaje(
        resultado.ya_registrado
          ? 'Ya estabas registrado en esta clase.'
          : '¡Presencia registrada!'
      );
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  };

  const iniciarEscaneo = async () => {
    if (permission && !permission.granted) {
      await requestPermission();
    }
    setEscaneando(true);
  };

  return (
    <ScrollView style={styles.pantalla}>
      <View style={styles.contenedor}>
        <Text style={styles.titulo}>Mis dictados</Text>

        {dictados.map((dictado) => (
          <View key={dictado.id_dictado} style={styles.tarjeta}>
            <Text style={styles.textoPrincipal}>
              {dictado.nombre_materia} - {dictado.grupo}
            </Text>
            <Text style={styles.textoSecundario}>
              {dictado.dia} {dictado.horario} · {dictado.apellido_profesor}
            </Text>
          </View>
        ))}

        {dictados.length === 0 && !cargando ? (
          <Text style={styles.mensaje}>No tenés dictados asignados.</Text>
        ) : null}

        <Text style={styles.etiqueta}>Registrar presencia</Text>

        <TouchableOpacity style={styles.boton} onPress={iniciarEscaneo}>
          <Text style={styles.textoBoton}>Escanear QR de la clase</Text>
        </TouchableOpacity>

        {escaneando ? (
          <View style={styles.camaraContenedor}>
            <CameraView
              style={styles.camara}
              facing="back"
              barcodeScannerSettings={{ barcodeTypes: ['qr'] }}
              onBarcodeScanned={({ data }) => {
                setEscaneando(false);
                registrarConCodigo(data);
              }}
            />
            <TouchableOpacity style={styles.boton} onPress={() => setEscaneando(false)}>
              <Text style={styles.textoBoton}>Cancelar</Text>
            </TouchableOpacity>
          </View>
        ) : null}

        <Text style={styles.etiqueta}>O ingresá el código manualmente</Text>
        <TextInput
          style={styles.campo}
          value={codigo}
          onChangeText={setCodigo}
          placeholder='Ej: {"id_clase":1,"token":"..."}'
          autoCapitalize="none"
          autoCorrect={false}
          nativeID="codigo-qr"
        />
        <TouchableOpacity
          style={[styles.boton, styles.botonCodigo]}
          onPress={() => registrarConCodigo(codigo)}
        >
          <Text style={styles.textoBoton}>Registrar con código</Text>
        </TouchableOpacity>

        {cargando ? (
          <ActivityIndicator size="large" color="#1f6feb" style={styles.cargando} />
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
  campo: {
    borderWidth: 1,
    borderColor: '#d0d7de',
    borderRadius: 8,
    padding: 12,
    backgroundColor: '#ffffff',
    fontSize: 16
  },
  tarjeta: {
    borderWidth: 1,
    borderColor: '#d0d7de',
    borderRadius: 8,
    padding: 12,
    marginTop: 8,
    backgroundColor: '#ffffff'
  },
  camaraContenedor: {
    marginTop: 16,
    borderRadius: 8,
    overflow: 'hidden'
  },
  camara: {
    height: 320,
    borderRadius: 8
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
  botonCodigo: {
    backgroundColor: '#2da44e'
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