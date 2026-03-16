' ═══════════════════════════════════════════════════════════════════════════════
' Formulario: frmMantenimiento (Código Behind)
' Descripción: Implementa la lógica CRUD completa para la gestión de
'              Productos (Catálogo e Inventario) y Clientes.
'              Incluye validaciones estrictas, búsqueda dinámica y manejo
'              de excepciones en todas las operaciones de base de datos.
' Autor: Equipo Ferree-Lara
' Fecha: Marzo 2026
' ═══════════════════════════════════════════════════════════════════════════════

Imports Microsoft.Data.Sqlite

Public Class frmMantenimiento

    ' ── Variables de estado para controlar el modo de operación ──
    ' Almacenan el ID del registro seleccionado para edición
    Private productoIdSeleccionado As Integer = 0
    Private clienteIdSeleccionado As Integer = 0

    ' Bandera que indica si estamos en modo "edición" o "nuevo registro"
    Private modoEdicionProducto As Boolean = False
    Private modoEdicionCliente As Boolean = False

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: Form_Load
    ' Descripción: Carga inicial del formulario. Llena los DataGridView con
    '              los registros existentes y configura el estado inicial.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub frmMantenimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarProductos()
        CargarClientes()
        ConfigurarEstadoInicialProductos()
        ConfigurarEstadoInicialClientes()
    End Sub

    ' ══════════════════════════════════════════════════════════════════════════
    '                        SECCIÓN: PRODUCTOS
    ' ══════════════════════════════════════════════════════════════════════════

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: CargarProductos
    ' Descripción: Consulta todos los productos activos de la base de datos
    '              y los muestra en el DataGridView de productos.
    ' Parámetro:   filtro - (Opcional) Texto para filtrar por código, nombre
    '              o categoría. Si está vacío, carga todos los productos.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub CargarProductos(Optional filtro As String = "")
        Try
            Using conexion As SqliteConnection = DatabaseHelper.ObtenerConexion()
                conexion.Open()

                Dim sql As String
                If String.IsNullOrWhiteSpace(filtro) Then
                    ' Cargar todos los productos activos
                    sql = "SELECT ProductoId, Codigo, Nombre, Descripcion, Categoria,
                           PrecioVenta, PrecioCosto, Stock, StockMinimo
                           FROM Productos WHERE Activo = 1
                           ORDER BY Nombre;"
                Else
                    ' Filtrar por código, nombre o categoría
                    sql = "SELECT ProductoId, Codigo, Nombre, Descripcion, Categoria,
                           PrecioVenta, PrecioCosto, Stock, StockMinimo
                           FROM Productos
                           WHERE Activo = 1
                             AND (Codigo LIKE @filtro
                              OR Nombre LIKE @filtro
                              OR Categoria LIKE @filtro)
                           ORDER BY Nombre;"
                End If

                Using comando As New SqliteCommand(sql, conexion)
                    If Not String.IsNullOrWhiteSpace(filtro) Then
                        comando.Parameters.AddWithValue("@filtro", $"%{filtro}%")
                    End If

                    ' Crear un DataTable para almacenar los resultados
                    Dim tabla As New DataTable()
                    Using lector As SqliteDataReader = comando.ExecuteReader()
                        tabla.Load(lector)
                    End Using

                    ' Asignar la tabla al DataGridView
                    dgvProductos.DataSource = tabla

                    ' Configurar nombres de columnas amigables
                    If dgvProductos.Columns.Count > 0 Then
                        dgvProductos.Columns("ProductoId").Visible = False
                        dgvProductos.Columns("Codigo").HeaderText = "Código"
                        dgvProductos.Columns("Nombre").HeaderText = "Nombre"
                        dgvProductos.Columns("Descripcion").HeaderText = "Descripción"
                        dgvProductos.Columns("Categoria").HeaderText = "Categoría"
                        dgvProductos.Columns("PrecioVenta").HeaderText = "P. Venta"
                        dgvProductos.Columns("PrecioVenta").DefaultCellStyle.Format = "N2"
                        dgvProductos.Columns("PrecioCosto").HeaderText = "P. Costo"
                        dgvProductos.Columns("PrecioCosto").DefaultCellStyle.Format = "N2"
                        dgvProductos.Columns("Stock").HeaderText = "Stock"
                        dgvProductos.Columns("StockMinimo").HeaderText = "Stock Mín."
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar productos:" & vbCrLf & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnProdNuevo_Click
    ' Descripción: Prepara el formulario para registrar un nuevo producto.
    '              Limpia los campos y habilita la entrada de datos.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnProdNuevo_Click(sender As Object, e As EventArgs) Handles btnProdNuevo.Click
        LimpiarCamposProducto()
        modoEdicionProducto = False
        productoIdSeleccionado = 0
        HabilitarCamposProducto(True)
        txtProdCodigo.Focus()
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnProdGuardar_Click
    ' Descripción: Guarda un nuevo producto o actualiza uno existente según
    '              el modo de operación (nuevo o edición).
    '              Valida todos los campos antes de persistir en la BD.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnProdGuardar_Click(sender As Object, e As EventArgs) Handles btnProdGuardar.Click
        ' Paso 1: Validar los campos del formulario
        If Not ValidarCamposProducto() Then Return

        Try
            Using conexion As SqliteConnection = DatabaseHelper.ObtenerConexion()
                conexion.Open()

                If modoEdicionProducto Then
                    ' ── MODO EDICIÓN: Actualizar producto existente ──
                    Dim sqlUpdate As String = "
                        UPDATE Productos SET
                            Codigo = @codigo, Nombre = @nombre,
                            Descripcion = @descripcion, Categoria = @categoria,
                            PrecioVenta = @precioVenta, PrecioCosto = @precioCosto,
                            Stock = @stock, StockMinimo = @stockMin
                        WHERE ProductoId = @id;"

                    Using cmd As New SqliteCommand(sqlUpdate, conexion)
                        AgregarParametrosProducto(cmd)
                        cmd.Parameters.AddWithValue("@id", productoIdSeleccionado)
                        cmd.ExecuteNonQuery()
                    End Using

                    MessageBox.Show("Producto actualizado exitosamente.",
                                  "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    ' ── MODO NUEVO: Insertar nuevo producto ──
                    ' Verificar que el código no exista previamente
                    Dim sqlCheck As String = "SELECT COUNT(*) FROM Productos WHERE Codigo = @codigo;"
                    Using cmdCheck As New SqliteCommand(sqlCheck, conexion)
                        cmdCheck.Parameters.AddWithValue("@codigo", txtProdCodigo.Text.Trim())
                        If Convert.ToInt64(cmdCheck.ExecuteScalar()) > 0 Then
                            MessageBox.Show("Ya existe un producto con ese código.",
                                          "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            txtProdCodigo.Focus()
                            Return
                        End If
                    End Using

                    Dim sqlInsert As String = "
                        INSERT INTO Productos (Codigo, Nombre, Descripcion, Categoria,
                            PrecioVenta, PrecioCosto, Stock, StockMinimo)
                        VALUES (@codigo, @nombre, @descripcion, @categoria,
                            @precioVenta, @precioCosto, @stock, @stockMin);"

                    Using cmd As New SqliteCommand(sqlInsert, conexion)
                        AgregarParametrosProducto(cmd)
                        cmd.ExecuteNonQuery()
                    End Using

                    MessageBox.Show("Producto registrado exitosamente.",
                                  "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using

            ' Recargar el grid y limpiar el formulario
            CargarProductos()
            LimpiarCamposProducto()
            ConfigurarEstadoInicialProductos()

        Catch ex As Exception
            MessageBox.Show("Error al guardar el producto:" & vbCrLf & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnProdEditar_Click
    ' Descripción: Carga los datos del producto seleccionado en el DataGridView
    '              en los campos del formulario para su modificación.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnProdEditar_Click(sender As Object, e As EventArgs) Handles btnProdEditar.Click
        If dgvProductos.CurrentRow Is Nothing Then
            MessageBox.Show("Seleccione un producto del listado para editar.",
                          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Obtener los datos de la fila seleccionada
        Dim fila As DataGridViewRow = dgvProductos.CurrentRow
        productoIdSeleccionado = Convert.ToInt32(fila.Cells("ProductoId").Value)
        modoEdicionProducto = True

        ' Cargar los datos en los campos del formulario
        txtProdCodigo.Text = fila.Cells("Codigo").Value.ToString()
        txtProdNombre.Text = fila.Cells("Nombre").Value.ToString()
        txtProdDescripcion.Text = fila.Cells("Descripcion").Value.ToString()
        cboProdCategoria.Text = fila.Cells("Categoria").Value.ToString()
        txtProdPrecioVenta.Text = Convert.ToDecimal(fila.Cells("PrecioVenta").Value).ToString("F2")
        txtProdPrecioCosto.Text = Convert.ToDecimal(fila.Cells("PrecioCosto").Value).ToString("F2")
        txtProdStock.Text = fila.Cells("Stock").Value.ToString()
        txtProdStockMin.Text = fila.Cells("StockMinimo").Value.ToString()

        ' Habilitar los campos para edición (código no editable en modo edición)
        HabilitarCamposProducto(True)
        txtProdCodigo.Enabled = False ' El código no debe cambiar al editar
        txtProdNombre.Focus()
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnProdEliminar_Click
    ' Descripción: Desactiva (eliminación lógica) el producto seleccionado.
    '              No se elimina físicamente para preservar el historial de ventas.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnProdEliminar_Click(sender As Object, e As EventArgs) Handles btnProdEliminar.Click
        If dgvProductos.CurrentRow Is Nothing Then
            MessageBox.Show("Seleccione un producto del listado para desactivar.",
                          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim nombreProducto As String = dgvProductos.CurrentRow.Cells("Nombre").Value.ToString()
        Dim resultado As DialogResult = MessageBox.Show(
            $"¿Está seguro que desea desactivar el producto '{nombreProducto}'?" & vbCrLf &
            "El producto no aparecerá en ventas pero se conservará en el historial.",
            "Confirmar Desactivación",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If resultado <> DialogResult.Yes Then Return

        Try
            Dim idProducto As Integer = Convert.ToInt32(dgvProductos.CurrentRow.Cells("ProductoId").Value)

            Using conexion As SqliteConnection = DatabaseHelper.ObtenerConexion()
                conexion.Open()

                Dim sql As String = "UPDATE Productos SET Activo = 0 WHERE ProductoId = @id;"
                Using cmd As New SqliteCommand(sql, conexion)
                    cmd.Parameters.AddWithValue("@id", idProducto)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Producto desactivado exitosamente.",
                          "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarProductos()
            LimpiarCamposProducto()

        Catch ex As Exception
            MessageBox.Show("Error al desactivar el producto:" & vbCrLf & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnProdBuscar_Click
    ' Descripción: Ejecuta la búsqueda de productos con el texto ingresado.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnProdBuscar_Click(sender As Object, e As EventArgs) Handles btnProdBuscar.Click
        CargarProductos(txtProdBuscar.Text.Trim())
    End Sub

    ''' <summary>
    ''' Permite buscar al presionar Enter en el campo de búsqueda.
    ''' </summary>
    Private Sub txtProdBuscar_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProdBuscar.KeyDown
        If e.KeyCode = Keys.Enter Then
            CargarProductos(txtProdBuscar.Text.Trim())
            e.SuppressKeyPress = True
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnProdLimpiar_Click
    ' Descripción: Limpia todos los campos y reinicia el estado del formulario.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnProdLimpiar_Click(sender As Object, e As EventArgs) Handles btnProdLimpiar.Click
        LimpiarCamposProducto()
        ConfigurarEstadoInicialProductos()
        CargarProductos()
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: ValidarCamposProducto
    ' Descripción: Valida estrictamente todos los campos del formulario de
    '              productos antes de guardar o actualizar.
    ' Retorna:     True si todos los campos son válidos, False si hay errores.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Function ValidarCamposProducto() As Boolean
        ' Validar campo Código (no vacío)
        If String.IsNullOrWhiteSpace(txtProdCodigo.Text) Then
            MessageBox.Show("El campo 'Código' es obligatorio.",
                          "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtProdCodigo.Focus()
            Return False
        End If

        ' Validar campo Nombre (no vacío)
        If String.IsNullOrWhiteSpace(txtProdNombre.Text) Then
            MessageBox.Show("El campo 'Nombre' es obligatorio.",
                          "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtProdNombre.Focus()
            Return False
        End If

        ' Validar que se seleccionó una categoría
        If cboProdCategoria.SelectedIndex < 0 Then
            MessageBox.Show("Debe seleccionar una categoría.",
                          "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboProdCategoria.Focus()
            Return False
        End If

        ' Validar Precio de Venta (numérico y positivo)
        Dim precioVenta As Decimal
        If Not Decimal.TryParse(txtProdPrecioVenta.Text, precioVenta) OrElse precioVenta < 0 Then
            MessageBox.Show("El 'Precio de Venta' debe ser un número válido mayor o igual a 0.",
                          "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtProdPrecioVenta.Focus()
            Return False
        End If

        ' Validar Precio de Costo (numérico y positivo)
        Dim precioCosto As Decimal
        If Not Decimal.TryParse(txtProdPrecioCosto.Text, precioCosto) OrElse precioCosto < 0 Then
            MessageBox.Show("El 'Precio de Costo' debe ser un número válido mayor o igual a 0.",
                          "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtProdPrecioCosto.Focus()
            Return False
        End If

        ' Validar Stock (entero y no negativo)
        Dim stock As Integer
        If Not Integer.TryParse(txtProdStock.Text, stock) OrElse stock < 0 Then
            MessageBox.Show("El 'Stock' debe ser un número entero mayor o igual a 0.",
                          "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtProdStock.Focus()
            Return False
        End If

        ' Validar Stock Mínimo (entero y no negativo)
        Dim stockMin As Integer
        If Not Integer.TryParse(txtProdStockMin.Text, stockMin) OrElse stockMin < 0 Then
            MessageBox.Show("El 'Stock Mínimo' debe ser un número entero mayor o igual a 0.",
                          "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtProdStockMin.Focus()
            Return False
        End If

        Return True
    End Function

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: AgregarParametrosProducto
    ' Descripción: Agrega los parámetros SQL comunes para INSERT/UPDATE de
    '              productos a partir de los valores de los campos.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub AgregarParametrosProducto(cmd As SqliteCommand)
        cmd.Parameters.AddWithValue("@codigo", txtProdCodigo.Text.Trim())
        cmd.Parameters.AddWithValue("@nombre", txtProdNombre.Text.Trim())
        cmd.Parameters.AddWithValue("@descripcion", txtProdDescripcion.Text.Trim())
        cmd.Parameters.AddWithValue("@categoria", cboProdCategoria.Text)
        cmd.Parameters.AddWithValue("@precioVenta", Decimal.Parse(txtProdPrecioVenta.Text))
        cmd.Parameters.AddWithValue("@precioCosto", Decimal.Parse(txtProdPrecioCosto.Text))
        cmd.Parameters.AddWithValue("@stock", Integer.Parse(txtProdStock.Text))
        cmd.Parameters.AddWithValue("@stockMin", Integer.Parse(txtProdStockMin.Text))
    End Sub

    ''' <summary>Limpia todos los campos de producto y reinicia variables.</summary>
    Private Sub LimpiarCamposProducto()
        txtProdCodigo.Clear()
        txtProdNombre.Clear()
        txtProdDescripcion.Clear()
        cboProdCategoria.SelectedIndex = -1
        txtProdPrecioVenta.Clear()
        txtProdPrecioCosto.Clear()
        txtProdStock.Clear()
        txtProdStockMin.Clear()
        txtProdBuscar.Clear()
        productoIdSeleccionado = 0
        modoEdicionProducto = False
    End Sub

    ''' <summary>Habilita o deshabilita los campos de entrada de productos.</summary>
    Private Sub HabilitarCamposProducto(habilitar As Boolean)
        txtProdCodigo.Enabled = habilitar
        txtProdNombre.Enabled = habilitar
        txtProdDescripcion.Enabled = habilitar
        cboProdCategoria.Enabled = habilitar
        txtProdPrecioVenta.Enabled = habilitar
        txtProdPrecioCosto.Enabled = habilitar
        txtProdStock.Enabled = habilitar
        txtProdStockMin.Enabled = habilitar
        btnProdGuardar.Enabled = habilitar
    End Sub

    ''' <summary>Configura el estado inicial de la pestaña de productos.</summary>
    Private Sub ConfigurarEstadoInicialProductos()
        HabilitarCamposProducto(False)
    End Sub

    ' ══════════════════════════════════════════════════════════════════════════
    '                        SECCIÓN: CLIENTES
    ' ══════════════════════════════════════════════════════════════════════════

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: CargarClientes
    ' Descripción: Consulta todos los clientes de la base de datos y los
    '              muestra en el DataGridView. Permite filtrar opcionalmente.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub CargarClientes(Optional filtro As String = "")
        Try
            Using conexion As SqliteConnection = DatabaseHelper.ObtenerConexion()
                conexion.Open()

                Dim sql As String
                If String.IsNullOrWhiteSpace(filtro) Then
                    sql = "SELECT ClienteId, Cedula, Nombre, Apellido,
                           Telefono, Correo, Direccion, FechaRegistro
                           FROM Clientes ORDER BY Apellido, Nombre;"
                Else
                    sql = "SELECT ClienteId, Cedula, Nombre, Apellido,
                           Telefono, Correo, Direccion, FechaRegistro
                           FROM Clientes
                           WHERE Cedula LIKE @filtro
                              OR Nombre LIKE @filtro
                              OR Apellido LIKE @filtro
                           ORDER BY Apellido, Nombre;"
                End If

                Using comando As New SqliteCommand(sql, conexion)
                    If Not String.IsNullOrWhiteSpace(filtro) Then
                        comando.Parameters.AddWithValue("@filtro", $"%{filtro}%")
                    End If

                    Dim tabla As New DataTable()
                    Using lector As SqliteDataReader = comando.ExecuteReader()
                        tabla.Load(lector)
                    End Using

                    dgvClientes.DataSource = tabla

                    If dgvClientes.Columns.Count > 0 Then
                        dgvClientes.Columns("ClienteId").Visible = False
                        dgvClientes.Columns("Cedula").HeaderText = "Cédula/ID"
                        dgvClientes.Columns("Nombre").HeaderText = "Nombre"
                        dgvClientes.Columns("Apellido").HeaderText = "Apellido"
                        dgvClientes.Columns("Telefono").HeaderText = "Teléfono"
                        dgvClientes.Columns("Correo").HeaderText = "Correo"
                        dgvClientes.Columns("Direccion").HeaderText = "Dirección"
                        dgvClientes.Columns("FechaRegistro").HeaderText = "Fecha Registro"
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar clientes:" & vbCrLf & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnCliNuevo_Click
    ' Descripción: Prepara el formulario para registrar un nuevo cliente.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnCliNuevo_Click(sender As Object, e As EventArgs) Handles btnCliNuevo.Click
        LimpiarCamposCliente()
        modoEdicionCliente = False
        clienteIdSeleccionado = 0
        HabilitarCamposCliente(True)
        txtCliCedula.Focus()
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnCliGuardar_Click
    ' Descripción: Guarda un nuevo cliente o actualiza uno existente.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnCliGuardar_Click(sender As Object, e As EventArgs) Handles btnCliGuardar.Click
        If Not ValidarCamposCliente() Then Return

        Try
            Using conexion As SqliteConnection = DatabaseHelper.ObtenerConexion()
                conexion.Open()

                If modoEdicionCliente Then
                    ' ── MODO EDICIÓN: Actualizar cliente existente ──
                    Dim sqlUpdate As String = "
                        UPDATE Clientes SET
                            Nombre = @nombre, Apellido = @apellido,
                            Cedula = @cedula, Telefono = @telefono,
                            Correo = @correo, Direccion = @direccion
                        WHERE ClienteId = @id;"

                    Using cmd As New SqliteCommand(sqlUpdate, conexion)
                        AgregarParametrosCliente(cmd)
                        cmd.Parameters.AddWithValue("@id", clienteIdSeleccionado)
                        cmd.ExecuteNonQuery()
                    End Using

                    MessageBox.Show("Cliente actualizado exitosamente.",
                                  "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    ' ── MODO NUEVO: Insertar nuevo cliente ──
                    ' Verificar que la cédula no esté duplicada
                    Dim sqlCheck As String = "SELECT COUNT(*) FROM Clientes WHERE Cedula = @cedula;"
                    Using cmdCheck As New SqliteCommand(sqlCheck, conexion)
                        cmdCheck.Parameters.AddWithValue("@cedula", txtCliCedula.Text.Trim())
                        If Convert.ToInt64(cmdCheck.ExecuteScalar()) > 0 Then
                            MessageBox.Show("Ya existe un cliente con esa cédula/identificación.",
                                          "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            txtCliCedula.Focus()
                            Return
                        End If
                    End Using

                    Dim sqlInsert As String = "
                        INSERT INTO Clientes (Nombre, Apellido, Cedula, Telefono, Correo, Direccion)
                        VALUES (@nombre, @apellido, @cedula, @telefono, @correo, @direccion);"

                    Using cmd As New SqliteCommand(sqlInsert, conexion)
                        AgregarParametrosCliente(cmd)
                        cmd.ExecuteNonQuery()
                    End Using

                    MessageBox.Show("Cliente registrado exitosamente.",
                                  "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using

            CargarClientes()
            LimpiarCamposCliente()
            ConfigurarEstadoInicialClientes()

        Catch ex As Exception
            MessageBox.Show("Error al guardar el cliente:" & vbCrLf & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnCliEditar_Click
    ' Descripción: Carga los datos del cliente seleccionado para su edición.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnCliEditar_Click(sender As Object, e As EventArgs) Handles btnCliEditar.Click
        If dgvClientes.CurrentRow Is Nothing Then
            MessageBox.Show("Seleccione un cliente del listado para editar.",
                          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim fila As DataGridViewRow = dgvClientes.CurrentRow
        clienteIdSeleccionado = Convert.ToInt32(fila.Cells("ClienteId").Value)
        modoEdicionCliente = True

        txtCliCedula.Text = fila.Cells("Cedula").Value.ToString()
        txtCliNombre.Text = fila.Cells("Nombre").Value.ToString()
        txtCliApellido.Text = fila.Cells("Apellido").Value.ToString()
        txtCliTelefono.Text = fila.Cells("Telefono").Value.ToString()
        txtCliCorreo.Text = fila.Cells("Correo").Value.ToString()
        txtCliDireccion.Text = fila.Cells("Direccion").Value.ToString()

        HabilitarCamposCliente(True)
        txtCliCedula.Enabled = False ' La cédula no se edita
        txtCliNombre.Focus()
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnCliEliminar_Click
    ' Descripción: Elimina el cliente seleccionado (solo si no tiene ventas
    '              asociadas y no es "Consumidor Final").
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnCliEliminar_Click(sender As Object, e As EventArgs) Handles btnCliEliminar.Click
        If dgvClientes.CurrentRow Is Nothing Then
            MessageBox.Show("Seleccione un cliente del listado para eliminar.",
                          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim idCliente As Integer = Convert.ToInt32(dgvClientes.CurrentRow.Cells("ClienteId").Value)
        Dim nombreCliente As String = dgvClientes.CurrentRow.Cells("Nombre").Value.ToString() & " " &
                                      dgvClientes.CurrentRow.Cells("Apellido").Value.ToString()

        ' No permitir eliminar "Consumidor Final" (ClienteId = 1)
        If idCliente = 1 Then
            MessageBox.Show("No se puede eliminar el registro 'Consumidor Final'.",
                          "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim resultado As DialogResult = MessageBox.Show(
            $"¿Está seguro que desea eliminar al cliente '{nombreCliente}'?",
            "Confirmar Eliminación",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If resultado <> DialogResult.Yes Then Return

        Try
            Using conexion As SqliteConnection = DatabaseHelper.ObtenerConexion()
                conexion.Open()

                ' Verificar que el cliente no tenga ventas asociadas
                Dim sqlCheck As String = "SELECT COUNT(*) FROM Ventas WHERE ClienteId = @id;"
                Using cmdCheck As New SqliteCommand(sqlCheck, conexion)
                    cmdCheck.Parameters.AddWithValue("@id", idCliente)
                    If Convert.ToInt64(cmdCheck.ExecuteScalar()) > 0 Then
                        MessageBox.Show(
                            "No se puede eliminar este cliente porque tiene ventas registradas.",
                            "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                ' Eliminar el cliente
                Dim sqlDelete As String = "DELETE FROM Clientes WHERE ClienteId = @id;"
                Using cmd As New SqliteCommand(sqlDelete, conexion)
                    cmd.Parameters.AddWithValue("@id", idCliente)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Cliente eliminado exitosamente.",
                          "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarClientes()
            LimpiarCamposCliente()

        Catch ex As Exception
            MessageBox.Show("Error al eliminar el cliente:" & vbCrLf & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnCliBuscar_Click
    ' Descripción: Filtra los clientes por cédula, nombre o apellido.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnCliBuscar_Click(sender As Object, e As EventArgs) Handles btnCliBuscar.Click
        CargarClientes(txtCliBuscar.Text.Trim())
    End Sub

    Private Sub txtCliBuscar_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCliBuscar.KeyDown
        If e.KeyCode = Keys.Enter Then
            CargarClientes(txtCliBuscar.Text.Trim())
            e.SuppressKeyPress = True
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnCliLimpiar_Click
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnCliLimpiar_Click(sender As Object, e As EventArgs) Handles btnCliLimpiar.Click
        LimpiarCamposCliente()
        ConfigurarEstadoInicialClientes()
        CargarClientes()
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: ValidarCamposCliente
    ' Descripción: Valida los campos obligatorios del formulario de clientes.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Function ValidarCamposCliente() As Boolean
        If String.IsNullOrWhiteSpace(txtCliCedula.Text) Then
            MessageBox.Show("El campo 'Cédula/ID' es obligatorio.",
                          "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCliCedula.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtCliNombre.Text) Then
            MessageBox.Show("El campo 'Nombre' es obligatorio.",
                          "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCliNombre.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtCliApellido.Text) Then
            MessageBox.Show("El campo 'Apellido' es obligatorio.",
                          "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCliApellido.Focus()
            Return False
        End If

        Return True
    End Function

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: AgregarParametrosCliente
    ' Descripción: Agrega los parámetros SQL comunes para clientes.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub AgregarParametrosCliente(cmd As SqliteCommand)
        cmd.Parameters.AddWithValue("@nombre", txtCliNombre.Text.Trim())
        cmd.Parameters.AddWithValue("@apellido", txtCliApellido.Text.Trim())
        cmd.Parameters.AddWithValue("@cedula", txtCliCedula.Text.Trim())
        cmd.Parameters.AddWithValue("@telefono", If(String.IsNullOrWhiteSpace(txtCliTelefono.Text), "", txtCliTelefono.Text.Trim()))
        cmd.Parameters.AddWithValue("@correo", If(String.IsNullOrWhiteSpace(txtCliCorreo.Text), "", txtCliCorreo.Text.Trim()))
        cmd.Parameters.AddWithValue("@direccion", If(String.IsNullOrWhiteSpace(txtCliDireccion.Text), "", txtCliDireccion.Text.Trim()))
    End Sub

    ''' <summary>Limpia todos los campos de cliente.</summary>
    Private Sub LimpiarCamposCliente()
        txtCliCedula.Clear()
        txtCliNombre.Clear()
        txtCliApellido.Clear()
        txtCliTelefono.Clear()
        txtCliCorreo.Clear()
        txtCliDireccion.Clear()
        txtCliBuscar.Clear()
        clienteIdSeleccionado = 0
        modoEdicionCliente = False
    End Sub

    ''' <summary>Habilita o deshabilita los campos de entrada de clientes.</summary>
    Private Sub HabilitarCamposCliente(habilitar As Boolean)
        txtCliCedula.Enabled = habilitar
        txtCliNombre.Enabled = habilitar
        txtCliApellido.Enabled = habilitar
        txtCliTelefono.Enabled = habilitar
        txtCliCorreo.Enabled = habilitar
        txtCliDireccion.Enabled = habilitar
        btnCliGuardar.Enabled = habilitar
    End Sub

    ''' <summary>Configura el estado inicial de la pestaña de clientes.</summary>
    Private Sub ConfigurarEstadoInicialClientes()
        HabilitarCamposCliente(False)
    End Sub

End Class
