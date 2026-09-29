# AGENTS.md

Cómo trabajo en este repo.

Proyecto: sistema de asistencia escolar (escritorio .NET · API Node · app Expo).
Documentación técnica del proyecto: [`context.md`](context.md).

## Identidad

Cada máquina define su usuario con `git config user.name`. Ese valor define a qué
rama pertenecés:

| `user.name`       | rama          |
|-------------------|---------------|
| `JuanI19T`        | `Juan-Torres` |
| `isabellacarrete` | `Isa-Vecco`   |

Antes de operar sobre una rama, verificá que la rama activa sea la de quien sos.
Si no coincide, **frená y preguntá**: trabajar sobre la rama de otra persona
contamina su trabajo, y separar después es más caro que preguntar.

No es autenticación: es una convención. Si el valor no está en la tabla, avisá y
pedí que se configure con `git config --global user.name "<usuario de GitHub>"`.

## Nunca perdás datos

⛔ Prohibidos, siempre, sin excepciones:

- `git reset --hard`
- `git clean` (con o sin flags)
- `git checkout -f`
- `git push --force`

Si un borrado parece necesario, **decime qué querés borrar y por qué** antes de
hacerlo.

Git ya garantiza el resto, así que no hace falta defenderse de eso:

- Un merge nunca borra commits.
- `git merge` falla solo si un merge pisaría un archivo sin commitear.
- Los archivos ignorados por `.gitignore` (`node_modules/`, `.env`,
  `credenciales.env`, `bin/`, `obj/`) no se tocan en un merge ni en un checkout.

## Traer cambios de main

⛔ **Nunca** merges `main`; usá siempre `origin/main`. La `main` local queda
atrasada porque nadie la mueve al mergear en GitHub, y mergearla aplica un estado
viejo sin avisar.

⛔ **Nunca** uses `git rebase`. El proyecto mergea por PR, y rebasar commits ya
publicados rompe el historial de `main`.

Antes de mergear, **siempre** mostrame:

1. Cuántos commits de `main` entran: `git log --oneline HEAD..origin/main`
2. Cuántos commits tenés que `main` no tiene: `git log --oneline origin/main..HEAD`
3. Si hay cambios sin commitear: `git status --porcelain`

Si alguno de los tres no está limpio, **preguntá en vez de asumir**. Después de
`git fetch origin`, integrá con `git merge origin/main`.

**Tu `main` local** se actualiza con fast-forward. Si falla, **no lo fuerces con
`-f`**: significa que divergió y hay que revisarla a mano.

## Conflictos

Nunca los resuelvas automáticamente. Avisame los archivos con
`git diff --name-only --diff-filter=U` y dejame resolverlos a mí en el editor.

Si hubo conflictos, **no actualices tu `main` local** con el merge a medias.

## Commits y PRs

- Nunca commitees directo a `main`. Todo desde tu rama, por PR.
- Un commit = un cambio con sentido, mensaje en español plano.
- No commitees ni pushees sin que te lo pida explícitamente.

## Credenciales

🔴 Las credenciales **no** van hardcodeadas. `Api/.env`, `App/.env` y
`credenciales.env` están ignorados por git; los `.example` solo llevan
placeholders.

Si tocás una credencial, **rotala** en el proveedor (Railway / Atlas), no solo en
el código. Ya tuvimos que rotar cadenas de MySQL y MongoDB que quedaron
versionadas en el historial.

Nunca versionar: `.env` · `credenciales.env` · `*.pem` · `*.key` · `*.zip`

## Estilo

- Identificadores y BD en español: `alumno`, `dictado`, `obtenerDictados`.
- En la API, rutas y JSON **sin acentos**.
- 🔴 Tablas de MySQL **siempre en minúscula**: Railway corre Linux, donde
  `ALUMNO` y `alumno` son tablas distintas. En Windows no se nota.
- 📌 El escritorio es MVC en capas. Toda integración HTTP futura va solo en
  `Modelo/` (DAO). Nunca en Vistas ni Controladores.
