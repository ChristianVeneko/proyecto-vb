Imports Microsoft.Data.Sqlite

' Formulario: frmVentas
' Descripción: Punto de venta.

Public Class frmVentas

    Private dtCarrito As New DataTable()

    Private Sub frmVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = $"Ferree-Lara - Punto de Venta | Cajero: {SesionActual.NombreCompleto}"
        
        ' Inicializar DataTable del carrito
        dtCarrito.Columns.Add("ProductoId", GetType(Integer))
        dtCarrito.Columns.Add("Codigo", GetType(String))
        dtCarrito.Columns.Add("Nombre", GetType(String))
        dtCarrito.Columns.Add("Precio", GetType(Decimal))
        dtCarrito.Columns.Add("Cantidad", GetType(Integer))
        dtCarrito.Columns.Add("Subtotal", GetType(Decimal))
        dgvCarrito.DataSource = dtCarrito

        CargarClientes()
        CargarMetodosPago()
    End Sub

    Private Sub CargarClientes()
        Try
            Using conexion = DatabaseHelper.ObtenerConexion()
                conexion.Open()
                Dim cmd As New SqliteCommand("SELECT ClienteId, Nombre || ' ' || Apellido AS NombreCompleto FROM Clientes", conexion)
                Dim reader = cmd.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)
                cboCliente.DataSource = dt
                cboCliente.DisplayMember = "NombreCompleto"
                cboCliente.ValueMember = "ClienteId"
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar clientes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarMetodosPago()
        cboMetodoPago.Items.Add("Efectivo")
        cboMetodoPago.Items.Add("Tarjeta")
        cboMetodoPago.Items.Add("Transferencia")
        cboMetodoPago.SelectedIndex = 0
    End Sub

    Private Sub btnBuscarProducto_Click(sender As Object, e As EventArgs) Handles btnBuscarProducto.Click
        Try
            Dim filtro = txtBuscarProducto.Text.Trim()
            Using conexion = DatabaseHelper.ObtenerConexion()
                conexion.Open()
                Dim query = "SELECT ProductoId, Codigo, Nombre, PrecioVenta, Stock FROM Productos WHERE Activo = 1 AND (Codigo LIKE @Filtro OR Nombre LIKE @Filtro)"
                Dim cmd As New SqliteCommand(query, conexion)
                cmd.Parameters.AddWithValue("@Filtro", "%" & filtro & "%")
                Dim dt As New DataTable()
                dt.Load(cmd.ExecuteReader())
                dgvBusqueda.DataSource = dt
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al buscar: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAgregarCarrito_Click(sender As Object, e As EventArgs) Handles btnAgregarCarrito.Click
        If dgvBusqueda.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un producto")
            Return
        End If

        Dim row = dgvBusqueda.SelectedRows(0)
        Dim id = Convert.ToInt32(row.Cells("ProductoId").Value)
        Dim codigo = row.Cells("Codigo").Value.ToString()
        Dim nombre = row.Cells("Nombre").Value.ToString()
        Dim precio = Convert.ToDecimal(row.Cells("PrecioVenta").Value)
        Dim stock = Convert.ToInt32(row.Cells("Stock").Value)
        Dim cantidad = Convert.ToInt32(nudCantidad.Value)

        If cantidad > stock Then
            MessageBox.Show("No hay stock suficiente.")
            Return
        End If

        ' Verificar si ya existe
        For Each r As DataRow In dtCarrito.Rows
            If Convert.ToInt32(r("ProductoId")) = id Then
                r("Cantidad") = Convert.ToInt32(r("Cantidad")) + cantidad
                r("Subtotal") = Convert.ToInt32(r("Cantidad")) * precio
                CalcularTotales()
                Return
            End If
        Next

        dtCarrito.Rows.Add(id, codigo, nombre, precio, cantidad, precio * cantidad)
        CalcularTotales()
    End Sub

    Private Sub btnQuitarProducto_Click(sender As Object, e As EventArgs) Handles btnQuitarProducto.Click
        If dgvCarrito.SelectedRows.Count > 0 Then
            dgvCarrito.Rows.RemoveAt(dgvCarrito.SelectedRows(0).Index)
            CalcularTotales()
        End If
    End Sub

    Private Sub btnVaciarCarrito_Click(sender As Object, e As EventArgs) Handles btnVaciarCarrito.Click
        dtCarrito.Clear()
        CalcularTotales()
    End Sub

    Private Sub CalcularTotales()
        Dim subtotal As Decimal = 0
        For Each r As DataRow In dtCarrito.Rows
            subtotal += Convert.ToDecimal(r("Subtotal"))
        Next
        Dim impuesto = subtotal * 0.15D
        Dim total = subtotal + impuesto

        lblSubtotal.Text = subtotal.ToString("C2")
        lblImpuesto.Text = impuesto.ToString("C2")
        lblTotal.Text = total.ToString("C2")
    End Sub

    Private Sub txtMontoPagado_TextChanged(sender As Object, e As EventArgs) Handles txtMontoPagado.TextChanged
        Try
            Dim subtotal As Decimal = 0
            For Each r As DataRow In dtCarrito.Rows
                subtotal += Convert.ToDecimal(r("Subtotal"))
            Next
            Dim total = subtotal + (subtotal * 0.15D)
            Dim pagado = Convert.ToDecimal(txtMontoPagado.Text)
            
            If pagado >= total Then
                lblCambio.Text = (pagado - total).ToString("C2")
            Else
                lblCambio.Text = "$0.00"
            End If
        Catch ex As Exception
            lblCambio.Text = "$0.00"
        End Try
    End Sub

    Private Sub btnProcesarVenta_Click(sender As Object, e As EventArgs) Handles btnProcesarVenta.Click
        If dtCarrito.Rows.Count = 0 Then
            MessageBox.Show("El carrito está vacío.")
            Return
        End If

        Dim subtotal As Decimal = 0
        For Each r As DataRow In dtCarrito.Rows
            subtotal += Convert.ToDecimal(r("Subtotal"))
        Next
        Dim impuesto = subtotal * 0.15D
        Dim total = subtotal + impuesto
        Dim pagado As Decimal = 0

        Decimal.TryParse(txtMontoPagado.Text, pagado)
        If pagado < total Then
            MessageBox.Show("Monto pagado insuficiente.")
            Return
        End If

        Try
            Using conexion = DatabaseHelper.ObtenerConexion()
                conexion.Open()
                Using trans = conexion.BeginTransaction()
                    Try
                        ' Insertar Venta
                        Dim idCliente = Convert.ToInt32(cboCliente.SelectedValue)
                        Dim numFactura = "FT-" & DateTime.Now.ToString("yyyyMMdd-HHmmss")
                        Dim cmdVenta As New SqliteCommand("INSERT INTO Ventas (NumeroFactura, ClienteId, UsuarioId, Subtotal, Impuesto, Total, MontoPagado, Cambio, MetodoPago) VALUES (@num, @cli, @usu, @sub, @imp, @tot, @pag, @cam, @met); SELECT last_insert_rowid();", conexion, trans)
                        cmdVenta.Parameters.AddWithValue("@num", numFactura)
                        cmdVenta.Parameters.AddWithValue("@cli", idCliente)
                        cmdVenta.Parameters.AddWithValue("@usu", SesionActual.UsuarioId)
                        cmdVenta.Parameters.AddWithValue("@sub", subtotal)
                        cmdVenta.Parameters.AddWithValue("@imp", impuesto)
                        cmdVenta.Parameters.AddWithValue("@tot", total)
                        cmdVenta.Parameters.AddWithValue("@pag", pagado)
                        cmdVenta.Parameters.AddWithValue("@cam", pagado - total)
                        cmdVenta.Parameters.AddWithValue("@met", cboMetodoPago.Text)
                        
                        Dim idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar())

                        ' Insertar Detalles y Actualizar Stock
                        For Each r As DataRow In dtCarrito.Rows
                            Dim idProd = Convert.ToInt32(r("ProductoId"))
                            Dim cant = Convert.ToInt32(r("Cantidad"))
                            Dim prec = Convert.ToDecimal(r("Precio"))
                            Dim subt = Convert.ToDecimal(r("Subtotal"))
                            
                            Dim cmdDetalle As New SqliteCommand("INSERT INTO Detalle_Ventas (VentaId, ProductoId, Cantidad, PrecioUnitario, SubtotalLinea) VALUES (@vid, @pid, @cant, @prec, @subt)", conexion, trans)
                            cmdDetalle.Parameters.AddWithValue("@vid", idVenta)
                            cmdDetalle.Parameters.AddWithValue("@pid", idProd)
                            cmdDetalle.Parameters.AddWithValue("@cant", cant)
                            cmdDetalle.Parameters.AddWithValue("@prec", prec)
                            cmdDetalle.Parameters.AddWithValue("@subt", subt)
                            cmdDetalle.ExecuteNonQuery()

                            Dim cmdStock As New SqliteCommand("UPDATE Productos SET Stock = Stock - @cant WHERE ProductoId = @pid", conexion, trans)
                            cmdStock.Parameters.AddWithValue("@cant", cant)
                            cmdStock.Parameters.AddWithValue("@pid", idProd)
                            cmdStock.ExecuteNonQuery()
                        Next

                        trans.Commit()

                        Dim factura As New frmFactura()
                        factura.VentaIdRecibido = idVenta
                        factura.ShowDialog()

                        dtCarrito.Clear()
                        CalcularTotales()
                        txtMontoPagado.Clear()

                    Catch ex As Exception
                        trans.Rollback()
                        MessageBox.Show("Error en transacción: " & ex.Message)
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

End Class
