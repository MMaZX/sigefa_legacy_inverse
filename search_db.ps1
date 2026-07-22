Add-Type -Path "c:\Users\qemu\Documents\Debug_gr\MySql.Data.dll"

$connectionString = "Server=192.168.1.129;Database=database_multi_final;Uid=root;Pwd=fulanito;Port=3307;default command timeout=30"
$conn = New-Object MySql.Data.MySqlClient.MySqlConnection($connectionString)

try {
    $conn.Open()
    Write-Output "Connected successfully to 192.168.1.129"
    
    # Query SHOW CREATE TABLE cajamovimiento
    Write-Output "`n--- SHOW CREATE TABLE cajamovimiento ---"
    $query = "SHOW CREATE TABLE database_multi_final.cajamovimiento"
    $cmd = New-Object MySql.Data.MySqlClient.MySqlCommand($query, $conn)
    $reader = $cmd.ExecuteReader()
    if ($reader.Read()) {
        Write-Output $reader.GetString(1)
    } else {
        Write-Output "Table cajamovimiento not found."
    }
    $reader.Close()

} catch {
    Write-Error $_.Exception.Message
} finally {
    if ($conn.State -eq [System.Data.ConnectionState]::Open) {
        $conn.Close()
    }
}
