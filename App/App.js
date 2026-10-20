import { useEffect, useState } from 'react';
import {
  KeyboardAvoidingView,
  Platform,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
  useWindowDimensions
} from 'react-native';
import { StatusBar } from 'expo-status-bar';
import { conectarEventos, iniciarSesion, limpiarToken } from './src/api';
import { colores } from './src/tema';
import Ondas from './src/componentes/Ondas';
import { IconoCandado, IconoFlecha, IconoOjo, IconoUsuario } from './src/componentes/Iconos';
import ProfesorPrincipal from './src/pantallas/ProfesorPrincipal';
import PreceptorPrincipal from './src/pantallas/PreceptorPrincipal';
import AlumnoPrincipal from './src/pantallas/AlumnoPrincipal';

export default function App() {
  const [sesion, setSesion] = useState(null);
  const [loginDni, setLoginDni] = useState('');
  const [loginPassword, setLoginPassword] = useState('');
  const [mostrarPassword, setMostrarPassword] = useState(false);
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState(null);
  const [evento, setEvento] = useState(null);
  const [eventosConectados, setEventosConectados] = useState(false);

  useEffect(() => {
    if (!sesion) {
      setEvento(null);
      setEventosConectados(false);
      return undefined;
    }

    setEvento(null);
    setEventosConectados(false);
    return conectarEventos(setEvento, setEventosConectados);
  }, [sesion?.dni, sesion?.rol]);

  const ingresar = async (dni = loginDni, contrasena = loginPassword) => {
    setCargando(true);
    setMensaje(null);
    try {
      const resultado = await iniciarSesion(dni, contrasena);
      setSesion(resultado.usuario);
    } catch (error) {
      setMensaje(error.message);
    } finally {
      setCargando(false);
    }
  };

  const cerrarSesion = () => {
    limpiarToken();
    setSesion(null);
    setLoginDni('');
    setLoginPassword('');
    setMostrarPassword(false);
    setMensaje(null);
  };

  return (
    <View style={styles.pantalla}>
      <StatusBar style="dark" />
      {!sesion ? (
        <LoginScreen
          dni={loginDni}
          onDni={setLoginDni}
          password={loginPassword}
          onPassword={setLoginPassword}
          mostrarPassword={mostrarPassword}
          onTogglePassword={() => setMostrarPassword((v) => !v)}
          cargando={cargando}
          mensaje={mensaje}
          onIngresar={ingresar}
        />
      ) : (
        <View style={styles.pantalla}>
          <View style={styles.tope}>
            <View style={styles.filaTexto}>
              <Text style={styles.textoPrincipal}>
                {sesion.rol} - {sesion.apellido}, {sesion.nombre}
              </Text>
              <Text style={styles.textoSecundario}>DNI {sesion.dni}</Text>
              <Text
                style={[
                  styles.estadoConexion,
                  !eventosConectados && styles.estadoConexionRespaldo
                ]}
              >
                {eventosConectados ? 'Sincronización en tiempo real' : 'Usando polling de respaldo'}
              </Text>
            </View>
            <TouchableOpacity style={styles.botonCerrar} onPress={cerrarSesion}>
              <Text style={styles.textoBoton}>Cerrar sesión</Text>
            </TouchableOpacity>
          </View>

          {sesion.rol === 'profesor' ? (
            <ProfesorPrincipal
              sesion={sesion}
              evento={evento}
              eventosConectados={eventosConectados}
            />
          ) : null}
          {sesion.rol === 'preceptor' ? (
            <PreceptorPrincipal evento={evento} eventosConectados={eventosConectados} />
          ) : null}
          {sesion.rol === 'alumno' ? (
            <AlumnoPrincipal
              sesion={sesion}
              evento={evento}
              eventosConectados={eventosConectados}
            />
          ) : null}

        </View>
      )}
    </View>
  );
}

function LoginScreen({
  dni,
  onDni,
  password,
  onPassword,
  mostrarPassword,
  onTogglePassword,
  cargando,
  mensaje,
  onIngresar
}) {
  const { width, height } = useWindowDimensions();
  const margenLateral = Math.max(16, width * 0.11);
  const anchoFormulario = Math.min(width - margenLateral * 2, 520);
  const altoOndaInferior = Math.min(Math.max(height * 0.18, 170), 276);
  const anchoOndas = width;

  const enviar = () => {
    if (!cargando) void onIngresar();
  };

  return (
    <View style={styles.pantalla}>
      <View style={styles.franjaInstitucional}>
        <Text style={styles.franjaTitulo}>E.E.S.T. N1 - Ara. General Belgrano</Text>
        <Text style={styles.franjaSubtitulo}>Tecnicatura en Programación</Text>
        <Text style={styles.franjaIntegrantes}>Isabella Carrete Vecco - Juan Torres Troschasky</Text>
      </View>

      <KeyboardAvoidingView
        style={styles.pantalla}
        behavior={Platform.OS === 'ios' ? 'padding' : undefined}
      >
        <ScrollView
          contentContainerStyle={styles.scrollLogin}
          keyboardShouldPersistTaps="handled"
        >
          <View style={[styles.formulario, { width: anchoFormulario }]}>
            <Text style={styles.tituloInstitucion}>E.E.S.T. N1</Text>
            <View style={styles.lineaAmarilla} />
            <Text style={styles.subtitulo}>SISTEMA INTEGRAL</Text>
            <Text style={styles.subtitulo}>DE ASISTENCIA</Text>

            <View style={styles.campo}>
              <View style={styles.campoIcono}>
                <IconoUsuario tamano={22} />
              </View>
              <View style={styles.divisor} />
              <TextInput
                style={styles.entrada}
                value={dni}
                onChangeText={onDni}
                placeholder="DNI"
                placeholderTextColor={colores.textoSuave}
                keyboardType="number-pad"
                nativeID="login-dni"
              />
            </View>

            <View style={[styles.campo, styles.campoContrasena]}>
              <View style={styles.campoIcono}>
                <IconoCandado tamano={22} />
              </View>
              <View style={styles.divisor} />
              <TextInput
                style={styles.entrada}
                value={password}
                onChangeText={onPassword}
                placeholder="Contraseña"
                placeholderTextColor={colores.textoSuave}
                secureTextEntry={!mostrarPassword}
                nativeID="login-contrasena"
              />
              <TouchableOpacity
                style={styles.botonOjo}
                onPress={onTogglePassword}
                accessibilityLabel="Mostrar u ocultar contraseña"
              >
                <IconoOjo tamano={22} oculto={!mostrarPassword} />
              </TouchableOpacity>
            </View>

            <TouchableOpacity
              style={[styles.botonIngresar, cargando && styles.botonDeshabilitado]}
              onPress={enviar}
              accessibilityState={{ disabled: cargando }}
            >
              <Text style={styles.textoIngresar}>Ingresar</Text>
              <View style={styles.flecha}>
                <IconoFlecha tamano={24} />
              </View>
            </TouchableOpacity>

            {mensaje ? <Text style={styles.mensaje}>{mensaje}</Text> : null}
          </View>

          <View style={[styles.ondas, styles.ondasInferior]}>
            <Ondas ancho={anchoOndas} alto={altoOndaInferior} posicion="inferior" />
          </View>
        </ScrollView>
      </KeyboardAvoidingView>
    </View>
  );
}

const styles = StyleSheet.create({
  pantalla: {
    flex: 1,
    backgroundColor: colores.crema
  },
  franjaInstitucional: {
    backgroundColor: colores.navy,
    paddingTop: 52,
    paddingBottom: 24,
    paddingHorizontal: 16,
    alignItems: 'center'
  },
  franjaTitulo: {
    color: colores.blanco,
    fontSize: 24,
    fontWeight: '600',
    letterSpacing: 4,
    textAlign: 'center',
    lineHeight: 34
  },
  franjaSubtitulo: {
    color: colores.blanco,
    fontSize: 24,
    fontWeight: '600',
    letterSpacing: 4,
    textAlign: 'center',
    lineHeight: 34,
    marginTop: 4
  },
  franjaIntegrantes: {
    color: colores.blanco,
    fontSize: 24,
    fontWeight: '600',
    letterSpacing: 4,
    textAlign: 'center',
    lineHeight: 34,
    marginTop: 12
  },
  scrollLogin: {
    flexGrow: 1,
    justifyContent: 'center',
    alignItems: 'center',
    paddingVertical: 24
  },
  ondas: {
    position: 'absolute',
    left: 0,
    right: 0
  },
  ondasSuperior: {
    top: 0
  },
  ondasInferior: {
    bottom: 0
  },
  formulario: {
    alignItems: 'center',
    zIndex: 2,
    marginTop: 8,
    marginBottom: 40
  },
  tituloInstitucion: {
    fontFamily: Platform.select({ ios: 'Georgia', android: 'serif', default: 'serif' }),
    fontWeight: 'bold',
    fontSize: 70,
    color: colores.navy,
    textAlign: 'center',
    alignSelf: 'stretch'
  },
  lineaAmarilla: {
    width: 120,
    height: 4,
    borderRadius: 3,
    backgroundColor: colores.amarillo,
    marginTop: 14,
    marginBottom: 18
  },
  subtitulo: {
    fontSize: 24,
    fontWeight: '600',
    letterSpacing: 4,
    color: colores.textoOscuro,
    textAlign: 'center',
    lineHeight: 34
  },
  campo: {
    width: '100%',
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1.5,
    borderColor: colores.borde,
    borderRadius: 16,
    backgroundColor: colores.crema,
    height: 62,
    marginTop: 24,
    paddingLeft: 14
  },
  campoContrasena: {
    marginTop: 14
  },
  campoIcono: {
    width: 30,
    alignItems: 'center',
    justifyContent: 'center'
  },
  divisor: {
    width: 1.5,
    height: 30,
    backgroundColor: colores.borde,
    marginHorizontal: 10
  },
  entrada: {
    flex: 1,
    fontSize: 16,
    color: colores.textoOscuro,
    paddingVertical: 0
  },
  botonOjo: {
    paddingHorizontal: 14
  },
  botonIngresar: {
    width: '100%',
    height: 62,
    borderRadius: 31,
    backgroundColor: colores.navy,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    marginTop: 28,
    shadowColor: '#000000',
    shadowOffset: { width: 0, height: 8 },
    shadowOpacity: 0.18,
    shadowRadius: 16,
    elevation: 6
  },
  botonDeshabilitado: {
    opacity: 0.6
  },
  textoIngresar: {
    color: colores.blanco,
    fontSize: 19,
    fontWeight: '600'
  },
  flecha: {
    position: 'absolute',
    right: 22
  },
  mensaje: {
    marginTop: 16,
    color: '#cf222e',
    fontSize: 14,
    textAlign: 'center'
  },
  tope: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 20,
    paddingVertical: 12,
    borderBottomWidth: 1,
    borderColor: '#eaeef2',
    backgroundColor: '#ffffff'
  },
  filaTexto: {
    flex: 1,
    marginRight: 8
  },
  textoPrincipal: {
    fontSize: 16,
    color: '#24292f'
  },
  textoSecundario: {
    fontSize: 13,
    color: '#57606a'
  },
  estadoConexion: {
    fontSize: 12,
    color: '#2da44e',
    marginTop: 2
  },
  estadoConexionRespaldo: {
    color: '#bf8700'
  },
  botonCerrar: {
    backgroundColor: '#6e7781',
    borderRadius: 8,
    paddingHorizontal: 12,
    paddingVertical: 8,
    alignItems: 'center'
  },
  textoBoton: {
    color: '#ffffff',
    fontSize: 16,
    fontWeight: '600'
  }
});
