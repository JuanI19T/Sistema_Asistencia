import { useCallback, useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  AppState,
  Platform,
  ScrollView,
  StyleSheet,
  Switch,
  Text,
  TouchableOpacity,
  View
} from 'react-native';
import {
  cambiarEstadoClase,
  guardarAsistencia,
  obtenerAlumnosDictado,
  obtenerAsistencia,
  obtenerClasesPreceptorHoy
} from '../api';
import { colores } from '../tema';
import { IconoAviso, IconoReloj, IconoTilde } from '../componentes/Iconos';

const INTERVALO_ASISTENCIA = 2000;
const INTERVALO_RECONCILIACION = 30000;

const ESTADOS = {

  asistida: { 
    etiqueta: 'Asistida', 
    borde: '#76BC91', 
    fondoHorario: '#E6F3E7', 
    fondo: '#DFF1E1', 
    texto: '#176C3B', 
    Icono: IconoTilde 
  },

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

function combinarEstados(alumnos, asistencia) {
  const estados = {};

  alumnos.forEach((alumno) => {
    estados[alumno.id_alumno] = { presente: 0, verificada: 0 };
  });

  asistencia.forEach((item) => {
    if (estados[item.id_alumno]) {
      estados[item.id_alumno] = {
        presente: item.presente,
        verificada: item.verificada
      };
    }
  });

  return estados;
}

export default function PreceptorPrincipal({ sesion, evento, eventosConectados }) {
  const [clases, setClases] = useState([]);
  const [seleccionada, setSeleccionada] = useState(null);
  const [alumnos, setAlumnos] = useState([]);
  const [estados, setEstados] = useState({});
  const [datosClaseCargados, setDatosClaseCargados] = useState(false);
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState(null);
  const claseSeleccionadaRef = useRef(null);
  const cargaClaseRef = useRef(0);
  const peticionAsistenciaRef = useRef(false);
  const modificadosRef = useRef(new Set());
  const editable = seleccionada?.estado === 'en_curso';

  const cargarClases = useCallback(async () => {
    setCargando(true);
    setMensaje(null);
    try {
      const respuesta = await obtenerClasesPreceptorHoy(sesion.id_preceptor);
      setClases(respuesta.clases || []);
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  }, []);

  const sincronizarClases = useCallback(async () => {
    try {
      const respuesta = await obtenerClasesPreceptorHoy(sesion.id_preceptor);
      const lista = respuesta.clases || [];
      setClases(lista);
      setSeleccionada((actual) => {
        if (!actual) return actual;
        return lista.find((clase) => clase.id_clase === actual.id_clase) ?? actual;
      });
    } catch (error) {
      if (!eventosConectados) setMensaje(error.message);
    }
  }, [eventosConectados]);

  useEffect(() => {
    cargarClases();
  }, [cargarClases]);

  useEffect(() => {
    return () => {
      claseSeleccionadaRef.current = null;
    };
  }, []);

  const elegirClase = async (clase) => {
    if (!clase.id_clase) {
      setMensaje(clase.estado === 'en_curso'
        ? 'La clase todavía no fue abierta por el profesor.'
        : 'La asistencia solo puede verificarse durante la clase.');
      return;
    }

    const idCarga = ++cargaClaseRef.current;
    claseSeleccionadaRef.current = clase;
    setSeleccionada(clase);
    setAlumnos([]);
    setEstados({});
    setDatosClaseCargados(false);
    modificadosRef.current.clear();
    setCargando(true);
    setMensaje(null);

    try {
      const [lista, asistencia] = await Promise.all([
        obtenerAlumnosDictado(clase.id_dictado),
        obtenerAsistencia(clase.id_clase)
      ]);

      if (
        idCarga !== cargaClaseRef.current ||
        claseSeleccionadaRef.current?.id_clase !== clase.id_clase
      ) {
        return;
      }

      setAlumnos(lista);
      setEstados(combinarEstados(lista, asistencia));
      setDatosClaseCargados(true);
    } catch (error) {
      if (idCarga === cargaClaseRef.current) setMensaje(error.message);
    } finally {
      if (idCarga === cargaClaseRef.current) setCargando(false);
    }
  };

  const refrescarAsistencia = useCallback(async (idClase) => {
    if (
      peticionAsistenciaRef.current ||
      claseSeleccionadaRef.current?.id_clase !== idClase
    ) {
      return;
    }

    peticionAsistenciaRef.current = true;
    try {
      const asistencia = await obtenerAsistencia(idClase);
      if (claseSeleccionadaRef.current?.id_clase !== idClase) return;

      setEstados((previos) => {
        const siguientes = { ...previos };
        let cambio = false;

        asistencia.forEach((item) => {
          if (
            !siguientes[item.id_alumno] ||
            modificadosRef.current.has(item.id_alumno)
          ) {
            return;
          }

          const estado = {
            presente: item.presente,
            verificada: item.verificada
          };
          if (
            siguientes[item.id_alumno].presente !== estado.presente ||
            siguientes[item.id_alumno].verificada !== estado.verificada
          ) {
            siguientes[item.id_alumno] = estado;
            cambio = true;
          }
        });

        return cambio ? siguientes : previos;
      });

      const estadoClase = asistencia.find((item) => item.estado)?.estado;
      if (estadoClase) {
        setSeleccionada((actual) => {
          if (!actual || actual.id_clase !== idClase || actual.estado === estadoClase) {
            return actual;
          }
          return { ...actual, estado: estadoClase };
        });
      }
    } catch (error) {
      if (claseSeleccionadaRef.current?.id_clase === idClase) {
        setMensaje(error.message);
      }
    } finally {
      peticionAsistenciaRef.current = false;
    }
  }, []);

  useEffect(() => {
    const idClase = seleccionada?.id_clase;
    if (!idClase || !datosClaseCargados) return undefined;

    let intervalo;
    const refrescar = () => {
      if (AppState.currentState === 'active') void refrescarAsistencia(idClase);
    };
    const iniciar = () => {
      if (intervalo) clearInterval(intervalo);
      const espera = eventosConectados ? INTERVALO_RECONCILIACION : INTERVALO_ASISTENCIA;
      intervalo = setInterval(refrescar, espera);
    };
    const subscription = AppState.addEventListener('change', (estadoApp) => {
      if (estadoApp === 'active') {
        refrescar();
        iniciar();
      } else if (intervalo) {
        clearInterval(intervalo);
        intervalo = null;
      }
    });

    if (AppState.currentState === 'active') iniciar();

    return () => {
      if (intervalo) clearInterval(intervalo);
      subscription.remove();
    };
  }, [datosClaseCargados, eventosConectados, seleccionada?.id_clase, refrescarAsistencia]);

  useEffect(() => {
    if (seleccionada) return undefined;

    let intervalo;
    const sincronizar = () => {
      if (AppState.currentState === 'active') void sincronizarClases();
    };
    const iniciar = () => {
      if (intervalo) clearInterval(intervalo);
      const espera = eventosConectados ? INTERVALO_RECONCILIACION : INTERVALO_ASISTENCIA;
      intervalo = setInterval(sincronizar, espera);
    };
    const subscription = AppState.addEventListener('change', (estadoApp) => {
      if (estadoApp === 'active') {
        sincronizar();
        iniciar();
      } else if (intervalo) {
        clearInterval(intervalo);
        intervalo = null;
      }
    });

    if (AppState.currentState === 'active') iniciar();

    return () => {
      if (intervalo) clearInterval(intervalo);
      subscription.remove();
    };
  }, [eventosConectados, seleccionada, sincronizarClases]);

  useEffect(() => {
    if (!evento) return;

    if (evento.tipo === 'conectado') {
      void sincronizarClases();
      if (seleccionada?.id_clase && datosClaseCargados) {
        void refrescarAsistencia(seleccionada.id_clase);
      }
      return;
    }

    if (
      ['asistencia.actualizada', 'asistencia.registrada'].includes(evento.tipo) &&
      evento.id_clase === seleccionada?.id_clase
    ) {
      void refrescarAsistencia(seleccionada.id_clase);
    }

    if (evento.tipo === 'clase.abierta') {
      void sincronizarClases();
      if (evento.id_clase === seleccionada?.id_clase) {
        void refrescarAsistencia(seleccionada.id_clase);
      }
    }

    if (evento.tipo === 'clase.cerrada') {
      void sincronizarClases();
      if (evento.id_clase === seleccionada?.id_clase) {
        setSeleccionada((actual) => (
          actual?.id_clase === evento.id_clase ? { ...actual, estado: 'cerrada' } : actual
        ));
      }
    }
  }, [datosClaseCargados, evento, refrescarAsistencia, seleccionada?.id_clase, sincronizarClases]);

  const cambiar = (idAlumno, campo, valor) => {
    modificadosRef.current.add(idAlumno);
    setEstados((previos) => ({
      ...previos,
      [idAlumno]: { ...previos[idAlumno], [campo]: valor ? 1 : 0 }
    }));
  };

  const volver = () => {
    cargaClaseRef.current += 1;
    claseSeleccionadaRef.current = null;
    setSeleccionada(null);
    setAlumnos([]);
    setEstados({});
    setDatosClaseCargados(false);
    modificadosRef.current.clear();
    setCargando(false);
    setMensaje(null);
    void sincronizarClases();
  };

  const cambiosPendientes = () => alumnos
    .filter((alumno) => modificadosRef.current.has(alumno.id_alumno))
    .map((alumno) => ({
      id_alumno: alumno.id_alumno,
      presente: estados[alumno.id_alumno]?.presente ?? 0,
      verificada: estados[alumno.id_alumno]?.verificada ?? 0
    }));

  const guardarCambiosPendientes = async (idClase) => {
    const registros = cambiosPendientes();
    if (registros.length === 0) return { registros, resultado: null };

    const resultado = await guardarAsistencia(idClase, registros);
    if (resultado.errores.length === 0) {
      modificadosRef.current.clear();
      void refrescarAsistencia(idClase);
    }
    return { registros, resultado };
  };

  const guardar = async () => {
    if (!seleccionada || cargando) return;
    if (seleccionada.estado !== 'en_curso') {
      setMensaje('La clase está cerrada y su asistencia ya no puede modificarse.');
      return;
    }

    setCargando(true);
    setMensaje(null);
    try {
      const { registros, resultado } = await guardarCambiosPendientes(seleccionada.id_clase);
      if (registros.length === 0) {
        setMensaje('No hay cambios para guardar.');
        return;
      }
      setMensaje(
        `Guardado: ${resultado.procesados.length} procesados, ${resultado.errores.length} errores.`
      );
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  };

  const cerrar = async () => {
    if (!seleccionada || cargando) return;
    if (seleccionada.estado !== 'en_curso') {
      setMensaje('La clase ya está cerrada.');
      return;
    }

    const idClase = seleccionada.id_clase;
    setCargando(true);
    setMensaje(null);
    try {
      const { registros, resultado } = await guardarCambiosPendientes(idClase);
      if (resultado && resultado.errores.length > 0) {
        setMensaje(
          `No se pudo cerrar la clase porque el guardado tuvo ${resultado.errores.length} errores.`
        );
        return;
      }

      await cambiarEstadoClase(idClase, 'cerrada');
      setSeleccionada((actual) => (
        actual?.id_clase === idClase ? { ...actual, estado: 'cerrada' } : actual
      ));
      setClases((previas) => previas.map((clase) => (
        clase.id_clase === idClase ? { ...clase, estado: 'cerrada' } : clase
      )));
      setMensaje(
        registros.length > 0 ? 'Asistencia guardada y clase cerrada.' : 'Clase cerrada.'
      );
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  };

  const presentes = alumnos.filter(
    (alumno) => estados[alumno.id_alumno]?.presente === 1
  ).length;
  const verificados = alumnos.filter(
    (alumno) => estados[alumno.id_alumno]?.verificada === 1
  ).length;

  return (
    <ScrollView style={styles.pantalla}>
      <View style={styles.contenedor}>
        <Text style={styles.titulo}>Mis Clases</Text>

        {!seleccionada ? (
          <>
            {cargando ? (
              <ActivityIndicator size="large" color="#1f6feb" style={styles.cargando} />
            ) : null}

            {clases.map((clase) => {
              const estado = ESTADOS[clase.estado] || ESTADOS.pendiente;
              const { Icono } = estado;
              return (
                <TouchableOpacity
                  key={clase.id_dictado}
                  style={[styles.tarjeta, { borderColor: estado.borde }]}
                  onPress={() => elegirClase(clase)}
                  activeOpacity={0.85}
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
                    <Text style={styles.resumenAsistencia}>
                      {clase.total_verificados}/{clase.total_alumnos} verificados
                    </Text>
                  </View>
                  <View style={styles.panelEstado}>
                    <View style={[styles.etiqueta, { backgroundColor: estado.fondo }]}>
                      <Icono tamano={14} color={estado.texto} />
                      <Text style={[styles.textoEtiqueta, { color: estado.texto }]}>{estado.etiqueta}</Text>
                    </View>
                  </View>
                </TouchableOpacity>
              );
            })}

            {clases.length === 0 && !cargando ? (
              <Text style={styles.mensaje}>No hay clases para hoy.</Text>
            ) : null}
          </>
        ) : (
          <>
            <View style={styles.fila}>
              <TouchableOpacity style={styles.botonVolver} onPress={volver}>
                <Text style={styles.textoBoton}>Volver</Text>
              </TouchableOpacity>
              <View style={styles.filaTexto}>
                <Text style={styles.textoPrincipal}>
                  {seleccionada.nombre_materia} - {seleccionada.grupo}
                </Text>
                <Text style={styles.textoSecundario}>
                  {seleccionada.estado} · {presentes} presentes, {verificados} verificados
                </Text>
              </View>
            </View>

            {cargando ? (
              <ActivityIndicator size="large" color="#1f6feb" style={styles.cargando} />
            ) : null}

            {alumnos.map((alumno) => (
              <View key={alumno.id_alumno} style={styles.fila}>
                <View style={styles.filaTexto}>
                  <Text style={styles.textoPrincipal}>
                    {alumno.apellido_alumno}, {alumno.nombre_alumno}
                  </Text>
                  <Text style={styles.textoSecundario}>{alumno.legajo_alumno}</Text>
                </View>
                <View style={styles.filaEstado}>
                  <Text style={styles.textoSecundario}>
                    {estados[alumno.id_alumno]?.presente === 1 ? 'Presente' : 'Ausente'}
                  </Text>
                  <Switch
                    value={estados[alumno.id_alumno]?.presente === 1}
                    onValueChange={(valor) => cambiar(alumno.id_alumno, 'presente', valor)}
                    disabled={cargando || !editable}
                  />
                  {seleccionada.estado === 'en_curso' ? (
                    <>
                      <Text style={styles.textoSecundario}>
                        {estados[alumno.id_alumno]?.verificada === 1
                          ? 'Verificado en persona'
                          : 'No verificado'}
                      </Text>
                      <Switch
                        value={estados[alumno.id_alumno]?.verificada === 1}
                        onValueChange={(valor) => cambiar(alumno.id_alumno, 'verificada', valor)}
                        disabled={cargando || !editable}
                      />
                    </>
                  ) : null}
                </View>
              </View>
            ))}

            {editable ? (
              <TouchableOpacity
                style={[styles.boton, styles.botonGuardar, cargando && styles.botonDeshabilitado]}
                onPress={cargando ? undefined : guardar}
                accessibilityState={{ disabled: cargando }}
              >
                <Text style={styles.textoBoton}>Guardar asistencia</Text>
              </TouchableOpacity>
            ) : null}

            {editable ? (
              <TouchableOpacity
                style={[styles.boton, styles.botonCerrar, cargando && styles.botonDeshabilitado]}
                onPress={cargando ? undefined : cerrar}
                accessibilityState={{ disabled: cargando }}
              >
                <Text style={styles.textoBoton}>Cerrar clase</Text>
              </TouchableOpacity>
            ) : null}
          </>
        )}

        {mensaje ? <Text style={styles.mensaje}>{mensaje}</Text> : null}
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({

  pantalla: {
    flex: 1,
    backgroundColor: colores.crema
  },

  contenedor: {
    padding: 20,
    maxWidth: 640,
    width: '100%',
    alignSelf: 'center'
  },

  titulo: {
    fontFamily: 'Georgia',
    fontSize: 34,
    fontWeight: 'bold',
    color: colores.navy,
    textAlign: 'center',
    marginVertical: 14
  },

  tarjeta: {
    flexDirection: 'row',
    borderWidth: 2,
    borderRadius: 10,
    overflow: 'hidden',
    marginBottom: 22,
    backgroundColor: '#FFFDF7',
    shadowColor: '#000000',
    shadowOffset: { width: 0, height: 3 },
    shadowOpacity: 0.08,
    shadowRadius: 6,
    elevation: 2
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
    color: colores.navy
  },

  docente: {
    fontSize: 14,
    color: '#66758B',
    marginTop: 4
  },

  resumenAsistencia: {
    fontSize: 12,
    color: '#66758B',
    marginTop: 8
  },

  panelEstado: {
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 8,
    marginLeft: 12
  },

  etiqueta: {
    flexDirection: 'row',
    alignItems: 'center',
    borderRadius: 8,
    paddingHorizontal: 12,
    paddingVertical: 6
  },

  textoEtiqueta: {
    fontSize: 13,
    fontWeight: '700',
    marginLeft: 6
  },
  
  fila: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderColor: '#eaeef2',
    paddingVertical: 10
  },

  filaTexto: {
    flex: 1,
    marginRight: 8
  },

  filaEstado: {
    alignItems: 'flex-end'
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

  botonGuardar: {
    backgroundColor: '#2da44e'
  },

  botonCerrar: {
    backgroundColor: '#cf222e'
  },

  botonDeshabilitado: {
    opacity: 0.6
  },

  botonVolver: {
    backgroundColor: '#6e7781',
    borderRadius: 8,
    paddingHorizontal: 12,
    paddingVertical: 8,
    marginRight: 8
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
