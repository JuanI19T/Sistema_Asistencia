```markdown
# Sistema Integral de Gestión de Asistencia Escolar

> Sistema para digitalizar el control de asistencia de alumnos y centralizar la gestión de alumnos, profesores, preceptores, cursos y materias. Proyecto académico EEST. Compuesto por 3 módulos: **aplicativo de escritorio** (administración), **API REST** (lógica compartida y tiempo real) y **app móvil** (registro de asistencia con QR).

![C#](https://img.shields.io/badge/C%23-.NET_Framework_4.7.2-239120?logo=csharp&logoColor=white)
![WinForms](https://img.shields.io/badge/UI-Windows_Forms+MaterialSkin-blue)
![MySQL](https://img.shields.io/badge/DB-MySQL-4479A1?logo=mysql&logoColor=white)
![MongoDB](https://img.shields.io/badge/Auth-MongoDB-47A248?logo=mongodb&logoColor=white)
![Node](https://img.shields.io/badge/API-Node+Express-339933?logo=node.js&logoColor=white)
![Expo](https://img.shields.io/badge/App-Expo_SDK_57-000020?logo=expo&logoColor=white)
![Estado](https://img.shields.io/badge/Estado-En_desarrollo-yellow)

## Índice

- [1. Descripción del proyecto](#1-descripción-del-proyecto)
- [2. Problemática](#2-problemática)
- [3. Alcance y funcionalidades](#3-alcance-y-funcionalidades)
- [4. Usuarios y roles](#4-usuarios-y-roles)
- [5. Tecnologías y arquitectura](#5-tecnologías-y-arquitectura)
- [6. Estructura del proyecto](#6-estructura-del-proyecto)
- [7. Requisitos previos](#7-requisitos-previos)
- [8. Instalación y configuración](#8-instalación-y-configuración)
- [9. Uso del sistema](#9-uso-del-sistema)
- [10. Metas y factores críticos de éxito](#10-metas-y-factores-críticos-de-éxito)
- [11. Cronograma](#11-cronograma)
- [12. Costos](#12-costos)
- [13. Roadmap](#13-roadmap)
- [14. Equipo y contexto académico](#14-equipo-y-contexto-académico)
- [15. Licencia](#15-licencia)
- [16. Flujo de trabajo (Git)](#16-flujo-de-trabajo-git)
- [17. Documentación adicional](#17-documentación-adicional)

## 1. Descripción del proyecto

Actualmente el control de asistencia en la institución se realiza mayormente **en papel**, lo que genera desorganización, riesgo de pérdida de datos, errores humanos y demora en los reportes.

Este proyecto desarrolla un **sistema integral de gestión de asistencia escolar** compuesto por:

1. **Aplicativo administrativo de escritorio** (`.net/`): gestión de información institucional (alumnos, profesores, preceptores, cursos, materias, dictados, inscripciones, usuarios).
2. **API REST** (`Api/`): expone los datos a la app móvil, emite eventos en tiempo real (SSE) y gestiona las clases con token QR.
3. **App móvil** (`App/`): registro de asistencia desde el celular para docentes, preceptores y alumnos (escaneo QR o código corto).

Objetivo: digitalizar el registro diario, mejorar la disponibilidad de la información y automatizar reportes en una plataforma segura y organizada.

## 2. Problemática

**Puntos débiles del sistema actual:**
- Registro en papel, con riesgo de pérdida y deterioro.
- Dificultad para consultar historial de asistencias.
- Procesos administrativos lentos.
- Errores en la carga manual de datos.
- Sin validaciones automáticas ni reportes inmediatos.
- Dependencia total de documentación física.

**Puntos fuertes que se conservan:**
- Experiencia de preceptores y docentes en la toma de asistencia.
- Estructura organizacional claramente definida.
- Reglas institucionales de asistencia/inasistencia ya conocidas.
- Metodología de trabajo establecida como base del nuevo sistema.

## 3. Alcance y funcionalidades

**Escritorio (WinForms):**

| Módulo | Descripción |
|---|---|
| Autenticación | Login de usuarios con control de sesión |
| Primer usuario | Creación del primer Administrador si no existen usuarios |
| Usuarios y perfiles | Alta, baja, modificación y activación/desactivación |
| Alumnos | ABM completo de alumnos |
| Profesores | ABM completo de profesores |
| Preceptores | ABM completo de preceptores (contraseña bcrypt compatible con la API) |
| Cursos / Especialidades | Administración de cursos y especialidades |
| Materias | ABM de materias con asignación de especialidad |
| Dictados | Gestión de dictados (materia + profesor + preceptor) |
| Inscripciones | Inscripción de alumnos a dictados |
| Permisos | Control de acceso según rol (menú filtrado en `FrmPrincipal`) |

**API (`Api/`, puerto 3000):**

| Endpoint | Descripción |
|---|---|
| `GET /api/health` | Verifica que el servidor responde |
| `GET /api/alumnos` | Lista alumnos (MySQL) |
| `POST /api/login` | Autenticación por rol: preceptor, profesor o alumno |
| `GET /api/dictados`, `GET /api/dictados/:id/alumnos`, `GET /api/dictados/alumno/:idAlumno` | Consulta de dictados e inscriptos |
| `GET /api/asistencia`, `POST /api/asistencia`, `POST /api/asistencia/lote` | Consulta y alta de asistencias |
| `GET /api/clases?fecha=`, `POST /api/clases`, `PATCH /api/clases/:id` | Apertura y estados de clase |
| `POST /api/clases/:id/token`, `POST /api/clases/:id/escanear`, `POST /api/clases/ingresar` | Token QR, escaneo y código corto |
| `GET /api/eventos` | Eventos en tiempo real (SSE, autenticado) |

**App móvil (`App/`):**

| Pantalla | Rol |
|---|---|
| `PreceptorPrincipal` | Preceptor: clases y asistencia |
| `ProfesorPrincipal` | Profesor: sus dictados y clases |
| `AlumnoPrincipal` | Alumno: escaneo QR e ingreso con código |

## 4. Usuarios y roles

| Rol | Permisos |
|---|---|
| **Administrador** | Configuración general, gestión de usuarios y mantenimiento (solo escritorio) |
| **Directivo** | Administración de alumnos, profesores, cursos, materias y consultas generales |
| **Preceptor** | Registro y seguimiento de asistencias e inasistencias (escritorio + app) |
| **Profesor** | Carga de asistencia de sus clases (escritorio + app) |
| **Alumno** | Registra su asistencia escaneando el QR (app) |

El menú principal (`FrmPrincipal`) aplica permisos automáticamente: el módulo de usuarios solo es visible para el Administrador. El login de la API solo contempla preceptor, profesor y alumno (no hay rol administrador en la API).

## 5. Tecnologías y arquitectura

**Escritorio (`.net/`):**
- **Lenguaje:** C# .NET Framework 4.7.2
- **Interfaz:** Windows Forms + MaterialSkin.2
- **Base relacional:** MySQL `gestion_asistencia_eest` (alumnos, profesores, preceptores, materias, especialidades, dictados, inscripciones, asistencias)
- **Base documental:** MongoDB Atlas (usuarios y autenticación del escritorio)
- **Drivers:** `MySql.Data` + `MongoDB.Driver` (+ `CryptSharpOfficial` **2.1.0.0** para bcrypt)
- **IDE:** Visual Studio (con workload .NET desktop development)
- **Arquitectura:** en capas tipo MVC

**API (`Api/`):**
- Node.js + Express 5, `mysql2`, `bcryptjs`, `cors`, `dotenv`
- Lee/escribe el mismo MySQL que el escritorio (única fuente de verdad compartida)

**App (`App/`):**
- React Native con Expo SDK 57, React 19
- Cámara (`expo-camera`) para escaneo QR, eventos en tiempo real (SSE)

**Capas del escritorio:**

```
Vista (Forms) <-> Controlador (Lógica de negocio) <-> Modelo (Entidades + DAO + Conexión)
```

- **Modelo/Entidades:** `Alumno`, `Profesor`, `Preceptor`, `Materia`, `Especialidad`, `Dictado`, `Inscripcion`, `Asistencia`, `Usuario`, `Rol`
- **Modelo/DAO:** `AlumnoDAO`, `ProfesorDAO`, `PreceptorDAO`, `MateriaDAO`, `EspecialidadDAO`, `DictadoDAO`, `InscripcionDAO` (MySQL) y `UsuarioDAO` (MongoDB)
- **Modelo/Conexion:** `conexionBD.cs` (MySQL), `ConexionMongo.cs` (Atlas)
- **Controlador:** `AlumnoController`, `ProfesorController`, `PreceptorController`, `MateriaController`, `EspecialidadController`, `DictadoController`, `InscripcionController`, `UsuarioController`
- **Vista:** `FrmLogin`, `FrmPrincipal`, `FrmInicio`, `FrmAlumnos`, `FrmProfesores`, `FrmPreceptores`, `FrmMaterias`, `FrmEspecialidades`, `FrmDictados`, `FrmInscripcion`, `FrmUsuarios`, `FrmPrimerUsuario`, `FrmConfiguracion`, `FrmConfirmarEliminar`
- **Utilidades:** `Sesion` (usuario actual en memoria), `Configuracion` (cadenas de conexión), `Logger` (logs en `bin/**/logs`), `DatosException`, `Ejecutor`

**Contraseñas:** las de preceptor/profesor en MySQL usan bcrypt (`$2a$`, costo 10), interoperable entre el escritorio (CryptSharp) y la API (bcryptjs).

> Detalle de sincronización escritorio ↔ app y decisiones pendientes: ver `PROPUESTA-SINCRONIZACION.md`.

## 6. Estructura del proyecto

```
Sistema_Asistencia/
├── README.md
├── LEEME_recomponer.md            # recomponer los 3 módulos desde un zip
├── PROPUESTA-SINCRONIZACION.md    # conexión escritorio <-> app (fases)
├── ComoFuncionaElSistema.pdf/.html
├── .net/                          # módulo escritorio
│   ├── CHECKLIST-MEJORAS.md
│   ├── secreto.config             # LOCAL, gitignored (URI de Atlas ofuscado)
│   └── SistemaAsistencia/
│       ├── SistemaAsistencia.slnx
│       ├── BD/2026-09-24_baja_logica_activo.sql
│       └── SistemaAsistencia/
│           ├── Program.cs
│           ├── App.config         # MySQL local (MongoAtlas se completa por wizard)
│           ├── packages.config
│           ├── Controlador/
│           ├── Modelo/Conexion|DAO|Entidades/
│           ├── Vista/Login|Principal|Alumnos|Profesores|Preceptores|Materias|Especialidades|Dictados|Inscripcion|Usuarios|PrimerUsuario|Configuracion|Comun/
│           └── Utilidades/
├── Api/                           # módulo API (Node/Express)
│   ├── index.js
│   ├── .env.example               # plantilla (el .env real es LOCAL, gitignored)
│   ├── config/db.js
│   ├── rutas/asistencias.js|clases.js|dictados.js|eventos.js|login.js
│   ├── scripts/migrar_a_clases.sql|agregar_codigo_clase.sql|seed.js
│   └── utilidades/
└── App/                           # módulo móvil (Expo)
    ├── App.js
    ├── app.json
    ├── src/api.js
    └── src/pantallas/AlumnoPrincipal.js|PreceptorPrincipal.js|ProfesorPrincipal.js
```

## 7. Requisitos previos

**Hardware:**
- PC con Windows 10 o superior (personal directivo / administrativo)
- Celular con Expo Go + PC en la misma red (app móvil)
- MySQL Server 8.x (local o remoto) + acceso a MongoDB Atlas

**Software:**
- Visual Studio 2022 o superior (workload .NET desktop development)
- .NET Framework 4.7.2 Developer Pack
- MySQL Server 8.x + MySQL Workbench
- Node.js 20+ (para `Api/` y `App/`)
- Expo Go en el celular (para `App/`)

## 8. Instalación y configuración

### 8.1 Escritorio (.NET)

1. **Clonar el repositorio** (trabajo siempre en la branch `Juan-Torres`, ver §16):
   ```bash
   git clone -b Juan-Torres https://github.com/JuanI19T/Sistema_Asistencia.git
   ```

2. **Abrir la solución:**
   Abrir `.net/SistemaAsistencia/SistemaAsistencia.slnx` en Visual Studio.

3. **Restaurar paquetes NuGet:**
   Visual Studio lo hace automático (clic derecho en la solución → *Restaurar paquetes NuGet* si hace falta). Incluye `MySql.Data`, `MongoDB.Driver`, `MaterialSkin.2` y `CryptSharpOfficial` (**2.1.0.0**, versionado fijo en `packages.config`).

4. **Configurar MySQL local:**
   Revisar la cadena `MySQL` en `SistemaAsistencia/App.config` (apunta a localhost por defecto).

5. **Compilar y ejecutar (F5):**
   - La primera vez se abre `FrmConfiguracion`: pegar el URI completo de MongoDB Atlas (empieza con `mongodb+srv://`). Se guarda ofuscado en `.net/secreto.config` (local, no se commitea).
   - Si no existen usuarios, se abre `FrmPrimerUsuario` para crear el primer Administrador.
   - Luego se accede con `FrmLogin`.

> Nota: compilar con el MSBuild de Visual Studio. `dotnet build` no es compatible con este proyecto clásico de .NET Framework.

### 8.2 Base de datos MySQL

1. Crear la base `gestion_asistencia_eest` y aplicar (¡borra datos anteriores!):
   ```bash
   mysql -u usuario -p gestion_asistencia_eest < Api/scripts/migrar_a_clases.sql
   ```
2. Script adicional del escritorio (baja lógica):
   `.net/SistemaAsistencia/BD/2026-09-24_baja_logica_activo.sql`
3. Cargar datos de prueba (idempotente; contraseña inicial = DNI):
   ```bash
   cd Api
   node scripts/seed.js
   ```

### 8.3 API (Node)

1. Crear el archivo de entorno y completarlo con los datos de tu MySQL:
   ```bash
   cd Api
   copy .env.example .env
   npm install
   ```
2. Levantar la API:
   ```bash
   node index.js
   ```
   Queda escuchando en `http://localhost:3000` (`GET /api/health` para verificar).

### 8.4 App móvil (Expo)

1. Instalar dependencias e iniciar:
   ```bash
   cd App
   npm install
   npx expo start
   ```
2. Escanear el QR con **Expo Go** en el celular (misma red WiFi que la PC; la app resuelve la IP del host automáticamente).

## 9. Uso del sistema

**Escritorio:**
1. Iniciar la app, crear el primer usuario Administrador si es la primera vez.
2. Iniciar sesión con usuario y contraseña.
3. Desde el menú principal:
   - **Administrador:** gestiona usuarios, alumnos, profesores, preceptores y materias.
   - **Directivo:** administra datos institucionales y consultas.
   - **Preceptor / Profesor:** gestionan dictados, inscripciones y datos.
4. La sesión actual muestra nombre y rol en `FrmPrincipal`. Los errores de arranque quedan en `bin/**/logs/log_*.txt`.

**API + App:**
1. Levantar MySQL y la API (`node index.js`).
2. El profesor/preceptor abre su clase desde la app (la API genera el token QR).
3. El alumno escanea el QR (o ingresa el código corto) y queda registrada su asistencia en tiempo real.

**Datos de prueba (del seed, contraseña = DNI):**

| Rol | Nombre | DNI |
|---|---|---|
| Profesor | Carlos Gutierrez | `30111222` |
| Profesor | María Fernández | `31222333` |
| Preceptor | Laura Martínez | `34555666` |
| Alumno | Juan Pérez | `45222001` |
| Alumno | Ana Gómez | `45222002` |
| Alumno | Luis Díaz | `45222003` |

## 10. Metas y factores críticos de éxito

**Metas:**
- Digitalizar 100% el control de asistencia.
- Reducir el uso de papel.
- Mejorar disponibilidad de la información.
- Facilitar administración de alumnos, profesores y cursos.
- Automatizar reportes de asistencia.

**Factores críticos:**
- Autenticación segura y funcional.
- Permisos correctos por rol.
- Disponibilidad permanente de la BD.
- Interfaz simple e intuitiva.
- Registro rápido de asistencias.
- Integridad y seguridad de los datos.
- Código extensible a futuro (módulo web).

## 11. Cronograma

| Etapa | Período estimado |
|---|---|
| Estudio de factibilidad | 29 de abril – primera semana |
| Análisis de requisitos | Mayo |
| Diseño del sistema | Junio – Julio |
| Desarrollo de la aplicación | Agosto – Octubre |
| Implementación y pruebas | Noviembre |
| Presentación EACP | Fin del ciclo lectivo |

**Tiempo total:** ~7 meses.

## 12. Costos

Proyecto académico sin costo de mano de obra.

| Concepto | Costo estimado |
|---|---|
| Desarrollo de software | Sin costo (proyecto académico) |
| MySQL Community Edition | $0 |
| Visual Studio Community | $0 |
| Node.js / Express / Expo | $0 |
| Equipamiento existente | Sin costo adicional |
| Capacitación de usuarios | Mínima |
| Mantenimiento anual | Bajo |

El principal costo futuro será el mantenimiento correctivo y evolutivo.

## 13. Roadmap

**Hecho:**
- [x] Login y gestión de primer usuario admin (escritorio)
- [x] ABM Alumnos, Profesores, Preceptores, Materias, Especialidades, Dictados, Inscripciones, Usuarios
- [x] Control de permisos por rol
- [x] Baja lógica (`activo`) en escritorio + script SQL
- [x] Credenciales Mongo en `secreto.config` (ofuscado, gitignored)
- [x] API REST: login, dictados, asistencias, clases con QR, health
- [x] Eventos en tiempo real (SSE) API → app
- [x] App móvil con 3 roles + escaneo QR + código corto

**Pendiente:**
- [ ] Refresco automático en la app ante cambios hechos desde el escritorio (outbox + triggers, ver propuesta Fase 2)
- [ ] Unificar joins de dictados (INNER vs LEFT) entre escritorio y API
- [ ] Filtrar `activo` en todos los listados de la API
- [ ] Exponer `id_preceptor` en `GET /api/dictados`
- [ ] Endpoints de escritura para dictados (`POST`, `PATCH`)
- [ ] Rol administrador en la API / unificar estrategia de identidad
- [ ] Backup/restauración de MySQL y Mongo
- [ ] Bloqueo por intentos fallidos de login, auditoría, tests

## 14. Equipo y contexto académico

Proyecto escolar desarrollado por estudiantes de la EEST como Evaluación Anual de Capacidades Profesionales (EACP). Migrado desde carpeta compartida de Drive a GitHub para control de versiones y trabajo colaborativo.

## 15. Licencia

Proyecto académico sin licencia definida por el momento.

## 16. Flujo de trabajo (Git)

- Branch de trabajo: **`Juan-Torres`**. Nunca se trabaja ni commitea directo en `main` ni en otras ramas.
- Todo cambio va a `main` únicamente vía **Pull Request** desde `Juan-Torres`, previa revisión y aprobación.
- No commitear nunca secretos ni generados: `secreto.config`, `Api/.env`, `*.zip`, `node_modules/`, `.expo/`, `bin/`, `obj/`, `.vs/`, `packages/` (ver `.gitignore`).

## 17. Documentación adicional

- `ComoFuncionaElSistema.pdf` / `.html`: descripción funcional del sistema.
- `PROPUESTA-SINCRONIZACION.md`: conexión escritorio ↔ app, problemas detectados y hoja de ruta por fases.
- `LEEME_recomponer.md`: recomponer los 3 módulos desde un zip (restauración limpia).
- `.net/CHECKLIST-MEJORAS.md`: checklist de mejoras del escritorio con estados.
```

## Para aplicar

1. Abrí `README.md` y reemplazá todo el contenido por el de arriba.
2. Verificá que compile/visualice bien (es solo markdown, sin riesgo).
3. Después: commit en `Juan-Torres` → push → entra al PR #2 que ya está abierto (o a uno nuevo si ese se mergea antes).

Dos decisiones que tomé y podés revertir: dejé §15 Licencia como "sin licencia definida" (el `LICENSE` de `App/` es el de la plantilla Expo, no del proyecto — no lo usé para no atribuir mal), y no puse nombres en §14 porque no los tengo.
