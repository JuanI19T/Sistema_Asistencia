import { useEffect, useState } from 'react';
import {
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View
} from 'react-native';
import { StatusBar } from 'expo-status-bar';
import { conectarEventos, iniciarSesion, limpiarToken } from './src/api';
import ProfesorPrincipal from './src/pantallas/ProfesorPrincipal';
import PreceptorPrincipal from './src/pantallas/PreceptorPrincipal';
import AlumnoPrincipal from './src/pantallas/AlumnoPrincipal';

const CUENTAS_PRUEBA = [
  { rol: 'Profesor', nombre: 'Carlos Gutiérrez', dni: '30111222' },
  { rol: 'Profesor', nombre: 'María Fernández', dni: '31222333' },
  { rol: 'Preceptor', nombre: 'Laura Martínez', dni: '34555666' },
  { rol: 'Alumno', nombre: 'Juan Pérez', dni: '45222001' },
  { rol: 'Alumno', nombre: 'Ana Gómez', dni: '45222002' },
  { rol: 'Alumno', nombre: 'Luis Díaz', dni: '45222003' }
];

export default function App() {
  const [sesion, setSesion] = useState(null);
  const [loginDni, setLoginDni] = useState('');
  const [loginPassword, setLoginPassword] = useState('');
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

  const ingresarRapido = (cuenta) => {
    setLoginDni(cuenta.dni);
    setLoginPassword(cuenta.dni);
    void ingresar(cuenta.dni, cuenta.dni);
  };

  const cerrarSesion = () => {
    limpiarToken();
    setSesion(null);
    setLoginDni('');
    setLoginPassword('');
    setMensaje(null);
  };

  return (
    <View style={styles.pantalla}>
      <StatusBar style="dark" />
      {!sesion ? (
        <ScrollView style={styles.pantalla}>
          <View style={styles.contenedor}>
            <Text style={styles.titulo}>Iniciar sesión</Text>

            <Text style={styles.etiqueta}>DNI</Text>
            <TextInput
              style={styles.campo}
              value={loginDni}
              onChangeText={setLoginDni}
              placeholder="ej. 34555666"
              keyboardType="number-pad"
              nativeID="login-dni"
            />

            <Text style={styles.etiqueta}>Contraseña</Text>
            <TextInput
              style={styles.campo}
              value={loginPassword}
              onChangeText={setLoginPassword}
              placeholder="Contraseña"
              secureTextEntry
              nativeID="login-contrasena"
            />

            <TouchableOpacity style={styles.boton} onPress={() => ingresar()}>
              <Text style={styles.textoBoton}>Ingresar</Text>
            </TouchableOpacity>

            {__DEV__ ? (
              <View style={styles.pruebas}>
                <Text style={styles.tituloPruebas}>Cuentas de prueba</Text>
                <Text style={styles.textoPruebas}>Tocá una cuenta para ingresar. La contraseña es el DNI.</Text>
                {CUENTAS_PRUEBA.map((cuenta) => (
                  <TouchableOpacity
                    key={`${cuenta.rol}-${cuenta.dni}`}
                    style={[styles.cuentaPrueba, cargando && styles.cuentaPruebaDeshabilitada]}
                    onPress={cargando ? undefined : () => ingresarRapido(cuenta)}
                    accessibilityState={{ disabled: cargando }}
                  >
                    <Text style={styles.cuentaTexto}>{cuenta.rol}</Text>
                    <Text style={styles.textoSecundario}>{cuenta.nombre} · DNI {cuenta.dni}</Text>
                  </TouchableOpacity>
                ))}
              </View>
            ) : null}

            {mensaje ? <Text style={styles.mensaje}>{mensaje}</Text> : null}
          </View>
        </ScrollView>
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

const styles = StyleSheet.create({
  pantalla: {
    flex: 1,
    backgroundColor: '#f6f8fa'
  },
  contenedor: {
    padding: 20,
    maxWidth: 640,
    width: '100%',
    alignSelf: 'center',
    marginTop: 40
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
  boton: {
    backgroundColor: '#1f6feb',
    borderRadius: 8,
    padding: 14,
    alignItems: 'center',
    marginTop: 16
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
  pruebas: {
    marginTop: 24,
    padding: 12,
    borderWidth: 1,
    borderColor: '#d0d7de',
    borderRadius: 8,
    backgroundColor: '#ffffff'
  },
  tituloPruebas: {
    fontSize: 16,
    fontWeight: '600',
    color: '#24292f'
  },
  textoPruebas: {
    fontSize: 13,
    color: '#57606a',
    marginTop: 4,
    marginBottom: 8
  },
  cuentaPrueba: {
    borderTopWidth: 1,
    borderColor: '#eaeef2',
    paddingVertical: 10
  },
  cuentaPruebaDeshabilitada: {
    opacity: 0.6
  },
  cuentaTexto: {
    fontSize: 14,
    fontWeight: '600',
    color: '#24292f'
  },
  mensaje: {
    marginTop: 16,
    color: '#cf222e',
    fontSize: 14
  }
});