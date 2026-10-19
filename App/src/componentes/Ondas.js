import Svg, { Path } from 'react-native-svg';
import { colores } from '../tema';

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
    d: 'M 0,20 C 180,-25 300,20 455,65 C 650,125 805,140 930,80 C 970,62 1000,42 1024,25 L 1024,0 L 0,0 Z'
  },
  {
    fill: colores.navy,
    d: 'M 0,125 C 145,45 270,55 430,115 C 620,185 760,220 920,145 C 965,125 1000,105 1024,85 L 1024,300 L 0,300 Z'
  }
];

const espejoHorizontal = (d) => {
  const tokens = d.match(/[A-Za-z]|-?\d*\.?\d+/g);
  let ejeX = true;
  return tokens
    .map((t) => {
      if (/[A-Za-z]/.test(t)) {
        ejeX = t !== 'V';
        return t;
      }
      if (ejeX) {
        ejeX = false;
        return String(1024 - Number(t));
      }
      ejeX = true;
      return t;
    })
    .join(' ');
};

const SUPERIORES = INFERIORES
  .filter((capa) => capa.d !== 'M 0,0 H 1024 V 300 H 0 Z')
  .map((capa) => ({
    fill: capa.fill,
    d: espejoHorizontal(capa.d)
  }))
  .reverse();

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
