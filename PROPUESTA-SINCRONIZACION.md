# Propuesta: conexión entre el aplicativo de escritorio y la app móvil

> Estado: propuesta para discusión. Fecha: 2026-09-25.

## 1. Estado actual (resumen de arquitectura)

| Componente | Stack | Datos | Cómo escribe |
|---|---|---|---|
| Escritorio (`SistemaAsistencia`) | .NET Framework 4.7.2, WinForms, MaterialSkin | MySQL `gestion_asistencia_eest` + **MongoDB (Atlas)** para la autenticación de usuario | Directo a MySQL desde las DAOs |
| API (`Api`) | Node.js / Express 5, mysql2, bcryptjs | MySQL (la misma) | Directo a MySQL |
| App móvil (`App`) | React Native (Expo) | MySQL a través de la API | Solo vía API (REST + SSE) |

- El MySQL local es la única fuente de verdad compartida: escritorio y API leen/escriben la misma base.
- La app recibe notificaciones en tiempo real vía **SSE** (`/api/eventos`), pero esos eventos solo los emite la **API**, y la API solo emite eventos por cambios que ella misma realiza (clases, asistencia).
- El escritorio **no tiene ninguna integración HTTP** hoy (no hay `HttpClient` en todo el proyecto) y **no escucha ni emite eventos**.
- Consecuencia directa del estado actual: **un cambio hecho desde el escritorio (ej.: cambiar el preceptor a cargo de un dictado) no llega a la app** hasta que esta vuelve a consultar (o re-loguea); y un cambio hecho desde la app no se refleja en el escritorio hasta que reconsulta o re-loguea.

## 2. Problemas detectados (detonantes de esta propuesta)

1. **Unidireccionalidad de eventos**: la SSE depende de que las escrituras pasen por la API. El escritorio escribe directo a MySQL → "eventos sordos".
2. **Falta de refresco automático**: en la app, la lista de dictados del profesor no se actualiza si no hay pantalla con clase seleccionada; y el escritorio no reconsulta nada en segundo plano.
3. **Desalineación de consultas** entre escritorio y API:
   - El escritorio usa `INNER JOIN PROFESOR` en `DictadoDAO`; la API usa `LEFT JOIN` (`Api/rutas/dictados.js`). Un dictado sin profesor queda oculto en escritorio pero visible en la app.
   - La API no filtra `activo` en listados (dictados, alumnos inscriptos), aunque ya lo hace en login.
   - `id_preceptor` existe en `dictado` pero la API no lo expone ni filtra por él.
4. **Identidad duplicada**: el escritorio autentica con MongoDB (PBKDF2, prefijo `pbkdf2$`, 10k iteraciones, `Rfc2898DeriveBytes`) mientras la API autentica con MySQL (`dni` + bcrypt). Comparten el MySQL recién cuando el ABM del escritorio carga `dni`/`contrasena` de `profesor`/`preceptor`. No hay un único "usuario" para ambos mundos, y no existe un rol administrador en la API.
5. **API con endpoints solo de lectura** para dictados (solo `GET`): no hay forma de que la API entere de una modificación de dictado, porque el endpoint ni siquiera existe para editarlo.

## 3. Alternativas para conectar escritorio y app

### Alternativa A — El escritorio consume la API (migración por pantallas)

El escritorio deja de escribir directo a MySQL y pasa sus operaciones a endpoints REST (métodos `POST/PATCH/PUT/DELETE` en la API). La API escribe en MySQL, genera evento SSE → la app se entera al instante. Para el camino inverso, la API provee un endpoint que el escritorio consulta o escucha.

Cómo mantener el MVC intacto:
- El HTTP vive **solo en la capa Modelo** (nuevo `DAO/RepositorioHttp` o `ClienteApi`). Vistas y Controladores no cambian su forma de llamar al modelo.
- Se puede ir **pantalla por pantalla**: un ABM nuevo lo hace por API, el resto sigue directo mientras se migra.

Ventajas:
- Lógica de negocio y validaciones centralizadas en un solo lugar.
- Eventos SSE en ambas direcciones.
- Seguridad/auditoría centrales; el escritorio se autentica con un token de servicio.
- Es el destino arquitectónico natural si el proyecto crece.

Desventajas / costos:
- El escritorio pasa a **depender de que la API esté levantada** (hoy es local; con servidor remoto requiere cola/retry si la red cae).
- Hay que **crear los endpoints que faltan** (dictados POST/PATCH, ABMs expuestos, sesión de servicio) con sus validaciones.
- Jacobo: el ABM más delicado (validaciones, duplicados, FK) hay que replicarlo/asegurarlo del lado de la API.
- Migración no trivial por el volumen de pantallas existentes.

**Veredicto**: meta a mediano plazo. No conviene como primer paso porque es la opción más cara y la única que cambia el modelo mental actual de los ABMs.

### Alternativa B — Tabla `outbox` + triggers en MySQL + worker que emite eventos (recomendada)

La base de datos ya es el bus compartido; aprovecharla para que **cualquier escritura** (de escritorio o de API) produzca un evento.

Mecánica:
1. Tabla nueva: `outbox_evento (id, entidad, operacion, clave, payload JSON, creado_en, procesado tinyint)`.
2. **Triggers** `AFTER INSERT/UPDATE/DELETE` sobre las tablas que interesan (`dictado`, `preceptor`, `profesor`, `alumno`, `materia`, `especialidad`, `inscribe`, `clase`, `asistencia`) que insertan una fila en `outbox_evento`.
3. Un **worker** (puede ser un proceso Node pequeño que ya vive en `Api`, p.ej. `worker/outbox.js`) lee filas `procesado=0`, las publica como **evento SSE** en la app (o las deja en una cola en memoria) y las marca como procesadas.
4. El escritorio **no necesita conocer nada**: su INSERT en MySQL ya generó el evento.

Ventajas:
- **Captura el 100% de las escrituras de ambos lados con cero cambios en el escritorio.**
- Transaccional: el trigger y el INSERT son atómicos (si el cambio se confirma, el evento existe).
- Audita cambios: el payload deja trazabilidad de quién/cuándo.
- La SSE sigue siendo emitida por la API → la app no cambia.
- El desktop puede además **leer** la outbox (o un endpoint "cambios desde <timestamp>") para enterarse de lo que cambió la app.

Desventajas / costos:
- Los triggers agregan escritura adicional por cada cambio (rendimiento menor en cargas altas; para una escuela, irrelevante).
- El payload lo arma el trigger (SQL) → poco flexible para objetos complejos; se puede mitigar dejando el payload mínimo (entidad, operación, id) y que el worker re-consulte el dato fresco.
- Hay que mantener los triggers al migrar el esquema (los scripts de migración deben crearlos).
- Si en el futuro todo pasa por la API (opción A), la outbox se vuelve redundante (aunque inofensiva).

**Veredicto**: la opción más equilibrada para el caso actual. Resuelve el problema real (cambios de escritorio visibles en la app) sin rehacer el escritorio.

### Alternativa C — Polling cruzado (fix rápido, sin eventos)

Sin cambiar la arquitectura, ambos lados reconsultarían periódicamente:
- En la app: refrescar listas con un intervalo suave (30–60 s) y al volver al foco / pull-to-refresh (React Native tiene `RefreshControl`).
- En el escritorio: un `System.Windows.Forms.Timer` que re-consulta los datos de la pantalla activa cada N segundos.

Ventajas:
- Muy simple, sin componentes nuevos, sin tocar la API.
- Cubre el caso concreto reportado (cambiar preceptor del dictado → profesor lo ve al refrescarse).

Desventajas:
- Latencia de decenas de segundos y tráfico de consultas repetidas.
- Es "casi-tiempo real": no hay push real; a medida que crezcan las pantallas, el Timer por pantalla es difícil de mantener.
- No resuelve el camino app→escritorio con la misma elegancia (requiere Timer en el escritorio igualmente).

**Veredicto**: buen parche de corto plazo para el caso más molesto ya reportado; no es la solución definitiva.

### Alternativa D — Broker pub/sub (Redis / MQTT / RabbitMQ / SignalR)

Un componente intermedio de mensajería: el escritorio publica cambios, y la API (o la app vía SSE/Socket) se suscribe.

Ventajas:
- Desacopla emisores y receptores; escala.
- SignalR daría "push" nativo al escritorio desde .NET.

Desventajas:
- **Introduce un componente nuevo (servidor de mensajería) que hoy no existe** en un entorno educativo local, y que debe administrarse (levantarlo, configurarlo, desplegarlo).
- Para el volumen de datos de una escuela es sobredimensionado.
- SignalR implicaría además decidir stack de hosting del escritorio.

**Veredicto**: descartada para hoy; se puede revisar si el proyecto crece a múltiples sedes o concurrencia alta.

### Alternativa E — El escritorio escribe "también" a la API (doble escritura, transaccional/complementaria)

El escritorio mantiene MySQL pero **además** notifica a la API de cada cambio (llamada HTTP no bloqueante). 

Ventajas:
- La app se entera por la SSE normal.
- No requiere triggers.

Desventajas:
- **Peligro de inconsistencia**: si la notificación falla o se pierde, el MySQL y los eventos divergen; hay que re-sincronizar.
- Hay que tocar todas las DAOs que escriben (mismo costo casi que la opción A pero con riesgo de duplicación de estado).
- Es la alternativa "peor de ambos mundos".

**Veredicto**: descartada por riesgo de inconsistencia silenciosa.

## 4. Comparativa rápida

| Criterio | A: escritorio→API | B: outbox+triggers | C: polling | D: broker | E: doble escritura |
|---|:-:|:-:|:-:|:-:|:-:|
| Costo de implementación | Alto | Medio-bajo | Muy bajo | Alto | Medio |
| Cambios en el escritorio | Muchos | **Ninguno** | Timer por pantalla | Conexión + publisher | Muchos |
| Tiempo real real (push) | Sí | Sí | No | Sí | Sí |
| Consistencia | Alta | Alta | Alta | Media | **Riesgosa** |
| Dependencia nueva | API on | Ninguna (triggers) | Ninguna | Broker | Ninguna |
| Requiere nueva infraestructura | No | No | No | **Sí** | No |

## 5. Recomendación (hoja de ruta por fases)

1. **Fase 0 — Ordenar el esquema (barato, desbloquea lo demás)**
   - Unificar `LEFT JOIN`/`INNER JOIN` en dictados entre escritorio y API.
   - Filtrar `activo` en todos los listados de la API (no solo login).
   - Exponer `id_preceptor` y el nombre del preceptor en `GET /api/dictados`.
   - Revisar qué pasa con inscriptos de alumnos dados de baja.

2. **Fase 1 — Quick win (sin arquitectura nueva)**
   - App: refresco suave (intervalo corto + `RefreshControl` + refresh al volver al foco) en `ProfesorPrincipal`.
   - Escritorio: `Timer` ligero en las pantallas que lo necesiten (FrmPrincipal / listados) por si la app cambió datos.

3. **Fase 2 — Outbox + triggers (Alternativa B, la recomendada)**
   - Script de migración con la tabla `outbox_evento` y los triggers.
   - Worker de outbox en `Api` que emite SSE.
   - Endpoint `GET /api/cambios?desde=timestamp` para que el escritorio pueda consumir cambios desde la app (si se quiere sincronizar por pull).

4. **Fase 3 — Migración gradual a la API (Alternativa A)**
   - Ir pasando pantallas del escritorio a endpoints REST, con el HTTP confinado en el Modelo.
   - Endpoints a crear: `dictados` POST/PATCH, ABMs para preceptor/profesor/alumno/materia/especialidad vía API, y un "usuario de servicio" o login de escritorio contra la API.
   - Mantener la outbox como red de seguridad mientras dure la transición.

## 6. Pendientes detectados (checklist)

Hitos ya cerrados:
- [x] SSE en la app sin polling sucio (`App/src/api.js` valida mensajes; reconexión automática).
- [x] Login de la API filtra `activo = 1` en preceptor/profesor/alumno (`Api/rutas/login.js`).
- [x] ABM de Preceptores en el escritorio con `dni` + contraseña bcrypt (CryptSharp) compatible con el login de la API.
- [x] Reemplazo de BCrypt.Net-Next (delay-signed, no carga en .NET Framework) por CryptSharpOfficial.

Pendientes técnicos:
- [ ] Refresco de listas del profesor en la app (polling suave / pull-to-refresh) al cambiar datos desde el escritorio.
- [ ] Unificar joins (INNER vs LEFT) de dictados en escritorio y API.
- [ ] Filtrar `activo` en listados de la API (dictados, alumnos inscriptos, etc.).
- [ ] Exponer `id_preceptor` + nombre del preceptor en `GET /api/dictados`.
- [ ] Endpoints de escritura para dictados (`POST`, `PATCH`) y demás ABMs si se opta por la Fase 3.
- [ ] Definir el rol administrador del lado de la API (hoy solo preceptor/profesor/alumno). El administrador del escritorio (Mongo) no existe en la API.
- [ ] Decidir y documentar la estrategia de identidad: ¿se unifica todo en MySQL+bcrypt o se mantiene Mongo para escritorio? (la opción A obligaría a unificar).
- [ ] `usuario` en MySQL ya tiene `activo`: revisar si se usa o sobra.
- [ ] Backup/restauración del MySQL y de Mongo (sin esto, "sincronizar" no protege nada).

Pendientes de proceso/entorno (anotados, no de código):
- [ ] El build del escritorio debe usar el **MSBuild de Visual Studio 18** (`C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe`); `dotnet msbuild` falla (MSB3822/3823) en proyectos clásicos.
- [ ] Las credenciales de BD están hardcodeadas a propósito (ver `AGENTS.md`): no cambiarlas.
- [ ] Pendiente empírico: probar el login móvil de un preceptor creado por el ABM nuevo (contraseña por defecto = DNI).

## 7. Riesgos y notas de implementación

- **No romper el MVC del escritorio**: cualquier integración HTTP debe vivir dentro del Modelo (DAO/Repository/adapter). Nunca en Vistas ni Controladores.
- **Offline**: si el escritorio pasa a depender de la API (opción A), definir cola local de reintento para no bloquear el trabajo con red caída.
- **SSE siempre de la API**: la app no debe aprender un segundo canal; cualquier solución debe terminar "alimentando" la SSE existente.
- **Triggers y migraciones**: si se opta por la outbox, los scripts (`Api/scripts/*.sql`) deben incluir triggers; revisar al recrear la base.
- **Contraseñas**: el hasheo de `contrasena` debe seguir el formato bcrypt standard (`$2a$10$…`); bcryptjs (API) y CryptSharp (escritorio) son interoperables, coste 10.