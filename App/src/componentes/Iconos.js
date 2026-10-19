import Svg, { Path, Circle, Line } from 'react-native-svg';
import { colores } from '../tema';

export function IconoUsuario({ tamano = 24, color = colores.textoOscuro }) {
  return (
    <Svg width={tamano} height={tamano} viewBox="0 0 24 24" fill="none">
      <Circle cx="12" cy="8" r="4" fill={color} />
      <Path d="M4 20c0-4 3.6-6.5 8-6.5s8 2.5 8 6.5Z" fill={color} />
    </Svg>
  );
}

export function IconoCandado({ tamano = 24, color = colores.textoOscuro }) {
  return (
    <Svg width={tamano} height={tamano} viewBox="0 0 24 24" fill="none">
      <Path
        d="M7 10V8a5 5 0 0 1 10 0v2"
        stroke={color}
        strokeWidth="2"
        strokeLinecap="round"
      />
      <Path d="M5 10h14a1 1 0 0 1 1 1v9a1 1 0 0 1-1 1H5a1 1 0 0 1-1-1v-9a1 1 0 0 1 1-1Z" fill={color} />
      <Circle cx="12" cy="15" r="1.6" fill={colores.crema} />
    </Svg>
  );
}

export function IconoOjo({ tamano = 24, color = colores.textoOscuro, oculto = true }) {
  return (
    <Svg width={tamano} height={tamano} viewBox="0 0 24 24" fill="none">
      <Path
        d="M2 12s3.5-6 10-6 10 6 10 6-3.5 6-10 6-10-6-10-6Z"
        stroke={color}
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <Circle cx="12" cy="12" r="2.6" fill={color} />
      {oculto ? (
        <Line x1="4" y1="20" x2="20" y2="4" stroke={color} strokeWidth="2" strokeLinecap="round" />
      ) : null}
    </Svg>
  );
}

export function IconoFlecha({ tamano = 24, color = colores.blanco }) {
  return (
    <Svg width={tamano} height={tamano} viewBox="0 0 24 24" fill="none">
      <Line x1="4" y1="12" x2="19" y2="12" stroke={color} strokeWidth="2.4" strokeLinecap="round" />
      <Path d="M13 6l6 6-6 6" stroke={color} strokeWidth="2.4" strokeLinecap="round" strokeLinejoin="round" />
    </Svg>
  );
}
