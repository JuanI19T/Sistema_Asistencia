import { useEffect, useMemo, useState } from 'react';
import { ActivityIndicator, ScrollView, StyleSheet, Text, View, useWindowDimensions } from 'react-native';
import { obtenerDictados, obtenerDictadosAlumno } from '../api';
import { colores } from '../tema';

const DIAS = ['LUNES', 'MARTES', 'MIERCOLES', 'JUEVES', 'VIERNES'];
const NOMBRES_DIAS = ['Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes'];

function sinAcentos(texto) {
  return (texto || '').normalize('NFD').replace(/[\u0300-\u036f]/g, '').toUpperCase().trim();
}

function hora(valor) {
  return typeof valor === 'string' ? valor.slice(0, 5) : '';
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

  // En web, el ancho de la ventana incluye la barra vertical de desplazamiento.
  // Reservamos ese espacio para que la quinta columna nunca quede recortada.
  const espacioDisponible = Math.max(0, width - 48);
  const separacion = 10;
  const anchoColumna = width >= 700
    ? Math.max(120, (espacioDisponible - separacion * 4) / 5)
    : 188;

  return (
    <ScrollView style={styles.pantalla} contentContainerStyle={styles.contenido}>
      <Text style={styles.titulo}>Horarios</Text>
      <Text style={styles.subtitulo}>Tus clases semanales</Text>
      {cargando ? <ActivityIndicator color={colores.azulReal} style={styles.cargando} /> : null}
      {mensaje ? <Text style={styles.mensaje}>{mensaje}</Text> : null}
      {!cargando && !mensaje ? (
        <ScrollView
          horizontal
          showsHorizontalScrollIndicator={false}
          contentContainerStyle={[styles.semana, width >= 700 && { width: espacioDisponible }]}
        >
          {porDia.map((clases, indice) => (
            <View
              style={[styles.columna, { width: anchoColumna, marginRight: indice < DIAS.length - 1 ? separacion : 0 }]}
              key={DIAS[indice]}
            >
              <View style={styles.encabezadoDia}><Text style={styles.nombreDia}>{NOMBRES_DIAS[indice]}</Text></View>
              {clases.length ? clases.map((clase) => (
                <View style={styles.clase} key={clase.id_dictado}>
                  <Text style={styles.hora}>{hora(clase.horario)} - {hora(clase.horario_fin)}</Text>
                  <Text style={styles.materia}>{clase.nombre_materia}</Text>
                  {sesion.rol !== 'profesor' && clase.apellido_profesor ? (
                    <Text style={styles.detalle}>Prof. {clase.apellido_profesor}</Text>
                  ) : null}
                  {clase.anio_materia || clase.division || clase.nombre_especialidad ? (
                    <Text style={styles.detalle}>
                      {clase.anio_materia ? `${clase.anio_materia}° año` : ''}
                      {clase.division ? ` · División ${clase.division}` : ''}
                      {clase.nombre_especialidad ? ` · ${clase.nombre_especialidad}` : ''}
                    </Text>
                  ) : null}
                  {(sesion.rol === 'preceptor' || sesion.rol === 'profesor') && (clase.division || clase.grupo) ? (
                    clase.grupo ? <Text style={styles.detalle}>Grupo {clase.grupo}</Text> : null
                  ) : null}
                </View>
              )) : <Text style={styles.sinClases}>Sin clases</Text>}
            </View>
          ))}
        </ScrollView>
      ) : null}
      {!cargando && !mensaje && !dictados.length ? <Text style={styles.vacio}>No hay horarios asignados.</Text> : null}
      <View style={styles.pie}><Text style={styles.textoPie}>Sistema Integral de Asistencia (SIA)</Text></View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  pantalla: { flex: 1, backgroundColor: colores.crema },
  contenido: { flexGrow: 1, paddingHorizontal: 16, paddingTop: 18, paddingBottom: 0 },
  titulo: { fontFamily: 'Georgia', fontWeight: 'bold', fontSize: 34, color: colores.navy, textAlign: 'center', marginVertical: 12 },
  subtitulo: { textAlign: 'center', color: colores.textoSuave, fontSize: 15, marginBottom: 18 },
  cargando: { marginTop: 30 },
  semana: { paddingBottom: 28 },
  columna: { width: 188, minWidth: 0, flexShrink: 0, minHeight: 230, backgroundColor: '#FFFDF7', borderRadius: 10, overflow: 'hidden', borderWidth: 1, borderColor: colores.borde },
  encabezadoDia: { backgroundColor: '#DCE9FA', paddingVertical: 12, alignItems: 'center', borderBottomWidth: 2, borderBottomColor: colores.azulReal },
  nombreDia: { color: colores.navy, fontWeight: '700', fontSize: 16 },
  clase: { margin: 9, padding: 11, borderRadius: 8, backgroundColor: '#F8F1D9', borderLeftWidth: 4, borderLeftColor: colores.amarillo },
  hora: { color: colores.navy, fontSize: 13, fontWeight: '700' },
  materia: { color: colores.textoOscuro, fontSize: 15, fontWeight: '700', marginTop: 5, flexShrink: 1, lineHeight: 19 },
  detalle: { color: colores.textoSuave, fontSize: 12, marginTop: 3 },
  sinClases: { color: colores.textoSuave, textAlign: 'center', marginTop: 28, fontSize: 13 },
  vacio: { textAlign: 'center', color: colores.textoSuave, marginTop: 20 },
  mensaje: { color: '#B4232C', textAlign: 'center', marginTop: 20 },
  pie: { backgroundColor: colores.navy, marginHorizontal: -16, marginTop: 'auto', paddingVertical: 26, alignItems: 'center' },
  textoPie: { color: colores.blanco, fontSize: 15 }
});
