import { useEffect, useMemo, useState } from 'react';
import { ActivityIndicator, ScrollView, StyleSheet, Text, View, useWindowDimensions } from 'react-native';
import Svg, { Defs, LinearGradient, Rect, Stop } from 'react-native-svg';
import { obtenerDictados, obtenerDictadosAlumno } from '../api';
import { colores } from '../tema';

const DIAS = ['LUNES', 'MARTES', 'MIERCOLES', 'JUEVES', 'VIERNES'];
const NOMBRES_DIAS = ['Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes'];
const COLORES_CLASE = [
  { fondo: '#FFFDF7', borde: '#0066FF' },
  { fondo: '#FFFDF7', borde: '#F67B00' },
  { fondo: '#FFFDF7', borde: '#6600FF' },
  { fondo: '#FFFDF7', borde: '#FF3399' },
  { fondo: '#FFFDF7', borde: '#009200' },
  { fondo: '#FFFDF7', borde: '#FFFF00' },
  { fondo: '#FFFDF7', borde: '#FF0000' },
  { fondo: '#FFFDF7', borde: '#00FFFF' },
  { fondo: '#FFFDF7', borde: '#53FF1D' },
  { fondo: '#FFFDF7', borde: '#AE85FF' },
  { fondo: '#FFFDF7', borde: '#4784FF' },
  { fondo: '#FFFDF7', borde: '#FF7979' },
  { fondo: '#FFFDF7', borde: '#AEFF85' },
  { fondo: '#FFFDF7', borde: '#00B082' },
  { fondo: '#FFFDF7', borde: '#E27100' },
  { fondo: '#FFFDF7', borde: '#6572FF' },
  { fondo: '#FFFDF7', borde: '#FF8FC7' },
  { fondo: '#FFFDF7', borde: '#FFA74F' },
  { fondo: '#FFFDF7', borde: '#ADFF09' },
  { fondo: '#FFFDF7', borde: '#B40000' }
];

function sinAcentos(texto) {
  return (texto || '').normalize('NFD').replace(/[\u0300-\u036f]/g, '').toUpperCase().trim();
}

function hora(valor) {
  return typeof valor === 'string' ? valor.slice(0, 5) : '';
}

function claveDeClase(clase, rol) {
  return rol === 'profesor'
    ? `${clase.id_materia || clase.nombre_materia}-${clase.grupo || 'sin-grupo'}-${clase.anio_materia || 'sin-anio'}-${clase.division || 'sin-division'}`
    : `${clase.id_materia || clase.nombre_materia}`;
}

function aclararColor(hex, proporcion = 0.84) {
  const rojo = parseInt(hex.slice(1, 3), 16);
  const verde = parseInt(hex.slice(3, 5), 16);
  const azul = parseInt(hex.slice(5, 7), 16);
  const aclarar = (valor) => Math.round(valor + (255 - valor) * proporcion).toString(16).padStart(2, '0');
  return `#${aclarar(rojo)}${aclarar(verde)}${aclarar(azul)}`;
}

function ClaseHorario({ clase, rol, color }) {
  const idGradiente = `gradiente-clase-${clase.id_dictado}`;
  return (
    <View style={styles.contenedorTarjeta}>
      <View style={[styles.clase, { borderLeftColor: color.borde }]}>
        <Svg pointerEvents="none" style={StyleSheet.absoluteFillObject}>
          <Defs>
            <LinearGradient id={idGradiente} x1="0" y1="0" x2="1" y2="1">
              <Stop offset="0" stopColor={aclararColor(color.borde, 0.8)} />
              <Stop offset="0.58" stopColor={aclararColor(color.borde, 0.9)} />
              <Stop offset="1" stopColor="#FFFDF7" />
            </LinearGradient>
          </Defs>
          <Rect x="0" y="0" width="100%" height="100%" rx="8" fill={`url(#${idGradiente})`} />
        </Svg>
        <Text style={styles.hora}>{hora(clase.horario)} - {hora(clase.horario_fin)}</Text>
        <Text style={styles.materia}>{clase.nombre_materia}</Text>
        {rol !== 'profesor' && clase.apellido_profesor ? (
          <Text style={styles.detalle}>Prof. {clase.apellido_profesor}</Text>
        ) : null}
        {clase.anio_materia || clase.division || clase.nombre_especialidad ? (
          <Text style={styles.detalle}>
            {clase.anio_materia ? `${clase.anio_materia}° año` : ''}
            {clase.division ? ` · División ${clase.division}` : ''}
            {clase.nombre_especialidad ? ` · ${clase.nombre_especialidad}` : ''}
          </Text>
        ) : null}
        {(rol === 'preceptor' || rol === 'profesor') && clase.grupo ? (
          <Text style={styles.detalle}>Grupo {clase.grupo}</Text>
        ) : null}
      </View>
    </View>
  );
}

export default function Horarios({ sesion }) {
  const { width } = useWindowDimensions();
  const [dictados, setDictados] = useState([]);
  const [cargando, setCargando] = useState(true);
  const [mensaje, setMensaje] = useState(null);

  useEffect(() => {
    let activo = true;
    setCargando(true);
    setMensaje(null);
    const cargar = sesion.rol === 'alumno'
      ? obtenerDictadosAlumno(sesion.id_alumno)
      : obtenerDictados();

    cargar.then((datos) => {
      if (!activo) return;
      const propios = sesion.rol === 'alumno'
        ? datos
        : datos.filter((dictado) => Number(dictado[`id_${sesion.rol}`]) === Number(sesion[`id_${sesion.rol}`]));
      setDictados(propios);
    }).catch((error) => {
      if (activo) setMensaje(error.message);
    }).finally(() => {
      if (activo) setCargando(false);
    });

    return () => { activo = false; };
  }, [sesion]);

  const porDia = useMemo(() => DIAS.map((dia) => (
    dictados
      .filter((dictado) => sinAcentos(dictado.dia) === dia)
      .sort((a, b) => String(a.horario).localeCompare(String(b.horario)))
  )), [dictados]);

  const coloresPorClase = useMemo(() => {
    const claves = [...new Set(dictados.map((clase) => claveDeClase(clase, sesion.rol)))];
    return new Map(claves.map((clave, indice) => [clave, COLORES_CLASE[indice % COLORES_CLASE.length]]));
  }, [dictados, sesion.rol]);

  // En web, el ancho de la ventana incluye la barra vertical de desplazamiento.
  // Reservamos ese espacio para que la quinta columna nunca quede recortada.
  const espacioDisponible = Math.max(0, width - 48);
  const separacion = 10;
  const anchoColumna = width >= 700
    ? Math.max(120, (espacioDisponible - separacion * 4) / 5)
    : 188;
  const renderColumna = (clases, indice, vertical = false) => (
    <View
      style={[
        styles.columna,
        vertical ? styles.columnaVertical : { width: anchoColumna, marginRight: indice < DIAS.length - 1 ? separacion : 0 }
      ]}
      key={DIAS[indice]}
    >
      <View style={styles.encabezadoDia}><Text style={styles.nombreDia}>{NOMBRES_DIAS[indice]}</Text></View>
      {clases.length ? clases.map((clase) => (
        <ClaseHorario
          key={clase.id_dictado}
          clase={clase}
          rol={sesion.rol}
          color={coloresPorClase.get(claveDeClase(clase, sesion.rol))}
                />
      )) : <Text style={styles.sinClases}>Sin clases</Text>}
    </View>
  );

  return (
    <ScrollView
      style={styles.pantalla}
      contentContainerStyle={[styles.contenido, width < 700 && { width, minWidth: width }]}
    >
      <Text style={styles.titulo}>Horarios</Text>
      <Text style={styles.subtitulo}>Tus clases semanales</Text>
      {cargando ? <ActivityIndicator color={colores.azulReal} style={styles.cargando} /> : null}
      {mensaje ? <Text style={styles.mensaje}>{mensaje}</Text> : null}
      {!cargando && !mensaje ? (
        width < 700 ? (
          <View style={styles.semanaVertical}>
            {porDia.map((clases, indice) => renderColumna(clases, indice, true))}
          </View>
        ) : (
          <ScrollView
            horizontal
            showsHorizontalScrollIndicator={false}
            contentContainerStyle={[styles.semana, { width: espacioDisponible }]}
          >
            {porDia.map((clases, indice) => renderColumna(clases, indice))}
          </ScrollView>
        )
      ) : null}
      {!cargando && !mensaje && !dictados.length ? <Text style={styles.vacio}>No hay horarios asignados.</Text> : null}
      <View style={styles.pie}><Text style={styles.textoPie}>Sistema Integral de Asistencia (SIA)</Text></View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  pantalla: { flex: 1, backgroundColor: colores.crema },
  contenido: { flexGrow: 1, width: '100%', alignItems: 'stretch', paddingHorizontal: 16, paddingTop: 18, paddingBottom: 0 },
  titulo: { fontFamily: 'Georgia', fontWeight: 'bold', fontSize: 34, color: colores.navy, textAlign: 'center', marginVertical: 12 },
  subtitulo: { textAlign: 'center', color: colores.textoSuave, fontSize: 15, marginBottom: 18 },
  cargando: { marginTop: 30 },
  semana: { paddingBottom: 28 },
  semanaVertical: { width: '100%', alignSelf: 'stretch', paddingBottom: 28 },
  columna: { width: 188, minWidth: 0, flexShrink: 0, alignItems: 'stretch', minHeight: 230, backgroundColor: '#FFFDF7', borderRadius: 10, overflow: 'hidden', borderWidth: 1, borderColor: colores.borde },
  columnaVertical: { width: '100%', alignSelf: 'stretch', marginBottom: 12 },
  encabezadoDia: { backgroundColor: '#DCE9FA', paddingVertical: 12, alignItems: 'center', borderBottomWidth: 2, borderBottomColor: colores.azulReal },
  nombreDia: { color: colores.navy, fontWeight: '700', fontSize: 16 },
  contenedorTarjeta: { width: '100%', alignSelf: 'stretch', paddingHorizontal: 9, paddingVertical: 9 },
  clase: {
    width: '100%',
    padding: 11,
    borderRadius: 8,
    overflow: 'hidden',
    backgroundColor: '#FFFDF7',
    borderLeftWidth: 4,
    borderLeftColor: colores.amarillo
  },
  hora: { color: colores.navy, fontSize: 13, fontWeight: '700' },
  materia: { color: colores.textoOscuro, fontSize: 15, fontWeight: '700', marginTop: 5, flexShrink: 1, lineHeight: 19 },
  detalle: { color: colores.textoSuave, fontSize: 12, marginTop: 3 },
  sinClases: { color: colores.textoSuave, textAlign: 'center', marginTop: 28, fontSize: 13 },
  vacio: { textAlign: 'center', color: colores.textoSuave, marginTop: 20 },
  mensaje: { color: '#B4232C', textAlign: 'center', marginTop: 20 },
  pie: { backgroundColor: colores.navy, marginHorizontal: -16, marginTop: 'auto', paddingVertical: 26, alignItems: 'center' },
  textoPie: { color: colores.blanco, fontSize: 15 }
});
