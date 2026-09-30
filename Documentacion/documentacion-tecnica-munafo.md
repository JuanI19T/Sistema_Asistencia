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
| 2 | [Descripción del problema real](#s2) | Situación actual, ineficiencias y solución tecnológica propuesta |
| 3 | [Objetivos](#s3) | Objetivo general y objetivos específicos |
| 4 | [Marco teórico](#s4) | Conceptos fundamentales, justificación y criterios de selección |
| 5 | [Alcance y límites](#s5) | Funcionalidades incluidas y funcionalidades excluidas |
| 6 | [Impacto y beneficios esperados](#s6) | Impacto operativo e institucional, y factibilidad |
| 7 | [Perfiles de usuario y roles](#s7) | Perfiles, matriz de permisos y reglas de interacción |
| 8 | [Metodología de trabajo y trazabilidad](#s8) | Organización por módulos, ciclo de desarrollo y control de versiones |

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
la trazabilidad individual. El repositorio fue creado el **15/09/2026**.

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

### 2.3 Solución tecnológica propuesta

Se propone el desarrollo del Sistema Integral de Asistencia Escolar (SIA): una solución formada por
**tres módulos interdependientes** que reemplazan el registro en papel y las planillas descentralizadas
por una única plataforma digital.

#### 2.3.1 Módulos del sistema

| N° | Módulo | Tecnología | Función |
|:---:|---|---|---|
| 1 | Aplicativo de escritorio | C# · .NET Framework · Windows Forms | Módulo administrativo para la gestión de alumnos, profesores, preceptores, especialidades, materias, dictados, inscripciones y usuarios, con control de permisos por rol y autenticación de usuarios. |
| 2 | API REST intermedia | Node.js · Express | Capa de servicios que actúa como puerta de acceso único a la base de datos, con autenticación por token (Bearer) y contraseñas cifradas con bcrypt. |
| 3 | Aplicación móvil | React Native · Expo | Módulo operativo que permite registrar la asistencia en el aula desde el celular, mediante escaneo de código QR o ingreso de un código corto generado por el docente al abrir la clase. |

#### 2.3.2 Infraestructura y reglas de negocio

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

#### 2.3.3 Cobertura del circuito completo

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

<a id="s3"></a>

## 3. Objetivos

### 3.1 Objetivo general

Desarrollar e implementar el Sistema Integral de Asistencia Escolar (SIA), una plataforma digital única
que digitalice el circuito completo del control de asistencia de la E.E.S.T. N° 1, desde la carga y
administración de los datos institucionales hasta el registro diario de asistencias en el aula y su
posterior consulta, reemplazando el registro manual en papel y las planillas de cálculo
descentralizadas por una solución centralizada, segura y operativa en tiempo real.

### 3.2 Objetivos específicos

| N° | Objetivo |
|:---:|---|
| 1 | Centralizar la información institucional en una única base de datos que actúe como fuente de verdad, eliminando la duplicación e inconsistencia de las planillas descentralizadas. |
| 2 | Digitalizar la administración institucional mediante un aplicativo de escritorio que permita el alta, baja y modificación de alumnos, profesores, preceptores, especialidades, materias, cursos, dictados e inscripciones. |
| 3 | Modelar las trayectorias escolares personalizadas de los estudiantes, permitiendo cursadas simultáneas de materias de distintos años, recursado e intensificación conforme a las reglas institucionales relevadas. |
| 4 | Garantizar la seguridad de acceso con un sistema de autenticación de usuarios y un control de permisos por rol (administrador, directivo, secretario, preceptor, profesor y alumno) que limite las acciones de cada perfil. |
| 5 | Agilizar el registro de asistencia en el aula mediante una aplicación móvil con escaneo de código QR o ingreso de código corto, de modo que el alumno registre su presencia en segundos sin interrumpir la clase. |
| 6 | Brindar visibilidad en tiempo real del estado de asistencia de una clase a través de la transmisión de eventos, permitiendo que preceptoría observe y corrija los registros mientras la clase transcurre. |
| 7 | Facilitar la consulta de información histórica, permitiendo acceder al historial de asistencia por alumno de forma inmediata, sin depender de la revisión manual de documentación física. |
| 8 | Reducir los errores de carga de datos incorporando validaciones automáticas en cada registro — asistencia de alumnos inscriptos en el dictado, apertura y estados de clase, y reglas de asistencia vigentes — que impidan cargas duplicadas o incorrectas. |
| 9 | Proteger la información sensible gestionando las credenciales de conexión fuera del código fuente y aplicando cifrado (bcrypt) sobre las contraseñas almacenadas. |
| 10 | Garantizar la extensibilidad del sistema mediante una arquitectura de servicios (API REST) separada de los clientes, que permita sumar nuevas funcionalidades en el futuro sin reescribir las aplicaciones existentes. |

---

<a id="s4"></a>

## 4. Marco teórico

### 4.1 Conceptos fundamentales

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

### 4.2 Justificación de tecnologías

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

### 4.3 Criterios generales de selección

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

<a id="s5"></a>

## 5. Alcance y límites

### 5.1 Alcance

El Sistema Integral de Asistencia Escolar (SIA) comprende el desarrollo e implementación de tres módulos
que cubren el circuito completo del control de asistencia de la institución.

#### 5.1.1 Módulo de Escritorio (administración)

| N° | Funcionalidad | Descripción |
|:---:|---|---|
| 1 | Autenticación de usuarios | Ingreso con usuario y contraseña, control de sesión y creación del primer Administrador cuando no existen usuarios en el sistema. |
| 2 | Gestión de usuarios y perfiles | Alta, baja, modificación y activación o desactivación de usuarios, con contraseñas cifradas. |
| 3 | Administración de alumnos, profesores y preceptores | Alta, baja, modificación y listados. |
| 4 | Administración de especialidades y materias | Catálogo curricular con asignación de especialidad. |
| 5 | Gestión de dictados | Asignación de materia, profesor y preceptor, con sus fechas. |
| 6 | Gestión de inscripciones | Inscripción de alumnos a dictados. |
| 7 | Control de permisos por rol | El menú se filtra según el perfil del usuario: administrador, directivo, preceptor y profesor. |

#### 5.1.2 Módulo de API REST (intermediación)

| N° | Funcionalidad | Descripción |
|:---:|---|---|
| 1 | Autenticación por rol | Autenticación de preceptor, profesor y alumno mediante token de seguridad. |
| 2 | Exposición de datos | Alumnos, dictados, dictados por alumno y listados de inscriptos. |
| 3 | Gestión de clases | Apertura de clase por dictado, estados y consulta por fecha. |
| 4 | Registro de asistencias | Consulta, carga individual y carga por lote. |
| 5 | Generación y validación de tokens de clase | Emisión de código QR, escaneo y código corto de ingreso. |
| 6 | Difusión de eventos en tiempo real | Difusión de eventos hacia la aplicación móvil. |

#### 5.1.3 Módulo de Aplicación Móvil

| N° | Funcionalidad | Descripción |
|:---:|---|---|
| 1 | Login por rol | Autenticación de preceptor, profesor y alumno. |
| 2 | Perfil preceptor | Gestión de clases y seguimiento de asistencia en vivo. |
| 3 | Perfil profesor | Acceso a sus dictados, apertura de clase y generación del código QR. |
| 4 | Perfil alumno | Registro de su presencia escaneando el QR o ingresando el código curto. |

#### 5.1.4 Infraestructura

| N° | Componente | Descripción |
|:---:|---|---|
| 1 | Base de datos centralizada | MySQL (`gestion_asistencia_eest`) como única fuente de verdad, compartida por el escritorio y la API. |
| 2 | Base de datos documental | MongoDB Atlas para la autenticación de usuarios del escritorio. |
| 3 | Despliegue en la nube | Despliegue de la API y de la base de datos para su operación desde los dispositivos móviles. |

### 5.2 Límites / fuera de alcance

El sistema no contempla en esta etapa las siguientes funcionalidades:

| N° | Límite | Descripción |
|:---:|---|---|
| 1 | Gestión de calificaciones y evaluación | No se registran notas ni se maneja la promoción de materias. |
| 2 | Automatización de la trayectoria académica completa | Si bien el modelo soporta dictados e inscripciones personalizadas, la asignación automática de recursado e intensificación conforme a la regla institucional (máximo de cinco materias, priorización del recursado, límite por año) queda fuera de esta versión y se resuelve de forma asistida por el personal. |
| 3 | Módulo económico-financiero | No se gestionan pagos, cuotas ni contribuciones. |
| 4 | Gestión de legajos completos | El sistema administra los datos académicos relacionados con la asistencia, sin digitalizar documentación extraacadémica. |
| 5 | Justificación y bitácora avanzadas | La justificación de inasistencias y la bitácora de acciones figuran como proyección, no como entrega funcional completa. |
| 6 | Soporte sin conexión | El registro de asistencia requiere conexión con la API; no existe cola de carga offline. |
| 7 | Sincronización bidireccional en vivo entre escritorio y aplicación móvil | El escritorio escribe directamente en la base, por lo que sus cambios no se difunden en tiempo real a la aplicación móvil, que los ve al volver a consultar. La difusión en vivo opera solo sobre los cambios que registra la propia API. |
| 8 | Notificaciones automáticas | No se envían avisos por correo electrónico, WhatsApp ni otros canales de mensajería. |
| 9 | Versión web completa del escritorio | Las funcionalidades administrativas se ofrecen únicamente como aplicación de escritorio Windows. |
| 10 | Rol administrador en la API | El login del servicio contempla profesor, preceptor y alumno; la administración de usuarios se realiza exclusivamente desde el escritorio. |
| 11 | Copias de seguridad automáticas | La persistencia confía en la infraestructura de nube contratada, sin un esquema propio de respaldo automático. |

---

<a id="s6"></a>

## 6. Impacto y beneficios esperados

### 6.1 Impacto operativo

La implementación del SIA transforma la operatoria diaria de la institución en los siguientes aspectos:

| N° | Aspecto | Descripción |
|:---:|---|---|
| 1 | Registro de asistencia inmediato | El alumno marca su presencia escaneando el QR o ingresando el código corto desde su celular, sin interrumpir la clase mientras el docente completa una planilla. |
| 2 | Eliminación del doble registro | Se suprime la transcripción del papel a las planillas administrativas; el dato se carga una sola vez y queda disponible para todos los módulos. |
| 3 | Reducción de tiempos administrativos | Búsquedas de estudiantes, armado de listados y cierres de planilla que antes demandaban horas de trabajo manual se resuelven con consultas inmediatas. |
| 4 | Control y corrección en tiempo real | Preceptoría observa la asistencia mientras transcurre la clase y puede corregir registros al momento, en lugar de hacerlo días después sobre el papel. |
| 5 | Disminución de errores | Las validaciones automáticas (alumno inscripto, estados de clase, reglas de asistencia) evitan cargas duplicadas o incorrectas que el registro manual no podía detectar. |
| 6 | Disponibilidad permanente de la información | Los datos centralizados en la nube pueden consultarse desde cualquier equipo autorizado, sin depender de la ubicación física de la documentación. |
| 7 | Gestión de trayectorias personalizadas | El modelo de datos acompaña la realidad institucional de cursadas simultáneas, recurso e intensificación, ordenando información que en el papel resultaba inmanejable. |

### 6.2 Impacto social e institucional

| N° | Aspecto | Descripción |
|:---:|---|---|
| 1 | Modernización de la gestión escolar | La institución avanza hacia un modelo digital de administración, alineado con las prácticas actuales de informatización de las organizaciones educativas. |
| 2 | Valorización de la información institucional | Dirección y preceptoría pasan a contar con datos confiables y actualizados para conocer la situación de matrícula, la regularidad de los estudiantes y las materias pendientes. |
| 3 | Autonomía y participación del alumno | El estudiante deja de ser un mero objeto del registro y pasa a participar activamente de su propia asistencia, lo que refuerza hábitos de responsabilidad. |
| 4 | Reconocimiento del rol docente y de preceptoría | Las tareas de control y seguimiento se elevan de un trabajo manual repetitivo a una función de supervisión y gestión apoyada en datos. |
| 5 | Fortalecimiento del vínculo con la comunidad educativa | La disponibilidad de un sistema institucional propio constituye un activo académico de la escuela, fruto del trabajo conjunto entre el equipo directivo y el equipo de desarrollo. |
| 6 | Experiencia formativa para los alumnos desarrolladores | El proyecto involucra a los integrantes del grupo en un problema real, con usuarios reales y requisitos relevados, fortaleciendo sus capacidades profesionales. |

### 6.3 Factibilidad

El proyecto se considera factible desde sus tres dimensiones:

| Dimensión | Evaluación |
|---|---|
| Factibilidad técnica | Las tecnologías seleccionadas (C#/.NET, MySQL, Node.js/Express, React Native/Expo) son de acceso libre, maduras y de amplia documentación. El equipo ya cuenta con un prototipo funcional operativo: los tres módulos desarrollados, la base de datos restaurable mediante script y el despliegue en la nube funcionando, lo que confirma la viabilidad técnica de la solución. |
| Factibilidad operativa | La institución ya posee la estructura, el equipamiento (computadoras Windows y teléfonos inteligentes) y el personal con experiencia en el procedimiento de toma de asistencia. El sistema fue diseñado a partir del relevamiento directo con el equipo directivo, por lo que contempla las reglas y la metodología de trabajo reales de la escuela. La capacitación requerida para los usuarios es mínima, dado que las interfaces simplifican tareas que el personal ya realiza. |
| Factibilidad económica | Al tratarse de un proyecto académico, no existe costo de mano de obra de desarrollo. Todas las herramientas y plataformas utilizadas son gratuitas (ediciones comunitarias o de código abierto). El equipamiento ya existe en la institución y el costo futuro se limita al mantenimiento correctivo y evolutivo, estimado como bajo, y a la eventual contratación de los servicios en la nube ya utilizados en la etapa de desarrollo. |

---

<a id="s7"></a>

## 7. Perfiles de usuario y roles

El sistema distingue seis perfiles de usuario, cada uno con un alcance propio de visibilidad y de
acciones. Todos los usuarios y sus datos se almacenan en una base de datos no relacional (MongoDB), y
es esa estructura la que respalda la gestión de identidades, perfiles y sesiones del sistema.

### 7.1 Perfiles del sistema

| Rol | Descripción | Alcance general |
|---|---|---|
| Administrador | Responsable de la configuración global del sistema. | Acceso total: gestión de usuarios, perfiles, configuración y mantenimiento. |
| Directivo | Conducción institucional. | Acceso amplio: administra personal y realiza la mayor parte de las funciones del sistema. |
| Secretario | Soporte administrativo. | Carga y modifica datos de profesores, preceptores y alumnos; sin acceso a directivos ni a otros secretarios. |
| Preceptor | Seguimiento cotidiano de los cursos. | Ve datos de alumnos y profesores; crea clases y modifica determinada información, con limitaciones. |
| Profesor | Dictado de las materias. | Ve únicamente los alumnos inscriptos a sus clases y los datos de esas clases; no modifica datos. |
| Alumno | Participante de las clases. | Ve solo sus datos personales y la información de las clases a las que asiste; no modifica datos. |

### 7.2 Descripción de los roles

#### Administrador

Cuenta con acceso a todo. Administra los usuarios y perfiles del sistema, la configuración general y el
mantenimiento de la plataforma. Es el único rol, junto con el directivo, habilitado para modificar la
información personal de cualquier usuario.

#### Directivo

Posee acceso a casi todas las funciones del sistema. Su rol se centra en la conducción institucional:

- **Carga del personal:** registra y da de alta a profesores, secretarios, preceptores y alumnos.
- **Modificación de datos:** actualiza la información de los perfiles que dependen de su gestión,
  incluida la información personal, junto con el administrador.

#### Secretario

Cumple funciones administrativas de soporte:

- Puede cargar y modificar datos de profesores, preceptores y alumnos.
- No puede modificar los datos de los directivos ni los de otros secretarios.

#### Preceptor

Rol operativo de seguimiento cotidiano:

- Ve la información de los alumnos y de los profesores.
- Interactúa con los profesores para asignarles cursos, sin poder modificar sus datos.
- Con los alumnos sí puede modificar datos y realizar acciones de gestión: inscribirlos en clases y
  registrar su asistencia.
- Puede crear clases y modificar cierta información, siempre dentro de un alcance limitado según la
  complejidad de la operación.

#### Profesor

Rol de dictado de clases:

- Ve únicamente los alumnos inscriptos a sus clases y los datos de esas clases: dictados, fechas y
  estado.
- Ve además la información de los preceptores y alumnos con los que interactúa.
- No puede modificar nada del sistema.

#### Alumno

Rol de participación:

- Ve solamente sus datos personales y la información de las clases a las que asiste.
- Puede ver la información de los profesores y preceptores relacionados con sus clases.
- No puede modificar nada del sistema.

### 7.3 Matriz de permisos por rol

| Acción | Administrador | Directivo | Secretario | Preceptor | Profesor | Alumno |
|---|:---:|:---:|:---:|:---:|:---:|:---:|
| Acceso total al sistema | X | - | - | - | - | - |
| Cargar personal | X | X | P | - | - | - |
| Modificar datos de usuarios | X | X | P | P | - | - |
| Modificar información personal de cualquier usuario | X | X | - | - | - | - |
| Ver alumnos inscriptos a un curso o clase | X | X | X | X | X | - |
| Ver datos de profesores | X | X | X | X | X | S |
| Ver datos de alumnos | X | X | X | X | - | - |
| Inscribir alumnos a clases | X | X | X | X | - | - |
| Crear clases | X | X | X | X | - | - |
| Registrar asistencia | X | X | X | X | - | - |
| Ver datos de preceptores involucrados | X | X | X | X | X | S |
| Ver y consultar su historial de asistencia | X | X | X | X | - | R |
| Modificar sus propios datos | - | - | - | - | - | - |

| Símbolo | Significado |
|:---:|---|
| X | La acción está disponible. |
| P | La acción es parcial. |
| S | Solo puede consultar datos relacionados con sus acciones en el sistema. |
| R | Solo puede acceder a acciones relacionadas con sus datos. |

### 7.4 Reglas de gestión de la información personal

La información personal de cada usuario no puede ser modificada por el propio usuario, cualquiera sea su
rol. Ese tipo de modificación queda reservada exclusivamente a los roles de Directivo y Administrador.

Los demás roles (secretario y preceptor) pueden operar sobre los datos académicos y operativos
(inscripciones, asistencia, asignación de cursos) dentro de los límites de su perfil, pero no sobre la
información personal de identidad de los usuarios.

### 7.5 Resumen de la interacción entre roles

| Quién | Con quién | Qué puede hacer |
|---|---|---|
| Directivo | Profesores, secretarios, preceptores y alumnos | Cargarlos, modificarlos y gestionar sus datos. |
| Secretario | Profesores, preceptores y alumnos | Cargarlos y modificar sus datos, sin directivos ni otros secretarios. |
| Preceptor | Profesores | Ver su información e interactuar para asignarles cursos, sin modificar sus datos. |
| Preceptor | Alumnos | Modificar sus datos, inscribirlos en clases y registrarles asistencia. |
| Profesor | Alumnos y preceptores | Solo visualización de la información de quienes interactúan con su cursada. |
| Alumno | Profesores y preceptores | Solo visualización de la información relacionada con sus clases. |

---

<a id="s8"></a>

## 8. Metodología de trabajo y trazabilidad

### 8.1 Metodología de desarrollo

#### 8.1.1 Organización del trabajo por módulos

El proyecto se desarrolló dividiendo el sistema en tres partes interdependientes, cuyo orden responde a
una lógica de dependencia de datos:

| N° | Módulo | Descripción |
|:---:|---|---|
| 1 | Aplicativo de escritorio | Módulo administrativo donde se cargan y administran los datos institucionales (alumnos, profesores, preceptores, materias, dictados, inscripciones y usuarios). Constituye la primera etapa del desarrollo, ya que debía quedar bastante avanzado para poder continuar con el resto de los módulos: toda la información que estos consumen se ingresa a la base de datos a través de él. |
| 2 | Aplicación móvil | Módulo operativo de registro de asistencia, desarrollado en React Native. |
| 3 | Página o aplicación web | Módulo complementario, también desarrollado en React Native, que comparte con la aplicación móvil la dependencia de los datos cargados desde el escritorio. |

La aplicación móvil y la web dependen de los datos que se ingresan a la base de datos mediante el
aplicativo de escritorio: no existen por fuera de la información institucional registrada en el módulo
administrativo. Esta relación determinó el orden de construcción y definió que el escritorio fuera el
primer entregable funcional de la solución.

#### 8.1.2 Evolución de la gestión del trabajo

El proceso de desarrollo atravesó dos etapas de organización bien diferenciadas:

| Etapa | Descripción |
|---|---|
| 1. Gestión previa a GitHub (Drive y canal de comunicación Discord) | En un inicio, el equipo trabajó con la información del proyecto distribuida en carpetas compartidas de Google Drive, y el archivo del aplicativo de escritorio se compartía enviándolo por un canal de Discord dedicado al proyecto o por WhatsApp. Esta modalidad presentó un problema operativo recurrente: el archivo solía corromperse con frecuencia, lo que obligaba a reconstruir versiones, dificultaba saber cuál era la copia vigente y ponía en riesgo la continuidad del trabajo realizado. |
| 2. Migración a GitHub | Ante esas limitaciones, el equipo creó el repositorio de GitHub del proyecto. En su creación, uno de los integrantes se encargó de subir todo el material desarrollado hasta ese momento, consolidando el avance existente y estableciendo el punto de partida desde el cual continuar el desarrollo. A partir de esa migración, la información dejó de dispersarse entre Drive, Discord y WhatsApp, y pasó a convivir en un único repositorio con historial y control de versiones. |

#### 8.1.3 Ciclo de desarrollo

El ciclo adoptado combina planificación por etapas y desarrollo incremental:

1. **Relevamiento:** identificación de las necesidades institucionales junto al equipo directivo.
2. **Análisis y diseño:** definición de requerimientos, reglas de negocio y modelo de datos.
3. **Construcción por módulos:** desarrollo secuencial del escritorio primero y de la app y la web
   después, sobre los datos que aquel administra.
4. **Pruebas:** verificación funcional de cada módulo antes de integrarlo.
5. **Integración y revisiones:** protocolo de incorporación de cambios descrito en la sección 8.2.

### 8.2 Traceabilidad y GitHub

#### 8.2.1 Control de versiones

El repositorio de GitHub constituye el instrumento formal de trazabilidad del proyecto: conserva el
historial de cada cambio, acredita la autoría individual del trabajo y permite reconstruir el estado del
sistema en cualquier momento.

A la fecha del informe, el repositorio contiene más de 25 commits, la documentación del proyecto, el
script de definición de la base de datos y el código fuente de los módulos desarrollados.

#### 8.2.2 Flujo de trabajo con ramas

El equipo adoptó una estrategia de trabajo basada en ramas (branches):

| Elemento | Descripción |
|---|---|
| Rama principal (main) | Integra la versión estable del proyecto. No recibe commits directos. |
| Rama individual por integrante | Cada desarrollador trabaja en su propia rama asignada, con autonomía para avanzar sobre los módulos bajo su responsabilidad. |
| Integración mediante Pull Request | Cuando un integrante completa un cambio, pushea su rama y solicita su incorporación a la rama principal mediante un Pull Request. Este paso permite revisar la modificación antes de integrarla y registra formalmente qué trabajo aportó cada uno. |

Este flujo garantiza que:

- Cada integrante pueda desarrollar en paralelo sin pisar el trabajo del otro.
- Ningún cambio ingresado a la rama principal esté sin revisión.
- La autoría de cada funcionalidad sea auditable, dejando constancia de quién realizó cada cambio y
  cuándo.

#### 8.2.3 Conservación y seguridad del repositorio

Para proteger la continuidad del trabajo se definieron reglas que el equipo respeta en cada operación:

- **Los secretos no se versionan:** las credenciales de conexión se mantienen fuera del repositorio, en
  archivos de entorno ignorados, protegiendo la seguridad del sistema.
- **Prohibidos los borrados destructivos:** operaciones como el descarte forzado del historial o la
  limpieza de archivos sin confirmar están fuera de uso, para garantizar que ninguna acción accidental
  implique pérdida de datos.
- **Los archivos generados no se suben:** dependencias y artefactos de compilación permanecen fuera del
  control de versiones, manteniendo el repositorio liviano y confiable.

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
