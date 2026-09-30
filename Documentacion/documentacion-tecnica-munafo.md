<p align="center">

# 📘 Documentación Técnica y Análisis de Requerimientos

> 🏫 **Sistema Integral de Asistencia Escolar (SIA)**
> EACP 2026 · Proyecto de Implementación de Sitios Web Dinámicos
> Docente a cargo: **Guillermo Munafo**

</p>

---

## 📑 Índice

| | Sección | Qué encontrás |
|---|---|---|
| [1](#s1) | [Datos generales del proyecto](#s1) | Identificación del proyecto, cliente, equipo y repositorio |
| [2](#s2) | [Descripción del problema real](#s2) | Situación actual e ineficiencias de la gestión en papel |
| [3](#s3) | [Solución tecnológica propuesta](#s3) | Los tres módulos, la infraestructura y el circuito cubierto |

**Leyenda:** 🔴 riesgo o prohibido · 🟡 pendiente o con salvedad · 🟢 seguro u opcional

---

<a id="s1"></a>

## 1. Datos generales del proyecto

| Campo | Detalle |
|---|---|
| **Nombre del Sistema / Proyecto** | Sistema Integral de Asistencia Escolar (SIA) |
| **Cliente / Organización** | Escuela de Educación Secundaria Técnica N° 1 "Ara. General Belgrano" (E.E.S.T. N° 1) |
| **Integrantes del grupo** | Isabella Lara Carrete Vecco y Juan Ignacio Torres Troschasky |
| **Materia** | Proyecto de Implementación de Sitios Web Dinámicos |
| **Docente a cargo** | Guillermo Munafo |
| **Repositorio de GitHub** | <https://github.com/JuanI19T/Sistema_Asistencia> |

El proyecto SIA no se limita a reemplazar un formulario en papel: **cubre el circuito completo** de la
información de asistencia, desde la carga de datos institucionales (alumnos, profesores, materias,
dictados, inscripciones) hasta el registro de asistencia en el aula y su posterior consulta. Por eso
se denomina *integral*.

El cliente es la E.E.S.T. N° 1, una institución secundaria técnica de la Provincia de Buenos Aires con
dirección, secretaria, docentes, preceptores y alumnos. El sistema se desarrolló como **Evaluación
Anual de Capacidades Profesionales (EACP)** de la carrera de Analista en Sistemas.

El trabajo fue realizado por dos integrantes, cada uno con su propia rama en el repositorio
(`Isa-Vecco` y `Juan-Torres`), e integrando a `main` **únicamente mediante Pull Request** para garantizar
la trazabilidad individual. El repositorio fue creado el **[FECHA PENDIENTE]**.

> 📌 La materia resulta pertinente porque el valor del sistema está en sus componentes web: una API
> REST con autenticación por token y emisión de eventos en tiempo real (SSE), consumida por una
> aplicación móvil.

---

<a id="s2"></a>

## 2. Descripción del problema real

### 2.1 Situación actual

Actualmente la E.E.S.T. N° 1 lleva a cabo la gestión de sus operaciones operativas y administrativas de
manera **manual**, utilizando registros en formato papel y planillas de cálculo descentralizadas.

El control diario de asistencia se realiza mayormente sobre libros y planillas físicas: cada docente
registra las presencias e inasistencias de su curso en papel y, al cierre del período, esos datos
deben transcribirse a formularios y listados administrativos. Los listados de alumnos, materias y
cursadas se mantienen en planillas de cálculo individuales, **sin una versión única compartida** entre
dirección, preceptoría y docentes.

Según el relevamiento institucional realizado con el equipo directivo (Acta del 29/04/2026), la
complejidad real es mayor que un simple listado de faltas: **las trayectorias escolares son altamente
personalizadas.** Un mismo estudiante puede cursar materias de distintos años en simultáneo, recursar
únicamente las materias que adeuda e intensificar otras, por lo que cada alumno presenta un conjunto de
cursadas diferente. Sobre esa estructura se construyen, además, las planillas de asistencia, cuyo
punto de partida es **siempre el estudiante**.

Con una base de datos en papel y planillas aisladas, la institución no cuenta con información
confiable, centralizada ni consultable en tiempo real para tomar decisiones sobre matrícula,
trayectorias ni regularidad.

### 2.2 Ineficiencias operativas destacables

| # | Ineficiencia | Consecuencia |
|---|---|---|
| 1 | 📉 **Riesgo de pérdida y deterioro de la información** | Los registros en soporte papel pueden extraviarse, destruirse o volverse ilegibles, sin copia de respaldo |
| 2 | 🔀 **Información descentralizada e inconsistente** | Al convivir varias planillas sin versión única, un mismo alumno o dictado puede estar cargado de forma distinta o duplicada en cada archivo |
| 3 | 🔍 **Dificultad para consultar el historial** | Rastrear las inasistencias de un alumno a lo largo del ciclo implica revisar planillas físicas una por una |
| 4 | 🐌 **Procesos administrativos lentos** | La búsqueda de un estudiante, el armado de listados y el cierre de planillas se hacen a mano, con demoras y trabajo repetitivo |
| 5 | ✍️ **Errores humanos en la carga de datos** | El paso del papel a las planillas y su transcripción favorece errores de tipeo, duplicaciones y cargas incorrectas |
| 6 | 🚫 **Ausencia de validaciones automáticas** | No se impide registrar una falta duplicada, cargar asistencia de un alumno no inscripto ni controlar límites de cursada (por ejemplo, el máximo de intensificaciones) |
| 7 | 📊 **Imposibilidad de generar reportes de forma inmediata** | Cualquier métrica —cantidad de estudiantes con todas sus materias regularizadas o con materias pendientes— exige un conteo manual |
| 8 | 🗂️ **Dependencia total de la documentación física** | Toda la gestión administrativa queda condicionada a la disponibilidad y al estado de las planillas en papel |
| 9 | 🔓 **Ausencia de control de acceso por perfil** | Las planillas no distinguen quién puede cargar, consultar o corregir datos, sin registro de qué acción realizó cada usuario |
| 10 | 📵 **Imposibilidad de operar desde el aula** | El registro solo puede efectuarse donde está la documentación física, con la clase detenida mientras el docente completa la planilla |
| 11 | 📡 **Sin visibilidad en tiempo real** | Preceptoría no puede seguir la asistencia mientras la clase transcurre ni corregirla al momento |

---

<a id="s3"></a>

## 3. Solución tecnológica propuesta

Se propone el desarrollo del **Sistema Integral de Asistencia (SIA)**: una solución formada por
**tres módulos interdependientes** que reemplazan el registro en papel y las planillas descentralizadas
por una única plataforma digital.

### 3.1 Los tres módulos

| # | Módulo | Stack | Función |
|---|---|---|---|
| 1 | 🖥️ **Aplicativo de escritorio** | C# · .NET Framework · Windows Forms | Módulo administrativo para la gestión de alumnos, profesores, preceptores, especialidades, materias, dictados, inscripciones y usuarios, con control de permisos por rol y autenticación de usuarios |
| 2 | 🔌 **API REST intermedia** | Node.js · Express | Capa de servicios que actúa como puerta de acceso único a la base de datos, con autenticación por token (Bearer) y contraseñas cifradas con bcrypt |
| 3 | 📱 **Aplicación móvil** | React Native · Expo | Módulo operativo que permite registrar la asistencia en el aula desde el celular, mediante escaneo de código QR o ingreso de un código corto generado por el docente al abrir la clase |

### 3.2 Infraestructura y reglas de negocio

El sistema se apoya sobre una infraestructura centralizada y segura:

- 🗄️ **Una única base de datos MySQL centralizada** (`gestion_asistencia_eest`) actúa como fuente única
  de verdad, eliminando la duplicación de planillas, con consulta de historial por alumno y generación
  de reportes en línea.
- 📡 **La app móvil actualiza la asistencia en tiempo real mediante eventos (SSE)**, de modo que
  preceptoría puede observar y corregir la asistencia mientras la clase transcurre, y el alumno registra
  su presencia en segundos sin detener la clase.
- 🧱 **La estructura de cursadas personalizadas** (recursado, intensificación, cursadas simultáneas de
  distintos años) queda modelada en la base, permitiendo cargar y consultar listados siempre *"desde el
  estudiante"*, conforme a la regla institucional relevada.
- 🛡️ **El control de acceso por rol** —administrador, directivo, preceptor, profesor y alumno— limita
  visibilidad y acciones según el perfil, y resuelve la necesidad de saber quién registró y corrigió
  cada dato.
- 📏 **El registro de clases sin docente ausente** y demás reglas institucionales de asistencia se
  incorporan como **reglas de negocio** del sistema.

### 3.3 Cobertura del circuito completo

Con esta solución, el SIA cubre el circuito completo de la información de asistencia:

```text
Carga de datos institucionales
        ↓
Apertura de clase
        ↓
Registro de asistencia en el aula
        ↓
Corrección por preceptoría
        ↓
Consulta y reportes
```

Digitalizando en su totalidad el proceso que hoy se lleva a cabo en papel.

---

## 🔗 Documentos relacionados

| Documento | Contenido |
|---|---|
| 📖 [`README.md`](../README.md) | Visión general del proyecto: alcance, tecnologías, instalación y uso |
| 📖 [`context.md`](../context.md) | Contexto técnico: arquitectura, cómo levantar cada módulo y mapa de archivos |
| 📖 [`AGENTS.md`](../AGENTS.md) | Reglas de trabajo: identidad por rama, Git, credenciales y estilo |

---

<p align="center">
Documento en construcción · se completará con los capítulos restantes<br>
Hecho con 💚 para la E.E.S.T. N° 1 · EACP 2026
</p>
