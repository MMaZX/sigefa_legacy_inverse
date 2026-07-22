Add-Type -Path "c:\Users\qemu\Documents\Debug_gr\MySql.Data.dll"

$connectionString = "Server=192.168.1.129;Database=database_multi_final;Uid=root;Pwd=fulanito;Port=3307;default command timeout=30"
$conn = New-Object MySql.Data.MySqlClient.MySqlConnection($connectionString)

try {
    $conn.Open()
    Write-Output "Connected successfully to 192.168.1.129"
    
    Write-Output "`n--- SHOW CREATE PROCEDURE AprobarRequerimientoAlmacen ---"
    $query1 = "SHOW CREATE PROCEDURE database_multi_final.AprobarRequerimientoAlmacen"
    $cmd1 = New-Object MySql.Data.MySqlClient.MySqlCommand($query1, $conn)
    $reader1 = $cmd1.ExecuteReader()
    if ($reader1.Read()) {
        Write-Output $reader1.GetString(2)
    } else {
        Write-Output "Procedure AprobarRequerimientoAlmacen not found."
    }
    $reader1.Close()

    Write-Output "`n--- SHOW CREATE PROCEDURE SeparandoStockAlAprobarReqAlmacen ---"
    $query2 = "SHOW CREATE PROCEDURE database_multi_final.SeparandoStockAlAprobarReqAlmacen"
    $cmd2 = New-Object MySql.Data.MySqlClient.MySqlCommand($query2, $conn)
    $reader2 = $cmd2.ExecuteReader()
    if ($reader2.Read()) {
        Write-Output $reader2.GetString(2)
    } else {
        Write-Output "Procedure SeparandoStockAlAprobarReqAlmacen not found."
    }
    $reader2.Close()

} catch {
    Write-Error $_.Exception.Message
} finally {
    if ($conn.State -eq [System.Data.ConnectionState]::Open) {
        $conn.Close()
    }
}
