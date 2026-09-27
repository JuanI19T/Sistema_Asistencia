# context.md — Contexto del proyecto

> Guía rápida para entender el proyecto (humanos y agentes de IA) antes de tocar código.
> Documentos hermanos: `README.md` (visión general), `LEEME_recomponer.md` (restauración desde cero),
> `PROPUESTA-SINCRONIZACION.md` (integración escritorio ↔ app), `ComoFuncionaElSistema.html/pdf`.

## 1. Qué es

Sistema integral de gestión de asistencia escolar para una EEST (proyecto académico, EACP).
Reemplaza el registro en papel por: un aplicativo de escritorio para administración, una API
intermedia y una app móvil para registrar asistencia en el aula (QR / código corto).

## 2. Los tres módulos

| Módulo | Carpeta | Stack | Rol |
|---|---|---|---|
| **Escritorio** | `.net/SistemaAsistencia/` | C# .NET Framework 4.7.2, WinForms, MaterialSkin | ABMs institucionales (alumnos, profesores, preceptores, materias, especialidades, dictados, inscripciones, usuarios). Escribe **directo a MySQL**. Autentica usuarios propios contra **MongoDB Atlas** |
| **API** | `Api/` | Node.js, Express 5, mysql2, bcryptjs | Único acceso a MySQL de la app. Endpoints REST + **SSE** (`/api/eventos`). Login por rol con `dni` + bcrypt, token Bearer |
| **App móvil** | `App/` | React Native + Expo 57 (Expo Go) | Login y pantallas por rol (profesor/preceptor/alumno). Escaneo QR (`expo-camera`), SSE con reconexión automática. **Habla solo con la API** (nunca directo a la BD) |

```
Escritorio ────────────────► MySQL ◄──────────────── API ◄───► App móvil
   │                          ▲                          │        │
   └──► MongoDB Atlas         └── (fuente de verdad)     └── SSE ─┘
```

- MySQL `gestion_asistencia_eest` es la única fuente de verdad compartida.
- La app obtiene la IP del host sola (`App/src/api.js`, `Constants.expoConfig.hostUri`); la API escucha en el puerto **3000**.
- El escritorio **no tiene integración HTTP** hoy: sus cambios no generan eventos SSE (ver §8).

## 3. Base de datos (MySQL)

Script definitivo: `.net/SistemaAsistencia/BD/2026-09-26_reconstruccion_definitiva.sql`
(reconstruye todo, **borra datos previos**, incluye catálogo y datos de prueba).

Tablas: `especialidad`, `materia`, `profesor`, `preceptor`, `alumno`, `dictado`, `inscribe`,
`clase`, `asistencia`, `usuario`.

Restaurar: `mysql -u usuario -p < .net/SistemaAsistencia/BD/2026-09-26_reconstruccion_definitiva.sql`

Otros scripts en `Api/scripts/` (`agregar_codigo_clase.sql`, `migrar_a_clases.sql`) — históricos de migración; los scripts futuros deben incluir triggers si se adopta la outbox (§8).

## 4. Arquitectura del escritorio (MVC en capas)

`.net/SistemaAsistencia/SistemaAsistencia/` con:

- `Vista/` — Forms por módulo (Login, Principal, Alumnos, Profesores, Preceptores, Materias, Especialidades, Dictados, Inscripcion, Usuarios, PrimerUsuario, Comun, Configuracion).
- `Controlador/` — un `*Controller` por módulo.
- `Modelo/Entidades/` + `Modelo/DAO/` — un DAO por tabla; `Modelo/Conexion/` (`conexionBD.cs` MySQL, `ConexionMongo.cs` Mongo Atlas).
- `Utilidades/Sesion.cs` — usuario actual en memoria.

**Regla inviolable: no romper el MVC.** Toda futura integración HTTP debe vivir solo en la
capa Modelo (DAO/repositorio). Nunca en Vistas ni Controladores.

## 5. Cómo levantar cada módulo

Orden sugerido: **BD → API → App** (el escritorio es independiente).

1. **BD**: restaurar el script definitivo (§3).
2. **API** (`Api/`):
   - `copy .env.example .env` y completar `DB_HOST/PORT/NAME/USER/PASS` + `SESSION_SECRET`.
   - `npm install` → `npm start` (o `npm run dev` con watch). Corre en `http://localhost:3000`.
3. **App** (`App/`):
   - `npm install` → `npx expo start` → escanear QR con **Expo Go**.
   - Requisito: PC y celular en la **misma red Wi-Fi** (o usar los scripts `*-usb` del package.json).
4. **Escritorio**:
   - Abrir `.net/SistemaAsistencia/SistemaAsistencia.slnx` en Visual Studio; restaurar NuGet; F5.
   - Build por consola: usar el **MSBuild de Visual Studio 18** (`C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe`). `dotnet msbuild` **falla** (MSB3822/3823) en este proyecto clásico.
5. **Debug VS Code**: `.vscode/launch.json` trae la config compuesta "Proyecto: API + App (F5)".

## 6. Cuentas de prueba (incluidas en el script definitivo)

Contraseña = DNI en todos los casos. Legajo: en personal es igual al DNI; en alumnos lo genera la escuela.

| Rol | Nombre | DNI | Legajo |
|---|---|---|---|
| Profesor | Carlos Gutierrez | `30111222` | `30111222` |
| Profesor | María Fernández | `31222333` | `31222333` |
| Preceptor | Laura Martínez | `34555666` | `34555666` |
| Alumno | Juan Pérez | `45222001` | `1001` |
| Alumno | Ana Gómez | `45222002` | `1002` |
| Alumno | Luis Díaz | `45222003` | `1003` |

La app muestra botones de login rápido con estas cuentas solo en `__DEV__` (`App/App.js`).

## 7. Flujo de negocio clave (asistencia en el aula)

1. El **profesor** abre una clase desde su dictado (`POST /api/clases`), genera QR + código corto.
2. Los **alumnos** marcan presencia escaneando el QR (`POST /api/clases/:id/escanear`) o ingresando el código (`POST /api/clases/ingresar`).
3. El **preceptor** ve la asistencia en vivo (SSE) y puede ajustarla.
4. Endpoints principales (ver índice en `/`): `/api/login`, `/api/dictados*`, `/api/clases*`, `/api/asistencia*`, `/api/eventos` (SSE autenticado con Bearer).

## 8. Integración escritorio ↔ app (estado y decisión pendiente)

Problema actual: el escritorio escribe directo a MySQL y la SSE solo emite eventos por
cambios hechos **por la API** → un cambio hecho en el escritorio (ej. reasignar preceptor de un
dictado) no llega a la app hasta que esta re-consulta, y viceversa.

`PROPUESTA-SINCRONIZACION.md` analiza 5 alternativas y recomienda por fases:

- **Fase 0**: ordenar esquema (unificar `INNER`/`LEFT JOIN` en dictados, filtrar `activo` en listados de la API, exponer `id_preceptor`).
- **Fase 1 (quick win)**: refresco suave en la app + `Timer` ligero en escritorio.
- **Fase 2 (recomendada)**: tabla `outbox_evento` + triggers MySQL + worker en la API que emita SSE — captura el 100% de las escrituras sin tocar el escritorio.
- **Fase 3 (mediano plazo)**: migrar pantallas del escritorio a endpoints REST, con el HTTP confinado al Modelo.

Pendientes técnicos detallados: ver checklist en `PROPUESTA-SINCRONIZACION.md` §6
(refresco de listas del profesor, unificar joins, filtrar `activo`, endpoints de escritura
para dictados, rol administrador en la API, backups de MySQL/Mongo, etc.).

## 9. Reglas y convenciones del proyecto

- **Git**: trabajar en la rama `Juan-Torres` (o rama propia); **PR obligatorio a `main`**, nunca commitear directo a main. Ramas existentes: `main`, `Juan-Torres`, `Isa-Vecco`.
- **Credenciales**: están hardcodeadas a propósito en `App.config` y `ConexionMongo.cs` del escritorio (y en `Api/.env` para la API). **No cambiarlas ni copiarlas a otros archivos**. `.env`, `*.pem`, `*.key` y `AGENTS.md` raíz están gitignored — jamás subirlos.
- **Contraseñas**: `contrasena` se hashea con **bcrypt coste 10** (`$2a$10$…`). bcryptjs (API) y CryptSharp (escritorio) son interoperables. **No usar BCrypt.Net-Next** (delay-signed, no carga en .NET Framework) — usar el paquete `CryptSharpOfficial`.
- El login del escritorio de MongoDB usa PBKDF2 propio (`pbkdf2$`, 10k iteraciones) — identidad separada de la API (pendiente unificar, §6 de la propuesta).
- **Expo**: la app usa Expo 57; leer la documentación versionada (https://docs.expo.dev/versions/v57.0.0/) antes de escribir código de la app (`App/AGENTS.md`).
- **Estilo de código**: identificadores y BD en español (`alumno`, `dictado`, `obtenerDictados`); API sin acentos en rutas/JSON.
- **Baja lica**: entidades con columna `activo`; el login de la API filtra `activo = 1` (faltó aplicarlo en otros listados — pendiente).

## 10. Mapa de archivos clave

| Archivo/Carpeta | Qué es |
|---|---|
| `Api/index.js` | Bootstrap de la API + índice de endpoints |
| `Api/rutas/` | `login.js`, `clases.js`, `dictados.js`, `asistencias.js`, `eventos.js` (SSE) |
| `Api/config/db.js` | Pool mysql2 (lee `.env`) |
| `App/App.js` | Login + enrutado por rol |
| `App/src/api.js` | Cliente HTTP/SSE de la app (detecta host, guarda token Bearer) |
| `App/src/pantallas/` | `ProfesorPrincipal.js`, `PreceptorPrincipal.js`, `AlumnoPrincipal.js` |
| `.net/.../SistemaAsistencia.slnx` | Solución del escritorio (abrir en Visual Studio) |
| `.net/.../Modelo/Conexion/` | Cadenas de conexión MySQL/Mongo |
| `.net/.../BD/` | Script SQL definitivo de reconstrucción |
| `.vscode/launch.json` | Debug de API + App en un F5 |
