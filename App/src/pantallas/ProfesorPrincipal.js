import { useCallback, useEffect, useState } from 'react';
import {
  ActivityIndicator,
  AppState,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
  useWindowDimensions
} from 'react-native';
import QRCode from 'react-native-qrcode-svg';
import { abrirClase, fechaDeHoy, obtenerClasesProfesorHoy, rotarToken } from '../api';
import { colores } from '../tema';
import { IconoAviso, IconoReloj, IconoTilde } from '../componentes/Iconos';

const INTERVALO_RESPALDO = 5000;
const INTERVALO_RECONCILIACION = 30000;

const ESTADOS = {

  asistida: { 
    etiqueta: 'Asistida', 
    borde: '#76BC91', 
    fondoHorario: '#E6F3E7', 
    fondo: '#DFF1E1', 
    texto: '#176C3B', 
    Icono: IconoTilde },

  en_curso: { 
    etiqueta: 'En curso', 
    borde: '#D96B70', 
    fondoHorario: '#FBE7E7', 
    fondo: '#F8DCDD', 
    texto: '#B4232C', 
    Icono: IconoReloj },

  pendiente: { 
    etiqueta: 'Pendiente', 
    borde: '#B8CCE8', 
    fondoHorario: '#E8F0FA', 
    fondo: '#DCE9FA', 
    texto: '#17307A', 
    Icono: IconoReloj },

  vencida: { 
    etiqueta: 'Vencida', 
    borde: '#525252', 
    fondoHorario: '#E8E8E8', 
    fondo: '#E2E2E2', 
    texto: '#333333', 
    Icono: IconoAviso }

};

function recortarHora(horario) {
  return horario && typeof horario === 'string' ? horario.slice(0, 5) : '';
}

function segundosRestantes(fecha) {
  if (!fecha) return 0;
  return Math.max(0, Math.floor((new Date(fecha).getTime() - Date.now()) / 1000));
}

export default function ProfesorPrincipal({ sesion, evento, eventosConectados }) {
  const { width } = useWindowDimensions();
  const esPantallaAngosta = width < 520;
  const [clases, setClases] = useState([]);
  const [claseSeleccionada, setClaseSeleccionada] = useState(null);
  const [claseActiva, setClaseActiva] = useState(null);
  const [restante, setRestante] = useState(0);
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState(null);

  const cargarClases = useCallback(async () => {
    setCargando(true);
    try {
      const respuesta = await obtenerClasesProfesorHoy(sesion.id_profesor);
      setClases(respuesta.clases || []);
      setClaseSeleccionada((anterior) => {
        if (!anterior) return respuesta.clases?.find((clase) => clase.estado === 'en_curso') || respuesta.clases?.[0] || null;
        return respuesta.clases?.find((clase) => clase.id_dictado === anterior.id_dictado) || null;
      });
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  }, [sesion.id_profesor]);

  useEffect(() => { void cargarClases(); }, [cargarClases]);

  useEffect(() => {
    const intervalo = setInterval(() => {
      if (AppState.currentState === 'active') void cargarClases();
    }, eventosConectados ? INTERVALO_RECONCILIACION : INTERVALO_RESPALDO);
    return () => clearInterval(intervalo);
  }, [cargarClases, eventosConectados]);

  useEffect(() => {
    if (evento?.tipo === 'conectado' || evento?.tipo === 'clase.abierta' || evento?.tipo === 'clase.cerrada') {
      void cargarClases();
    }
  }, [cargarClases, evento]);

  useEffect(() => {
    setRestante(segundosRestantes(claseActiva?.token_valido_hasta));
    const intervalo = setInterval(() => setRestante(segundosRestantes(claseActiva?.token_valido_hasta)), 1000);
    return () => clearInterval(intervalo);
  }, [claseActiva?.token_valido_hasta]);

  const abrirSeleccionada = async () => {
    const dictado = claseSeleccionada;
    if (!dictado) {
      setMensaje('No hay una clase seleccionada para abrir.');
      return;
    }

    if (dictado.estado !== 'en_curso') {
      setMensaje('La clase solo puede abrirse durante su horario programado.');
      return;
    }

    setCargando(true);
    setMensaje(null);
    try {
      const clase = await abrirClase(dictado.id_dictado, fechaDeHoy());
      setClaseActiva(clase);
      await cargarClases();
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
      setClaseActiva(await rotarToken(claseActiva.id_clase));
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  };

  const enCurso = claseActiva?.estado === 'en_curso';
  const codigoQR = enCurso && claseActiva.token
    ? JSON.stringify({ id_clase: claseActiva.id_clase, token: claseActiva.token })
    : '';
  const puedeAbrir = claseSeleccionada?.estado === 'en_curso';

  const seleccionarClase = (clase) => {
    setMensaje(null);
    setClaseSeleccionada(clase);
  };

  return (
    <ScrollView style={styles.pantalla} contentContainerStyle={styles.contenido}>
      <View style={styles.centrado}>
        <Text style={styles.titulo}>Mis Clases</Text>

        {clases.map((clase) => {
          const estado = ESTADOS[clase.estado] || ESTADOS.pendiente;
          const { Icono } = estado;
          return (
            <TouchableOpacity
              key={clase.id_dictado}
              activeOpacity={0.85}
              onPress={() => seleccionarClase(clase)}
              style={[styles.tarjeta, esPantallaAngosta && styles.tarjetaAngosta, { borderColor: estado.borde }]}
            >
              <View style={[styles.panelHorario, esPantallaAngosta && styles.panelHorarioAngosto, { backgroundColor: estado.fondoHorario }]}>
                <Text style={styles.hora}>{recortarHora(clase.horario)}</Text>
                <View style={[styles.lineaHorario, { backgroundColor: estado.borde || colores.azulReal }]} />
                <Text style={styles.dia}>{clase.dia}</Text>
              </View>
              <View style={styles.panelDatos}>
                <Text style={styles.materia}>{clase.nombre_materia}</Text>
                <Text style={styles.docente}>
                  {clase.apellido_profesor ? `Prof. ${clase.apellido_profesor}` : 'Sin docente'}
                  {` - ${recortarHora(clase.horario)} - ${recortarHora(clase.horario_fin)}`}
                </Text>
              </View>
              <View style={[styles.panelEstado, esPantallaAngosta && styles.panelEstadoAngosto]}>
                <View style={[styles.etiqueta, { backgroundColor: estado.fondo }]}>
                  <Icono tamano={14} color={estado.texto} />
                  <Text style={[styles.textoEtiqueta, { color: estado.texto }]}>{estado.etiqueta}</Text>
                </View>
              </View>
            </TouchableOpacity>
          );
        })}

        {!clases.length && !cargando ? <Text style={styles.vacio}>No tenés clases programadas para hoy.</Text> : null}

        <View style={styles.separador} />
        <Text style={styles.titulo}>Abrir Clase</Text>
        <Text style={styles.instruccion}>Seleccioná una clase y abrila únicamente durante su horario.</Text>
        <TouchableOpacity
          style={[styles.botonAbrir, !puedeAbrir && styles.botonNoDisponible, cargando && styles.botonDeshabilitado]}
          onPress={abrirSeleccionada}
          disabled={cargando}
        >
          {cargando ? <ActivityIndicator color={colores.blanco} /> : <Text style={styles.textoBoton}>Abrir clase</Text>}
        </TouchableOpacity>

        {claseActiva && enCurso ? (
          <View style={styles.panelQR}>
            <Text style={styles.subtituloQR}>Clase abierta</Text>
            <Text style={styles.datosClaseQR}>{claseActiva.nombre_materia}</Text>
            <Text style={styles.detalleClaseQR}>
              {claseActiva.apellido_profesor ? `Prof. ${claseActiva.apellido_profesor}` : 'Sin docente'}
            </Text>
            <Text style={styles.detalleClaseQR}>
              {claseActiva.dia} · {recortarHora(claseActiva.horario)} - {recortarHora(claseActiva.horario_fin)}
            </Text>
            <Text style={styles.detalleClaseQR}>Grupo {claseActiva.grupo}</Text>
            <View style={styles.qrContenedor}><QRCode value={codigoQR} size={220} /></View>
            <Text style={styles.textoSecundario}>El QR vence en {restante}s · los alumnos lo escanean para registrarse.</Text>
            {claseActiva.codigo ? <Text style={styles.codigoClase}>{claseActiva.codigo}</Text> : null}
            <TouchableOpacity style={styles.botonRotar} onPress={rotar} disabled={cargando}>
              <Text style={styles.textoBoton}>Rotar QR</Text>
            </TouchableOpacity>
          </View>
        ) : null}

        {mensaje ? <Text style={styles.mensaje}>{mensaje}</Text> : null}
      </View>
      <View style={styles.pie}><Text style={styles.textoPie}>Sistema Integral de Asistencia (SIA)</Text></View>
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
    marginBottom: 22, 
    shadowColor: '#000', 
    shadowOffset: { 
      width: 0, 
      height: 3 
      }, 
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

  panelHorarioAngosto: { 
    width: '23%', 
    paddingHorizontal: 4 
  },

  hora: { 
    fontFamily: Platform.select({ 
      ios: 'Avenir Next', 
      android: 'sans-serif', 
      default: 'Arial' 
      }), 
    fontWeight: '600', 
    fontSize: 24, color: colores.navy 
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
    minWidth: 0, 
    paddingVertical: 18, 
    paddingHorizontal: 16, 
    justifyContent: 'center', 
    alignItems: 'flex-start' 
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

  etiqueta: { 
    flexDirection: 'row', 
    alignItems: 'center', 
    alignSelf: 'center', 
    borderRadius: 8, 
    paddingHorizontal: 12, 
    paddingVertical: 6 
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

  instruccion: { 
    textAlign: 'center', 
    color: '#66758B', 
    fontSize: 14, 
    marginBottom: 12 
  },

  botonAbrir: { 
    backgroundColor: colores.navy, 
    borderRadius: 10, 
    minHeight: 58, 
    justifyContent: 'center', 
    alignItems: 'center' 
  },

  botonRotar: { 
    backgroundColor: colores.azulReal, 
    borderRadius: 10, minHeight: 48, 
    justifyContent: 'center', 
    alignItems: 'center', 
    marginTop: 16, 
    paddingHorizontal: 20 
  },

  botonDeshabilitado: { 
    opacity: 0.6 
  },

  botonNoDisponible: { 
    backgroundColor: colores.textoSuave 
  },

  textoBoton: { 
    color: colores.blanco, 
    fontSize: 17, 
    fontWeight: '700' 
  },

  panelQR: { 
    alignItems: 'center', 
    marginTop: 24, 
    padding: 16, 
    borderWidth: 2, 
    borderColor: colores.borde, 
    borderRadius: 10, 
    backgroundColor: '#FFFDF7' 
  },

  subtituloQR: { 
    fontFamily: 'Georgia', 
    fontWeight: 'bold', 
    fontSize: 24, 
    color: colores.navy, 
    marginBottom: 14 
  },

  datosClaseQR: { 
    fontFamily: 'Georgia', 
    fontWeight: 'bold', 
    fontSize: 21, 
    color: colores.navy, 
    textAlign: 'center' 
  },

  detalleClaseQR: { 
    color: '#66758B', 
    fontSize: 14, 
    textAlign: 'center', 
    marginTop: 3 
  },

  qrContenedor: { 
    backgroundColor: colores.blanco, 
    padding: 16, 
    borderRadius: 10 
  },

  codigoClase: { 
    fontSize: 28, 
    fontWeight: 'bold', 
    letterSpacing: 5, 
    color: colores.navy, 
    marginTop: 16 
  },

  textoSecundario: { 
    fontSize: 14, 
    color: '#66758B', 
    textAlign: 'center', 
    marginTop: 12 
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
    width: '100%', marginTop: 'auto', 
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
