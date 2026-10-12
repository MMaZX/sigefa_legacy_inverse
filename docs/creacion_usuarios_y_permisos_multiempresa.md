# 📘 Guía Técnica: Creación de Usuarios y Control de Accesos Multi-Empresa (Legacy SIGEFA ➔ Backend Laravel)

## 1. Diagnóstico de la Limitación Actual en Legacy C#

En el sistema legacy C#, al registrar un usuario en el formulario `frmGestionUsuario.cs`, el código ejecuta la siguiente instrucción:

```csharp
// Asigna únicamente la empresa en la que el Administrador inició sesión actualmente
admAcce.InsertAccesoEmp(usu.CodUsuarioNuevo, frmLogin.iCodEmpresa, frmLogin.iCodUser);
```

**Consecuencia:** El usuario recién creado queda restringido **únicamente a 1 sola empresa** (`frmLogin.iCodEmpresa`). Para asignarle otras empresas en el sistema legacy, era necesario iniciar sesión independientemente en cada empresa o asignarlo mediante formularios secundarios.

**Objetivo en Laravel:** Permitir que la API del backend reciba un arreglo de empresas (`empresas: [...]`) y asocie el usuario recién creado a **múltiples empresas y sus respectivas sucursales/almacenes** dentro de una sola transacción.

---

## 2. Flujo de Creación de Usuario (Paso a Paso)

Para crear un usuario completo con sus accesos a varias empresas, el backend de Laravel debe seguir esta secuencia:

```
+-------------------+             +--------------------------------------+
| Laravel Backend   |             | MySQL Database (SIGEFA)              |
+-------------------+             +--------------------------------------+
          |                                          |
          |--- 1. CALL GuardaUsuario(...) ---------->| (Inserta usuario principal)
          |<-- Returns newid (ID Usuario) -----------|
          |                                          |
          |=== Por cada Empresa (empresa_id) ========|
          |--- 2. CALL GuardaAccesoEmpresa(...) ---->| (Asigna empresa al usuario)
          |                                          |
          |---=== Por cada Sucursal de la Empresa ===|
          |--- 3. CALL GuardaAccesoSucursal(...) --->| (Asigna sucursal)
          |                                          |
          |---=== Por cada Almacén / Módulo =========|
          |--- 4. CALL GuardaAcceso(...) ----------->| (Asigna menú/formulario)
          +------------------------------------------+
```

---

## 3. Stored Procedures Involucrados

### 3.1. `GuardaUsuario` (Creación Base)
Crea el perfil del usuario, credenciales y configuración inicial.

- **Archivo C# de referencia:** [MysqlUsuario.cs](file:///home/fulanito/development/sigefa_legacy_inverse/SIGEFA.InterMySql/MysqlUsuario.cs#L27-L58)
- **Firma / Parámetros:**
  ```sql
  CALL GuardaUsuario(
      p_dni VARCHAR(15),             -- DNI del usuario (NULL si está vacío)
      p_nombre VARCHAR(100),         -- Nombre
      p_apellido VARCHAR(100),       -- Apellido
      p_fechanac DATE,               -- Fecha de nacimiento
      p_direccion VARCHAR(150),      -- Dirección
      p_telefono VARCHAR(20),        -- Teléfono fijo
      p_celular VARCHAR(20),         -- Celular
      p_email VARCHAR(100),          -- Correo electrónico
      p_usuario VARCHAR(50),         -- Username para login
      p_contraseña VARCHAR(255),     -- Password
      p_contraemail VARCHAR(100),    -- Clave de email (para SMTP)
      p_hostmail VARCHAR(100),       -- Host de correo
      p_host VARCHAR(100),           -- Host / Nombre de equipo
      p_nivel INT,                   -- Nivel/Rol (1: Admin, 2: Vendedor, etc.)
      p_estado TINYINT(1),           -- Estado (1: Activo, 0: Inactivo)
      p_codusu INT,                  -- ID del usuario Administrador que crea el registro
      p_pcodigocanalventa VARCHAR(6),-- Código del Canal de Venta por defecto (ej: '001001')
      p_accesocanalventa TINYINT(1), -- Permiso "Acceso a Todos" los canales (1 o 0)
      OUT newid INT                  -- Retorna el ID autogenerado del usuario
  );
  ```

---

### 3.2. `GuardaAccesoEmpresa` (Vincular Usuario ↔ Empresa)
Asigna el permiso a un usuario para ingresar a una empresa. **(Llamar por cada empresa asignada)**.

- **Archivo C# de referencia:** [MysqlAcceso.cs](file:///home/fulanito/development/sigefa_legacy_inverse/SIGEFA.InterMySql/MysqlAcceso.cs#L120-L124)
- **Firma / Parámetros:**
  ```sql
  CALL GuardaAccesoEmpresa(
      p_codusu INT,   -- ID del usuario creado (newid)
      p_codemp INT,   -- ID de la Empresa
      p_coduser INT   -- ID del usuario Administrador creador
  );
  ```

---

### 3.3. `GuardaAccesoSucursal` (Vincular Usuario ↔ Empresa ↔ Sucursal)
Asigna a qué sucursales de cada empresa tiene acceso el usuario.

- **Archivo C# de referencia:** [MysqlAccesoSucursales.cs](file:///home/fulanito/development/sigefa_legacy_inverse/SIGEFA.InterMySql/MysqlAccesoSucursales.cs#L28-L36)
- **Firma / Parámetros:**
  ```sql
  CALL GuardaAccesoSucursal(
      p_codusua INT,  -- ID del usuario (newid)
      p_codemp INT,   -- ID de la Empresa
      p_codsuc INT,   -- ID de la Sucursal
      p_codusu INT,   -- ID del usuario Administrador creador
      OUT newid INT   -- Retorna ID del registro de acceso
  );
  ```

---

### 3.4. `GuardaAcceso` (Vincular Usuario ↔ Almacén ↔ Módulo/Formulario)
Define a qué módulos/pantallas tiene permiso el usuario dentro de un almacén.

- **Archivo C# de referencia:** [MysqlAcceso.cs](file:///home/fulanito/development/sigefa_legacy_inverse/SIGEFA.InterMySql/MysqlAcceso.cs#L28-L35)
- **Firma / Parámetros:**
  ```sql
  CALL GuardaAcceso(
      p_codusua INT,  -- ID del usuario (newid)
      p_codalm INT,   -- ID del Almacén
      p_codfor INT,   -- ID del Formulario / Módulo (ej: 1 = Ventas, 2 = Productos)
      p_codusu INT,   -- ID del usuario Administrador creador
      OUT newid INT   -- Retorna ID del registro de acceso
  );
  ```

---

### 3.5. Procedimientos de Actualización y Limpieza (Edición de Usuarios)

Al **modificar** un usuario existente:

| Stored Procedure | Propósito |
|---|---|
| `ActualizaUsuario` | Actualiza la información personal, credenciales, rol y canal de venta del usuario. |
| `LimpiaAccesosSucursales(codusua, codEmpre)` | Borra los accesos a sucursales de esa empresa antes de reinsertar los nuevos. |
| `LimpiaAccesos(codusua, codalma)` | Borra los permisos de módulos de ese almacén antes de reinsertar los nuevos. |

---

## 4. Tablas Afectadas en MySQL y su Propósito en el ERP

| Tabla | Stored Procedure Afectado | Propósito / Qué afecta en el Sistema |
|---|---|---|
| **`usuario`** | `GuardaUsuario`, `ActualizaUsuario`, `EliminarUsuario` | Almacena los datos personales, usuario, contraseña, nivel/rol global y configuración del canal de venta. **Afecta la autenticación general y el nivel de privilegio.** |
| **`accesoempresa`** | `GuardaAccesoEmpresa` | Almacena la relación **Usuario ↔ Empresa**. **Afecta el inicio de sesión (Login):** Si una empresa no está registrada aquí para ese `codUsuario`, el usuario no podrá seleccionarla al iniciar sesión ni ver su información. |
| **`accesosucursal`** | `GuardaAccesoSucursal`, `LimpiaAccesosSucursales` | Relación **Usuario ↔ Empresa ↔ Sucursal**. **Afecta la selección de sucursales:** Restringe a cuáles sucursales de una empresa puede ingresar o facturar. |
| **`acceso`** | `GuardaAcceso`, `LimpiaAccesos` | Relación **Usuario ↔ Almacén ↔ Formulario/Módulo**. **Afecta el Menú Principal:** Filtra los botones y opciones del menú en base a las pantallas a las que el usuario tiene acceso en el almacén activo. |

---

## 5. Recomendación para el Request Payload en Laravel (API Contract)

Para que el backend permita asignar **múltiples empresas, sucursales y permisos**, el payload JSON enviado al endpoint `POST /api/v1/usuarios` debería estructurarse así:

```json
{
  "dni": "72819203",
  "nombre": "Juan",
  "apellido": "Pérez",
  "fechanac": "1990-05-15",
  "direccion": "Av. Principal 123",
  "telefono": "014567890",
  "celular": "987654321",
  "email": "juan.perez@empresa.com",
  "usuario": "jperez",
  "password": "Password123",
  "nivel": 2,
  "estado": true,
  "codigo_canal_venta": "001001",
  "acceso_canal_venta_todos": true,
  "empresas": [
    {
      "empresa_id": 1,
      "sucursales_ids": [10, 11],
      "almacenes": [
        {
          "almacen_id": 100,
          "formularios_ids": [1, 2, 5, 8]
        }
      ]
    },
    {
      "empresa_id": 2,
      "sucursales_ids": [20],
      "almacenes": [
        {
          "almacen_id": 200,
          "formularios_ids": [1, 2]
        }
      ]
    }
  ]
}
```

### Lógica sugerida en el Controller / Service de Laravel:

```php
DB::transaction(function () use ($request) {
    // 1. Crear el usuario base
    $userId = $this->usuarioRepository->guardaUsuario($request->validated());

    // 2. Recorrer las empresas seleccionadas
    foreach ($request->empresas as $empresa) {
        // Asignar empresa
        $this->usuarioRepository->guardaAccesoEmpresa($userId, $empresa['empresa_id']);

        // Asignar sucursales de esa empresa
        foreach ($empresa['sucursales_ids'] as $sucursalId) {
            $this->usuarioRepository->guardaAccesoSucursal($userId, $empresa['empresa_id'], $sucursalId);
        }

        // Asignar permisos de módulos por almacén
        foreach ($empresa['almacenes'] as $almacen) {
            foreach ($almacen['formularios_ids'] as $formId) {
                $this->usuarioRepository->guardaAcceso($userId, $almacen['almacen_id'], $formId);
            }
        }
    }
});
```
