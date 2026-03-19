' ═══════════════════════════════════════════════════════════════════════════════
' Módulo: DatabaseHelper
' Descripción: Módulo central para la gestión de la base de datos SQLite.
'              Contiene la cadena de conexión, la creación de tablas y los
'              datos iniciales (seed) del sistema Ferree-Lara.
' Autor: Equipo Ferree-Lara
' Fecha: Marzo 2026
' ═══════════════════════════════════════════════════════════════════════════════

Imports Microsoft.Data.Sqlite
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

''' <summary>
''' Módulo estático que centraliza todas las operaciones de acceso a la base
''' de datos SQLite. Proporciona métodos para obtener conexiones, inicializar
''' las tablas y poblar datos iniciales del sistema.
''' </summary>
Module DatabaseHelper

    ' ── Ruta del archivo de base de datos ──
    ' Se almacena en la misma carpeta del ejecutable para facilitar la portabilidad.
    Private ReadOnly RutaBaseDatos As String = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "FerreeLaraDB.db")

    ' ── Cadena de conexión reutilizable ──
    Public ReadOnly CadenaConexion As String = $"Data Source={RutaBaseDatos}"

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: ObtenerConexion
    ' Descripción: Crea y retorna una nueva conexión SQLite. El llamador es
    '              responsable de abrir y cerrar (o usar Using).
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Function ObtenerConexion() As SqliteConnection
        Return New SqliteConnection(CadenaConexion)
    End Function

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: InicializarBaseDatos
    ' Descripción: Crea las tablas del sistema si no existen y puebla los datos
    '              iniciales predeterminados.
    '              Debe llamarse una sola vez al iniciar la aplicación.
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub InicializarBaseDatos()
        Using conexion As SqliteConnection = ObtenerConexion()
            conexion.Open()

            ' ── Habilitar claves foráneas en SQLite ──
            Dim comandoPragma As New SqliteCommand("PRAGMA foreign_keys = ON;", conexion)
            comandoPragma.ExecuteNonQuery()

            ' ──────────────────────────────────────────────────────────────────
            ' TABLA: Usuarios
            ' Descripción: Almacena las credenciales y roles de los usuarios
            '              del sistema (Administrador, Cajero).
            ' ──────────────────────────────────────────────────────────────────
            Dim sqlUsuarios As String = "
                CREATE TABLE IF NOT EXISTS Usuarios (
                    UsuarioId       INTEGER PRIMARY KEY AUTOINCREMENT,
                    NombreUsuario   TEXT    NOT NULL UNIQUE,
                    Contrasena      TEXT    NOT NULL,
                    NombreCompleto  TEXT    NOT NULL,
                    Rol             TEXT    NOT NULL CHECK(Rol IN ('Administrador','Cajero')),
                    Activo          INTEGER NOT NULL DEFAULT 1,
                    FechaCreacion   TEXT    NOT NULL DEFAULT (datetime('now','localtime'))
                );"
            EjecutarComando(sqlUsuarios, conexion)

            ' ──────────────────────────────────────────────────────────────────
            ' TABLA: Clientes
            ' Descripción: Registra los clientes de la ferretería. Incluye un
            '              registro especial "Consumidor Final" para ventas
            '              sin cliente identificado.
            ' ──────────────────────────────────────────────────────────────────
            Dim sqlClientes As String = "
                CREATE TABLE IF NOT EXISTS Clientes (
                    ClienteId       INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre          TEXT    NOT NULL,
                    Apellido        TEXT    NOT NULL,
                    Telefono        TEXT,
                    Correo          TEXT,
                    Direccion       TEXT,
                    Cedula          TEXT    UNIQUE,
                    FechaRegistro   TEXT    NOT NULL DEFAULT (datetime('now','localtime'))
                );"
            EjecutarComando(sqlClientes, conexion)

            ' ──────────────────────────────────────────────────────────────────
            ' TABLA: Productos
            ' Descripción: Catálogo completo de productos de la ferretería.
            '              Incluye categoría, precio, costo, stock actual y
            '              stock mínimo para alertas de reabastecimiento.
            ' ──────────────────────────────────────────────────────────────────
            Dim sqlProductos As String = "
                CREATE TABLE IF NOT EXISTS Productos (
                    ProductoId      INTEGER PRIMARY KEY AUTOINCREMENT,
                    Codigo          TEXT    NOT NULL UNIQUE,
                    Nombre          TEXT    NOT NULL,
                    Descripcion     TEXT,
                    Categoria       TEXT    NOT NULL,
                    PrecioVenta     REAL    NOT NULL CHECK(PrecioVenta >= 0),
                    PrecioCosto     REAL    NOT NULL CHECK(PrecioCosto >= 0),
                    Stock           INTEGER NOT NULL DEFAULT 0 CHECK(Stock >= 0),
                    StockMinimo     INTEGER NOT NULL DEFAULT 5,
                    Activo          INTEGER NOT NULL DEFAULT 1,
                    FechaRegistro   TEXT    NOT NULL DEFAULT (datetime('now','localtime'))
                );"
            EjecutarComando(sqlProductos, conexion)

            ' ──────────────────────────────────────────────────────────────────
            ' TABLA: Ventas
            ' Descripción: Encabezado de cada transacción de venta. Registra
            '              el cliente, el usuario que realizó la venta, los
            '              totales y el método de pago.
            ' ──────────────────────────────────────────────────────────────────
            Dim sqlVentas As String = "
                CREATE TABLE IF NOT EXISTS Ventas (
                    VentaId         INTEGER PRIMARY KEY AUTOINCREMENT,
                    NumeroFactura   TEXT    NOT NULL UNIQUE,
                    ClienteId       INTEGER NOT NULL,
                    UsuarioId       INTEGER NOT NULL,
                    FechaVenta      TEXT    NOT NULL DEFAULT (datetime('now','localtime')),
                    Subtotal        REAL    NOT NULL DEFAULT 0,
                    Impuesto        REAL    NOT NULL DEFAULT 0,
                    Total           REAL    NOT NULL DEFAULT 0,
                    MontoPagado     REAL    NOT NULL DEFAULT 0,
                    Cambio          REAL    NOT NULL DEFAULT 0,
                    MetodoPago      TEXT    NOT NULL DEFAULT 'Efectivo',
                    Estado          TEXT    NOT NULL DEFAULT 'Completada'
                        CHECK(Estado IN ('Completada','Anulada')),
                    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId),
                    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId)
                );"
            EjecutarComando(sqlVentas, conexion)

            ' ──────────────────────────────────────────────────────────────────
            ' TABLA: Detalle_Ventas
            ' Descripción: Líneas individuales de cada venta. Cada registro
            '              representa un producto vendido con su cantidad,
            '              precio unitario y subtotal de línea.
            ' ──────────────────────────────────────────────────────────────────
            Dim sqlDetalleVentas As String = "
                CREATE TABLE IF NOT EXISTS Detalle_Ventas (
                    DetalleId       INTEGER PRIMARY KEY AUTOINCREMENT,
                    VentaId         INTEGER NOT NULL,
                    ProductoId      INTEGER NOT NULL,
                    Cantidad        INTEGER NOT NULL CHECK(Cantidad > 0),
                    PrecioUnitario  REAL    NOT NULL CHECK(PrecioUnitario >= 0),
                    SubtotalLinea   REAL    NOT NULL CHECK(SubtotalLinea >= 0),
                    FOREIGN KEY (VentaId)    REFERENCES Ventas(VentaId),
                    FOREIGN KEY (ProductoId) REFERENCES Productos(ProductoId)
                );"
            EjecutarComando(sqlDetalleVentas, conexion)

            ' ── Insertar datos iniciales (solo si las tablas están vacías) ──
            InsertarDatosIniciales(conexion)

        End Using
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: InsertarDatosIniciales
    ' Descripción: Pobla las tablas con datos base necesarios para que
    '              el sistema funcione desde la primera ejecución:
    '              - Un usuario Administrador (admin / admin123)
    '              - Un usuario Cajero (cajero / cajero123)
    '              - Un cliente "Consumidor Final"
    '              - Productos del catálogo típico de ferretería
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub InsertarDatosIniciales(conexion As SqliteConnection)

        ' ── Verificar si ya existen usuarios (evitar duplicados) ──
        Dim cmdConteo As New SqliteCommand("SELECT COUNT(*) FROM Usuarios;", conexion)
        Dim cantidadUsuarios As Long = Convert.ToInt64(cmdConteo.ExecuteScalar())

        If cantidadUsuarios > 0 Then
            Return ' Ya se ejecutó el seed anteriormente
        End If

        ' ── Usuarios del sistema ──
        Dim sqlInsertUsuarios As String = "
            INSERT INTO Usuarios (NombreUsuario, Contrasena, NombreCompleto, Rol) VALUES
                ('admin',  @passAdmin,  'Carlos Martínez (Administrador)', 'Administrador'),
                ('cajero', @passCajero, 'María López (Cajera)',            'Cajero');"
        Dim cmdUsuarios As New SqliteCommand(sqlInsertUsuarios, conexion)
        cmdUsuarios.Parameters.AddWithValue("@passAdmin", EncriptarContrasena("admin123"))
        cmdUsuarios.Parameters.AddWithValue("@passCajero", EncriptarContrasena("cajero123"))
        cmdUsuarios.ExecuteNonQuery()

        ' ── Cliente "Consumidor Final" (para ventas sin cliente registrado) ──
        Dim sqlInsertClienteCF As String = "
            INSERT INTO Clientes (Nombre, Apellido, Telefono, Correo, Direccion, Cedula) VALUES
                ('Consumidor', 'Final', 'N/A', 'N/A', 'N/A', '0000000000');"
        EjecutarComando(sqlInsertClienteCF, conexion)

        ' ── Productos predeterminados (catálogo típico de ferretería) ──
        Dim sqlInsertProductos As String = "
            INSERT INTO Productos (Codigo, Nombre, Descripcion, Categoria, PrecioVenta, PrecioCosto, Stock, StockMinimo) VALUES
                ('FT-001', 'Martillo de Uña 16oz',      'Martillo de acero forjado con mango de fibra de vidrio', 'Herramientas Manuales', 185.00, 120.00, 25, 5),
                ('FT-002', 'Destornillador Phillips #2', 'Destornillador de punta Phillips con mango ergonómico', 'Herramientas Manuales', 65.00,  35.00,  40, 10),
                ('FT-003', 'Cinta Métrica 5m',          'Cinta de medición retráctil de 5 metros',              'Medición',              95.00,  55.00,  30, 8),
                ('FT-004', 'Taladro Percutor 600W',     'Taladro eléctrico de impacto con velocidad variable',  'Herramientas Eléctricas', 1250.00, 850.00, 10, 3),
                ('FT-005', 'Cemento Gris 42.5kg',       'Saco de cemento Portland tipo I',                      'Materiales de Construcción', 220.00, 165.00, 50, 15),
                ('FT-006', 'Tubo PVC 1/2 pulgada',      'Tubo de PVC para agua fría, 3 metros de largo',        'Plomería',              45.00,  28.00,  60, 20),
                ('FT-007', 'Llave Ajustable 10 pulg',   'Llave ajustable de acero al cromo vanadio',            'Herramientas Manuales', 210.00, 140.00, 15, 5),
                ('FT-008', 'Pintura Blanca Mate 1gal',  'Pintura de interior/exterior acabado mate',            'Pinturas',              350.00, 245.00, 20, 5),
                ('FT-009', 'Cable Eléctrico 12AWG (m)',  'Cable THHN calibre 12 por metro',                     'Eléctricos',            18.00,  11.00,  200, 50),
                ('FT-010', 'Candado 50mm',               'Candado de latón macizo con 3 llaves',                'Cerrajería',            145.00, 90.00,  35, 10);"
        EjecutarComando(sqlInsertProductos, conexion)

    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: EncriptarContrasena
    ' Descripción: Aplica un hash SHA-256 a la contraseña en texto plano para
    '              almacenarla de forma segura en la base de datos.
    '              NOTA: Para producción se recomienda bcrypt o PBKDF2.
    ' Parámetro:   contrasenaPlana - La contraseña en texto claro.
    ' Retorna:     String con el hash SHA-256 en formato hexadecimal.
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Function EncriptarContrasena(contrasenaPlana As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            ' Convertir la contraseña a bytes, calcular el hash
            Dim bytesHash As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasenaPlana))

            ' Convertir cada byte del hash a su representación hexadecimal
            Dim constructorHash As New StringBuilder()
            For Each byteIndividual As Byte In bytesHash
                constructorHash.Append(byteIndividual.ToString("x2"))
            Next

            Return constructorHash.ToString()
        End Using
    End Function

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: EjecutarComando (Privado)
    ' Descripción: Ejecuta un comando SQL que no retorna datos (CREATE, INSERT,
    '              UPDATE, DELETE) usando una conexión ya abierta.
    ' Parámetros:  sql      - La sentencia SQL a ejecutar.
    '              conexion - La conexión SQLite abierta.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub EjecutarComando(sql As String, conexion As SqliteConnection)
        Using comando As New SqliteCommand(sql, conexion)
            comando.ExecuteNonQuery()
        End Using
    End Sub

End Module
