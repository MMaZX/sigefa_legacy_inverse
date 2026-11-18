<#
.SYNOPSIS
  Publica el launcher compilado en el servidor, para que instalar-cliente.ps1 lo copie a cada PC.

.DESCRIPTION
  Hace dos cosas:
  1. Instalacion nueva: copia SIGEFA.Launcher.exe y su .config a <Servidor>\launcher\ y deja en el .config la ruta del
     servidor (RutaActualizacion) para que las PCs no tengan que editar nada (lo usa instalar-cliente.ps1).
  2. Actualizacion de las PCs que ya lo tienen: arma SIGEFA.Launcher-<build>.zip (con el exe), calcula su SHA256 y
     escribe <Servidor>\launcher.manifest AL FINAL. Al abrirse, cada launcher compara su build con el de esa ficha y,
     si hay uno mas nuevo, se reemplaza solo (sigue funcionando si el servidor no responde).

  El build del launcher sale de su propio contador (contador-launcher.txt, junto a este script, fuera de git):
  SIGEFA.Launcher.csproj compila ultimo + 1. El servidor manda: si ya tiene un build igual o mayor al compilado, el
  contador se alinea con el y hay que compilar de nuevo.

  Un launcher roto es el peor caso (ninguna PC podria arreglarlo sola), por eso: el launcher viejo comprueba el sha256
  y que el nuevo arranca antes de cambiarse, y guarda el anterior como SIGEFA.Launcher.exe.anterior. Aun asi, prueba
  cada launcher nuevo en una PC antes de publicarlo.

.EXAMPLE
  .\publicar-launcher.ps1 -Servidor \\192.168.1.3\sgesystem\SISTEMA\actualizador
#>
[CmdletBinding()]
param(
    # Carpeta del servidor que leen los clientes (la misma que usa publicar.ps1).
    [Parameter(Mandatory)][string]$Servidor,
    # Salida de compilar SIGEFA.Launcher en Release. Por defecto SIGEFA.Launcher\bin\Release\net461 del repositorio.
    [string]$Origen = '',
    # Cuantos zips del launcher conservar en el servidor (los mas recientes). Minimo 1.
    [int]$ConservarZips = 3,
    # Publica aunque el build compilado no sea mayor que el del servidor.
    [switch]$Forzar
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$raiz = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
if (-not $Origen) { $Origen = Join-Path $raiz 'SIGEFA.Launcher\bin\Release\net461' }
$Origen = (Resolve-Path $Origen).Path
if ($ConservarZips -lt 1) { $ConservarZips = 1 }

# Historial persistente de builds del launcher: una linea "build|fecha|origen" por publicacion (la lee SIGEFA.Launcher.csproj).
$archivoContador = Join-Path $PSScriptRoot 'contador-launcher.txt'

function Leer-UltimoContador {
    if (-not (Test-Path $archivoContador)) { return 0 }
    $ultimo = 0
    foreach ($linea in Get-Content $archivoContador) {
        $m = [regex]::Match($linea, '^\s*(\d+)\|')
        if ($m.Success -and [int]$m.Groups[1].Value -gt $ultimo) { $ultimo = [int]$m.Groups[1].Value }
    }
    return $ultimo
}

# Solo agrega si supera al ultimo: la ultima linea siempre es el maximo.
function Registrar-Contador([int]$numero, [string]$origen) {
    if ($numero -le (Leer-UltimoContador)) { return $false }
    $linea = '{0}|{1}|{2}' -f $numero, (Get-Date -Format 'yyyy-MM-dd HH:mm'), $origen
    [IO.File]::AppendAllText($archivoContador, $linea + "`n", (New-Object Text.UTF8Encoding($false)))
    return $true
}

# Build del exe que se va a publicar: se le pregunta al propio exe (--build-launcher devuelve su build como codigo de
# salida), asi el numero siempre es el del archivo que sube y nunca el de una compilacion anterior.
function Leer-BuildDelExe([string]$exe) {
    $proceso = Start-Process -FilePath $exe -ArgumentList '--build-launcher' -PassThru -WindowStyle Hidden
    if (-not $proceso.WaitForExit(20000)) {
        try { $proceso.Kill() } catch { }
        throw "El launcher no respondio a --build-launcher en 20 segundos. Compila SIGEFA.Launcher de nuevo en Release."
    }
    return [int]$proceso.ExitCode
}

$configuracion = Split-Path (Split-Path $Origen -Parent) -Leaf
$build = Leer-BuildDelExe (Join-Path $Origen 'SIGEFA.Launcher.exe')
if ($build -le 0) { throw "No pude leer el build del SIGEFA.Launcher.exe de '$Origen'. Compila SIGEFA.Launcher de nuevo en $configuracion (un exe anterior a la autoactualizacion no sabe decir su build)." }

# El servidor manda: si ya tiene un launcher igual o mayor, se alinea el contador y hay que recompilar.
$rutaFicha = Join-Path $Servidor 'launcher.manifest'
if (Test-Path $rutaFicha) {
    $actual = [regex]::Match((Get-Content $rutaFicha -Raw), '(?m)^\s*build\s*=\s*(\d+)')
    if ($actual.Success) {
        $enServidor = [int]$actual.Groups[1].Value
        if (Registrar-Contador $enServidor 'servidor') {
            Write-Host "Contador del launcher alineado con el servidor: ultimo build publicado = $enServidor." -ForegroundColor Yellow
        }
        if ($build -le $enServidor -and -not $Forzar) {
            throw "El servidor ya tiene el launcher $enServidor (el contador quedo en $enServidor). Compila SIGEFA.Launcher de nuevo en ${configuracion}: saldra el build $($enServidor + 1) o mayor. Con -Forzar publicas igual."
        }
    }
}

foreach ($archivo in 'SIGEFA.Launcher.exe', 'SIGEFA.Launcher.exe.config') {
    if (-not (Test-Path (Join-Path $Origen $archivo))) {
        throw "Falta $archivo en '$Origen'. Compila SIGEFA.Launcher en Release primero."
    }
}

# --- 1) Zip versionado + ficha (actualizacion de las PCs que ya tienen el launcher)
New-Item -ItemType Directory $Servidor -Force | Out-Null
$temporal = Join-Path ([IO.Path]::GetTempPath()) ('sigefa_pub_launcher_' + [Guid]::NewGuid().ToString('N'))
try {
    $contenido = Join-Path $temporal 'contenido'
    New-Item -ItemType Directory $contenido -Force | Out-Null
    Copy-Item (Join-Path $Origen 'SIGEFA.Launcher.exe') $contenido
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $zipLocal = Join-Path $temporal "SIGEFA.Launcher-$build.zip"
    [IO.Compression.ZipFile]::CreateFromDirectory($contenido, $zipLocal, [IO.Compression.CompressionLevel]::Optimal, $false)
    $sha = (Get-FileHash $zipLocal -Algorithm SHA256).Hash.ToLowerInvariant()
    $tamano = (Get-Item $zipLocal).Length

    $zipServidor = Join-Path $Servidor "SIGEFA.Launcher-$build.zip"
    Copy-Item $zipLocal "$zipServidor.parcial" -Force
    if ((Get-FileHash "$zipServidor.parcial" -Algorithm SHA256).Hash.ToLowerInvariant() -ne $sha) {
        Remove-Item "$zipServidor.parcial" -Force
        throw 'La copia del zip del launcher al servidor llego danada. No se publico nada.'
    }
    Move-Item "$zipServidor.parcial" $zipServidor -Force
}
finally {
    Remove-Item $temporal -Recurse -Force -ErrorAction SilentlyContinue
}

# --- 2) Instalacion nueva: lo que copia instalar-cliente.ps1
$destino = Join-Path $Servidor 'launcher'
New-Item -ItemType Directory $destino -Force | Out-Null
Copy-Item (Join-Path $Origen 'SIGEFA.Launcher.exe') $destino -Force
Copy-Item (Join-Path $Origen 'SIGEFA.Launcher.exe.config') $destino -Force
# Tambien se deja el instalador para que cada PC lo corra directo desde el servidor.
Copy-Item (Join-Path $PSScriptRoot 'instalar-cliente.ps1') $destino -Force

# Dejar la ruta del servidor ya escrita en el .config que se reparte.
$rutaConfig = Join-Path $destino 'SIGEFA.Launcher.exe.config'
$xml = New-Object Xml.XmlDocument
$xml.Load($rutaConfig)
$nodo = $xml.SelectSingleNode("//appSettings/add[@key='RutaActualizacion']")
if (-not $nodo) { throw "El .config del launcher no tiene la clave RutaActualizacion." }
$nodo.SetAttribute('value', $Servidor)
$xml.Save($rutaConfig)

# --- 3) La ficha va AL FINAL (primero a un temporal y luego se renombra): ningun launcher la lee antes de que el zip este completo
$lineas = @("build=$build", "sha256=$sha", "archivo=SIGEFA.Launcher-$build.zip", "tamano=$tamano")
$rutaTemporal = Join-Path $Servidor 'launcher.manifest.tmp'
[IO.File]::WriteAllText($rutaTemporal, (($lineas -join "`n") + "`n"), (New-Object Text.UTF8Encoding($false)))
Move-Item $rutaTemporal $rutaFicha -Force
[void](Registrar-Contador $build 'publicado')

# --- 4) Limpiar zips antiguos del launcher. El recien publicado NUNCA se borra.
Get-ChildItem $Servidor -Filter 'SIGEFA.Launcher-*.zip' | ForEach-Object {
    $n = [regex]::Match($_.Name, '^SIGEFA\.Launcher-(\d+)\.zip$')
    if ($n.Success) { [pscustomobject]@{ Build = [int]$n.Groups[1].Value; Ruta = $_.FullName } }
} | Where-Object { $_.Build -ne $build } | Sort-Object Build -Descending | Select-Object -Skip ($ConservarZips - 1) | ForEach-Object {
    Remove-Item $_.Ruta -Force
    Write-Host "  zip antiguo eliminado: $(Split-Path $_.Ruta -Leaf)"
}

Write-Host "Launcher $build publicado en $Servidor" -ForegroundColor Green
Write-Host "  zip:      $zipServidor ($([math]::Round($tamano / 1KB, 0)) KB)"
Write-Host "  sha256:   $sha"
Write-Host "  ficha:    $rutaFicha"
Write-Host "Instalacion nueva en $destino" 
Write-Host "  RutaActualizacion = $Servidor"
Write-Host "Cada PC lo instala con:  powershell -ExecutionPolicy Bypass -File `"$destino\instalar-cliente.ps1`" -Abrir"
