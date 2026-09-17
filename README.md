# Sistema Integral de Gestión de Asistencia Escolar

> Aplicación de escritorio para digitalizar el control de asistencia de alumnos y centralizar la gestión de alumnos, profesores, cursos y materias. Proyecto académico EEST.

![C#](https://img.shields.io/badge/C%23-.NET_Framework_4.7.2-239120?logo=csharp&logoColor=white)
![WinForms](https://img.shields.io/badge/UI-Windows_Forms-blue)
![MySQL](https://img.shields.io/badge/DB-MySQL-4479A1?logo=mysql&logoColor=white)
![MongoDB](https://img.shields.io/badge/Auth-MongoDB-47A248?logo=mongodb&logoColor=white)
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

## 1. Descripción del proyecto

Actualmente el control de asistencia en la institución se realiza mayormente **en papel**, lo que genera desorganización, riesgo de pérdida de datos, errores humanos y demora en los reportes.

Este proyecto desarrolla un **sistema integral de gestión de asistencia escolar** compuesto por:

1.  **Aplicativo administrativo de escritorio:** gestión de información institucional (alumnos, profesores, cursos, materias, usuarios).
2.  **Módulo web (planificado):** registro diario de asistencia desde celulares o PC para docentes y preceptores.

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

Funcionalidades de la primera versión:

| Módulo | Descripción |
|---|---|
| Autenticación | Login de usuarios con control de sesión |
| Usuarios y perfiles | Alta, baja, modificación y activación/desactivación |
| Alumnos | ABM completo de alumnos |
| Profesores | ABM completo de profesores |
| Cursos | Administración de cursos |
| Materias | ABM de materias con asignación de especialidad |
| Asistencias | Registro diario de asistencias |
| Inasistencias | Justificación de inasistencias |
| Historial | Consulta de historial de asistencia por alumno |
| Auditoría | Bitácora de acciones de usuarios |
| Permisos | Control de acceso según rol |

## 4. Usuarios y roles

| Rol | Permisos |
|---|---|
| **Administrador** | Configuración general, gestión de usuarios y mantenimiento |
| **Directivo** | Administración de alumnos, profesores, cursos, materias y consultas generales |
| **Preceptor** | Registro y seguimiento de asistencias e inasistencias |
| **Profesor** | Carga de asistencia de sus clases |
| **Alumno** | Consulta de su historial (según permisos) |

El menú principal (`FrmPrincipal`) aplica permisos automáticamente: el módulo de usuarios solo es visible para el Administrador.

## 5. Tecnologías y arquitectura

**Stack:**
- **Lenguaje:** C# .NET Framework 4.7.2
- **Interfaz:** Windows Forms (WinForms)
- **Base relacional:** MySQL (alumnos, profesores, materias, especialidades, asistencias)
- **Base documental:** MongoDB (usuarios y autenticación)
- **Drivers:** `MySql.Data` + `MongoDB.Driver`
- **IDE:** Visual Studio Community
- **Arquitectura:** En capas tipo MVC

**Capas:**

```
Vista (Forms) <-> Controlador (Lógica de negocio) <-> Modelo (Entidades + DAO + Conexión)
```

- **Modelo/Entidades:** `Alumno`, `Profesor`, `Materia`, `Especialidad`, `Asistencia`, `Usuario`, `Rol`
- **Modelo/DAO:** `AlumnoDAO`, `ProfesorDAO`, `MateriaDAO`, `EspecialidadDAO` (MySQL) y `UsuarioDAO` (MongoDB con contador autoincremental atómico)
- **Controlador:** `AlumnoController`, `ProfesorController`, `MateriaController`, `EspecialidadController`, `UsuarioController`
- **Vista:** `FrmLogin`, `FrmPrincipal`, `FrmAlumnos`, `FrmProfesores`, `FrmMaterias`, `FrmUsuarios`, `FrmPrimerUsuario`
- **Utilidades:** `Sesion` (usuario actual en memoria)

## 6. Estructura del proyecto

```
SistemaAsistencia/
├── SistemaAsistencia.slnx
└── SistemaAsistencia/
    ├── Program.cs
    ├── App.config
    ├── Controlador/
    │   ├── AlumnoController.cs
    │   ├── ProfesorController.cs
    │   ├── MateriaController.cs
    │   ├── EspecialidadController.cs
    │   └── UsuarioController.cs
    ├── Modelo/
    │   ├── Conexion/ (conexionBD.cs, ConexionMongo.cs)
    │   ├── DAO/ (AlumnoDAO, ProfesorDAO, etc.)
    │   └── Entidades/ (Alumno, Profesor, etc.)
    ├── Vista/
    │   ├── Login/FrmLogin.cs
    │   ├── Principal/FrmPrincipal.cs
    │   ├── Alumnos/FrmAlumnos.cs
    │   ├── Profesores/FrmProfesores.cs
    │   ├── Materias/FrmMaterias.cs
    │   ├── Usuarios/FrmUsuarios.cs
    │   └── PrimerUsuario/FrmPrimerUsuario.cs
    └── Utilidades/Sesion.cs
```

## 7. Requisitos previos

**Hardware:**
- PC con Windows 10 o superior (personal directivo / administrativo)
- Celular o PC con internet (docentes / preceptores para módulo web)
- Servidor local o remoto para MySQL / MongoDB

**Software:**
- Windows 10+
- Visual Studio 2022 Community (con workload .NET desktop development)
- MySQL Server 8.x + MySQL Workbench
- Acceso a MongoDB (local o Atlas)
- .NET Framework 4.7.2 Developer Pack

## 8. Instalación y configuración

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/TU-USUARIO/TU-REPO.git
   ```

2. **Abrir la solución:**
   Abrir `SistemaAsistencia/SistemaAsistencia.slnx` en Visual Studio.

3. **Restaurar paquetes NuGet:**
   Visual Studio lo hace automático. Paquetes requeridos:
   - `MySql.Data`
   - `MongoDB.Driver`

4. **Configurar bases de datos:**
   - Crear la base MySQL `gestion_asistencia_eest` e importar el script SQL (si aplica).
   - Configurar cadenas de conexión en:
     - `Modelo/Conexion/conexionBD.cs`
     - `Modelo/Conexion/ConexionMongo.cs`

5. **Compilar y ejecutar (F5):**
   - Si no existen usuarios, se abre `FrmPrimerUsuario` para crear el primer Administrador.
   - Luego se accede con `FrmLogin`.

> Recomendado: agregar un `.gitignore` de Visual Studio para ignorar `bin/`, `obj/`, `.vs/`, `packages/`.

## 9. Uso del sistema

1. Iniciar la app, crear el primer usuario Administrador si es la primera vez.
2. Iniciar sesión con usuario y contraseña.
3. Desde el menú principal:
   - **Administrador:** gestiona usuarios, alumnos, profesores y materias.
   - **Directivo:** administra datos institucionales y consultas.
   - **Preceptor / Profesor:** registra asistencias diarias.
4. La sesión actual muestra nombre y rol en `FrmPrincipal`.

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
| Equipamiento existente | Sin costo adicional |
| Capacitación de usuarios | Mínima |
| Mantenimiento anual | Bajo |

El principal costo futuro será el mantenimiento correctivo y evolutivo.

## 13. Roadmap

- [x] Login y gestión de primer usuario admin
- [x] ABM Alumnos, Profesores, Materias, Usuarios
- [x] Control de permisos por rol
- [ ] Registro diario de asistencias
- [ ] Justificación de inasistencias e historial por alumno
- [ ] Bitácora de acciones
- [ ] Reportes automáticos
- [ ] Módulo web móvil para docentes/preceptores

## 14. Equipo y contexto académico

Proyecto escolar desarrollado por estudiantes de la EEST como Evaluación Anual de Capacidades Profesionales (EACP). Migrado desde carpeta compartida de Drive a GitHub para control de versiones y trabajo colaborativo.

## 15. Licencia

Proyecto con fines educativos. Todos los derechos reservados a sus autores e institución educativa.
```

**Tip extra:** después de crear el repo en GitHub, marca la casilla `Add a README file` en **NO**, porque vas a subir este `README.md` manualmente. Y agrega un `.gitignore` tipo `VisualStudio` al crearlo.
