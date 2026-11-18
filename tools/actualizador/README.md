# Actualizador de SIGEFA: scripts

Cada PC abre `SIGEFA.Launcher.exe`. El launcher lee `latest.manifest` del servidor, instala la version nueva si hay
una y abre SIGEFA. Si el servidor no responde, abre la version que ya tiene instalada.

En el servidor (`\\192.168.1.3\sgesystem\SISTEMA\actualizador`) solo hay:

```
actualizador\
  latest.manifest        <- la "ficha": build, sha256, archivo del zip
  SIGEFA-<build>.zip     <- la version completa (SIGEFA.exe suelto en la raiz del zip)
  launcher\              <- SIGEFA.Launcher.exe + .config + instalar-cliente.ps1, para instalar en cada PC
```

Los clientes solo necesitan **lectura** en esa carpeta. La escritura es solo para quien publica.
Los comprobantes electronicos **nunca** van al servidor: siguen en `C:\DOCUMENTOS-<RUC>` de cada PC.

## Publicar una version nueva (en la PC donde compilas)

1. Compila SIGEFA en **Release**. La salida queda en `bin\Release\net461`.
2. Corre:

```powershell
.\tools\actualizador\publicar.ps1 `
    -Origen   .\bin\Release\net461 `
    -Servidor \\192.168.1.3\sgesystem\SISTEMA\actualizador `
    -Notas    "Que cambia en esta version"
```

El script toma el numero de build que quedo compilado dentro de `SIGEFA.exe`, arma el zip, lo verifica, lo sube y
escribe la ficha al final. Se detiene si: el build no coincide con el compilado, el servidor ya tiene uno igual o
mayor, o `SIGEFA.exe` se compilo con cambios sin commitear. Antes de publicar muestra a que base de datos apunta el
`SIGEFA.exe.config` empaquetado: **verificala**.

**Contador de builds.** `tools\actualizador\contador-build.txt` (ignorado por git, una linea `build|fecha|origen` por
publicacion) guarda el historial. `SIGEFA.csproj` compila `max(ultimo + 1, commits de la rama)`: si recompilas sin un
commit nuevo despues de publicar, el numero sube igual. El servidor manda: si otra PC u otro programador publico algo
mayor, `publicar.ps1` alinea el contador con `latest.manifest`, se detiene y hay que **compilar de nuevo**. Para empezar
en una PC nueva basta con ejecutar `publicar.ps1` una vez (falla y alinea) o crear el archivo con `<build>|x|servidor`.

Por defecto, las PCs reciben el aviso (boton **"Buscar Actualizaciones"** arriba a la derecha, azul con un punto de aviso; rojo si es obligatoria) pero pueden
seguir trabajando. `-Obligatoria` (o `-MinBuild <n>`) ademas **no deja iniciar sesion** a las PCs con un build menor:
usalo cuando la version nueva cambia algo que el sistema viejo no puede convivir (por ejemplo, la base de datos).

## Instalar el launcher (una vez)

1. Compila `SIGEFA.Launcher\SIGEFA.Launcher.csproj` en **Release**.
2. Publicalo en el servidor (una sola vez, o si el launcher cambia):

```powershell
.\tools\actualizador\publicar-launcher.ps1 -Servidor \\192.168.1.3\sgesystem\SISTEMA\actualizador
```

3. En cada PC (sin administrador), un solo comando:

```powershell
powershell -ExecutionPolicy Bypass -File "\\192.168.1.3\sgesystem\SISTEMA\actualizador\launcher\instalar-cliente.ps1" -Abrir
```

Crea un acceso directo **"SIGEFA (launcher)"** en el escritorio y **no toca el acceso directo anterior**.
Cuando confirmes que funciona, retiras el viejo.

## Donde queda todo en cada PC

```
%LocalAppData%\SIGEFA\
  launcher\               SIGEFA.Launcher.exe + .config
  versions\<build>\       cada version de SIGEFA (se conservan la activa y la anterior)
  current.txt             numero de la version activa
  documentos\             copias de trabajo (XML sin firmar, CDR temporales)
  launcher.log            por que una PC no se actualizo
```
