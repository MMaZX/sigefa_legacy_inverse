<#
.SYNOPSIS
  Instala el launcher de SIGEFA en esta PC y crea el acceso directo. Se corre una vez por PC, sin permisos de administrador.

.DESCRIPTION
  Copia el launcher desde <Servidor>\launcher\ a una carpeta local (no se ejecuta desde el servidor, asi que la PC
  no depende de el para arrancar) y crea un acceso directo en el escritorio. NO borra ni cambia el acceso directo
  anterior de SIGEFA: queda el viejo hasta que confirmes que el nuevo funciona.
  El launcher instala SIGEFA solo la primera vez que se abre.

.EXAMPLE
  .\instalar-cliente.ps1 -Abrir
#>
[CmdletBinding()]
param(
    [string]$Servidor = '\\192.168.1.3\sgesystem\SISTEMA\actualizador',
    # Nombre del acceso directo. Distinto del actual para no pisarlo.
    [string]$NombreAcceso = 'SIGEFA (launcher)',
    # Carpeta local donde queda el launcher.
    [string]$Destino = (Join-Path $env:LOCALAPPDATA 'SIGEFA\launcher'),
    # Carpeta del acceso directo. Por defecto, el escritorio del usuario.
    [string]$CarpetaAcceso = [Environment]::GetFolderPath('Desktop'),
    # Abre el launcher al terminar.
    [switch]$Abrir
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$origen = Join-Path $Servidor 'launcher'
if (-not (Test-Path (Join-Path $origen 'SIGEFA.Launcher.exe'))) {
    throw "No encontre el launcher en '$origen'. Primero hay que correr publicar-launcher.ps1 (o revisa que el servidor este accesible)."
}

New-Item -ItemType Directory $Destino -Force | Out-Null
try {
    Copy-Item (Join-Path $origen '*') $Destino -Force
}
catch {
    throw "No pude copiar el launcher (cierralo si esta abierto y reintenta): $($_.Exception.Message)"
}
$exe = Join-Path $Destino 'SIGEFA.Launcher.exe'

New-Item -ItemType Directory $CarpetaAcceso -Force | Out-Null
$rutaAcceso = Join-Path $CarpetaAcceso "$NombreAcceso.lnk"
$acceso = (New-Object -ComObject WScript.Shell).CreateShortcut($rutaAcceso)
$acceso.TargetPath = $exe
$acceso.WorkingDirectory = $Destino
$acceso.IconLocation = "$exe,0"
$acceso.Description = 'SIGEFA (se actualiza solo desde el servidor)'
$acceso.Save()

Write-Host 'Listo.' -ForegroundColor Green
Write-Host "  Launcher:       $exe"
Write-Host "  Acceso directo: $rutaAcceso"
Write-Host '  La primera vez que lo abras instalara SIGEFA (unos segundos).'
if ($Abrir) { Start-Process $exe }
