const { Router } = require('express');
const { requiereRol } = require('../utilidades/autenticacion');
const { conectarEventos } = require('../utilidades/eventos');

const router = Router();

router.get('/', requiereRol(['profesor', 'preceptor', 'alumno']), (req, res) => {
  conectarEventos(req, res, req.usuario);
});

module.exports = router;
