Add-Type -Path "c:\Users\qemu\Documents\Debug_gr\MySql.Data.dll"

$connectionString = "Server=192.168.1.129;Database=database_multi_final;Uid=root;Pwd=fulanito;Port=3307;default command timeout=30"
$conn = New-Object MySql.Data.MySqlClient.MySqlConnection($connectionString)

$procs = @(
    "ReporteVentasDiarioPdfCajaMovimiento",
    "ReporteNotasCreditosExcel",
    "ReporteVentasDiarioExcelAgrup",
    "LIstadoAlmacenXUbicacionExcel",
    "ListaCajaChicaDiaria",
    "ReporteVentasDiarioExcelPagosAgrup",
    "ReporteVentasCreditoDiarioExcel",
    "ListaCajaEgresos",
    "ListaNotasCreditos",
    "ListaCajaIngresos",
    "ListaCajaIngresosTarjeta",
    "ListaCajaIngresosTransferencia"
)

try {
    $conn.Open()
    foreach ($proc in $procs) {
        Write-Output "`n=========================================="
        Write-Output "--- PROCEDURE: $proc ---"
        Write-Output "=========================================="
        try {
            $query = "SHOW CREATE PROCEDURE database_multi_final.$proc"
            $cmd = New-Object MySql.Data.MySqlClient.MySqlCommand($query, $conn)
            $reader = $cmd.ExecuteReader()
            if ($reader.Read()) {
                Write-Output $reader.GetString(2)
            } else {
                Write-Output "Procedure $proc not found."
            }
            $reader.Close()
        } catch {
            Write-Output "Error reading procedure ${proc}: $($_.Exception.Message)"
        }
    }
} catch {
    Write-Error $_.Exception.Message
} finally {
    if ($conn.State -eq [System.Data.ConnectionState]::Open) {
        $conn.Close()
    }
}
