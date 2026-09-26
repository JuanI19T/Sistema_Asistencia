# Checklist de mejoras — SistemaAsistencia — Aplicativo de escritorio

> Versión del proyecto: **v06** — Windows Forms / .NET Framework 4.7.2 / MaterialSkin.
> **Alcance de este documento: el aplicativo de escritorio (ABM de datos).**
> El manejo y los reportes de asistencia se realizan en el portal React Native (API + app) y **quedan fuera de este documento** (ver "Fuera de alcance").
> Vamos resolviendo los ítems uno por uno y anotamos aquí cómo quedó resuelto cada uno.

---

## Resumen

| # | Mejora | Prioridad | Categoría | Estado |
|---|--------|-----------|-----------|--------|
| 1 | Proteger credenciales de conexión (solo Mongo) | Alta | Seguridad | [ ] Implementada — falta prueba runtime + pasos en Atlas |
| 2 | Hash de contraseñas fuerte (PBKDF2 + sal) | Alta | Seguridad | [ ] Implementada — falta prueba runtime |
| 3 | Manejo de errores centralizado + logging | Alta | Robustez | [ ] Implementada — falta prueba runtime |
| 4 | Baja lógica consistente en todas las entidades | Alta | Integridad | [ ] Implementada — falta script SQL y prueba runtime |
| 7 | Validaciones de integridad | Media | Integridad | [ ] Pendiente — requiere BD |
| 8 | Bloqueo por intentos fallidos de login | Media | Seguridad | [ ] Pendiente |
| 9 | Unificar motor de datos (Mongo vs MySQL) | Baja | Deuda técnica | [ ] Pendiente — decisión abierta |
| 10 | Refactor de navegación en FrmPrincipal | Baja | Deuda técnica | [ ] Pendiente |
| 11 | Auditoría de acciones | Baja | Trazabilidad | [ ] Pendiente — requiere BD |
| 12 | Centralizar versión de la aplicación | Baja | Mantenimiento | [ ] Pendiente |
| 13 | .gitignore + ordenar artefactos | Baja | Repo/Entrega | [ ] Pendiente |
| 14 | Tests unitarios básicos | Baja | Calidad | [ ] Pendiente |

**Fuera de alcance de WinForms (decidido el 24/9/2026):**
- **Ítem 5 — Módulo Asistencia** y **Ítem 6 — Reportes de asistencia**: se gestionan en el portal React Native. No se crea la tabla `ASISTENCIA` desde este proyecto (la define el portal). No duplica responsabilidades.

**Orden sugerido:** primero probar en runtime los ítems 1-4 (lista de "Qué falta" abajo) y luego seguir 12 → 13 → 10 → 8 → 7 → 11 → 9 → 14.

---

## 1 — Proteger credenciales de conexión

**Problema**
Las cadenas de conexión vivían en `App.config` en claro. Al compartirse el proyecto en zips y sin `.gitignore`, las credenciales viajaban con la entrega.

**Qué se hizo**
- **Alcance: solo Mongo** (decisión del usuario; MySQL es local, no sensible, quedó como en v06).
- `secreto.config` se crea **en la carpeta que CONTIENE a la solución** (`.net\`, al lado de este CHECKLIST); si no hay `.slnx`/`.sln` cae al lado del exe.
- Valor guardado **ofuscado con XOR+Base64** (no DPAPI, no sobrevive el copiado entre PCs).
- `Configuracion.cs` (`ObtenerCadena`) lee: `secreto.config` → fallback `App.config`.
- `Program.cs`: si no existe `secreto.config`, abre `FrmConfiguracion` (wizard que pide solo el URI de Mongo, obligatorio).
- `App.config`: `MongoAtlas` vacío; `MySQL` intacto.

**Qué falta probar / hacer**
1. **Prueba runtime A (carpeta limpia)**: borrar `secreto.config` → debe abrir el wizard, pedir el URI, y conectar a Mongo.
2. **Prueba runtime B (carpeta funcional)**: dejar `secreto.config` creado → debe iniciar directo sin wizard.
3. **Pasos manuales en Atlas (pendientes)**: usuario de Mongo con privilegio mínimo + IP Access List + **rotar la password** (la anterior circuló en zips y capturas).

**Archivos:** `Utilidades/Configuracion.cs`, `Vista/Configuracion/FrmConfiguracion.cs`, `Program.cs`, `ConexionMongo.cs`, `App.config`, `csproj`.

**Estado:** [ ] Implementada el 24/9/2026 — compila OK; falta prueba runtime + pasos en Atlas

---

## 2 — Hash de contraseñas fuerte (PBKDF2 + sal)

**Problema**
`UsuarioDAO.HashearContrasena` usaba SHA-256 sin salt (vulnerable a fuerza bruta y tablas rainbow).

**Qué se hizo**
- PBKDF2 (`Rfc2898DeriveBytes`, sin dependencias nuevas): sal 16 B, 10000 iteraciones, clave 32 B.
- Formato almacenado: `pbkdf2$<sal base64>$<clave base64>` en el campo `Contrasena` (Mongo).
- Comparación en **tiempo constante**.
- **Migración transparente**: en el login, si el hash guardado es SHA-256 viejo (64 hex), se valida y se actualiza a PBKDF2 en ese mismo login.

**Qué falta probar / hacer**
1. Login con un usuario **creado antes** de esta mejora → debe entrar y el hash de Mongo quedar convertido a `pbkdf2$...`.
2. Login con un usuario **nuevo** → funciona y guarda PBKDF2.
3. Contraseña incorrecta → sigue devolviendo usuario nulo (login denegado), sin error.

**Archivos:** `Modelo/DAO/UsuarioDAO.cs` (`Login`, `HashearContrasena`, `VerificarContrasena`, migración), `Modelo/Entidades/Usuario.cs`.

**Estado:** [ ] Implementada el 24/9/2026 — compila OK; falta prueba runtime

---

## 3 — Manejo de errores centralizado + logging

**Problema**
Los DAO MySQL explotaban en la UI sin try/catch y sin dejar registro de qué pasó.

**Qué se hizo**
- `Utilidades/Logger.cs`: log en `logs/log_yyyyMMdd.txt` al lado del exe (candado de hilos; no rompe la app si falla el logging).
- `Utilidades/DatosException.cs`: mensaje amigable + `InnerException` técnica.
- `Utilidades/Ejecutor.cs`: captura → loguea → relanza `DatosException` (deja pasar `DatosException` intacta).
- Los **7 controladores** envueltos en `Ejecutor` (las vistas ya tenían try/catch y ahora reciben mensajes amigables).
- `Program.cs`: try/catch global del arranque con log + MessageBox.

**Qué falta probar / hacer**
1. Apagar MySQL (o poner una cadena inválida) y disparar una búsqueda → debe salir "Ocurrió un error al procesar la operación '...'" + crear `logs/log_<fecha>.txt` con el detalle completo.
2. Revisar que el log no contenga datos sensibles (no debería, solo excepciones técnicas).

**Archivos:** `Utilidades/Logger.cs`, `Utilidades/DatosException.cs`, `Utilidades/Ejecutor.cs`, los 7 `Controlador/*Controller.cs`, `Program.cs`.

**Estado:** [ ] Implementada el 24/9/2026 — compila OK; falta prueba runtime

---

## 4 — Baja lógica consistente en todas las entidades

**Problema**
`Usuario` tenía baja lógica; `Alumno`, `Profesor`, `Materia`, `Especialidad` eliminaban físico (`DELETE`), rompiendo el histórico.

**Qué se hizo**
- Columna `activo TINYINT(1) NOT NULL DEFAULT 1` en `ALUMNO`, `PROFESOR`, `MATERIA`, `ESPECIALIDAD`, `DICTADO`.
- **Baja lógica (todos los roles)**: `DarDeBaja(id)` → `UPDATE activo = 0`; listados con `WHERE activo = 1`.
- **Eliminación definitiva (solo rol Administrador)**: `EliminarDefinitivo(id)` → transacción que borra hijos (`INSCRIBE`/`DICTADO`/`MATERIA`) y luego el padre (evita huérfanos).
- Chequeo de rol en dos capas (botón oculto + el controlador lanza `DatosException` si no es admin).
- `ObtenerDependencias(id)` + diálogo **`FrmConfirmarEliminar`** que lista los registros vinculados antes de decidir.
- `Utilidades/Sesion.cs`: `UsuarioActualRolAdministrador()`.
- Script de migración: `BD/2026-09-24_baja_logica_activo.sql`.

**Qué falta probar / hacer**
1. **Ejecutar el script SQL** en la PC local (y en cada PC). No es idempotente: si corre de nuevo, error 1060 "Duplicate column name 'activo'" (inofensivo). Verificar con `INFORMATION_SCHEMA.COLUMNS`.
2. **Baja lógica**: dar de baja un alumno → desaparece del listado y sigue en la BD (SELECT directo).
3. **Hard-delete admin**: el diálogo debe mostrar los registros vinculados (p. ej. "3 Inscripciones en dictados") y borrarlos junto al padre.
4. **No-admin**: el botón "Eliminar definitivamente" no debe aparecer; si se fuerza la llamada, el controlador tira `DatosException`.
5. Nota DBeaver: el diagrama ER abre en "Read-Only" y no refleja los ALTER; desactivar el modo + F5.

**Archivos:** las 5 `Modelo/DAO/*DAO.cs`, los 5 `Controlador/*Controller.cs`, las 5 vistas (`Vista/{Alumnos,Profesores,Materias,Especialidades,Dictados}/Frm*.cs`), `Vista/Comun/FrmConfirmarEliminar.cs`, `Modelo/DAO/Dependencia.cs`, `BD/2026-09-24_baja_logica_activo.sql`.

**Estado:** [x] Implementada el 24/9/2026 — compila OK; **falta ejecutar el script SQL y probar en runtime**

---

## 7 — Validaciones de integridad

**Problema**
No se valida legajo duplicado en alumnos ni solapamiento al inscribir; los duplicados dependen de errores SQL genéricos.

**Qué falta hacer**
1. **BD (requiere autorización):** índice `UNIQUE` en `ALUMNO.legajo_alumno` (script en `BD/`).
2. `AlumnoDAO.Agregar`/`Modificar`: capturar la excepción **1062 (duplicado)** y relanzar `DatosException` con mensaje claro ("Ya existe un alumno con ese legajo").
3. (Opcional) Validación previa en `AlumnoController` antes de insertar/actualizar, y chequeo de solapamiento en `InscripcionDAO.Agregar` (ya evita duplicados con `NOT EXISTS`; evaluar mensajes).

**Archivos:** `Modelo/DAO/AlumnoDAO.cs`, `Modelo/DAO/InscripcionDAO.cs`, `Controlador/AlumnoController.cs`, script en `BD/`.

**Estado:** [ ] Pendiente — requiere autorización para la BD (índices)

---

## 8 — Bloqueo por intentos fallidos de login

**Problema**
`FrmLogin` permite reintentos ilimitados → fuerza bruta.

**Qué falta hacer**
1. En `Vista/Login/FrmLogin.cs`: contador de intentos fallidos en memoria (p. ej. 3).
2. Al superar el umbral: deshabilitar el botón "Iniciar sesión" durante 30 s (Timer) mostrando "Intente de nuevo en X segundos"; al expirar, reiniciar contador.
3. Resetear el contador al lograr un login correcto.
4. (Opcional, futuro) Bloqueo temporal de cuenta en BD.

**Archivos:** `Vista/Login/FrmLogin.cs` (+ `.Designer.cs` si se agrega el Timer al diseño).

**Estado:** [ ] Pendiente

---

## 9 — Unificar motor de datos (Mongo vs MySQL)

**Problema**
Usuarios en **MongoDB Atlas**, el resto en **MySQL**. Dos motores = dos conexiones, sin transacciones globales y más superficie de secrets/configuración.

**Qué falta hacer (previa decisión del usuario, alineada con el portal RN)**
- **Opción A (recomendada): mover usuarios a MySQL.**
  1. Script: `CREATE TABLE USUARIO` (id autoincrement, `nombre_usuario` UNIQUE, `contrasena`, `rol`, `activo`) en `BD/`.
  2. Reescribir `UsuarioDAO` sobre MySQL (login/hash PBKDF2 ya probado se conserva tal cual).
  3. Quitar `ConexionMongo`, driver MongoDB y `Bson` del proyecto (menos dependencias y secrets).
  4. Retirar el wizard `FrmConfiguracion` y el `secreto.config` de Mongo (cae todo a `App.config` MySQL). **Cuidado:** esto revierte el ítem 1 si Mongo desaparece; ajustar checklist 1/9.
  5. Migrar los usuarios existentes de Mongo (script/ad-hoc).
- **Opción B: migrar todo a Mongo.** Descartada salvo que el portal RN la justifique.
- **A definir con vos:** ¿qué motor usa la API del portal RN? Conviene que coincidan.

**Archivos:** `Modelo/DAO/UsuarioDAO.cs`, `Modelo/Conexion/ConexionMongo.cs`, `Utilidades/Configuracion.cs`, `Vista/Configuracion/FrmConfiguracion.cs`, `App.config`, `csproj`.

**Estado:** [ ] Pendiente — decisión abierta

---

## 10 — Refactor de navegación en FrmPrincipal

**Problema**
`FrmPrincipal` tiene `AgregarOpcion` repetido con coordenadas manuales, un `Dictionary<string, OpcionMenu>` frágil (claves por string), y workarounds de MaterialSkin (`Environment.Exit`, flag `navegando`).

**Qué falta hacer**
1. Encapsular el menú con un `enum Modulo` + registro declarativo de opciones (título, fábrica, acento, rol requerido).
2. Reemplazar las claves de string por el enum (colisión a compilación).
3. Documentar (o simplificar) `Environment.Exit` y el flag `navegando`, manteniéndolos solo donde son necesarios.
4. Probar: arranque, navegación entre secciones, multi-monitor, "Cerrar sesión" y "Salir", permisos (Usuarios solo Admin).

**Archivos:** `Vista/Principal/FrmPrincipal.cs` (+ `.Designer.cs` si se reorganizan paneles).

**Estado:** [ ] Pendiente

---

## 11 — Auditoría de acciones

**Problema**
No queda registro de quién hizo qué (alta/baja/modificación).

**Qué falta hacer**
1. **BD (requiere autorización):** tabla `AUDITORIA` (id, `id_usuario`, `nombre_usuario`, `accion`, `entidad`, `entidad_id`, `fecha`, `detalle`) — script en `BD/`.
2. Helper `Utilidades/Auditoria.cs`: inserta un registro usando `Sesion.UsuarioActual`.
3. Llamarlo tras las operaciones de alto impacto: `Agregar`, `Modificar`, `DarDeBaja`, `EliminarDefinitivo`, gestión de usuarios.
4. (Alternativa más simple si no se quiere tabla) campos `created_at`/`updated_by` por tabla.

**Archivos:** `BD/` (script), `Utilidades/Auditoria.cs`, `Controlador/*.cs`.

**Estado:** [ ] Pendiente — requiere autorización para la BD

---

## 12 — Centralizar versión de la aplicación

**Problema**
La versión está hardcodeada como "ESCUELA • v1.0" en `FrmPrincipal.cs:184`, sin relación con la nomenclatura v06.

**Qué falta hacer**
1. Definir versión real en `Properties/AssemblyInfo.cs` (`AssemblyVersion`/`InformationalVersion`, p. ej. `0.6.x`).
2. Leerla en runtime: `Assembly.GetExecutingAssembly().GetName().Version` y mostrar "ESCUELA • v0.6.x" en la marca del menú.
3. Quitar el literal fijo.

**Archivos:** `Vista/Principal/FrmPrincipal.cs` (línea ~184), `Properties/AssemblyInfo.cs`.

**Estado:** [ ] Pendiente (rápido, ~5 min)

---

## 13 — .gitignore + ordenar artefactos

**Problema**
No hay `.gitignore`; `bin/`, `obj/`, `packages/` y `.vs/` inflan los zips y arrastran artefactos/secrets.

**Qué falta hacer**
1. Crear `.gitignore` en la raíz de solución `.net/` ignorando: `bin/`, `obj/`, `packages/`, `.vs/`, `*.user`, `logs/`, `secreto.config`.
2. Decidir si `App.config` va versionado (hoy trae `MySQL=root/root` local — analizar con el ítem 9).
3. Revisar qué entra en los zips de entrega (no incluir `obj/`, `bin/` de Debug, `packages/`).

**Archivos:** `.gitignore` (nuevo, en `.net/` o raíz del repo cuando exista).

**Estado:** [ ] Pendiente

---

## 14 — Tests unitarios básicos

**Problema**
No hay ninguna prueba automatizada.

**Qué falta hacer**
1. Extraer las funciones puras de hashing (`HashearContrasena`, `VerificarContrasena`, `EsHashViejo`, migración) a una clase estática testeable `Utilidades/Hash.cs` y hacer que `UsuarioDAO` las use.
2. Proyecto de test **MSTest** net472 en `SistemaAsistencia/` (o carpeta `Tests/`).
3. Cubrir al menos: hash/verificación PBKDF2, hash inválido/distorsionado, comparación en tiempo constante, y (si no hay BD en CI) el contador lógico de IDs de usuarios.

**Archivos:** `Utilidades/Hash.cs` (nuevo), `Modelo/DAO/UsuarioDAO.cs` (referenciarlo), proyecto de tests (nuevo).

**Estado:** [ ] Pendiente

---

## Bitácora de resolución

> Acá anotamos, por cada ítem, la decisión final y cómo quedó resuelto.

| Fecha | # | Decisión / cómo se resolvió | Resultado |
|------|---|------------------------------|-----------|
| 24/9/2026 | 1 | Implementado (solo Mongo): `secreto.config` en carpeta que contiene a la solución + wizard primer arranque (solo URI Mongo) + ofuscación XOR+Base64. `App.config` sin URI real. MySQL revertido a su comportamiento original. Compila OK. | Implementada — falta prueba runtime + pasos en Atlas |
| 24/9/2026 | 2 | Implementado PBKDF2+sal (16 B sal, 10000 iter, 32 B clave), comparación en tiempo constante, migración transparente del hash SHA-256 viejo en el primer login. Compila OK. El usuario confirmó "Funciona". | Implementada — falta prueba runtime |
| 24/9/2026 | 3 | Implementado `Logger` (`logs/log_yyyyMMdd.txt`), `DatosException`, `Ejecutor`; los 7 controladores envueltos; try/catch global en `Program.cs`. Compila OK. | Implementada — falta prueba runtime |
| 24/9/2026 | 4 | Implementado: columna `activo` en 5 tablas (script en `BD/`), `DarDeBaja` para todos, `EliminarDefinitivo` transaccional solo Admin, `ObtenerDependencias` + diálogo `FrmConfirmarEliminar`. 5 vistas actualizadas. Compila OK. | Implementada — falta ejecutar script SQL y probar |
| 24/9/2026 | 4 | Ajuste de UI: `FrmConfirmarEliminar` reescrito con `TableLayoutPanel` + `FlowLayoutPanel` (sin solaparse ni cortarse), panel de dependencias con scroll. Compila OK. | En runtime |
| 24/9/2026 | 4 | Script SQL no idempotente: correr UNA vez por PC (error 1060 si se re-ejecuta, inofensivo). | Aplicado en PC local |
| 24/9/2026 | 4 | DBeaver: el diagrama ER abre en "Read-Only" y no refleja los ALTER; desactivar Read-Only + F5. La columna `activo` sí existe en la BD (confirmado por error 1060). | Solo DBeaver |
| 24/9/2026 | 5-6 | **Decisión de arquitectura:** manejo y reportes de asistencia pasan al portal React Native; este WinForms queda como ABM de datos. No se crea `ASISTENCIA` desde acá. | Fuera de alcance de WinForms |
| 24/9/2026 | 12-14 | Documentación reorganizada: se reescribió este CHECKLIST para el alcance del escritorio y se detalló "Qué falta" por ítem. | Documentación |

---

## Notas generales

- **Base de datos:** ningún cambio de tablas/índices se ejecuta sin autorización previa (AGENTS.md). Los scripts van en `BD/` y se corren una vez por PC.
- **Rol de la IA:** asesora, propone soluciones y (por decisión del usuario en esta etapa) aplica los cambios de código directamente; para BD siempre pide autorización primero.
- **Alcance:** el manejo y los reportes de asistencia (ex ítems 5 y 6) se realizan en el **portal React Native**; este proyecto se limita al ABM de datos.
- **Entregas:** mantener el `secreto.config` fuera de los zips (ver ítem 1) y excluir `bin/`, `obj/`, `packages/` y `.vs/` (ver ítem 13).