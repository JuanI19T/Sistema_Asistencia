<div align="center">

# Documentación Técnica y Análisis de Requerimientos

## Sistema Integral de Asistencia Escolar (SIA)

**Proyecto Final de Programación**
**Evaluación Anual de Capacidades Profesionales (EACP)**

| | |
|---|---|
| **Docente a cargo** | Guillermo Munafo |
| **Materia** | Proyecto de Implementación de Sitios Web Dinámicos |
| **Ciclo lectivo** | 2026 |

</div>

---

## Índice

| Sección | Título | Contenido |
|:---:|---|---|
| 1 | [Datos generales del proyecto](#s1) | Identificación del proyecto, cliente, equipo y repositorio |
| 2 | [Descripción del problema real](#s2) | Situación actual e ineficiencias de la gestión en papel |
| 3 | [Solución tecnológica propuesta](#s3) | Módulos del sistema, infraestructura y circuito cubierto |
| 4 | [Objetivos](#s4) | Objetivo general y objetivos específicos |
| 5 | [Marco teórico](#s5) | Conceptos fundamentales, justificación y criterios de selección |

---

<a id="s1"></a>

## 1. Datos generales del proyecto

### 1.1 Identificación

| Campo | Detalle |
|---|---|
| Nombre del Sistema / Proyecto | Sistema Integral de Asistencia Escolar (SIA) |
| Cliente / Organización | Escuela de Educación Secundaria Técnica N° 1 "Ara. General Belgrano" (E.E.S.T. N° 1) |
| Integrantes del grupo | Isabella Lara Carrete Vecco y Juan Ignacio Torres Troschasky |
| Materia | Proyecto de Implementación de Sitios Web Dinámicos |
| Docente a cargo | Guillermo Munafo |
| Repositorio de GitHub | <https://github.com/JuanI19T/Sistema_Asistencia> |

### 1.2 Presentación

El proyecto SIA no se limita a reemplazar un formulario en papel: **cubre el circuito completo** de la
información de asistencia, desde la carga de datos institucionales (alumnos, profesores, materias,
dictados, inscripciones) hasta el registro de asistencia en el aula y su posterior consulta. Por ello
se denomina *integral*.

El cliente es la E.E.S.T. N° 1, una institución secundaria técnica de la Provincia de Buenos Aires con
dirección, secretaria, docentes, preceptores y alumnos. El sistema se desarrolló como Evaluación Anual
de Capacidades Profesionales (EACP) de la carrera de Analista en Sistemas.

El trabajo fue realizado por dos integrantes, cada uno con su propia rama en el repositorio
(`Isa-Vecco` y `Juan-Torres`), e integrando a `main` únicamente mediante Pull Request para garantizar
la trazabilidad individual. El repositorio fue creado el **[FECHA PENDIENTE]**.

La materia resulta pertinente porque el valor del sistema se concentra en sus componentes web: una
API REST con autenticación por token y emisión de eventos en tiempo real (SSE), consumida por una
aplicación móvil.

---

<a id="s2"></a>

## 2. Descripción del problema real

### 2.1 Situación actual

Actualmente la Escuela de Educación Secundaria Técnica N° 1 "Ara. General Belgrano" lleva a cabo la
gestión de sus operaciones operativas y administrativas de manera **manual**, utilizando registros en
formato papel y planillas de cálculo descentralizadas.

El control diario de asistencia se realiza mayormente sobre libros y planillas físicas: cada docente
registra las presencias e inasistencias de su curso en papel y, al cierre del período, esos datos deben
transcribirse a formularios y listados administrativos. Los listados de alumnos, materias y cursadas se
mantienen en planillas de cálculo individuales, **sin una versión única compartida** entre dirección,
preceptoría y docentes.

Según el relevamiento institucional realizado con el equipo directivo (Acta del 29/04/2026), la
complejidad real es mayor que un simple listado de faltas: **las trayectorias escolares son altamente
personalizadas.** Un mismo estudiante puede cursar materias de distintos años en simultáneo, recursar
únicamente las materias que adeuda e intensificar otras, por lo que cada alumno presenta un conjunto de
cursadas diferente. Sobre esa estructura se construyen, además, las planillas de asistencia, cuyo
punto de partida es siempre el estudiante.

Con una base de datos en papel y planillas aisladas, la institución no cuenta con información
confiable, centralizada ni consultable en tiempo real para tomar decisiones sobre matrícula,
trayectorias ni regularidad.

### 2.2 Ineficiencias operativas destacables

Esta metodología genera las siguientes ineficiencias operativas:

| N° | Ineficiencia | Descripción |
|:---:|---|---|
| 1 | Riesgo de pérdida y deterioro de la información | Los registros en soporte papel pueden extraviarse, destruirse o volverse ilegibles, sin que exista copia de respaldo. |
| 2 | Información descentralizada e inconsistente | Al convivir varias planillas de cálculo sin versión única, un mismo alumno o dictado puede estar cargado de forma distinta, o duplicada, en cada archivo. |
| 3 | Dificultad para consultar el historial de asistencia | Rastrear las inasistencias de un alumno a lo largo del ciclo implica revisar planillas físicas una por una. |
| 4 | Procesos administrativos lentos | La búsqueda de un estudiante, el armado de listados y el cierre de planillas se realizan de forma manual, con demoras y trabajo repetitivo. |
| 5 | Errores humanos en la carga de datos | El paso del papel a las planillas y su posterior transcripción favorece errores de tipeo, duplicaciones y cargas incorrectas. |
| 6 | Ausencia de validaciones automáticas | El sistema actual no impide registrar una falta duplicada, cargar asistencia de un alumno no inscripto ni controlar límites de cursada, por ejemplo el máximo de intensificaciones. |
| 7 | Imposibilidad de generar reportes de forma inmediata | Cualquier métrica, como la cantidad de estudiantes con todas sus materias regularizadas o con materias pendientes, exige un conteo manual. |
| 8 | Dependencia total de la documentación física | Toda la gestión administrativa queda condicionada a la disponibilidad y al estado de las planillas en papel. |
| 9 | Ausencia de control de acceso por perfil | Las planillas no distinguen quién puede cargar, consultar o corregir datos, sin registro de qué acción realizó cada usuario. |
| 10 | Imposibilidad de operar desde el aula | El registro de asistencia solo puede efectuarse donde está la documentación física, con la clase detenida mientras el docente completa la planilla. |
| 11 | Sin visibilidad en tiempo real | Preceptoría no puede ir siguiendo la asistencia mientras la clase transcurre ni corregirla al momento. |

---

<a id="s3"></a>

## 3. Solución tecnológica propuesta

Se propone el desarrollo del Sistema Integral de Asistencia Escolar (SIA): una solución formada por
**tres módulos interdependientes** que reemplazan el registro en papel y las planillas descentralizadas
por una única plataforma digital.

### 3.1 Módulos del sistema

| N° | Módulo | Tecnología | Función |
|:---:|---|---|---|
| 1 | Aplicativo de escritorio | C# · .NET Framework · Windows Forms | Módulo administrativo para la gestión de alumnos, profesores, preceptores, especialidades, materias, dictados, inscripciones y usuarios, con control de permisos por rol y autenticación de usuarios. |
| 2 | API REST intermedia | Node.js · Express | Capa de servicios que actúa como puerta de acceso único a la base de datos, con autenticación por token (Bearer) y contraseñas cifradas con bcrypt. |
| 3 | Aplicación móvil | React Native · Expo | Módulo operativo que permite registrar la asistencia en el aula desde el celular, mediante escaneo de código QR o ingreso de un código corto generado por el docente al abrir la clase. |

### 3.2 Infraestructura y reglas de negocio

El sistema se apoya sobre una infraestructura centralizada y segura:

1. **Base de datos centralizada.** Una única base MySQL (`gestion_asistencia_eest`) actúa como fuente
   única de verdad, eliminando la duplicación de planillas, con consulta de historial por alumno y
   generación de reportes en línea.
2. **Actualización en tiempo real.** La aplicación móvil actualiza la asistencia mediante eventos
   (SSE), de modo que preceptoría puede observar y corregir la asistencia mientras la clase transcurre,
   y el alumno registra su presencia en segundos sin detener la clase.
3. **Trayectorias escolares modeladas.** La estructura de cursadas personalizadas (recursado,
   intensificación, cursadas simultáneas de distintos años) queda modelada en la base, permitiendo
   cargar y consultar listados siempre desde el estudiante, conforme a la regla institucional relevada.
4. **Control de acceso por rol.** Los perfiles administrador, directivo, preceptor, profesor y alumno
   limitan la visibilidad y las acciones disponibles, y resuelven la necesidad de saber quién registró y
   corrigió cada dato.
5. **Reglas institucionales incorporadas.** El registro de clases sin docente ausente y las demás
   reglas de asistencia se incorporan como reglas de negocio del sistema.

### 3.3 Cobertura del circuito completo

Con esta solución, el SIA cubre el circuito completo de la información de asistencia:

| N° | Etapa del circuito | Descripción |
|:---:|---|---|
| 1 | Carga de datos institucionales | Alta y administración de alumnos, profesores, materias, dictados e inscripciones. |
| 2 | Apertura de clase | El docente abre la clase y el sistema genera el token QR y el código corto. |
| 3 | Registro de asistencia en el aula | El alumno registra su presencia desde el dispositivo móvil. |
| 4 | Corrección por preceptoría | Preceptoría revisa y corrige los registros en tiempo real. |
| 5 | Consulta y reportes | La información se consulta por alumno y se consolidan reportes. |

El proceso que hoy se lleva a cabo en papel queda digitalizado en su totalidad.

---

<a id="s4"></a>

## 4. Objetivos

### 4.1 Objetivo general

Desarrollar e implementar el Sistema Integral de Asistencia Escolar (SIA), una plataforma digital única
que digitalice el circuito completo del control de asistencia de la E.E.S.T. N° 1, desde la carga y
administración de los datos institucionales hasta el registro diario de asistencias en el aula y su
posterior consulta, reemplazando el registro manual en papel y las planillas de cálculo
descentralizadas por una solución centralizada, segura y operativa en tiempo real.

### 4.2 Objetivos específicos

| N° | Objetivo |
|:---:|---|
| 1 | Centralizar la información institucional en una única base de datos que actúe como fuente de verdad, eliminando la duplicación e inconsistencia de las planillas descentralizadas. |
| 2 | Digitalizar la administración institucional mediante un aplicativo de escritorio que permita el alta, baja y modificación de alumnos, profesores, preceptores, especialidades, materias, cursos, dictados e inscripciones. |
| 3 | Modelar las trayectorias escolares personalizadas de los estudiantes, permitiendo cursadas simultáneas de materias de distintos años, recursado e intensificación conforme a las reglas institucionales relevadas. |
| 4 | Garantizar la seguridad de acceso con un sistema de autenticación de usuarios y un control de permisos por rol (administrador, directivo, preceptor, profesor y alumno) que limite las acciones de cada perfil. |
| 5 | Agilizar el registro de asistencia en el aula mediante una aplicación móvil con escaneo de código QR o ingreso de código corto, de modo que el alumno registre su presencia en segundos sin interrumpir la clase. |
| 6 | Brindar visibilidad en tiempo real del estado de asistencia de una clase a través de la transmisión de eventos, permitiendo que preceptoría observe y corrija los registros mientras la clase transcurre. |
| 7 | Facilitar la consulta de información histórica, permitiendo acceder al historial de asistencia por alumno de forma inmediata, sin depender de la revisión manual de documentación física. |
| 8 | Reducir los errores de carga de datos incorporando validaciones automáticas en cada registro — asistencia de alumnos inscriptos en el dictado, apertura y estados de clase, y reglas de asistencia vigentes — que impidan cargas duplicadas o incorrectas. |
| 9 | Proteger la información sensible gestionando las credenciales de conexión fuera del código fuente y aplicando cifrado (bcrypt) sobre las contraseñas almacenadas. |
| 10 | Garantizar la extensibilidad del sistema mediante una arquitectura de servicios (API REST) separada de los clientes, que permita sumar nuevas funcionalidades en el futuro sin reescribir las aplicaciones existentes. |

---

<a id="s5"></a>

## 5. Marco teórico

### 5.1 Conceptos fundamentales

**1. Sistema de información.** Un sistema de información es el conjunto de componentes
interrelacionados que recolectan, procesan, almacenan y distribuyen información para apoyar la toma de
decisiones y el control de una organización. En este proyecto, el sistema abandona el soporte físico de
registro (papel y planillas) y pasa a una estructura digital centralizada, en la que los datos se
capturan una sola vez, se validan al ingresar y se consultan de inmediato. El SIA se organiza, en
consecuencia, como un sistema de información integral, que cubre desde la captura de datos en el aula
hasta la gestión administrativa institucional.

**2. Base de datos y modelo Entidad-Relación (DER).** La base de datos es el repositorio centralizado
donde se persiste la información del dominio. Este proyecto emplea dos tipos de bases según su
naturaleza:

- *Base de datos relacional:* organiza los datos en tablas vinculadas mediante relaciones, garantizando
  la integridad referencial mediante claves primarias y foráneas, y se consulta con SQL. Es la base
  sobre la que se construye el modelo Entidad-Relación, en el que cada concepto real del dominio
  (alumno, profesor, preceptor, materia, especialidad, dictado, clase, asistencia, usuario) es una
  entidad con atributos y relaciones explícitas.
- *Base de datos documental:* almacena datos en documentos independientes, lo que la hace adecuada
  para perfiles de información que no responden a un esquema rígido, como los usuarios y su
  autenticación.

La necesidad de modelar trayectorias escolares personalizadas (cursadas simultáneas de materias de
distintos años, recursado, intensificación) es lo que justifica una base relacional: los vínculos de
tipo alumno-materia, alumno-dictado y alumno-asistencia exigen integridad y consultas complejas que una
planilla de cálculo no puede garantizar.

**3. Arquitectura en capas (Modelo-Vista-Controlador).** La arquitectura MVC separa la aplicación en
tres responsabilidades:

- *Modelo:* la representación de los datos y la lógica de acceso a la base de datos (entidades, DAO y
  conexión).
- *Vista:* la interfaz con la que interactúa el usuario.
- *Controlador:* la lógica que coordina y orquesta las operaciones entre ambos.

Esta separación favorece el mantenimiento, la prueba y la trazabilidad de cada módulo. El aplicativo de
escritorio implementa esta arquitectura en capas, de modo que toda futura integración con servicios
externos debe residir únicamente en el Modelo, preservando la independencia de la interfaz y de la
lógica de control.

**4. Arquitectura cliente-servidor y servicios web (API REST).** La arquitectura cliente-servidor
distribuye la aplicación entre clientes que solicitan recursos y servidores que los ofrecen. Sobre este
modelo, el proyecto incorpora una API REST, un servicio web que expone los recursos del sistema a
través de operaciones HTTP (GET, POST, PATCH), con respuestas en formato JSON.

La API actúa como puerta de acceso única a la base de datos para la aplicación móvil: los clientes
móviles nunca acceden directamente a la base, sino a través de la interfaz pública de servicios. Esto
permite que una misma lógica de negocio se comparta entre distintos clientes y que el escritorio
conserve su acceso directo sin romper el contrato de datos.

**5. Autenticación y control de acceso.** La autenticación verifica la identidad de quien intenta
ingresar al sistema; el control de acceso por roles determina qué acciones puede realizar cada usuario
luego de autenticarse. En el proyecto:

- Las contraseñas nunca se almacenan en texto plano, sino cifradas con un algoritmo de hashing
  (bcrypt), que produce un valor irreversible e incluye un factor de complejidad configurable.
- El ingreso desde la API está protegido con tokens de seguridad, credenciales temporales y firmadas que
  el cliente móvil envía en cada solicitud y que el servidor verifica, lo que permite administrar la
  sesión sin mantener estado en el servidor.

El modelo de roles del sistema (administrador, directivo, preceptor, profesor y alumno) traduce el
organigrama institucional en permisos concretos de lectura y escritura.

**6. Eventos en tiempo real (Server-Sent Events).** Los Server-Sent Events (SSE) permiten que el
servidor envíe datos al cliente de forma asíncrona y unidireccional sobre una conexión HTTP persistente.
A diferencia de la consulta periódica en la que el cliente pregunta por novedades, SSE invierte el
flujo: cuando un alumno registra su presencia, la API empuja ese cambio a los clientes suscriptos y
preceptoría ve la asistencia actualizarse en vivo, sin recargar ni volver a consultar.

**7. Código QR y captura en el aula.** El código QR es una matriz de puntos que codifica información
legible por una cámara. En el sistema, cada clase abierta por un docente genera un token QR y un código
corto alternativo; el alumno lo escanea desde su celular y su asistencia queda registrada en segundos,
eliminando la necesidad de que la clase se detenga mientras el docente completa una planilla en papel.

**8. Aplicación móvil multiplataforma.** La aplicación móvil es un cliente del sistema que se ejecuta
en teléfonos celulares. Este proyecto utiliza React Native sobre Expo, un marco que permite desarrollar
una única base de código en JavaScript/TypeScript y ejecutarla en múltiples plataformas (Android e
iOS). Expo simplifica el acceso al hardware del dispositivo (cámara, red) y acelera el ciclo de
desarrollo y prueba sobre dispositivos físicos.

**9. Conceptos de dominio: dictado, inscripción y trayectoria escolar.** Para el sistema, el dictado es
la unidad que vincula una materia con un profesor y un preceptor en un ciclo lectivo; la inscripción
asigna un alumno a ese dictado; y la trayectoria escolar describe el recorrido individual de un
estudiante a lo largo de su cursada. Como la institución no agrupa a todos los alumnos de un año bajo
una única estructura rígida, sino que cada estudiante puede cursar materias de distintos años en
simultáneo, el modelo de datos no parte del curso, sino del estudiante y sus trayectorias; la
asistencia se registra siempre en el contexto de un dictado y de una clase concreta.

### 5.2 Justificación de tecnologías

| Tecnología | Función en el proyecto | Justificación |
|---|---|---|
| C# con .NET Framework 4.7.2 | Lenguaje del aplicativo de escritorio | Maduro, tipado y con amplio soporte para aplicaciones Windows; permite construir el módulo administrativo con control total sobre la lógica de negocio y la integración con bases de datos. |
| Windows Forms + MaterialSkin.2 | Interfaz gráfica del escritorio | Framework nativo de Windows para interfaces de escritorio; MaterialSkin agrega un aspecto visual moderno y uniforme con bajo costo de desarrollo. |
| MySQL | Base de datos relacional central | Gratuito, confiable y ampliamente difundido; soporta integridad referencial, consultas SQL complejas y despliegue en la nube, condiciones necesarias para modelar dictados, inscripciones y trayectorias. |
| MongoDB Atlas | Base documental para usuarios del escritorio | Almacenamiento en la nube y flexibilidad de esquema para el perfil de usuarios y su autenticación, independiente del esquema relacional de negocio. |
| Node.js + Express 5 | API REST intermedia | Ecosistema liviano y asincrónico, ideal para servicios web de alto volumen de solicitudes concurrentes, como múltiples alumnos registrando asistencia en simultáneo. |
| mysql2 | Conector de la API con MySQL | Conexión eficiente mediante pool de conexiones, reutilizable y segura. |
| bcrypt / bcryptjs | Hash de contraseñas | Algoritmo de hash lento por diseño, que dificulta ataques de fuerza bruta; interoperable entre el escritorio y la API para validar la misma contraseña desde ambos módulos. |
| Token de sesión (Bearer) | Autenticación de la app contra la API | Permite sesiones sin estado en el servidor, portables y verificables, ideales para un cliente móvil que se reconecta. |
| Server-Sent Events | Actualización en tiempo real de la asistencia | Más simple que la comunicación bidireccional para el caso de uso (difusión de cambios de asistencia hacia los suscriptos), con reconexión automática y compatibilidad nativa HTTP. |
| React Native + Expo SDK | Aplicación móvil multiplataforma | Código único multiplataforma, desarrollo ágil sobre dispositivo físico (Expo Go) y acceso directo a la cámara para el escaneo de QR. |
| expo-camera | Escaneo de código QR | API oficial de Expo para captura de imágenes y decodificación de códigos, sin módulos nativos adicionales. |
| Entorno de variables (.env) | Gestión de credenciales | Externaliza las cadenas de conexión fuera del código fuente, evitando que secretos queden versionados en el repositorio. |
| Git y GitHub (flujo de ramas + Pull Request) | Control de versiones y trazabilidad | Registra el historial completo, permite el trabajo colaborativo en ramas individuales y audita la autoría de cada cambio antes de integrarse a la rama principal. |

### 5.3 Criterios generales de selección

Las tecnologías fueron elegidas priorizando los siguientes criterios:

1. **Costo cero.** Todas las herramientas seleccionadas son de uso gratuito, por licencia de software
   libre o ediciones comunitarias, condición relevante para un proyecto académico sin presupuesto.
2. **Compatibilidad con el ambiente institucional.** El módulo administrativo corre sobre el sistema
   operativo Windows ya presente en la institución.
3. **Complejidad adecuada al problema.** Cada tecnología resuelve una necesidad concreta detectada en
   el relevamiento, sin agregar complejidad innecesaria; por ejemplo, SSE en lugar de una comunicación
   bidireccional completa.
4. **Extensibilidad.** La separación entre escritorio, API y aplicación móvil permite sumar nuevos
   clientes o funcionalidades a futuro sin reescribir el sistema.
5. **Seguridad por diseño.** Cifrado de contraseñas, tokens de sesión, permisos por rol y credenciales
   fuera del repositorio.

---

## Documentos relacionados

| Documento | Contenido |
|---|---|
| [`README.md`](../README.md) | Visión general del proyecto: alcance, tecnologías, instalación y uso |
| [`context.md`](../context.md) | Contexto técnico: arquitectura, módulos, base de datos y mapa de archivos |
| [`AGENTS.md`](../AGENTS.md) | Reglas de trabajo: identidad por rama, Git, credenciales y estilo |

---

<div align="center">

*Documento en construcción. Los capítulos restantes se incorporarán en versiones posteriores.*

Sistema Integral de Asistencia Escolar · EACP 2026

</div>
