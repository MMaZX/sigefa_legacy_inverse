# Accesos de usuario (permisos por módulo y submódulo)

Documenta cómo el módulo de Usuarios, botón **Accesos**, guarda y carga los permisos. Sirve como referencia para reimplementarlo en un backend.

Los procedures se leyeron en la base local (`database_multi_final`, MySQL). No se contrastaron con producción.

## Modelo

Un acceso es una fila `(usuario, almacén, formulario)`. Si la fila existe, el usuario puede entrar a ese formulario (módulo o submódulo) en ese almacén. Los permisos son **por usuario y por almacén**.

El árbol de módulos sale de la tabla `formulario`: `padre = 0` marca los nodos raíz y `padre = codFormulario` marca los hijos.

### Tabla `accesos`

| Columna | Tipo | Notas |
|---|---|---|
| `codAcceso` | int PK autoincremental | |
| `codUsuario` | int NOT NULL | FK a `usuario` |
| `codAlmacen` | int NOT NULL | FK a `almacen` |
| `codFormulario` | int NOT NULL | FK a `formulario` |
| `codUser` | int NOT NULL | Usuario que otorgó el acceso (auditoría) |
| `fecharegistro` | datetime NOT NULL | `NOW()` al insertar |

No hay índice único sobre `(codUsuario, codAlmacen, codFormulario)`. Nada impide filas duplicadas.

## Procedures

### `CargaFormularios()`

```sql
SELECT codFormulario, descripcion, padre, nivel FROM formulario;
```

Devuelve el árbol completo, que el cliente arma con la columna `padre`.

### `CargaAccesos(codusua INT, codalma INT)`

```sql
SELECT codFormulario FROM accesos
WHERE codUsuario = codusua AND codAlmacen = codalma;
```

Devuelve los formularios permitidos. El cliente marca esos nodos en el árbol.

### `LimpiaAccesos(codusua INT, codalma INT)`

```sql
DELETE FROM accesos
WHERE codUsuario = codusua AND codAlmacen = codalma;
```

### `GuardaAcceso(codusua INT, codalm INT, codfor INT, codusu INT, OUT newid INT)`

```sql
INSERT INTO accesos (codUsuario, codAlmacen, codFormulario, codUser, fecharegistro)
VALUES (codusua, codalm, codfor, codusu, NOW());
SET newid = LAST_INSERT_ID();
```

| Parámetro | Significado |
|---|---|
| `codusua` | Usuario que recibe el permiso |
| `codalm` | Almacén |
| `codfor` | Formulario permitido |
| `codusu` | Usuario logueado que está otorgando el permiso |
| `newid` | OUT, id generado |

## Flujo de "Guardar"

Código de referencia: `SIGEFA.Formularios/frmAccesos.cs` (`btnGuardar_Click`).

1. Se recorre el árbol y se arma la lista `codigos` de `codFormulario`:
   - se incluyen los nodos tildados;
   - se incluyen también los nodos padre que tengan al menos un hijo directo tildado.
2. `CALL LimpiaAccesos(codUsuario, codAlmacen)` borra los accesos actuales del usuario en ese almacén.
3. Por cada `codFormulario` de la lista: `CALL GuardaAcceso(codUsuario, codAlmacen, codFormulario, codUserLogueado, @newid)`.

Es un **reemplazo completo** (borrar e insertar), no un upsert. Para el backend, el endpoint equivalente recibe `{ codUsuario, codAlmacen, codFormularios[] }` y reemplaza el conjunto.

## Flujo de carga

1. `CargaFormularios()` para armar el árbol.
2. `CargaAccesos(codUsuario, codAlmacen)` para marcar los nodos. Se vuelve a ejecutar al cambiar el almacén.

## Observaciones para el backend

- **Falta transacción.** El código actual ejecuta el `DELETE` y los `INSERT` por separado. Si falla un insert, el usuario queda con permisos parciales. Conviene envolver todo en una transacción.
- **Padres implícitos.** El cliente WinForms decide qué padres guardar. Mirar solo hijos directos puede dejar sin guardar un padre cuyos tildados estén a nivel nieto (no verificado). Es más seguro que el backend calcule los ancestros de cada formulario tildado.
- **Sin unicidad.** Se recomienda agregar un `UNIQUE (codUsuario, codAlmacen, codFormulario)`, o deduplicar la lista antes de insertar.
- **Posible desajuste de almacén.** En el cliente, el `DELETE` usa el valor del combo de almacén y los `INSERT` usan la variable `AlmacenSeleccionado`. No se verificó que siempre coincidan. Un backend nuevo debe usar un único `codAlmacen` para ambas operaciones.
- **Auditoría.** `codUser` es el usuario que otorga el permiso, no el que lo recibe. Debe salir de la sesión autenticada y no del cuerpo de la petición.

## Procedure relacionado (fuera de esta pantalla)

`GuardaAccesoEmpresa(codusu, codemp, coduser)` guarda el acceso de un usuario a una empresa. Lo invoca `clsAdmAcceso.InsertAccesoEmp`. No forma parte del flujo del botón Guardar de Accesos.
