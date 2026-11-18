<#
.SYNOPSIS
  Publica una version de SIGEFA en la carpeta del servidor para que los launchers la instalen.

.DESCRIPTION
  Toma la carpeta de salida de la compilacion (donde esta SIGEFA.exe), arma SIGEFA-<build>.zip, calcula su
  SHA256 y escribe latest.manifest AL FINAL (asi ninguna PC lee la ficha antes de que el zip este completo).

  Protecciones:
  - Usa como numero de build el que quedo compilado DENTRO de SIGEFA.exe (se lee del propio exe, no de obj).
    Si pasas -Build y no coincide, se detiene (tampoco -Forzar lo salta): el aviso de "hay version nueva" compara ese numero.
  - No publica un build menor o igual al que ya esta en el servidor (salvo -Forzar).
  - Lleva un contador persistente (contador-build.txt, junto a este script, fuera de git): historial de builds
    publicados. SIGEFA.csproj compila max(ultimo + 1, commits), asi recompilar sin commit nuevo no repite el numero.
    El servidor manda: si esta por delante del contador (otra PC u otro programador publico), el contador se alinea
    con el y hay que recompilar.
  - No publica algo compilado con cambios sin commitear (salvo -Forzar).
  - Nunca incluye carpetas "documentos" ni archivos .log (son datos de trabajo, no parte de la version).
  - Muestra a que base de datos apunta el SIGEFA.exe.config empaquetado (sin la clave).

  Conexion a la base de datos: con -HostBd, -UserBd y -PasswordBd (los tres juntos) el script reescribe ConnNegocio
  SOLO en la copia que va al zip (el SIGEFA.exe.config de la carpeta de salida no se toca). Se conservan base de
  datos y demas opciones de la cadena original; el puerto pasa a -PuertoBd (3306 por defecto). Asi quien compila no
  necesita llevar la conexion de produccion dentro del codigo. La clave nunca se imprime.

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
    # Numero de build. Opcional: si lo indicas debe coincidir con el compilado dentro de SIGEFA.exe (solo sirve para comprobar).
    [int]$Build = 0,
    # Build minimo que puede iniciar sesion (0 = ninguno obligatorio).
    [int]$MinBuild = 0,
    # Atajo: obliga a actualizar a este mismo build (MinBuild = Build).
    [switch]$Obligatoria,
    # Texto corto para la ficha (una linea).
    [string]$Notas = '',
    # Cuantos zips conservar en el servidor (los mas recientes). Minimo 1.
    [int]$ConservarZips = 3,
    # Conexion que se escribe en el SIGEFA.exe.config del zip. Se indican los tres juntos o ninguno.
    [string]$HostBd = '',
    [string]$UserBd = '',
    [string]$PasswordBd = '',
    # Puerto de MySQL; solo se aplica cuando se indica -HostBd.
    [int]$PuertoBd = 3306,
    # Salta las protecciones de "no retroceder" y de cambios sin commitear (nunca la coincidencia con el build del exe).
    [switch]$Forzar
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# Raiz del repositorio: este script vive en <raiz>\tools\actualizador.
$raiz = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent

# Pone (o agrega) una clave de la cadena de conexion, respetando el resto de la cadena.
function Poner-Clave([string]$cadena, [string]$alias, [string]$nombre, [string]$valor) {
    $patron = '(?i)(^|;)\s*(?:' + $alias + ')\s*=[^;]*'
    $regex = New-Object System.Text.RegularExpressions.Regex($patron)
    if ($regex.IsMatch($cadena)) {
        # "$" en el valor se duplica para que .NET lo tome literal en el reemplazo.
        return $regex.Replace($cadena, ('${1}' + $nombre + '=' + ($valor -replace '\$', '$$$$')), 1)
    }
    return $cadena.TrimEnd(';') + ';' + $nombre + '=' + $valor
}

# Cadena de conexion que llevara el zip a partir de la original.
function Aplicar-Conexion([string]$cadena) {
    $cadena = Poner-Clave $cadena 'Server|Host|Data Source' 'Server' $HostBd
    $cadena = Poner-Clave $cadena 'Uid|User Id|User|Username' 'Uid' $UserBd
    $cadena = Poner-Clave $cadena 'Pwd|Password' 'Pwd' $PasswordBd
    $cadena = Poner-Clave $cadena 'Port' 'Port' ([string]$PuertoBd)
    return $cadena
}

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

# Lee lo que quedo compilado DENTRO de SIGEFA.exe (VersionCompilacion). No se lee el archivo de obj: si el exe no se
# regenero (por ejemplo, estaba abierto al compilar) obj dice un numero y el exe otro, y las PCs avisarian
# "hay version nueva" para siempre.
# Se hace con Windows PowerShell 5.1 (siempre esta en Windows) en un proceso aparte: PowerShell 7 no soporta cargar un
# ensamblado "solo para reflexion", y asi el script anda igual desde cualquiera de las dos consolas.
function Leer-Compilacion([string]$exe) {
    $lector = @'
$ErrorActionPreference = 'Stop'
$bytes = [IO.File]::ReadAllBytes($env:SIGEFA_EXE_A_LEER)
$ensamblado = [Reflection.Assembly]::ReflectionOnlyLoad($bytes)
$tipo = $ensamblado.GetType('SIGEFA.Administradores.VersionApp.VersionCompilacion')
if (-not $tipo) { exit 2 }
function Valor($nombre) { $campo = $tipo.GetField($nombre); if ($campo) { $campo.GetRawConstantValue() } }
'{0}|{1}|{2}' -f (Valor 'Build'), (Valor 'Commit'), (Valor 'ConCambiosLocales')
'@
    try {
        $powershell = Join-Path $env:SystemRoot 'System32\WindowsPowerShell\v1.0\powershell.exe'
        $codificado = [Convert]::ToBase64String([Text.Encoding]::Unicode.GetBytes($lector))
        $env:SIGEFA_EXE_A_LEER = $exe
        $salida = & $powershell -NoProfile -NonInteractive -EncodedCommand $codificado 2>$null
        if ($LASTEXITCODE -ne 0) { return $null }
        $partes = ("$salida".Trim()) -split '\|'
        if ($partes.Count -ne 3) { return $null }
        [pscustomobject]@{
            Build      = [int]$partes[0]
            Commit     = [string]$partes[1]
            ConCambios = ($partes[2] -eq 'True')
        }
    }
    catch { return $null }
    finally { Remove-Item Env:\SIGEFA_EXE_A_LEER -ErrorAction SilentlyContinue }
}

# --- 0) Conexion a inyectar: los tres datos juntos y sin caracteres que rompan la cadena
$inyectaConexion = [bool]($HostBd -or $UserBd -or $PasswordBd)
if ($inyectaConexion) {
    if (-not ($HostBd -and $UserBd -and $PasswordBd)) { throw 'Indica -HostBd, -UserBd y -PasswordBd juntos (o ninguno).' }
    if ("$HostBd$UserBd$PasswordBd" -match '[;"]') { throw 'HostBd, UserBd y PasswordBd no pueden contener ; ni comillas dobles.' }
    if ($PuertoBd -le 0 -or $PuertoBd -gt 65535) { throw "-PuertoBd ($PuertoBd) no es un puerto valido." }
}

# --- 1) Validar el origen
$Origen = (Resolve-Path $Origen).Path
if (-not (Test-Path (Join-Path $Origen 'SIGEFA.exe'))) {
    throw "No encontre SIGEFA.exe en '$Origen'. Indica la carpeta de salida de la compilacion (por ejemplo bin\Release\net461)."
}
$compilado = Leer-Compilacion (Join-Path $Origen 'SIGEFA.exe')

# --- 2) Numero de build: siempre el que lleva SIGEFA.exe por dentro. Si no se puede leer, no se publica (ni con -Forzar):
#        sin saberlo, el zip podria llevar un exe de otro build.
if (-not $compilado -or $compilado.Build -le 0) {
    throw 'No pude leer el numero de build compilado dentro de SIGEFA.exe (sin git, o el exe no es de esta version). Compila de nuevo desde un checkout con git.'
}
if ($Build -gt 0 -and $Build -ne $compilado.Build) {
    # Ni siquiera -Forzar lo permite: el zip llevaria un exe que se cree de otro build y las PCs avisarian actualizacion para siempre.
    throw "-Build $Build no coincide con el build compilado dentro de SIGEFA.exe ($($compilado.Build)). Quita -Build y, si hace falta otro numero, compila de nuevo."
}
$Build = $compilado.Build
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
        $cadenaFinal = $cs.connectionString
        if ($inyectaConexion) { $cadenaFinal = Aplicar-Conexion $cadenaFinal }
        $servidorBd = [regex]::Match($cadenaFinal, '(?i)Server=([^;]+)').Groups[1].Value
        $puertoMostrado = [regex]::Match($cadenaFinal, '(?i)Port=([^;]+)').Groups[1].Value
        $baseBd = [regex]::Match($cadenaFinal, '(?i)Database=([^;]+)').Groups[1].Value
        $origenConexion = $(if ($inyectaConexion) { 'inyectada por parametros' } else { 'la del SIGEFA.exe.config de origen' })
        Write-Host "Conexion empaquetada ($origenConexion): servidor ${servidorBd}:$puertoMostrado, base $baseBd  <-- verifica que sea la correcta" -ForegroundColor Yellow
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

    if ($inyectaConexion) {
        $configZip = Join-Path $contenido 'SIGEFA.exe.config'
        if (-not (Test-Path $configZip)) { throw "No hay SIGEFA.exe.config en '$Origen': no hay donde escribir la conexion de -HostBd/-UserBd/-PasswordBd." }
        $xmlZip = New-Object System.Xml.XmlDocument
        $xmlZip.PreserveWhitespace = $true
        $xmlZip.Load($configZip)
        $nodo = $xmlZip.SelectSingleNode("/configuration/connectionStrings/add[@name='ConnNegocio']")
        if (-not $nodo) { throw "El SIGEFA.exe.config de '$Origen' no tiene la conexion 'ConnNegocio'." }
        $nodo.SetAttribute('connectionString', (Aplicar-Conexion $nodo.GetAttribute('connectionString')))
        $xmlZip.Save($configZip)
        # Comprobacion: lo que quedo escrito en la copia del zip es lo pedido.
        $xmlVerif = New-Object System.Xml.XmlDocument
        $xmlVerif.Load($configZip)
        $escrita = $xmlVerif.SelectSingleNode("/configuration/connectionStrings/add[@name='ConnNegocio']").GetAttribute('connectionString')
        if ($escrita -notmatch ('(?i)(^|;)\s*Server=' + [regex]::Escape($HostBd) + '(;|$)')) { throw 'No pude confirmar la conexion escrita en el zip. No se publico nada.' }
    }

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
