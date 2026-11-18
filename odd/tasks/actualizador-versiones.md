# actualizador-versiones

Objetivo: cliente liviano por PC que instala y mantiene actualizado SIGEFA desde el servidor, con aviso obligatorio y bloqueo de login si el cliente esta desactualizado.
Rama: `feat/actualizador-versiones` (desde `main` b4c46c5). Entrega: ask-on-risk. Runner: xunit (`dotnet test SIGEFA.Tests`, net48), ejecutado en la VM win10 (192.168.122.44) porque en Linux no compila.

## Specs

- S1. Fuente de la version: "un archivo con la firma que pese 1 kb" que el cliente consulta; "una vez que lee la firma entiende si la firma coincide o no". Es el manifiesto `latest.json` en el share del servidor.
- S2. Consulta periodica: "leer cada 3 minutos si existe una nueva versión por una clave sha256 o algo que me permita saber la versión".
- S3. Aviso imperativo: "si el programa detecta que hay una versión DEBE mostrar en la parte superior una version de "actualizar cliente sigefa"".
- S4. Cliente liviano: "cada computador tenga un ejecutable super liviano que lo que haga es instalar el SIGEFA, en una version" y "ya tenga preparada la forma de descargar la version desde el servidor para poder tener el cliente siempre actualizado".
- S5. Bloqueo: "no se puede iniciar sesión si es que el cliente no está actualizado".
- S6. Sin sobrecargar el servidor: el cliente "se conecta a mysql correctamente ... sin hacer que el servidor se sobrecargue o pase algo diferente".
- S7. Alcance de red (usuario, 2026-11-18): "si todas las pcs, conectan a la lan del servidor no hay sucursales fuera aun, asi que normal y sin problema". Canal = share SMB, sin HTTP.

Supuestos (no pedidos por el usuario, a confirmar si cambian el alcance):
- A1. La version se ordena por un numero de build creciente; el SHA256 solo verifica integridad (un hash no dice cual es mas nuevo).
- A2. Instalacion por usuario en `%LocalAppData%\SIGEFA\versions\<build>\`, sin admin, con rollback.
- A3. Si el manifiesto no responde, no se bloquea (fail-open) para que una caida del servidor no pare las ventas.
- A4. Dentro de la sesion solo hay aviso; el bloqueo es al iniciar sesion (no se cierra la app a mitad de una venta).
- A5. Prerrequisito: los comprobantes SUNAT (XML/CDR) hoy se escriben junto al exe (el share). Con instalacion local deben ir a una ruta configurable.
- A6. Firma del manifiesto (RSA) y gate en MySQL quedan para una fase posterior.

## Tasks

- [ ] T0 (A5) RutaDocumentos configurable (`RutasSistema`), mismo comportamiento por defecto. Route: inline.
- [ ] T1 (S1,S2,A1) Dominio puro: leer manifiesto, comparar builds, decidir si hay/obliga actualizar, verificar SHA256. Route: inline.
- [ ] T2 (A1) Numero de build en `VersionCompilacion` (target de MSBuild). Route: inline.
- [ ] T3 (S4,A2) Instalador por versiones: descargar zip, verificar, extraer, puntero `current`, rollback. Route: inline.
- [ ] T4 (S4) Proyecto `SIGEFA.Launcher` (net461, sin dependencias) y su acceso directo. Route: inline.
- [ ] T5 (S2,S3,A3) Polling cada 3 min en `mdi_Menu` y barra superior "Actualizar cliente SIGEFA". Route: inline.
- [ ] T6 (S5,A3,A4) Bloqueo de login si `build < minBuild`. Route: inline.
- [ ] T7 (S1) Script `publicar.ps1` (Release, zip, sha256, manifiesto al final). Route: inline.
- [ ] T8 Verificacion en VM, prueba manual extremo a extremo y docs.

## Log

- L1 (2026-11-18, usuario): investigacion pedida, luego "ok armalo en una rama aparte esta funcionalidad que quiero crear contigo porfa, y si todas las pcs, conectan a la lan del servidor no hay sucursales fuera aun, asi que normal y sin problema, por el momento no hay ninguna ip publica."
  - Pedido original de la investigacion: "vamos a armar un sub-programa que se encargue de leer cada 3 minutos si existe una nueva versión por una clave sha256 o algo que me permita saber la versión si es que requiere de una o no."
- L2 (2026-11-18): hallazgos de la investigacion. El exe corre en cada PC (no en el servidor) y la conexion MySQL ya es directa (`app.config` ConnNegocio). `app.config` tiene `loadFromRemoteSources=true`. `SIGEFA.csproj` es net461; tests net48. `AssemblyVersion` fija en 1.0.0.0; `GenerarVersionCompilacion` ya genera Commit/Rama/Asunto/Fecha. Comprobantes: `Program.Carpeta*` = `./documentos/...` (relativo al cwd, 39 usos) y `Facturacion.cs` usa `BaseDirectory + "documentos\"`.
- L3: pendiente de verificar en el share real: contenido, tamaño del paquete, "Iniciar en" del acceso directo, Crystal runtime en las PCs.
