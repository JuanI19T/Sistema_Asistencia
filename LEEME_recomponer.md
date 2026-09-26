# Proyecto Escuela v7 - Guía para recomponer el proyecto

Este `.zip` (`ProyectoEscuela_v7.zip`) contiene los 3 módulos del sistema.
Los siguientes pasos reconstruyen cada módulo desde cero.

> Excluidos del zip a propósito: `node_modules`, `.expo`, `.git`, `.vs`,
> las carpetas `bin`, `obj` y `packages`, logs, `.env` y `secreto.config`
> (contienen credenciales). Por eso se piden pasos de restauración abajo.

---

## 1) Módulo `.net` (SistemaAsistencia - escritorio C# / WinForms)

1. Abrir `SistemaAsistencia.slnx` en Visual Studio.
2. La carpeta `packages` (paquetes NuGet) no viaja en el zip.
   - Al abrir la solución, VS la restaura sola; si no, clic derecho sobre la
     solución -> **Restaurar paquetes NuGet** (o `nuget restore`).
3. En `App.config` cargar las credenciales reales:
   - cadena de conexión MySQL
   - URI de MongoDB Atlas (el `secreto.config` no va en el zip)
4. Compilar y ejecutar normalmente.

---

## 2) Módulo `ApiAsistencia` (API Node / Express)

1. Crear el archivo de entorno:

   ```
   copy .env.example .env
   ```

   y completar `DB_HOST`, `DB_PORT`, `DB_USER`, `DB_PASSWORD`, `DB_NAME`
   con los valores de tu MySQL.

2. Reconstruir la base de datos (¡borra cualquier dato anterior!):

   ```
   mysql -u usuario -p < scripts/migrar_a_clases.sql
   ```

3. Instalar dependencias (usa el `package-lock.json` incluido):

   ```
   npm install
   ```

4. Cargar datos de prueba (idempotente; contraseña inicial = DNI):

   ```
   node scripts/seed.js
   ```

5. Levantar la API:

   ```
   node index.js
   ```

   Quedará escuchando en `http://localhost:3000`.

---

## 3) Módulo `AppAsistencia` (App React Native / Expo)

1. Instalar dependencias (trae `expo-camera`, `react-native-svg`,
   `react-native-qrcode-svg`, ya declarados en `package.json`):

   ```
   npm install
   ```

2. Iniciar Expo:

   ```
   npx expo start
   ```

3. Escanear el QR con **Expo Go** en el celular.
4. Requisito de red: la PC y el celular deben estar en la misma red.
   La app obtiene la IP del host automáticamente (`src/api.js`),
   así que no hace falta configurar nada manualmente.

---

## Orden sugerido

Base de datos (`scripts/migrar_a_clases.sql` + `seed.js`) -> API -> App.

## Credenciales de prueba (dataset del seed)

Contraseña = DNI en todos los casos:

| Rol       | Nombre          | DNI       |
|-----------|-----------------|-----------|
| Profesor  | Carlos Gutierrez| `30111222`|
| Profesor  | María Fernández | `31222333`|
| Preceptor | Laura Martínez  | `34555666`|
| Alumno    | Juan Pérez      | `45222001`|
| Alumno    | Ana Gómez       | `45222002`|
| Alumno    | Luis Díaz       | `45222003`|