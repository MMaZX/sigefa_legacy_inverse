@echo off
REM Instala el launcher de SIGEFA en esta PC (correr en la PC piloto, no en el servidor).
REM No abre el launcher al terminar (omitir -Abrir).
powershell -ExecutionPolicy Bypass -File "\\192.168.1.3\sgesystem\SISTEMA\actualizador\launcher\instalar-cliente.ps1"
echo.
pause
