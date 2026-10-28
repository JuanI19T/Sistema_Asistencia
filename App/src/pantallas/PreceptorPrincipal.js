import { useCallback, useEffect, useRef, useState } from 'react';
import {
  ActivityIndicator,
  AppState,
  Platform,
  ScrollView,
  StyleSheet,
  Switch,
  Text,
  TextInput,
  TouchableOpacity,
  useWindowDimensions,
  View
} from 'react-native';
import {
  cambiarEstadoClase,
  fechaDeHoy,
  guardarAsistencia,
  abrirClasePreceptor,
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
    etiqueta: 'Verificada', 
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
    etiqueta: 'No Verificada', 
    borde: '#525252', 
    fondoHorario: '#E8E8E8', 
    fondo: '#E2E2E2', 
    texto: '#333333', 
    Icono: IconoAviso }

};

function recortarHora(horario) {
  return horario && typeof horario === 'string' ? horario.slice(0, 5) : '';
}

function formatearFechaHora(valor) {
  if (!valor) return 'Sin registro';
  if (valor instanceof Date) {
    return formatearMinutos(valor.getUTCHours() * 60 + valor.getUTCMinutes() - 180);
  }
  const texto = String(valor);
  if (/^\d{2}:\d{2}$/.test(texto)) return texto;
  const partes = texto.match(/^\d{4}-\d{2}-\d{2}[T ](\d{2}):(\d{2})/);
  if (partes) {
    return formatearMinutos(Number(partes[1]) * 60 + Number(partes[2]) - 180);
  }
  return texto;
}

function formatearMinutos(total) {
  const normalizado = (total + 1440) % 1440;
  const horas = String(Math.floor(normalizado / 60)).padStart(2, '0');
  const minutos = String(normalizado % 60).padStart(2, '0');
  return `${horas}:${minutos}`;
}

function nombreProfesor(clase) {
  const nombre = clase?.nombre_profesor || '';
  const apellido = clase?.apellido_profesor || '';
  if (apellido && nombre) return `${apellido}, ${nombre}`;
  return apellido || nombre || 'Sin docente';
}

function etiquetaMetodo(metodo) {
  if (metodo === 'qr') return 'QR';
  if (metodo === 'codigo') return 'Código';
  if (metodo === 'manual') return 'Manual';
  return 'Sin registro';
}

function datosDictado(clase) {
  const ordinales = {
    1: '1ro',
    2: '2do',
    3: '3ro',
    4: '4to',
    5: '5to',
    6: '6to',
    7: '7mo'
  };
  const anio = ordinales[Number(clase?.anio_materia)] || 'Año sin dato';
  const division = clase?.division ? `División ${clase.division}` : 'División sin dato';
  const grupo = clase?.grupo ? `Grupo ${clase.grupo}` : 'Grupo completo';
  return `${anio} · ${division} · ${grupo}`;
}

function Interruptor({ value, onValueChange, disabled }) {
  if (Platform.OS !== 'web') {
    return (
      <Switch
        value={value}
        onValueChange={onValueChange}
        disabled={disabled}
        trackColor={{ false: '#B9B9B9', true: colores.amarillo }}
        thumbColor="#FFFFFF"
        ios_backgroundColor="#B9B9B9"
      />
    );
  }

  return (
    <TouchableOpacity
      style={[styles.interruptorWeb, value && styles.interruptorWebActivo, disabled && styles.interruptorWebDeshabilitado]}
      onPress={() => !disabled && onValueChange(!value)}
      activeOpacity={0.8}
      accessibilityRole="switch"
      accessibilityState={{ checked: value, disabled }}
    >
      <View style={styles.circuloInterruptorWeb} />
    </TouchableOpacity>
  );
}

function combinarEstados(alumnos, asistencia) {
  const estados = {};

  alumnos.forEach((alumno) => {
    estados[alumno.id_alumno] = {
      presente: 0,
      verificada: 0,
        metodo_registro: null,
      registrado_en: null,
      dni: alumno.dni
    };
  });

  asistencia.forEach((item) => {
    if (estados[item.id_alumno]) {
      estados[item.id_alumno] = {
        presente: item.presente,
        verificada: item.verificada,
        metodo_registro: item.metodo_registro,
        registrado_en: item.hora_registro || item.registrado_en,
        dni: item.dni || estados[item.id_alumno]?.dni
      };
    }
  });

  return estados;
}

export default function PreceptorPrincipal({ sesion, evento, eventosConectados }) {
  const { width } = useWindowDimensions();
  const esPantallaAngosta = width < 520;
  const [clases, setClases] = useState([]);
  const [seleccionada, setSeleccionada] = useState(null);
  const [alumnos, setAlumnos] = useState([]);
  const [estados, setEstados] = useState({});
  const [datosClaseCargados, setDatosClaseCargados] = useState(false);
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState(null);
  const [claseParaAbrir, setClaseParaAbrir] = useState(null);
  const [motivoApertura, setMotivoApertura] = useState('profesor_retrasado');
  const [detalleMotivo, setDetalleMotivo] = useState('');
  const [profesorLlegoEn, setProfesorLlegoEn] = useState('');
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
      if (clase.estado === 'en_curso') {
        setClaseParaAbrir(clase);
        setSeleccionada(clase);
        setMensaje(null);
      } else {
        setMensaje('La asistencia solo puede verificarse durante la clase.');
      }
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
      const estadoAsistencia = asistencia.find((item) => item.estado)?.estado;
      if (estadoAsistencia) {
        setSeleccionada((actual) => (
          actual?.id_clase === clase.id_clase
            ? { ...actual, estado: estadoAsistencia }
            : actual
        ));
      }
      setDatosClaseCargados(true);
    } catch (error) {
      if (idCarga === cargaClaseRef.current) setMensaje(error.message);
    } finally {
      if (idCarga === cargaClaseRef.current) setCargando(false);
    }
  };

  const abrirManual = async () => {
    if (!claseParaAbrir || cargando) return;
    setCargando(true);
    setMensaje(null);
    try {
      const fecha = fechaDeHoy();
      const clase = await abrirClasePreceptor(claseParaAbrir.id_dictado, fecha, {
        motivo_apertura: motivoApertura,
        detalle_motivo: detalleMotivo,
        profesor_llego_en: profesorLlegoEn || null
      });
      setClaseParaAbrir(null);
      await cargarClases();
      await elegirClase({ ...claseParaAbrir, ...clase, estado: 'en_curso' });
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
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
            verificada: item.verificada,
            metodo_registro: item.metodo_registro,
            registrado_en: item.hora_registro || item.registrado_en,
            dni: item.dni || siguientes[item.id_alumno].dni
          };
          if (
            siguientes[item.id_alumno].presente !== estado.presente ||
            siguientes[item.id_alumno].verificada !== estado.verificada ||
            siguientes[item.id_alumno].metodo_registro !== estado.metodo_registro ||
            siguientes[item.id_alumno].registrado_en !== estado.registrado_en ||
            siguientes[item.id_alumno].dni !== estado.dni
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
  const estadoSeleccionada = seleccionada
    ? (ESTADOS[seleccionada.estado] || ESTADOS.pendiente)
    : ESTADOS.pendiente;

  return (
    <ScrollView style={styles.pantalla} contentContainerStyle={styles.contenido}>
      <View style={styles.contenedor}>
        <Text style={styles.titulo}>{seleccionada ? 'Asistencia de Mis Alumnos' : 'Mis Clases'}</Text>

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
                  style={[styles.tarjeta, esPantallaAngosta && styles.tarjetaAngosta, { borderColor: estado.borde }]}
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
                    <Text style={styles.datosDictado}>{datosDictado(clase)}</Text>
                    <Text style={styles.resumenAsistencia}>
                      {clase.total_verificados}/{clase.total_alumnos} verificados
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

            {clases.length === 0 && !cargando ? (
              <Text style={styles.mensaje}>No hay clases para hoy.</Text>
            ) : null}
          </>
        ) : (
          <>
            <TouchableOpacity style={styles.botonVolver} onPress={volver}>
              <Text style={styles.textoBotonVolver}>← Volver</Text>
            </TouchableOpacity>

            <View style={styles.resumenClase}>
              <Text style={styles.resumenMateria}>{seleccionada.nombre_materia}</Text>
              <Text style={styles.resumenDato}>
                Profesor: {nombreProfesor(seleccionada)}
              </Text>
              <Text style={styles.resumenDato}>
                Horario: {recortarHora(seleccionada.horario)} - {recortarHora(seleccionada.horario_fin)}
              </Text>
              <Text style={styles.resumenDato}>{datosDictado(seleccionada)}</Text>
              {seleccionada.id_clase ? <View style={styles.metricasClase}>
                <View style={styles.metrica}>
                  <Text style={styles.valorMetrica}>{formatearFechaHora(seleccionada.abierta_en)}</Text>
                  <Text style={styles.etiquetaMetrica}>Apertura</Text>
                </View>
                <View style={styles.metrica}>
                  <Text style={styles.valorMetrica}>{presentes}/{alumnos.length}</Text>
                  <Text style={styles.etiquetaMetrica}>Presentes</Text>
                </View>
                <View style={styles.metrica}>
                  <Text style={styles.valorMetrica}>{verificados}/{alumnos.length}</Text>
                  <Text style={styles.etiquetaMetrica}>Verificados</Text>
                </View>
              </View> : null}
            </View>

            {!seleccionada.id_clase ? (
              <View style={styles.formularioApertura}>
                <Text style={styles.subtituloFormulario}>Abrir clase manualmente</Text>
                <Text style={styles.resumenDato}>
                  La clase todavía no fue abierta. La apertura del preceptor no generará QR ni código.
                </Text>
                <Text style={styles.instruccionFormulario}>
                  La asistencia se tomará únicamente de forma manual.
                </Text>
                {[
                  ['profesor_retrasado', 'El profesor está retrasado y necesitamos tomar asistencia.'],
                  ['profesor_sin_acceso', 'El profesor no tiene forma de abrir la clase desde su cuenta.'],
                  ['profesor_sin_app', 'El profesor todavía no tiene la app para abrir la clase.'],
                  ['otro', 'Otro motivo']
                ].map(([valor, texto]) => (
                  <TouchableOpacity
                    key={valor}
                    style={[styles.opcionMotivo, motivoApertura === valor && styles.opcionMotivoActiva]}
                    onPress={() => setMotivoApertura(valor)}
                  >
                    <Text style={styles.textoOpcionMotivo}>{texto}</Text>
                  </TouchableOpacity>
                ))}
                {motivoApertura === 'otro' ? (
                  <TextInput
                    style={styles.inputMotivo}
                    value={detalleMotivo}
                    onChangeText={setDetalleMotivo}
                    placeholder="Escribí el motivo"
                    multiline
                  />
                ) : null}
                <TextInput
                  style={styles.inputMotivo}
                  value={profesorLlegoEn}
                  onChangeText={setProfesorLlegoEn}
                  placeholder="Hora de llegada del profesor (opcional, HH:mm)"
                />
                <TouchableOpacity style={styles.botonAbrirManual} onPress={abrirManual} disabled={cargando}>
                  <Text style={styles.textoBotonAccion}>Abrir y tomar asistencia manual</Text>
                </TouchableOpacity>
              </View>
            ) : null}

            {seleccionada.id_clase ? (
              <>
                <Text style={styles.subtituloAlumnos}>Asistencia de Alumnos</Text>

            {cargando ? (
              <ActivityIndicator size="large" color="#1f6feb" style={styles.cargando} />
            ) : null}

            {alumnos.map((alumno, index) => (
              <View key={alumno.id_alumno} style={[styles.filaAlumno, index % 2 === 1 && styles.filaAlumnoAlterna]}>
                <View style={styles.filaTexto}>
                  <Text style={styles.nombreAlumno}>
                    {alumno.apellido_alumno}, {alumno.nombre_alumno}
                  </Text>
                    <Text style={styles.datoAlumno}>
                      Legajo: {alumno.legajo_alumno} · DNI: {alumno.dni || estados[alumno.id_alumno]?.dni || 'Sin dato'}
                    </Text>
                  <Text style={styles.datoAlumno}>
                    Registro: {formatearFechaHora(estados[alumno.id_alumno]?.registrado_en)}
                    {' · Método: '}{etiquetaMetodo(estados[alumno.id_alumno]?.metodo_registro)}
                  </Text>
                </View>
                <View style={styles.filaEstado}>
                  <Text style={styles.estadoAlumno}>
                    {estados[alumno.id_alumno]?.presente === 1 ? 'Presente' : 'Ausente'}
                  </Text>
                  <Interruptor
                    value={estados[alumno.id_alumno]?.presente === 1}
                    onValueChange={(valor) => cambiar(alumno.id_alumno, 'presente', valor)}
                    disabled={cargando || !editable}
                  />
                  <>
                    <Text style={styles.estadoAlumno}>
                      {estados[alumno.id_alumno]?.verificada === 1
                        ? 'Verificado'
                        : 'No Verificado'}
                    </Text>
                    <Interruptor
                      value={estados[alumno.id_alumno]?.verificada === 1}
                      onValueChange={(valor) => cambiar(alumno.id_alumno, 'verificada', valor)}
                      disabled={cargando || !editable}
                    />
                  </>
                </View>
              </View>
            ))}

            {editable ? (
              <View style={[styles.filaBotonesAccion, esPantallaAngosta && styles.filaBotonesAccionVertical]}>
                <TouchableOpacity
                  style={[styles.botonAccion, styles.botonGuardar, cargando && styles.botonDeshabilitado]}
                  onPress={cargando ? undefined : guardar}
                  accessibilityState={{ disabled: cargando }}
                >
                  <Text style={styles.textoBotonAccion}>Guardar Asistencia</Text>
                </TouchableOpacity>
                <TouchableOpacity
                  style={[styles.botonAccion, styles.botonCerrar, cargando && styles.botonDeshabilitado]}
                  onPress={cargando ? undefined : cerrar}
                  accessibilityState={{ disabled: cargando }}
                >
                  <Text style={styles.textoBotonAccionCerrar}>Cerrar Clase</Text>
                </TouchableOpacity>
              </View>
            ) : null}
              </>
            ) : null}
          </>
        )}

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

  contenedor: {
    paddingHorizontal: 20,
    paddingTop: 20,
    paddingBottom: 28,
    maxWidth: 720,
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
    marginBottom: 32,
    backgroundColor: '#FFFDF7',
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

  panelDatosAngosto: {
    width: '100%',
    paddingVertical: 14
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

  panelEstadoAngosto: {
    width: '100%',
    paddingVertical: 12,
    marginLeft: 0
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

  datosDictado: {
    fontSize: 13,
    color: '#66758B',
    marginTop: 6
  },

  filaAlumno: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#FFFDF7',
    paddingHorizontal: 14,
    paddingVertical: 10,
    marginBottom: 1
  },

  filaAlumnoAlterna: {
    backgroundColor: '#F0F0EC'
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

  subtituloAlumnos: {
    fontFamily: 'Georgia',
    fontSize: 24,
    fontWeight: 'bold',
    color: colores.navy,
    marginTop: 24,
    marginBottom: 12
  },

  nombreAlumno: {
    fontSize: 16,
    fontWeight: '700',
    color: colores.navy
  },

  datoAlumno: {
    fontSize: 13,
    color: '#66758B',
    marginTop: 4
  },

  estadoAlumno: {
    fontSize: 13,
    fontWeight: '700',
    color: colores.navy,
    marginBottom: 3
  },

  interruptorWeb: {
    width: 38,
    height: 14,
    borderRadius: 7,
    backgroundColor: '#B9B9B9',
    padding: 0,
    justifyContent: 'center',
    alignItems: 'flex-start',
    marginBottom: 2
  },

  interruptorWebActivo: {
    backgroundColor: colores.amarillo,
    alignItems: 'flex-end'
  },

  circuloInterruptorWeb: {
    width: 20,
    height: 20,
    borderRadius: 10,
    backgroundColor: colores.blanco,
    borderWidth: 1,
    borderColor: '#D2CEC5',
    shadowColor: '#000000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.18,
    shadowRadius: 2,
    elevation: 2
  },

  interruptorWebDeshabilitado: {
    opacity: 0.65
  },

  boton: {
    backgroundColor: '#1f6feb',
    borderRadius: 8,
    padding: 14,
    alignItems: 'center',
    marginTop: 16
  },

  botonGuardar: {
    backgroundColor: colores.navy
  },

  botonCerrar: {
    backgroundColor: colores.amarillo
  },

  botonDeshabilitado: {
    opacity: 0.6
  },

  filaBotonesAccion: {
    flexDirection: 'row',
    gap: 12,
    marginTop: 20
  },

  filaBotonesAccionVertical: {
    flexDirection: 'column'
  },

  botonAccion: {
    flex: 1,
    minHeight: 54,
    borderRadius: 10,
    justifyContent: 'center',
    alignItems: 'center'
  },

  textoBotonAccion: {
    color: colores.blanco,
    fontSize: 16,
    fontWeight: '700'
  },

  textoBotonAccionCerrar: {
    color: colores.navy,
    fontSize: 16,
    fontWeight: '700'
  },

  botonVolver: {
    backgroundColor: colores.amarillo,
    borderRadius: 8,
    minHeight: 36,
    alignSelf: 'flex-start',
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 12,
    marginBottom: 14
  },

  textoBotonVolver: {
    color: colores.navy,
    fontSize: 15,
    fontWeight: '700'
  },

  resumenClase: {
    backgroundColor: '#FFFDF7',
    borderRadius: 10,
    padding: 18,
    marginBottom: 20
  },

  formularioApertura: {
    backgroundColor: '#FFFDF7',
    borderRadius: 10,
    padding: 18,
    marginBottom: 20
  },

  subtituloFormulario: {
    fontFamily: 'Georgia',
    fontSize: 21,
    fontWeight: 'bold',
    color: colores.navy,
    marginBottom: 8
  },

  instruccionFormulario: {
    color: '#66758B',
    fontSize: 13,
    marginVertical: 10
  },

  opcionMotivo: {
    padding: 10,
    marginTop: 5,
    borderRadius: 7,
    backgroundColor: '#F0F0EC'
  },

  opcionMotivoActiva: {
    backgroundColor: '#FFF3A6'
  },

  textoOpcionMotivo: {
    color: colores.navy,
    fontSize: 13
  },

  inputMotivo: {
    borderWidth: 1,
    borderColor: colores.borde,
    borderRadius: 7,
    minHeight: 42,
    padding: 10,
    marginTop: 10,
    color: colores.textoOscuro,
    backgroundColor: colores.blanco
  },

  botonAbrirManual: {
    backgroundColor: colores.navy,
    borderRadius: 9,
    minHeight: 48,
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 14,
    paddingHorizontal: 12
  },

  resumenMateria: {
    fontFamily: 'Georgia',
    fontSize: 24,
    fontWeight: 'bold',
    color: colores.navy,
    marginBottom: 8
  },

  resumenDato: {
    color: '#66758B',
    fontSize: 14,
    marginTop: 4
  },

  metricasClase: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginTop: 18,
    paddingTop: 14,
    borderTopWidth: 1,
    borderTopColor: '#D2CEC5'
  },

  metrica: {
    flex: 1,
    alignItems: 'center'
  },

  valorMetrica: {
    color: colores.navy,
    fontSize: 17,
    fontWeight: '700',
    textAlign: 'center'
  },

  etiquetaMetrica: {
    color: '#66758B',
    fontSize: 12,
    marginTop: 4,
    textAlign: 'center'
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

  panelHorarioAngosto: {
    width: '100%',
    flexDirection: 'row',
    paddingVertical: 12,
    gap: 12
  },

  textoPie: {
    color: colores.blanco,
    fontSize: 15,
    textAlign: 'center'
  }

});
