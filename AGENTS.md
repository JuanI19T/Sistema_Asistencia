# Proyecto Escuela - Reglas de trabajo (OpenCode)

## Branch obligatoria

- Toda sesión de OpenCode en este proyecto INICIA en la branch `Juan-Torres`.
- Al arrancar, ejecutar `git branch --show-current` y si no es `Juan-Torres`,
  hacer `git checkout Juan-Torres` antes de cualquier otro trabajo.
- NUNCA trabajar, commitear ni pushear directo en `main`.
- Todos los commits y push van a `origin/Juan-Torres`.

## Pull Requests

- NUNCA mergear a `main` sin Pull Request.
- El flujo es: trabajo en `Juan-Torres` -> push -> abrir PR `Juan-Torres -> main`
  para revisión del compañero -> el otro revisa y mergea.
- OpenCode solo crea el PR, no lo mergea.
- Para traer cambios de `main`: `git fetch origin` y
  `git merge origin/main` estando en `Juan-Torres`. Nunca al revés.

## Secretos (repo público)

- No commitear nunca: `.net/secreto.config`, `Api/.env`, `*.zip`,
  `node_modules/`, `.expo/`, `bin/`, `obj/`, `.vs/`, `packages/`.
- `ConexionMongo.cs` y `conexionBD.cs` tienen credenciales hardcodeadas por
  consigna del profesor: no modificarlas, pero recordar que son visibles.
