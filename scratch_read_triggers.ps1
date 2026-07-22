Add-Type -Path "c:\Users\qemu\Documents\Debug_gr\MySql.Data.dll"

$connectionString = "Server=192.168.1.129;Database=database_multi_final;Uid=root;Pwd=fulanito;Port=3307;default command timeout=30"
$conn = New-Object MySql.Data.MySqlClient.MySqlConnection($connectionString)

$outputFile = "c:\Users\qemu\Documents\sigefa_legacy\trigger_details.txt"
if (Test-Path $outputFile) { Remove-Item $outputFile }

try {
    $conn.Open()
    
    $triggers = @(
        "ActualizaStockInsert",
        "ActualizaStockdisponible",
        "ActualizaStockInsertS",
        "ActualizaStockdisponibleTransferencia",
        "ModificacionStockDisponibleSegunReqAlmacenParaVenta",
        "ActualizaDisponibleAprobarTransferencia"
    )

    foreach ($t in $triggers) {
        Add-Content $outputFile "`n========================================================"
        Add-Content $outputFile "TRIGGER: $t"
        Add-Content $outputFile "========================================================"
        $query = "SHOW CREATE TRIGGER database_multi_final.$t"
        $cmd = New-Object MySql.Data.MySqlClient.MySqlCommand($query, $conn)
        try {
            $reader = $cmd.ExecuteReader()
            if ($reader.Read()) {
                Add-Content $outputFile $reader.GetString(2)
            } else {
                Add-Content $outputFile "Trigger $t not found."
            }
            $reader.Close()
        } catch {
            Add-Content $outputFile "Error reading trigger $t : $_"
        }
    }

} catch {
    Write-Error $_.Exception.Message
} finally {
    if ($conn.State -eq [System.Data.ConnectionState]::Open) {
        $conn.Close()
    }
}
