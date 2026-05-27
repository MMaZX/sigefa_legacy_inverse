# SIGEFA — Legacy ERP

C# WinForms app targeting .NET Framework 4.8 (x86), connected to MySQL.
Peruvian ERP (Sistema de Gestión de Facturación) with SUNAT electronic invoicing.

## Build

- Open `SIGEFA.sln` in **Visual Studio 2022+** with **.NET Framework 4.8 SDK** and **Crystal Reports runtime** installed.
- Build configurations: `Debug|x86` / `Release|x86` (32-bit only).
- **All DLL references point to `..\..\Debug_gr\`** relative to `SIGEFA\SIGEFA.csproj`. That directory must exist with all required binaries (DevComponents, Telerik, Bunifu, MySql.Data, CrystalDecisions, etc.). No NuGet restore — precompiled DLLs only.
- No tests, no linter, no formatter, no CI/CD.

## Architecture

```
Formularios (WinForms UI) → Administradores (Business Logic)
    → Interfaces (Repository Contracts) → InterMySql (MySQL DAL)
    → Entidades (DTOs)
```

- No DI container. Admin classes instantiate their MySQL repo directly (e.g., `new MysqlProducto()`).
- Entry point: `SIGEFA.Program.Main()` → `frmLogin`.
- Main MDI form: `mdi_Menu` (DevComponents DotNetBar Ribbon).

## Key directories

| Directory | Purpose |
|-----------|---------|
| `SIGEFA/Formularios/` | 100+ WinForms (`frm*.cs` + `frm*.Designer.cs`) |
| `SIGEFA/Administradores/` | Business logic / service layer (`clsAdm*.cs`) |
| `SIGEFA/Interfaces/` | Repository interfaces (`I*.cs`) |
| `SIGEFA/InterMySql/` | MySQL implementations (`Mysql*.cs`) |
| `SIGEFA/Entidades/` | Entity/DTO classes |
| `SIGEFA/Reportes/` | Crystal Reports (`.rpt` files) + viewer forms |
| `SIGEFA/Conexion/` | `clsConexionMysql.cs` — DB connection + backup |
| `SIGEFA/SunatFacElec/` | SUNAT electronic invoice components |

## Database

- MySQL 8.x via `MySql.Data`. Connection string in `SIGEFA/app.config` (`ConnNegocio`).
- Active server: `192.168.100.51:3307`, db: `database_multi_final`, user: `root` (plaintext password).
- DNI/RUC lookup via `https://sgesystems.com/consulta_*?api_token=948961635` (also in app.config).
- No schema/migration files — schema assumed to exist externally.

## Quirks & gotchas

- **Code was decompiled** — all `.cs` files contain `// Token:` comments. Variable names may differ from original. Designer files may be out of sync.
- **Culture hardcoded** to `es-PE` in `Program.cs:126` — date, number, currency formatting follows Peru locale.
- **Startup creates** `./documentos/{CDR,Boletas,Facturas,NC,ND,RESUMEN,GUIAS}` — working directory must be writable.
- **Crystal Reports runtime** required at build and runtime.
- **SUNAT integration** — communicates with Peru tax authority SOAP web services for electronic invoices, credit notes, debit notes, etc.
- **Not a git repo** — no `.gitignore`, VS user files (`.vs/`) included.
