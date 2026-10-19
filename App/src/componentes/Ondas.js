import Svg, { Path } from 'react-native-svg';
import { colores } from '../tema';

export default function Ondas({ ancho, alto, posicion }) {
  const esSuperior = posicion === 'superior';
  return (
    <Svg
      width={ancho}
      height={alto}
      viewBox="0 0 1024 295"
      preserveAspectRatio="none"
    >
      {esSuperior ? (
        <>
          <Path
            d="M1024 0 V30 C820 80 620 204 420 196 C260 190 120 150 0 108 Z"
            fill={colores.amarillo}
          />
          <Path
            d="M1024 0 V40 C820 90 600 210 380 200 C220 192 100 230 0 250 Z"
            fill={colores.azulReal}
          />
          <Path
            d="M0 0 H1024 V0 C860 40 620 148 400 160 C240 168 90 200 0 210 Z"
            fill={colores.navy}
          />
        </>
      ) : (
        <>
          <Path
            d="M0 187 C180 120 420 90 640 100 C820 108 940 190 1024 230 V295 H0 Z"
            fill={colores.amarillo}
          />
          <Path
            d="M0 128 C160 76 400 56 620 76 C800 92 940 196 1024 226 V295 H0 Z"
            fill={colores.azulReal}
          />
          <Path
            d="M0 84 C180 40 420 20 640 28 C820 34 940 160 1024 180 V295 H0 Z"
            fill={colores.navy}
          />
        </>
      )}
    </Svg>
  );
}
