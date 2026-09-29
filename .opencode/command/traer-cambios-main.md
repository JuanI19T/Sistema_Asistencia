---
description: Trae a tu rama los últimos cambios de origin/main, verificando antes que no tengas cambios sin guardar ni commits sin pushear. Actualiza también tu rama main local.
agent: build
---

Traé a tu rama los cambios más recientes de `main`.

Ejecutá los pasos **en orden**. No avances al siguiente si el actual falla o si te
detengo. Cuando necesites confirmación, usá la herramienta `question`.

## Paso 1 — Verificar identidad y rama

### 1a. ¿Quién sos?

`git config user.name`

Viene del config **global** de la máquina, no del repo. Es lo más parecido a una
identidad que tenemos — **no es autenticación**, es una convención.

Si el valor está vacío, avisá que hay que configurarlo con
`git config --global user.name "<usuario de GitHub>"` y frená.

### 1b. Mapeo declarado

| `user.name`      | rama          | persona |
|------------------|---------------|---------|
| `JuanI19T`       | `Juan-Torres` | Juan    |
| `isabellacarrete`| `Isa-Vecco`   | Isa     |

- **Coincide** → seguí al 1c.
- **No coincide, o el valor no está en la tabla** → **detenete** y decime:

  > Tu `git config user.name` dice `<valor>`, que corresponde a `<persona>`,
  > pero estás en la rama `<rama>`. Parece que estás trabajando en la rama
  > de otra persona. Si te seguís, esos cambios pueden terminar en su rama.

  Ofrecé con `question`:
  1. Cambiar a mi rama correcta: `git checkout <rama esperada>`
  2. Trabajar en esta rama igual — lo confirmo yo explícitamente
  3. Cancelar el comando

  ⛔ No mergees nada sin respuesta.

### 1c. ¿No estás en `main`?

`git branch --show-current`

- Si es `main` → **detenete**. Fusionar `main` consigo mismo no tiene sentido.

Guardá el resultado como `$RAMA`.

## Paso 2 — Verificar cambios sin guardar

`git status --porcelain`

- Vacío → seguí.
- Hay líneas → **detenete**. Mostrá la lista exacta y preguntá con `question`:
  1. Commitear los cambios
  2. Guardarlos con `git stash` (se recuperan con `git stash pop`)
  3. Descartarlos
  4. Cancelar

⛔ No mergees nada hasta que responda.

ℹ️ Los ignorados por `.gitignore` — `node_modules/`, `.env`, `credenciales.env`,
`bin/`, `obj/`, `.vs/` — **no** aparecen acá y **no** bloquean el merge. No los
borres ni los menciones como problema.

## Paso 3 — Descargar la información de GitHub

`git fetch origin`

No modifica ninguno de tus archivos. Solo actualiza `origin/main` y
`origin/$RAMA` con el estado real del remoto.

⚠️ De acá en adelante toda referencia a main es **`origin/main`**.

⛔ **Nunca** merges `main`. La local suele estar atrasada; mergearla te traería
un estado viejo sin avisar.

## Paso 4 — Verificar que no estés adelantado

`git rev-list --left-right --count origin/$RAMA...HEAD`

- **Primero > 0** → tu rama local está atrás. Corré
  `git pull --ff-only origin $RAMA` y volvé a este paso.
- **Segundo > 0** → tenés commits sin pushear. Avisame cuántos y preguntá si
  los subo con `git push origin $RAMA` antes de seguir.
- **`0 0`** → sincronizado. Seguí.

## Paso 5 — Mostrar qué entra de main

Ejecutá estos dos comandos y mostrame ambos resultados:

- `git log --oneline HEAD..origin/main` → qué commits de `main` te faltan
- `git log --oneline origin/main..HEAD` → cuántos commits tenés que `main` no tiene

- **El primero está vacío** → no hay nada que mergear. Decímelo explícitamente
  junto con cuántos commits tenés de sobra, salteá el paso 6 y seguí al 8.
- **Con commits** → mostrá la lista y **pedí confirmación** antes de mergear.

## Paso 6 — Fusionar

`git merge origin/main`

⛔ Nunca `git rebase`. Nunca merges `main` en vez de `origin/main`.

## Paso 7 — Conflictos

Si el merge falla por conflicto, **dejalo pausado para que lo resuelvas vos**.
Nunca lo resuelvas automáticamente.

1. Mostrá los archivos con `git diff --name-only --diff-filter=U`
2. Explicá cómo continuar:
   - Resolvés los archivos en tu editor
   - `git add <archivos>` para marcarlos resueltos
   - `git commit` para cerrar el merge
3. Ofrecé `git merge --abort` si preferís empezar de nuevo
4. ⛔ **Salteá el paso 8** — no actualices `main` con el merge a medias

## Paso 8 — Actualizar tu rama main local

Solo si no hubo conflictos. Tu `main` local queda atrasada porque nadie la mueve
al mergear en GitHub.

```
git checkout main
git merge --ff-only origin/main
git checkout $RAMA
```

- Si `--ff-only` falla, ⛔ **no lo fuerces** con `-f`. Avisame que tu `main` local
  divergió y que hay que revisarla a mano.
- Volvé **siempre** a `$RAMA` al final, aunque algo falle en el medio.

## Paso 9 — Reporte

- `git log --oneline -1`
- Cuántos commits entraron de `main` y qué archivos cambiaron
- Cuánto se pongió al día tu `main` local
