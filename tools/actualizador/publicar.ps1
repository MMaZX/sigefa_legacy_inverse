<#
.SYNOPSIS
  Publica una version de SIGEFA en la carpeta del servidor para que los launchers la instalen.

.DESCRIPTION
  Toma la carpeta de salida de la compilacion (donde esta SIGEFA.exe), arma SIGEFA-<build>.zip, calcula su
  SHA256 y escribe latest.manifest AL FINAL (asi ninguna PC lee la ficha antes de que el zip este completo).

  Protecciones:
  - Usa como numero de build el que quedo compilado dentro de SIGEFA.exe (obj\<config>\...\VersionCompilacion.g.cs).
    Si pasas -Build y no coincide, se detiene: el aviso de "hay version nueva" compara ese numero.
  - No publica un build menor o igual al que ya esta en el servidor (salvo -Forzar).
  - Lleva un contador persistente (contador-build.txt, junto a este script, fuera de git): historial de builds
    publicados. SIGEFA.csproj compila max(ultimo + 1, commits), asi recompilar sin commit nuevo no repite el numero.
    El servidor manda: si esta por delante del contador (otra PC u otro programador publico), el contador se alinea
    con el y hay que recompilar.
  - No publica algo compilado con cambios sin commitear (salvo -Forzar).
  - Nunca incluye carpetas "documentos" ni archivos .log (son datos de trabajo, no parte de la version).
  - Muestra a que base de datos apunta el SIGEFA.exe.config empaquetado (sin la clave).

  Actualizacion obligatoria: -Obligatoria exige este build para iniciar sesion (equivale a -MinBuild <build>).
  Sin ella, las PCs reciben el aviso parpadeante pero pueden seguir trabajando hasta actualizar.

.EXAMPLE
  .\publicar.ps1 -Origen C:\ruta\sigefa_legacy\bin\Release\net461 -Servidor \\192.168.1.3\sgesystem\SISTEMA\actualizador
#>
[CmdletBinding()]
param(
    # Carpeta con SIGEFA.exe y todo lo que necesita (la salida de compilar en Release).
    [Parameter(Mandatory)][string]$Origen,
    # Carpeta del servidor que leen los clientes.
    [Parameter(Mandatory)][string]$Servidor,
    # Numero de build. Si no lo indicas, se toma el compilado dentro de SIGEFA.exe.
    [int]$Build = 0,
    # Build minimo que puede iniciar sesion (0 = ninguno obligatorio).
    [int]$MinBuild = 0,
    # Atajo: obliga a actualizar a este mismo build (MinBuild = Build).
    [switch]$Obligatoria,
    # Texto corto para la ficha (una linea).
    [string]$Notas = '',
    # Cuantos zips conservar en el servidor (los mas recientes). Minimo 1.
    [int]$ConservarZips = 3,
    # Salta las protecciones de numero de build y de cambios sin commitear.
    [switch]$Forzar
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# Raiz del repositorio: este script vive en <raiz>\tools\actualizador.
$raiz = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent

# Historial persistente de builds publicados: una linea "build|fecha|origen" por publicacion. La lee SIGEFA.csproj.
$archivoContador = Join-Path $PSScriptRoot 'contador-build.txt'

function Leer-UltimoContador {
    if (-not (Test-Path $archivoContador)) { return 0 }
    $ultimo = 0
    foreach ($linea in Get-Content $archivoContador) {
        $m = [regex]::Match($linea, '^\s*(\d+)\|')
        if ($m.Success -and [int]$m.Groups[1].Value -gt $ultimo) { $ultimo = [int]$m.Groups[1].Value }
    }
    return $ultimo
}

# Agrega un build al historial solo si supera al ultimo (la ultima linea siempre es el maximo, que es lo que lee el csproj).
function Registrar-Contador([int]$numero, [string]$origen) {
    if ($numero -le (Leer-UltimoContador)) { return $false }
    $linea = '{0}|{1}|{2}' -f $numero, (Get-Date -Format 'yyyy-MM-dd HH:mm'), $origen
    [IO.File]::AppendAllText($archivoContador, $linea + "`n", (New-Object Text.UTF8Encoding($false)))
    return $true
}

# Lee lo que MSBuild dejo compilado dentro de SIGEFA.exe para la configuracion indicada (Release, Debug...).
function Leer-Compilacion([string]$configuracion) {
    $carpetaObj = Join-Path $raiz 'obj'
    if (-not (Test-Path $carpetaObj)) { return $null }
    $archivo = Get-ChildItem $carpetaObj -Recurse -Filter 'VersionCompilacion.g.cs' -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -match ('\\' + [regex]::Escape($configuracion) + '\\') } |
        Sort-Object LastWriteTime -Descending | Select-Object -First 1
    if (-not $archivo) { return $null }
    $texto = Get-Content $archivo.FullName -Raw
    $b = [regex]::Match($texto, 'Build = (\d+);')
    [pscustomobject]@{
        Build      = $(if ($b.Success) { [int]$b.Groups[1].Value } else { 0 })
        Commit     = [regex]::Match($texto, 'Commit = "([^"]*)"').Groups[1].Value
        ConCambios = ($texto -match 'ConCambiosLocales = true')
    }
}

# --- 1) Validar el origen
$Origen = (Resolve-Path $Origen).Path
if (-not (Test-Path (Join-Path $Origen 'SIGEFA.exe'))) {
    throw "No encontre SIGEFA.exe en '$Origen'. Indica la carpeta de salida de la compilacion (por ejemplo bin\Release\net461)."
}
$configuracion = Split-Path (Split-Path $Origen -Parent) -Leaf
$compilado = Leer-Compilacion $configuracion

# --- 2) Numero de build
if ($Build -le 0) {
    if (-not $compilado -or $compilado.Build -le 0) {
        throw 'No pude saber el numero de build. Compila desde un checkout con git (o indica -Build y -Forzar).'
    }
    $Build = $compilado.Build
}
elseif ($compilado -and $compilado.Build -gt 0 -and $compilado.Build -ne $Build -and -not $Forzar) {
    throw "-Build $Build no coincide con el build compilado dentro de SIGEFA.exe ($($compilado.Build)). Quita -Build o usa -Forzar."
}
if ($compilado -and $compilado.ConCambios -and -not $Forzar) {
    throw 'SIGEFA.exe se compilo con cambios sin commitear: no se puede saber que codigo contiene. Haz commit y compila de nuevo (o usa -Forzar).'
}
if ($Obligatoria) { $MinBuild = $Build }
if ($MinBuild -gt $Build) { throw "-MinBuild ($MinBuild) no puede ser mayor que -Build ($Build)." }
if ($ConservarZips -lt 1) { $ConservarZips = 1 }

# --- 3) Servidor: no retroceder
New-Item -ItemType Directory $Servidor -Force | Out-Null
$rutaManifiesto = Join-Path $Servidor 'latest.manifest'
if (Test-Path $rutaManifiesto) {
    $actual = [regex]::Match((Get-Content $rutaManifiesto -Raw), '(?m)^\s*build\s*=\s*(\d+)')
    if ($actual.Success) {
        $enServidor = [int]$actual.Groups[1].Value
        # El servidor es la fuente de verdad: si esta por delante del contador local, se alinea.
        if (Registrar-Contador $enServidor 'servidor') {
            Write-Host "Contador alineado con el servidor: ultimo build publicado = $enServidor." -ForegroundColor Yellow
        }
        if ($Build -le $enServidor -and -not $Forzar) {
            throw "El servidor ya tiene la version $enServidor (el contador quedo en $enServidor). Compila de nuevo en Release: saldra el build $($enServidor + 1) o mayor. Con -Forzar publicas igual."
        }
    }
}

# --- 4) Mostrar a que base de datos apunta lo que se esta por publicar (nunca la clave)
$rutaConfig = Join-Path $Origen 'SIGEFA.exe.config'
if (Test-Path $rutaConfig) {
    try {
        $xml = [xml](Get-Content $rutaConfig -Raw)
        $cs = $xml.configuration.connectionStrings.add | Where-Object { $_.name -eq 'ConnNegocio' } | Select-Object -First 1
        $servidorBd = [regex]::Match($cs.connectionString, '(?i)Server=([^;]+)').Groups[1].Value
        $baseBd = [regex]::Match($cs.connectionString, '(?i)Database=([^;]+)').Groups[1].Value
        Write-Host "Conexion empaquetada en SIGEFA.exe.config: servidor $servidorBd, base $baseBd  <-- verifica que sea la correcta" -ForegroundColor Yellow
    }
    catch { Write-Warning 'No pude leer la conexion de SIGEFA.exe.config.' }
}
else { Write-Warning "No hay SIGEFA.exe.config en '$Origen': la version no llevara cadena de conexion." }

# --- 5) Armar el zip en una carpeta temporal local, verificarlo y recien despues subirlo
$temporal = Join-Path ([IO.Path]::GetTempPath()) ('sigefa_pub_' + [Guid]::NewGuid().ToString('N'))
try {
    $contenido = Join-Path $temporal 'contenido'
    New-Item -ItemType Directory $contenido -Force | Out-Null
    # Se excluyen las carpetas "documentos" y los .log: son datos de trabajo, no parte de la version.
    robocopy $Origen $contenido /E /XD documentos /XF *.log /NFL /NDL /NJH /NJS /NP | Out-Null
    if ($LASTEXITCODE -ge 8) { throw "robocopy fallo (codigo $LASTEXITCODE)." }

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $zipLocal = Join-Path $temporal "SIGEFA-$Build.zip"
    [IO.Compression.ZipFile]::CreateFromDirectory($contenido, $zipLocal, [IO.Compression.CompressionLevel]::Optimal, $false)
    $sha = (Get-FileHash $zipLocal -Algorithm SHA256).Hash.ToLowerInvariant()
    $tamano = (Get-Item $zipLocal).Length

    $zipServidor = Join-Path $Servidor "SIGEFA-$Build.zip"
    Copy-Item $zipLocal "$zipServidor.parcial" -Force
    if ((Get-FileHash "$zipServidor.parcial" -Algorithm SHA256).Hash.ToLowerInvariant() -ne $sha) {
        Remove-Item "$zipServidor.parcial" -Force
        throw 'La copia del zip al servidor llego danada. No se publico nada.'
    }
    Move-Item "$zipServidor.parcial" $zipServidor -Force
}
finally {
    Remove-Item $temporal -Recurse -Force -ErrorAction SilentlyContinue
}

# --- 6) Escribir la ficha AL FINAL (primero a un temporal y luego se renombra)
$lineas = @("build=$Build")
if ($MinBuild -gt 0) { $lineas += "minbuild=$MinBuild" }
$lineas += "sha256=$sha"
$lineas += "archivo=SIGEFA-$Build.zip"
if ($tamano -lt [int]::MaxValue) { $lineas += "tamano=$tamano" }
if ($compilado -and $compilado.Commit) { $lineas += "commit=$($compilado.Commit)" }
$notaLimpia = ($Notas -replace '[\r\n]+', ' ').Trim()
if ($notaLimpia) { $lineas += "notas=$notaLimpia" }
$rutaTemporal = Join-Path $Servidor 'latest.manifest.tmp'
[IO.File]::WriteAllText($rutaTemporal, (($lineas -join "`n") + "`n"), (New-Object Text.UTF8Encoding($false)))
Move-Item $rutaTemporal $rutaManifiesto -Force

# --- 7) Limpiar zips antiguos (conserva los mas recientes). El recien publicado NUNCA se borra, ni siquiera si se
#        publico un build menor con -Forzar (si no, la ficha apuntaria a un archivo que ya no existe).
Get-ChildItem $Servidor -Filter 'SIGEFA-*.zip' | ForEach-Object {
    $n = [regex]::Match($_.Name, '^SIGEFA-(\d+)\.zip$')
    if ($n.Success) { [pscustomobject]@{ Build = [int]$n.Groups[1].Value; Ruta = $_.FullName } }
} | Where-Object { $_.Build -ne $Build } | Sort-Object Build -Descending | Select-Object -Skip ($ConservarZips - 1) | ForEach-Object {
    Remove-Item $_.Ruta -Force
    Write-Host "  zip antiguo eliminado: $(Split-Path $_.Ruta -Leaf)"
}

$commitContador = $(if ($compilado -and $compilado.Commit) { $compilado.Commit } else { 'sin-commit' })
[void](Registrar-Contador $Build $commitContador)

Write-Host ''
Write-Host "Publicada la version $Build" -ForegroundColor Green
Write-Host "  zip:      $zipServidor ($([math]::Round($tamano / 1MB, 1)) MB)"
Write-Host "  sha256:   $sha"
Write-Host "  manifest: $rutaManifiesto"
