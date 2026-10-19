import Svg, { Path } from 'react-native-svg';
import { colores } from '../tema';

const SUPERIORES = [
  {
    fill: colores.navy,
    d: 'M 0,300 H 1024 V 0 H 0 Z'
  },
  {
    fill: colores.amarillo,
    d: 'M 0,260 C 180,305 300,260 455,215 C 650,155 805,140 930,200 C 970,218 1000,238 1024,255 L 1024,205 C 875,125 730,100 540,150 C 340,202 170,280 0,175 Z'
  },
  {
    fill: colores.azulReal,
    d: 'M 0,300 H 1024 V 255 C 900,185 790,155 650,175 C 450,205 270,290 100,280 C 60,278 25,272 0,260 Z'
  },
  {
    fill: colores.crema,
    d: 'M 0,260 C 180,305 300,260 455,215 C 650,155 805,140 930,200 C 970,218 1000,238 1024,255 L 1024,300 L 0,300 Z'
  },
  {
    fill: colores.navy,
    d: 'M 0,175 C 145,255 270,245 430,185 C 620,115 760,80 920,155 C 965,175 1000,195 1024,215 L 1024,0 L 0,0 Z'
  }
];

const INFERIORES = [
  {
    fill: colores.navy,
    d: 'M 0,0 H 1024 V 300 H 0 Z'
  },
  {
    fill: colores.amarillo,
    d: 'M 0,40 C 180,-5 300,40 455,85 C 650,145 805,160 930,100 C 970,82 1000,62 1024,45 L 1024,95 C 875,175 730,200 540,150 C 340,98 170,20 0,125 Z'
  },
  {
    fill: colores.azulReal,
    d: 'M 0,0 H 1024 V 45 C 900,115 790,145 650,125 C 450,95 270,10 100,20 C 60,22 25,28 0,40 Z'
  },
  {
    fill: colores.crema,
    d: 'M 0,40 C 180,-5 300,40 455,85 C 650,145 805,160 930,100 C 970,82 1000,62 1024,45 L 1024,0 L 0,0 Z'
  },
  {
    fill: colores.navy,
    d: 'M 0,125 C 145,45 270,55 430,115 C 620,185 760,220 920,145 C 965,125 1000,105 1024,85 L 1024,300 L 0,300 Z'
  }
];

export default function Ondas({ ancho, alto, posicion }) {
  const capas = posicion === 'superior' ? SUPERIORES : INFERIORES;

  return (
    <Svg
      width={ancho}
      height={alto}
      viewBox="0 0 1024 300"
      preserveAspectRatio="none"
    >
      {capas.map((capa, i) => (
        <Path key={i} d={capa.d} fill={capa.fill} />
      ))}
    </Svg>
  );
}
