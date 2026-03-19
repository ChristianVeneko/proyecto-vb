Imports Microsoft.Data.Sqlite
Imports System.Drawing.Printing

' Formulario: frmFactura
' Descripción: Vista que enlista los productos comprados de la venta para
'              conferir al cliente los montos gastados. Permite imprimir el recibo.

Public Class frmFactura

    ' Campo público para recibir el ID de la venta desde frmVentas
    Public VentaIdRecibido As Integer = 0

    ' Controles del formulario
    Private rtbFactura As New RichTextBox()
    Private pnlBotones As New Panel()
    Private btnImprimir As New Button()
    Private btnCerrar As New Button()

    Private Sub frmFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = $"Ferree-Lara - Factura | Venta #{VentaIdRecibido}"
        Me.Size = New Size(500, 600)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ConfigurarControles()
        GenerarRecibo()
    End Sub

    Private Sub ConfigurarControles()
        ' Panel de botones
        pnlBotones.Dock = DockStyle.Bottom
        pnlBotones.Height = 50
        pnlBotones.Padding = New Padding(10)

        ' Botón Imprimir
        btnImprimir.Text = "Imprimir"
        btnImprimir.Size = New Size(100, 30)
        btnImprimir.Location = New Point(pnlBotones.Width \ 2 - 110, 10)
        btnImprimir.Anchor = AnchorStyles.None
        AddHandler btnImprimir.Click, AddressOf btnImprimir_Click

        ' Botón Cerrar
        btnCerrar.Text = "Cerrar"
        btnCerrar.Size = New Size(100, 30)
        btnCerrar.Location = New Point(pnlBotones.Width \ 2 + 10, 10)
        btnCerrar.Anchor = AnchorStyles.None
        AddHandler btnCerrar.Click, AddressOf btnCerrar_Click

        pnlBotones.Controls.Add(btnImprimir)
        pnlBotones.Controls.Add(btnCerrar)

        ' RichTextBox para el recibo
        rtbFactura.Dock = DockStyle.Fill
        rtbFactura.Font = New Font("Consolas", 10)
        rtbFactura.ReadOnly = True
        rtbFactura.BackColor = Color.White

        Me.Controls.Add(rtbFactura)
        Me.Controls.Add(pnlBotones)
    End Sub

    Private Sub GenerarRecibo()
        Try
            Using conexion = DatabaseHelper.ObtenerConexion()
                conexion.Open()

                ' Consultar encabezado de la venta
                Dim sqlVenta = "
                    SELECT v.NumeroFactura, v.FechaVenta, v.Subtotal, v.Impuesto,
                           v.Total, v.MontoPagado, v.Cambio, v.MetodoPago,
                           c.Nombre || ' ' || c.Apellido AS Cliente, c.Cedula,
                           u.NombreCompleto AS Cajero
                    FROM Ventas v
                    INNER JOIN Clientes c ON v.ClienteId = c.ClienteId
                    INNER JOIN Usuarios u ON v.UsuarioId = u.UsuarioId
                    WHERE v.VentaId = @VentaId"

                Dim cmdVenta As New SqliteCommand(sqlVenta, conexion)
                cmdVenta.Parameters.AddWithValue("@VentaId", VentaIdRecibido)

                Dim reader = cmdVenta.ExecuteReader()
                If reader.Read() Then
                    Dim numFactura = reader("NumeroFactura").ToString()
                    Dim fecha = Convert.ToDateTime(reader("FechaVenta")).ToString("dd/MM/yyyy HH:mm")
                    Dim cliente = reader("Cliente").ToString()
                    Dim cedula = reader("Cedula").ToString()
                    Dim cajero = reader("Cajero").ToString()
                    Dim metodoPago = reader("MetodoPago").ToString()
                    Dim subtotal = Convert.ToDecimal(reader("Subtotal"))
                    Dim impuesto = Convert.ToDecimal(reader("Impuesto"))
                    Dim total = Convert.ToDecimal(reader("Total"))
                    Dim montoPagado = Convert.ToDecimal(reader("MontoPagado"))
                    Dim cambio = Convert.ToDecimal(reader("Cambio"))
                    reader.Close()

                    Dim sb As New Text.StringBuilder()
                    sb.AppendLine("════════════════════════════════════════")
                    sb.AppendLine("              FERREE-LARA               ")
                    sb.AppendLine("   Ferretería y Soluciones Técnicas     ")
                    sb.AppendLine("        Tel: (504) 2222-3333            ")
                    sb.AppendLine("════════════════════════════════════════")
                    sb.AppendLine()
                    sb.AppendLine($"Factura No.: {numFactura}")
                    sb.AppendLine($"Fecha/Hora : {fecha}")
                    sb.AppendLine($"Cliente    : {cliente}")
                    sb.AppendLine($"Cédula     : {cedula}")
                    sb.AppendLine($"Cajero     : {cajero}")
                    sb.AppendLine($"Método Pago: {metodoPago}")
                    sb.AppendLine()
                    sb.AppendLine("════════════════════════════════════════")
                    sb.AppendLine(String.Format("{0,-22} {1,4} {2,10}", "PRODUCTO", "CANT", "SUBTOTAL"))
                    sb.AppendLine("────────────────────────────────────────")

                    ' Consultar detalle de productos
                    Dim sqlDetalle = "
                        SELECT p.Nombre, d.Cantidad, d.PrecioUnitario, d.SubtotalLinea
                        FROM Detalle_Ventas d
                        INNER JOIN Productos p ON d.ProductoId = p.ProductoId
                        WHERE d.VentaId = @VentaId"

                    Dim cmdDetalle As New SqliteCommand(sqlDetalle, conexion)
                    cmdDetalle.Parameters.AddWithValue("@VentaId", VentaIdRecibido)
                    Dim readerDet = cmdDetalle.ExecuteReader()

                    While readerDet.Read()
                        Dim producto = readerDet("Nombre").ToString()
                        If producto.Length > 22 Then producto = producto.Substring(0, 19) & "..."
                        Dim cant = readerDet("Cantidad").ToString()
                        Dim subtL = Convert.ToDecimal(readerDet("SubtotalLinea")).ToString("N2")

                        sb.AppendLine(String.Format("{0,-22} {1,4} {2,10}", producto, cant, subtL))
                    End While
                    readerDet.Close()

                    sb.AppendLine()
                    sb.AppendLine("════════════════════════════════════════")
                    sb.AppendLine(String.Format("{0,-28} {1,10}", "SUBTOTAL:", subtotal.ToString("N2")))
                    sb.AppendLine(String.Format("{0,-28} {1,10}", "IVA (15%):", impuesto.ToString("N2")))
                    sb.AppendLine("────────────────────────────────────────")
                    sb.AppendLine(String.Format("{0,-28} {1,10}", "TOTAL:", total.ToString("N2")))
                    sb.AppendLine()
                    sb.AppendLine(String.Format("{0,-28} {1,10}", "PAGADO:", montoPagado.ToString("N2")))
                    sb.AppendLine(String.Format("{0,-28} {1,10}", "CAMBIO:", cambio.ToString("N2")))
                    sb.AppendLine()
                    sb.AppendLine("════════════════════════════════════════")
                    sb.AppendLine("        ¡GRACIAS POR SU COMPRA!         ")
                    sb.AppendLine("     Vuelva pronto a Ferree-Lara        ")
                    sb.AppendLine("════════════════════════════════════════")

                    rtbFactura.Text = sb.ToString()
                Else
                    rtbFactura.Text = "Error: No se encontró la venta."
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar factura: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs)
        Try
            Dim printDoc As New PrintDocument()
            AddHandler printDoc.PrintPage, AddressOf PrintPage_Handler

            Dim printDialog As New PrintDialog()
            printDialog.Document = printDoc

            If printDialog.ShowDialog() = DialogResult.OK Then
                printDoc.Print()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al imprimir: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintPage_Handler(sender As Object, e As PrintPageEventArgs)
        Dim fuente As New Font("Consolas", 10)
        Dim brush As New SolidBrush(Color.Black)
        Dim lineas = rtbFactura.Text.Split(vbLf.ToCharArray())
        Dim y As Single = e.MarginBounds.Top

        For Each linea In lineas
            e.Graphics.DrawString(linea, fuente, brush, e.MarginBounds.Left, y)
            y += fuente.GetHeight(e.Graphics)
        Next
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

End Class
