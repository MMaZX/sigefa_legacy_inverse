Add-Type -Path "c:\Users\qemu\Documents\Debug_gr\MySql.Data.dll"

$connectionString = "Server=192.168.1.129;Database=database_multi_final;Uid=root;Pwd=fulanito;Port=3307;default command timeout=30"
$conn = New-Object MySql.Data.MySqlClient.MySqlConnection($connectionString)

try {
    $conn.Open()
    Write-Output "Connected successfully to 192.168.1.129"
    
    $triggers = @(
        "ActualizaStockInsert",
        "ActualizaStockdisponible",
        "ActualizaStockInsertS",
        "ActualizaStockdisponibleTransferencia",
        "ModificacionStockDisponibleSegunReqAlmacenParaVenta",
        "ActualizaDisponibleAprobarTransferencia"
    )

    foreach ($t in $triggers) {
        Write-Output "`n--- SHOW CREATE TRIGGER $t ---"
        $query = "SHOW CREATE TRIGGER database_multi_final.$t"
        $cmd = New-Object MySql.Data.MySqlClient.MySqlCommand($query, $conn)
        try {
            $reader = $cmd.ExecuteReader()
            if ($reader.Read()) {
                Write-Output $reader.GetString(2)
            } else {
                Write-Output "Trigger $t not found."
            }
            $reader.Close()
        } catch {
            Write-Output "Error reading trigger $t : $_"
        }
    }

} catch {
    Write-Error $_.Exception.Message
} finally {
    if ($conn.State -eq [System.Data.ConnectionState]::Open) {
        $conn.Close()
    }
}
