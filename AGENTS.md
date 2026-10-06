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

- MySQL 8.x / MariaDB / MySQL 5.7 via `MySql.Data`. Connection string in `SIGEFA/app.config` (`ConnNegocio`).
- Active local server: `127.0.0.1:3307` (Docker `manager_mysql`), db: `database_multi_final`, user: `root`, password: `fulanito`.
- Production server (READ-ONLY): `192.168.1.3:3306`, db: `database_multi_final`, user: `ia-model-user`, password: `9isxDFsQSMqQWT1PUOcCBAw7`. Para consultar producción usar SIEMPRE estas credenciales de solo lectura. Están traqueadas en git a propósito (usuario sin permisos de escritura).
- Config file for scripts/tools: `db_config.yaml` (extracted from `/home/fulanito/www/gruporicardoapi/.env`).
- DNI/RUC lookup via `https://sgesystems.com/consulta_*?api_token=948961635` (also in app.config).
- No schema/migration files — schema assumed to exist externally.

## Quirks & gotchas

- **Code was decompiled** — all `.cs` files contain `// Token:` comments. Variable names may differ from original. Designer files may be out of sync.
- **Culture hardcoded** to `es-PE` in `Program.cs:126` — date, number, currency formatting follows Peru locale.
- **Startup creates** `./documentos/{CDR,Boletas,Facturas,NC,ND,RESUMEN,GUIAS}` — working directory must be writable.
- **Crystal Reports runtime** required at build and runtime.
- **SUNAT integration** — communicates with Peru tax authority SOAP web services for electronic invoices, credit notes, debit notes, etc.
- **Not a git repo** — no `.gitignore`, VS user files (`.vs/`) included.

## Code conventions (anti-patterns to avoid)

Most existing code is decompiled, so long `if/else` chains and repeated assignments are common. Do not copy that style into new code, and clean it up when a task already touches the same method.

- **Nested `if`s are an anti-pattern.** Do not add a third level of nesting. Prefer guard clauses (return early), extract a well-named method, or use a `switch` on the discriminating value (`switch` with grouped `case`s works in C# 10; this project already uses file-scoped namespaces).
- **Do not repeat the same cast or lookup** (e.g. `Convert.ToInt32(cmbMetodoPago.SelectedValue)`) in every branch: read it once into a local variable.
- **Centralize UI state rules in a small helper** (see `PagoCamposHelper`): one place decides which controls are enabled or cleared per payment method. Never re-enable controls with a blanket `Enabled = true` helper.
- **Separate concerns**: UI rules (enabled/cleared), data logic, and side effects (dialogs, focus, I/O) go in different methods. Helpers must not touch the database.
- **Keep the legacy route untouched** unless the task says otherwise; new behavior goes behind the `VentaCierreRuta=nueva` flag and is verified with `git diff -w`.
- **Never swallow errors with a bare `MessageBox` in new code**: record them (see `VentaCierreRegistroErrores`) and surface them as a warning or error in the UI.

## graphify

This project has a knowledge graph at graphify-out/ with god nodes, community structure, and cross-file relationships.

When the user types `/graphify`, invoke the `skill` tool with `skill: "graphify"` before doing anything else.

Rules:
- For codebase questions, first run `graphify query "<question>"` when graphify-out/graph.json exists. Use `graphify path "<A>" "<B>"` for relationships and `graphify explain "<concept>"` for focused concepts. These return a scoped subgraph, usually much smaller than GRAPH_REPORT.md or raw grep output.
- Dirty graphify-out/ files are expected after hooks or incremental updates; dirty graph files are not a reason to skip graphify. Only skip graphify if the task is about stale or incorrect graph output, or the user explicitly says not to use it.
- If graphify-out/wiki/index.md exists, use it for broad navigation instead of raw source browsing.
- Read graphify-out/GRAPH_REPORT.md only for broad architecture review or when query/path/explain do not surface enough context.
- After modifying code, run `graphify update .` to keep the graph current (AST-only, no API cost).
