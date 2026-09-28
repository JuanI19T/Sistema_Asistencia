<p align="center">

# 🛡️ Política de Seguridad

![Seguridad](https://img.shields.io/badge/Seguridad-Reporte_privado-blue?logo=github)
![Soporte](https://img.shields.io/badge/Soporte-solo_main-green)
![Proyecto](https://img.shields.io/badge/Proyecto-Acad%C3%A9mico_EEST-yellow)

> 🔒 **Por favor no abras un issue público para vulnerabilidades.** Reportalas en privado 👇

</p>

## 📣 Cómo reportar

1. Vía preferida: **Security > Report a vulnerability** (reporte privado de GitHub) en este repo.
2. Alternativa: 📧 **juanitorres192008@gmail.com** con asunto `[SECURITY] Sistema_Asistencia - breve descripción`.

Incluí por favor: 🧩 módulo afectado (`🖥️ escritorio` / `🔌 Api/` / `📱 App/`), 📝 pasos para reproducir, 💥 impacto estimado y 📞 tu contacto.

> [!NOTE]
> ⏱️ Respondemos en ~5 días hábiles (período de clases). Si es crítico (fuga de datos, bypass de login) priorizamos fix en `main` vía PR.

## ✅ Versiones soportadas

| Versión | Soporte |
|---|---|
| `main` (último estado) | ✅ |
| Ramas personales / snapshots viejos | ❌ |

> Solo damos parches sobre `main`. No hay LTS ni backports.

## 🎯 Alcance

- 🖥️ Escritorio `.net/`, 🔌 API `Api/`, 📱 App `App/` y `BD/`.
- 🚫 Fuera de alcance: `node_modules/`, `.expo/`, bases locales desechables (`localhost:3306`), DoS y tooling de terceros.

> [!CAUTION]
> ⛔ No publiques el fallo hasta que lo corrijamos y te avisemos. Gracias por darnos tiempo de arreglarlo 🙏
