import Svg, { Path } from 'react-native-svg';
import { colores } from '../tema';

const VIEW_W = 1024;
const VIEW_H = 320;

function capas({ arriba }) {
  const techo = arriba ? 'V0 H0 Z' : 'V320 H0 Z';
  return [
    {
      fill: colores.navy,
      d: `M0 ${arriba ? 150 : 170} C180 ${arriba ? 70 : 250} 420 ${arriba ? 20 : 300} 640 ${
        arriba ? 30 : 290
      } C820 ${arriba ? 40 : 280} 940 ${arriba ? 120 : 200} 1024 ${
        arriba ? 170 : 150
      } ${techo}`
    },
    {
      fill: colores.azulReal,
      d: `M0 ${arriba ? 205 : 115} C180 ${arriba ? 140 : 180} 420 ${arriba ? 95 : 225} 640 ${
        arriba ? 100 : 220
      } C820 ${arriba ? 110 : 210} 940 ${arriba ? 185 : 135} 1024 ${
        arriba ? 220 : 100
      } ${techo}`
    },
    {
      fill: colores.amarillo,
      d: `M0 ${arriba ? 255 : 65} C180 ${arriba ? 200 : 120} 420 ${arriba ? 165 : 155} 640 ${
        arriba ? 168 : 152
      } C820 ${arriba ? 175 : 145} 940 ${arriba ? 235 : 85} 1024 ${
        arriba ? 268 : 52
      } ${techo}`
    }
  ];
}

export default function Ondas({ ancho, alto, posicion }) {
  const arriba = posicion === 'superior';
  const capasDibujo = capas({ arriba });

  return (
    <Svg
      width={ancho}
      height={alto}
      viewBox={`0 0 ${VIEW_W} ${VIEW_H}`}
      preserveAspectRatio="none"
    >
      {capasDibujo.map((capa, i) => (
        <Path key={i} d={capa.d} fill={capa.fill} />
      ))}
    </Svg>
  );
}
