import Svg, { Path } from 'react-native-svg';
import { colores } from '../tema';

const SUPERIORES = [
  {
    fill: colores.amarillo,
    d: 'M 0,210 C 120,145 205,100 330,108 C 410,110 470,130 550,145 C 720,175 875,220 1024,145 L 1024,0 L 0,0 Z'
  },
  {
    fill: colores.azulReal,
    d: 'M 0,155 C 120,90 205,45 330,53 C 410,55 470,75 550,90 C 720,120 875,165 1024,90 L 1024,190 C 720,230 330,160 0,300 Z'
  },
  {
    fill: colores.navy,
    d: 'M 0,115 C 120,50 205,5 330,13 C 410,15 470,35 550,50 C 720,80 875,125 1024,50 L 1024,150 C 720,190 330,120 0,260 Z'
  }
];

const INFERIORES = [
  {
    fill: colores.amarillo,
    d: 'M 0,40 C 155,-5 280,45 440,100 C 610,160 760,185 900,120 C 960,95 1000,65 1024,45 L 1024,95 C 870,205 730,235 540,170 C 350,105 190,40 0,125 Z'
  },
  {
    fill: colores.azulReal,
    d: 'M 0,40 C 155,-5 280,45 440,100 C 610,160 760,185 900,120 C 960,95 1000,65 1024,45 L 1024,0 L 0,0 Z'
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
