# tema-fluent

Estado: **PLAN PROPUESTO, PENDIENTE DE APROBACIÓN** (2026-10-06). Ninguna tarea iniciada.
Espejo Engram: tópico `odd/tema-fluent/tasks` (proyecto `sigefa_legacy_inverse`).

## Objetivo

Modernizar el aspecto visual de SIGEFA aplicando el tema **Fluent** de Telerik UI for WinForms a nivel global, y armonizando el estilo de DevComponents DotNetBar (`StyleManager`), sin modificar manualmente los más de 300 formularios ni actualizar las librerías precompiladas.

1. Configurar `ThemeResolutionService.ApplicationThemeName = "Fluent"` globalmente al iniciar la aplicación.
2. Armonizar la barra Ribbon y controles de DevComponents en `Program.cs` y `mdi_Menu` con un estilo acorde (ej. `eStyle.Metro` u `Office2010Silver` en lugar del anticuado `Office2007Black`).
3. Proveer un flag en `appSettings` (`TemaVisual`, valores `fluent` | `legacy`, por defecto `fluent`) para permitir revertir al aspecto original sin recompilar si se detecta alguna anomalía visual.

## Problema (evidencia)

- En [`SIGEFA/Program.cs:62`](file:///home/fulanito/development/sigefa_legacy_inverse/SIGEFA/Program.cs#L62), la aplicación tiene fijado en duro `StyleManager.Style = eStyle.Office2007Black`, dando una estética obsoleta y pesada de Office 2007 a los controles DevComponents (incluyendo el Ribbon principal).
- Los controles de Telerik (`RadGridView`, etc., presentes en ~170 formularios) no tienen un `ApplicationThemeName` definido en tiempo de arranque, por lo que caen en el estilo base o en configuraciones individuales heterogéneas.
- La librería `Telerik.WinControls.Themes.Fluent.dll` ya está presente en `..\Debug_gr\` y debidamente referenciada en [`SIGEFA.csproj`](file:///home/fulanito/development/sigefa_legacy_inverse/SIGEFA.csproj), pero se encuentra inactiva porque no se inicializa en el arranque.

## Restricciones (obligatorias)

1. **Cero actualización de DLLs**: Las librerías de `..\Debug_gr\` se mantienen idénticas. No se agregan ni reemplazan binarios de Telerik ni DevComponents.
2. **Prohibido retocar formularios individuales a mano**: No alterar propiedades en los archivos `frm*.Designer.cs`. El cambio debe gobernarse de forma centralizada en el ciclo de vida del arranque (`Program.cs` / `mdi_Menu`).
3. **Reversibilidad por configuración**: El cambio debe estar condicionado por la clave `TemaVisual` en `appSettings` (`fluent` vs `legacy`). Si el flag está ausente o tiene valor `legacy`, el comportamiento visual debe ser idéntico al actual.
4. **Target .NET Framework 4.6.1/4.8 (x86)**: Todo código debe respetar la versión de lenguaje y dependencias existentes.
5. **Commits**: Conventional Commits limpios, sin `Co-Authored-By` ni atribuciones de IA.
6. **Compilación en VM**: Como el entorno Linux no compila proyectos con Crystal Reports y dependencias Windows, la verificación de build se ejecuta en la máquina virtual de pruebas.

## Reparto por agente

| Agente | Tareas | Rol |
|---|---|---|
| **opencode** | T1, T2 | Configuración en `app.config`, inicialización de Fluent y DevComponents en `Program.cs` |
| **claude** | T3, T4 | Build en la VM Windows por SSH y verificación/reporte de humo |

## Tareas

Cada tarea cierra con un commit de unidad de trabajo (Conventional Commit) y registra su hash y evidencia observada.

### Pre-requisito P0
- [x] Rama `feat/tema-fluent` creada desde `main` (2026-10-06).

### T1 — Configuración de tema en `app.config`
- [ ] Agregar la clave `<add key="TemaVisual" value="fluent" />` en `appSettings` de [`app.config`](file:///home/fulanito/development/sigefa_legacy_inverse/app.config) con comentario explicativo (`fluent` = Fluent de Telerik + Metro/Office2010; `legacy` = Office2007Black sin ThemeResolutionService).

### T2 — Inicialización global del tema Fluent en `Program.cs`
- [ ] En [`SIGEFA/Program.cs`](file:///home/fulanito/development/sigefa_legacy_inverse/SIGEFA/Program.cs), leer el flag `TemaVisual`.
- [ ] Si es `fluent`:
  - Registrar e inicializar `new Telerik.WinControls.Themes.FluentTheme()`.
  - Establecer `Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "Fluent"`.
  - Ajustar `StyleManager.Style` a `eStyle.Metro` (o `eStyle.Office2010Silver`).
- [ ] Si es `legacy` (o clave ausente):
  - Mantener `StyleManager.Style = eStyle.Office2007Black` sin registrar tema Telerik.

### T3 — Build en VM de pruebas
- [ ] Compilar en la VM Windows (`Debug|x86`) y comprobar ausencia de errores de enlace con las librerías de Telerik y DevComponents.

### T4 — Verificación de humo visual en pantallas críticas
- [ ] Probar inicio de sesión en `frmLogin`.
- [ ] Probar Ribbon y contenedor en `mdi_Menu`.
- [ ] Probar grilla y controles en `frmVenta2019` y `frmProductosLista`.

## Protocolo de traspaso entre agentes (obligatorio)

Cada agente, al empezar:
1. Consultar contexto en Engram (`mem_search "tema-fluent"`).
2. Leer `odd/tasks/tema-fluent.md`.
3. Trabajar **únicamente** en los archivos asignados a su tarea.
4. **No compilar en Linux** ni declarar que compila sin evidencia de la VM.
5. Al terminar: commit convencional (sin `Co-Authored-By` ni menciones de IA), marcar la casilla con hash y evidencia en este archivo, actualizar el espejo en Engram (`odd/tema-fluent/tasks`) y **detenerse**.

## Riesgos y decisiones

- **Contraste de DevComponents Ribbon**: `eStyle.Metro` ofrece un estilo plano moderno pero muy blanco; `eStyle.Office2010Silver` ofrece mayor delimitación en pestañas. Se evaluará durante la prueba visual.
- **Rendimiento**: La inicialización de `FluentTheme` en el arranque es liviana y no genera sobrecarga de I/O de disco.
