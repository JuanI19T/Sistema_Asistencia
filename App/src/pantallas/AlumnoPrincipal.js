import { useCallback, useEffect, useState } from 'react';
import {
  ActivityIndicator,
  AppState,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
  useWindowDimensions
} from 'react-native';
import { CameraView, useCameraPermissions } from 'expo-camera';
import { escanearClase, ingresarConCodigo, obtenerClasesAlumnoHoy } from '../api';
import { colores } from '../tema';
import {
  IconoAviso,
  IconoCodigo,
  IconoQR,
  IconoReloj,
  IconoTilde
} from '../componentes/Iconos';

const INTERVALO_RESPALDO = 5000;
const INTERVALO_RECONCILIACION = 30000;

const ESTADOS = {
  
  asistida: {
    etiqueta: 'Asistida',
    detalle: 'Presencia registrada',
    borde: '#76BC91',
    fondoHorario: '#E6F3E7',
    fondoEtiqueta: '#DFF1E1',
    texto: '#176C3B',
    Icono: IconoTilde
  },

  en_curso: {
    etiqueta: 'En curso',
    detalle: 'Horario actual',
    borde: '#D96B70',
    fondoHorario: '#FBE7E7',
    fondoEtiqueta: '#F8DCDD',
    texto: '#B4232C',
    Icono: IconoReloj
  },

  pendiente: {
    etiqueta: 'Pendiente',
    detalle: 'Clase futura',
    borde: '#B8CCE8',
    fondoHorario: '#E8F0FA',
    fondoEtiqueta: '#DCE9FA',
    texto: '#17307A',
    Icono: IconoReloj
  },

  vencida: {
    etiqueta: 'Vencida',
    detalle: 'Clase finalizada',
    borde: '#525252',
    fondoHorario: '#E8E8E8',
    fondoEtiqueta: '#E2E2E2',
    texto: '#333333',
    Icono: IconoAviso
  }

};

function recortarHora(horario) {
  if (!horario || typeof horario !== 'string') return '';
  return horario.slice(0, 5);
}

export default function AlumnoPrincipal({ sesion, evento, eventosConectados }) {
  const { width } = useWindowDimensions();
  const esPantallaAngosta = width < 520;
  const [clases, setClases] = useState([]);
  const [escaneando, setEscaneando] = useState(false);
  const [codigo, setCodigo] = useState('');
  const [cargando, setCargando] = useState(false);
  const [enviando, setEnviando] = useState(false);
  const [mensaje, setMensaje] = useState(null);
  const [permission, requestPermission] = useCameraPermissions();

  const cargarClases = useCallback(async () => {
    setCargando(true);
    setMensaje(null);
    try {
      const resp = await obtenerClasesAlumnoHoy(sesion.id_alumno);
      setClases(resp.clases || []);
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  }, [sesion.id_alumno]);

  const sincronizar = useCallback(async () => {
    try {
      const resp = await obtenerClasesAlumnoHoy(sesion.id_alumno);
      setClases(resp.clases || []);
    } catch (error) {
      if (!eventosConectados) setMensaje(error.message);
    }
  }, [eventosConectados, sesion.id_alumno]);

  useEffect(() => {
    cargarClases();
  }, [cargarClases]);

  useEffect(() => {
    const intervalo = setInterval(() => {
      if (AppState.currentState === 'active') void sincronizar();
    }, eventosConectados ? INTERVALO_RECONCILIACION : INTERVALO_RESPALDO);

    return () => clearInterval(intervalo);
  }, [eventosConectados, sincronizar]);

  useEffect(() => {
    if (!evento) return;

    if (evento.tipo === 'conectado' || evento.tipo === 'clase.abierta') {
      void cargarClases();
    } else if (evento.tipo === 'asistencia.registrada') {
      setMensaje('Tu asistencia fue actualizada.');
      void cargarClases();
    } else if (evento.tipo === 'qr.rotado') {
      setMensaje('El QR de la clase cambió. Escaneá el código nuevo.');
    } else if (evento.tipo === 'clase.cerrada') {
      setMensaje('Una clase se cerró.');
      void cargarClases();
    }
  }, [cargarClases, evento]);

  const registrarConCodigo = async (texto) => {
    setEnviando(true);
    setMensaje(null);
    try {
      let resultado;
      const limpio = (texto || '').trim();

      if (/^\d{6}$/.test(limpio)) {
        resultado = await ingresarConCodigo(limpio);
      } else {
        const { id_clase, token } = JSON.parse(limpio);
        resultado = await escanearClase(id_clase, token);
      }

      setMensaje(
        resultado.ya_registrado
          ? 'Ya estabas registrado en esta clase.'
          : '¡Presencia registrada!'
      );
      void cargarClases();
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setEnviando(false);
    }
  };

  const iniciarEscaneo = async () => {
    if (permission && !permission.granted) {
      await requestPermission();
    }
    setEscaneando(true);
  };

  const registrarManual = () => {
    if (!codigo.trim() || enviando) return;
    void registrarConCodigo(codigo);
  };

  const hayClases = clases.length > 0;

  return (
    <ScrollView style={styles.pantalla} contentContainerStyle={styles.contenido}>
      <View style={styles.centrado}>
        <Text style={styles.titulo}>Mis Clases</Text>

        {clases.map((clase) => {
          const estado = ESTADOS[clase.estado] || ESTADOS.pendiente;
          const { Icono } = estado;
          return (
            <View
              key={`${clase.id_dictado}-${clase.id_clase || 'x'}`}
              style={[styles.tarjeta, esPantallaAngosta && styles.tarjetaAngosta, { borderColor: estado.borde }]}
            >
              <View style={[styles.panelHorario, { backgroundColor: estado.fondoHorario }]}>
                <Text style={styles.hora}>{recortarHora(clase.horario)}</Text>
                <View style={[styles.lineaHorario, { backgroundColor: estado.borde }]} />
                <Text style={styles.dia}>{clase.dia}</Text>
              </View>

              <View style={styles.panelDatos}>
                <Text style={styles.materia}>{clase.nombre_materia}</Text>
                <Text style={styles.docente}>
                  {clase.apellido_profesor ? `Prof. ${clase.apellido_profesor}` : 'Sin docente'}
                  {` - ${recortarHora(clase.horario)} - ${recortarHora(clase.horario_fin)}`}
                </Text>
              </View>

              <View
                style={[
                  styles.panelEstado,
                  esPantallaAngosta && styles.panelEstadoAngosto
                ]}
              >
                <View style={[styles.etiqueta, { backgroundColor: estado.fondoEtiqueta, marginTop: 0 }]}>
                  <Icono tamano={14} color={estado.texto} />
                  <Text style={[styles.textoEtiqueta, { color: estado.texto }]}> 
                    {estado.etiqueta}
                  </Text>
                </View>
              </View>
            </View>
          );
        })}

        {!hayClases && !cargando ? (
          <Text style={styles.vacio}>No tenés clases programadas para hoy.</Text>
        ) : null}

        <View style={styles.separador} />

        <Text style={styles.titulo}>Registrar Presencia</Text>

        <TouchableOpacity
          style={styles.botonEscanear}
          onPress={iniciarEscaneo}
          activeOpacity={0.85}
        >
          <IconoQR tamano={26} color={colores.blanco} />
          <Text style={styles.textoEscanear}>Escanear QR</Text>
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
            <TouchableOpacity style={styles.botonCancelar} onPress={() => setEscaneando(false)}>
              <Text style={styles.textoCancelar}>Cancelar</Text>
            </TouchableOpacity>
          </View>
        ) : null}

        <View style={styles.filaRegistro}>
          <View style={styles.campoCodigo}>
            <IconoCodigo tamano={22} />
            <TextInput
              style={styles.entradaCodigo}
              value={codigo}
              onChangeText={setCodigo}
              placeholder="Código"
              placeholderTextColor={colores.textoSuave}
              autoCapitalize="none"
              autoCorrect={false}
              nativeID="codigo-qr"
            />
          </View>
          <TouchableOpacity
            style={[styles.botonRegistrar, enviando && styles.botonDeshabilitado]}
            onPress={registrarManual}
            disabled={enviando}
            activeOpacity={0.85}
          >
            {enviando ? (
              <ActivityIndicator color={colores.navy} />
            ) : (
              <Text style={styles.textoRegistrar}>Registrar</Text>
            )}
          </TouchableOpacity>
        </View>

        {cargando && !enviando ? (
          <ActivityIndicator size="large" color={colores.navy} style={styles.cargando} />
        ) : null}

        {mensaje ? <Text style={styles.mensaje}>{mensaje}</Text> : null}
      </View>

      <View style={styles.pie}>
        <Text style={styles.textoPie}>Sistema Integral de Asistencia (SIA)</Text>
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({

  pantalla: {
    flex: 1,
    backgroundColor: colores.crema
  },

  contenido: {
    flexGrow: 1
  },

  centrado: {
    paddingHorizontal: 20,
    paddingTop: 20,
    paddingBottom: 28,
    maxWidth: 720,
    width: '100%',
    alignSelf: 'center'
  },

  titulo: {
    fontFamily: 'Georgia',
    fontWeight: 'bold',
    fontSize: 34,
    color: colores.navy,
    textAlign: 'center',
    marginVertical: 14
  },

  tarjeta: {
    flexDirection: 'row',
    borderWidth: 2,
    borderRadius: 10,
    overflow: 'hidden',
    backgroundColor: '#FFFDF7',
    marginBottom: 32,
    shadowColor: '#000000',
    shadowOffset: { width: 0, height: 3 },
    shadowOpacity: 0.08,
    shadowRadius: 6,
    elevation: 2
  },

  tarjetaAngosta: {
    flexWrap: 'wrap'
  },

  panelHorario: {
    width: '23%',
    paddingVertical: 18,
    paddingHorizontal: 10,
    alignItems: 'center',
    justifyContent: 'center'
  },

  hora: {
    fontFamily: Platform.select({ 
      ios: 'Avenir Next', 
      android: 'sans-serif', 
      default: 'Arial' 
      }),
    fontWeight: '600',
    fontSize: 24,
    color: colores.navy
  },

  lineaHorario: {
    width: 44,
    height: 2,
    borderRadius: 1,
    marginVertical: 10
  },

  dia: {
    fontSize: 12,
    letterSpacing: 1,
    fontWeight: '600',
    color: '#66758B',
    textAlign: 'center'
  },

  panelDatos: {
    flex: 1,
    paddingVertical: 18,
    paddingHorizontal: 16,
    justifyContent: 'center',
    alignItems: 'flex-start'
  },
  panelEstado: {
    width: 'auto',
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 8,
    marginLeft: 12
  },

  panelEstadoAngosto: {
    width: '100%',
    paddingVertical: 10,
    marginLeft: 0
  },

  materia: {
    fontFamily: 'Georgia',
    fontWeight: 'bold',
    fontSize: 21,
    color: colores.navy,
    textAlign: 'left'
  },

  docente: {
    fontSize: 14,
    color: '#66758B',
    marginTop: 4,
    textAlign: 'left'
  },

  etiqueta: {
    flexDirection: 'row',
    alignItems: 'center',
    alignSelf: 'center',
    borderRadius: 8,
    paddingHorizontal: 12,
    paddingVertical: 6,
    marginTop: 12
  },

  textoEtiqueta: {
    fontSize: 13,
    fontWeight: '700',
    marginLeft: 6
  },

  vacio: {
    textAlign: 'center',
    color: '#66758B',
    fontSize: 15,
    marginTop: 8,
    marginBottom: 16
  },

  separador: {
    height: 1.5,
    backgroundColor: colores.borde,
    marginVertical: 24
  },

  botonEscanear: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: colores.navy,
    borderRadius: 10,
    paddingVertical: 20,
    paddingHorizontal: 20,
    shadowColor: '#000000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 3
  },

  textoEscanear: {
    color: colores.blanco,
    fontSize: 20,
    fontWeight: '700',
    marginLeft: 12
  },

  camaraContenedor: {
    marginTop: 16,
    borderRadius: 10,
    overflow: 'hidden'
  },

  camara: {
    height: 320
  },

  botonCancelar: {
    backgroundColor: colores.textoSuave,
    paddingVertical: 14,
    alignItems: 'center'
  },

  textoCancelar: {
    color: colores.blanco,
    fontSize: 16,
    fontWeight: '600'
  },

  filaRegistro: {
    flexDirection: 'row',
    alignItems: 'stretch',
    marginTop: 16
  },

  campoCodigo: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1.5,
    borderColor: colores.borde,
    borderRadius: 10,
    backgroundColor: '#FFFDF7',
    paddingHorizontal: 16,
    minHeight: 58
  },

  entradaCodigo: {
    flex: 1,
    fontSize: 16,
    color: colores.textoOscuro,
    marginLeft: 12,
    paddingVertical: 12
  },

  botonRegistrar: {
    backgroundColor: colores.amarillo,
    borderRadius: 10,
    paddingHorizontal: 22,
    justifyContent: 'center',
    alignItems: 'center',
    marginLeft: 12,
    minWidth: 120
  },

  botonDeshabilitado: {
    opacity: 0.6
  },

  textoRegistrar: {
    color: colores.navy,
    fontSize: 17,
    fontWeight: '700'
  },

  cargando: {
    marginTop: 20
  },

  mensaje: {
    marginTop: 16,
    color: '#B4232C',
    fontSize: 14,
    textAlign: 'center'
  },

  pie: {
    backgroundColor: colores.navy,
    width: '100%',
    marginTop: 'auto',
    paddingVertical: 26,
    paddingHorizontal: 20,
    alignItems: 'center',
    justifyContent: 'center'
  },

  textoPie: {
    color: colores.blanco,
    fontSize: 15,
    textAlign: 'center'
  }

});
