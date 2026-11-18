<#
.SYNOPSIS
  Publica el launcher compilado en el servidor, para que instalar-cliente.ps1 lo copie a cada PC.

.DESCRIPTION
  Copia SIGEFA.Launcher.exe y su .config a <Servidor>\launcher\ y deja en el .config la ruta del servidor
  (RutaActualizacion) para que las PCs no tengan que editar nada. Se hace una vez, y de nuevo solo si el
  launcher mismo cambia (es un programa chico y estable: las versiones de SIGEFA no lo tocan).

.EXAMPLE
  .\publicar-launcher.ps1 -Servidor \\192.168.1.3\sgesystem\SISTEMA\actualizador
#>
[CmdletBinding()]
param(
    # Carpeta del servidor que leen los clientes (la misma que usa publicar.ps1).
    [Parameter(Mandatory)][string]$Servidor,
    # Salida de compilar SIGEFA.Launcher en Release. Por defecto SIGEFA.Launcher\bin\Release\net461 del repositorio.
    [string]$Origen = ''
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$raiz = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
if (-not $Origen) { $Origen = Join-Path $raiz 'SIGEFA.Launcher\bin\Release\net461' }
$Origen = (Resolve-Path $Origen).Path

foreach ($archivo in 'SIGEFA.Launcher.exe', 'SIGEFA.Launcher.exe.config') {
    if (-not (Test-Path (Join-Path $Origen $archivo))) {
        throw "Falta $archivo en '$Origen'. Compila SIGEFA.Launcher en Release primero."
    }
}

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

Write-Host "Launcher publicado en $destino" -ForegroundColor Green
Write-Host "  RutaActualizacion = $Servidor"
Write-Host "Cada PC lo instala con:  powershell -ExecutionPolicy Bypass -File `"$destino\instalar-cliente.ps1`" -Abrir"
