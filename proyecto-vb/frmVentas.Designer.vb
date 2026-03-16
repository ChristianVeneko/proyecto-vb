' ═══════════════════════════════════════════════════════════════════════════════
' Formulario: frmVentas (Archivo Designer)
' Descripción: Punto de venta (POS) de Ferree-Lara. Diseño dividido en:
'              - Lado izquierdo: búsqueda de productos y carrito (DataGridView)
'              - Lado derecho: selección de cliente, panel de totales y pago
' ═══════════════════════════════════════════════════════════════════════════════

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVentas
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()

        ' ── Controles principales ──
        Me.pnlEncabezado = New Panel()
        Me.lblTituloPOS = New Label()
        Me.lblCajeroInfo = New Label()

        ' ── Zona izquierda: Búsqueda + Carrito ──
        Me.pnlIzquierda = New Panel()
        Me.lblBuscarProducto = New Label()
        Me.txtBuscarProducto = New TextBox()
        Me.btnBuscarProducto = New Button()
        Me.dgvBusqueda = New DataGridView()
        Me.lblCantidad = New Label()
        Me.nudCantidad = New NumericUpDown()
        Me.btnAgregarCarrito = New Button()
        Me.lblTituloCarrito = New Label()
        Me.dgvCarrito = New DataGridView()
        Me.btnQuitarProducto = New Button()
        Me.btnVaciarCarrito = New Button()

        ' ── Zona derecha: Cliente + Totales + Pago ──
        Me.pnlDerecha = New Panel()
        Me.pnlCliente = New Panel()
        Me.lblSelCliente = New Label()
        Me.cboCliente = New ComboBox()

        Me.pnlTotales = New Panel()
        Me.lblEtiqSubtotal = New Label()
        Me.lblSubtotal = New Label()
        Me.lblEtiqImpuesto = New Label()
        Me.lblImpuesto = New Label()
        Me.lblSepTotal = New Label()
        Me.lblEtiqTotal = New Label()
        Me.lblTotal = New Label()

        Me.pnlPago = New Panel()
        Me.lblEtiqMetodo = New Label()
        Me.cboMetodoPago = New ComboBox()
        Me.lblEtiqMontoPagado = New Label()
        Me.txtMontoPagado = New TextBox()
        Me.lblEtiqCambio = New Label()
        Me.lblCambio = New Label()

        Me.btnProcesarVenta = New Button()
        Me.btnCancelar = New Button()

        Me.SuspendLayout()

        ' ══════════════════════════════════════════════════════════════════════
        ' ENCABEZADO
        ' ══════════════════════════════════════════════════════════════════════
        Me.pnlEncabezado.BackColor = Color.FromArgb(76, 175, 80)
        Me.pnlEncabezado.Dock = DockStyle.Top
        Me.pnlEncabezado.Name = "pnlEncabezado"
        Me.pnlEncabezado.Size = New Size(1150, 50)

        Me.lblTituloPOS.AutoSize = True
        Me.lblTituloPOS.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        Me.lblTituloPOS.ForeColor = Color.White
        Me.lblTituloPOS.Location = New Point(15, 10)
        Me.lblTituloPOS.Name = "lblTituloPOS"
        Me.lblTituloPOS.Text = "Punto de Venta"

        Me.lblCajeroInfo.AutoSize = True
        Me.lblCajeroInfo.Font = New Font("Segoe UI", 9.5F)
        Me.lblCajeroInfo.ForeColor = Color.White
        Me.lblCajeroInfo.Location = New Point(900, 15)
        Me.lblCajeroInfo.Name = "lblCajeroInfo"
        Me.lblCajeroInfo.Text = "Cajero: ---"

        Me.pnlEncabezado.Controls.Add(Me.lblTituloPOS)
        Me.pnlEncabezado.Controls.Add(Me.lblCajeroInfo)

        ' ══════════════════════════════════════════════════════════════════════
        ' PANEL IZQUIERDO (Búsqueda de productos y carrito)
        ' ══════════════════════════════════════════════════════════════════════
        Dim fuenteStd As New Font("Segoe UI", 10.0F)
        Dim fuenteLabel As New Font("Segoe UI", 9.5F)
        Dim colorLabel As Color = Color.FromArgb(64, 64, 64)

        Me.pnlIzquierda.Location = New Point(10, 58)
        Me.pnlIzquierda.Name = "pnlIzquierda"
        Me.pnlIzquierda.Size = New Size(750, 600)

        ' ── Búsqueda de productos ──
        Me.lblBuscarProducto.AutoSize = True
        Me.lblBuscarProducto.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Me.lblBuscarProducto.ForeColor = colorLabel
        Me.lblBuscarProducto.Location = New Point(5, 5)
        Me.lblBuscarProducto.Name = "lblBuscarProducto"
        Me.lblBuscarProducto.Text = "Buscar Producto:"

        Me.txtBuscarProducto.Font = fuenteStd
        Me.txtBuscarProducto.Location = New Point(5, 30)
        Me.txtBuscarProducto.Name = "txtBuscarProducto"
        Me.txtBuscarProducto.Size = New Size(310, 27)
        Me.txtBuscarProducto.PlaceholderText = "Código o nombre del producto..."

        Me.btnBuscarProducto.BackColor = Color.FromArgb(33, 150, 243)
        Me.btnBuscarProducto.FlatAppearance.BorderSize = 0
        Me.btnBuscarProducto.FlatStyle = FlatStyle.Flat
        Me.btnBuscarProducto.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Me.btnBuscarProducto.ForeColor = Color.White
        Me.btnBuscarProducto.Location = New Point(325, 29)
        Me.btnBuscarProducto.Name = "btnBuscarProducto"
        Me.btnBuscarProducto.Size = New Size(90, 29)
        Me.btnBuscarProducto.Text = "Buscar"
        Me.btnBuscarProducto.Cursor = Cursors.Hand

        ' ── Grid de resultados de búsqueda ──
        Me.dgvBusqueda.Location = New Point(5, 65)
        Me.dgvBusqueda.Name = "dgvBusqueda"
        Me.dgvBusqueda.Size = New Size(740, 130)
        Me.dgvBusqueda.AllowUserToAddRows = False
        Me.dgvBusqueda.AllowUserToDeleteRows = False
        Me.dgvBusqueda.ReadOnly = True
        Me.dgvBusqueda.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvBusqueda.MultiSelect = False
        Me.dgvBusqueda.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvBusqueda.BackgroundColor = Color.White
        Me.dgvBusqueda.BorderStyle = BorderStyle.FixedSingle
        Me.dgvBusqueda.RowHeadersVisible = False
        Me.dgvBusqueda.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F)
        Me.dgvBusqueda.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Me.dgvBusqueda.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243)
        Me.dgvBusqueda.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Me.dgvBusqueda.EnableHeadersVisualStyles = False

        ' ── Cantidad y botón agregar ──
        Me.lblCantidad.AutoSize = True
        Me.lblCantidad.Font = fuenteLabel
        Me.lblCantidad.ForeColor = colorLabel
        Me.lblCantidad.Location = New Point(5, 205)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Text = "Cantidad:"

        Me.nudCantidad.Font = fuenteStd
        Me.nudCantidad.Location = New Point(80, 202)
        Me.nudCantidad.Name = "nudCantidad"
        Me.nudCantidad.Size = New Size(80, 27)
        Me.nudCantidad.Minimum = 1
        Me.nudCantidad.Maximum = 9999
        Me.nudCantidad.Value = 1

        Me.btnAgregarCarrito.BackColor = Color.FromArgb(76, 175, 80)
        Me.btnAgregarCarrito.FlatAppearance.BorderSize = 0
        Me.btnAgregarCarrito.FlatStyle = FlatStyle.Flat
        Me.btnAgregarCarrito.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        Me.btnAgregarCarrito.ForeColor = Color.White
        Me.btnAgregarCarrito.Location = New Point(175, 200)
        Me.btnAgregarCarrito.Name = "btnAgregarCarrito"
        Me.btnAgregarCarrito.Size = New Size(170, 32)
        Me.btnAgregarCarrito.Text = "Agregar al Carrito"
        Me.btnAgregarCarrito.Cursor = Cursors.Hand

        ' ── Título del carrito ──
        Me.lblTituloCarrito.AutoSize = True
        Me.lblTituloCarrito.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Me.lblTituloCarrito.ForeColor = colorLabel
        Me.lblTituloCarrito.Location = New Point(5, 245)
        Me.lblTituloCarrito.Name = "lblTituloCarrito"
        Me.lblTituloCarrito.Text = "Carrito de Compras:"

        ' ── Grid del carrito ──
        Me.dgvCarrito.Location = New Point(5, 270)
        Me.dgvCarrito.Name = "dgvCarrito"
        Me.dgvCarrito.Size = New Size(740, 275)
        Me.dgvCarrito.AllowUserToAddRows = False
        Me.dgvCarrito.AllowUserToDeleteRows = False
        Me.dgvCarrito.ReadOnly = True
        Me.dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvCarrito.MultiSelect = False
        Me.dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvCarrito.BackgroundColor = Color.White
        Me.dgvCarrito.BorderStyle = BorderStyle.FixedSingle
        Me.dgvCarrito.RowHeadersVisible = False
        Me.dgvCarrito.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F)
        Me.dgvCarrito.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Me.dgvCarrito.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(76, 175, 80)
        Me.dgvCarrito.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Me.dgvCarrito.EnableHeadersVisualStyles = False
        Me.dgvCarrito.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 245)

        ' ── Botones de carrito ──
        Me.btnQuitarProducto.BackColor = Color.FromArgb(255, 152, 0)
        Me.btnQuitarProducto.FlatAppearance.BorderSize = 0
        Me.btnQuitarProducto.FlatStyle = FlatStyle.Flat
        Me.btnQuitarProducto.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Me.btnQuitarProducto.ForeColor = Color.White
        Me.btnQuitarProducto.Location = New Point(5, 553)
        Me.btnQuitarProducto.Name = "btnQuitarProducto"
        Me.btnQuitarProducto.Size = New Size(160, 32)
        Me.btnQuitarProducto.Text = "Quitar Seleccionado"
        Me.btnQuitarProducto.Cursor = Cursors.Hand

        Me.btnVaciarCarrito.BackColor = Color.FromArgb(211, 47, 47)
        Me.btnVaciarCarrito.FlatAppearance.BorderSize = 0
        Me.btnVaciarCarrito.FlatStyle = FlatStyle.Flat
        Me.btnVaciarCarrito.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Me.btnVaciarCarrito.ForeColor = Color.White
        Me.btnVaciarCarrito.Location = New Point(175, 553)
        Me.btnVaciarCarrito.Name = "btnVaciarCarrito"
        Me.btnVaciarCarrito.Size = New Size(140, 32)
        Me.btnVaciarCarrito.Text = "Vaciar Carrito"
        Me.btnVaciarCarrito.Cursor = Cursors.Hand

        Me.pnlIzquierda.Controls.AddRange(New Control() {
            lblBuscarProducto, txtBuscarProducto, btnBuscarProducto,
            dgvBusqueda, lblCantidad, nudCantidad, btnAgregarCarrito,
            lblTituloCarrito, dgvCarrito, btnQuitarProducto, btnVaciarCarrito})

        ' ══════════════════════════════════════════════════════════════════════
        ' PANEL DERECHO (Cliente, Totales, Pago)
        ' ══════════════════════════════════════════════════════════════════════
        Me.pnlDerecha.Location = New Point(770, 58)
        Me.pnlDerecha.Name = "pnlDerecha"
        Me.pnlDerecha.Size = New Size(370, 600)

        ' ── Panel de selección de cliente ──
        Me.pnlCliente.BackColor = Color.White
        Me.pnlCliente.BorderStyle = BorderStyle.FixedSingle
        Me.pnlCliente.Location = New Point(0, 5)
        Me.pnlCliente.Name = "pnlCliente"
        Me.pnlCliente.Size = New Size(365, 75)

        Me.lblSelCliente.AutoSize = True
        Me.lblSelCliente.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Me.lblSelCliente.ForeColor = colorLabel
        Me.lblSelCliente.Location = New Point(10, 8)
        Me.lblSelCliente.Name = "lblSelCliente"
        Me.lblSelCliente.Text = "Cliente:"

        Me.cboCliente.Font = fuenteStd
        Me.cboCliente.Location = New Point(10, 35)
        Me.cboCliente.Name = "cboCliente"
        Me.cboCliente.Size = New Size(340, 27)
        Me.cboCliente.DropDownStyle = ComboBoxStyle.DropDownList

        Me.pnlCliente.Controls.Add(Me.lblSelCliente)
        Me.pnlCliente.Controls.Add(Me.cboCliente)

        ' ── Panel de totales ──
        Me.pnlTotales.BackColor = Color.White
        Me.pnlTotales.BorderStyle = BorderStyle.FixedSingle
        Me.pnlTotales.Location = New Point(0, 90)
        Me.pnlTotales.Name = "pnlTotales"
        Me.pnlTotales.Size = New Size(365, 170)

        Dim fuenteTotales As New Font("Segoe UI", 12.0F)
        Dim fuenteTotalGrande As New Font("Segoe UI", 18.0F, FontStyle.Bold)

        Me.lblEtiqSubtotal.Font = fuenteTotales
        Me.lblEtiqSubtotal.ForeColor = colorLabel
        Me.lblEtiqSubtotal.Location = New Point(10, 15)
        Me.lblEtiqSubtotal.Name = "lblEtiqSubtotal"
        Me.lblEtiqSubtotal.Size = New Size(150, 25)
        Me.lblEtiqSubtotal.Text = "Subtotal:"

        Me.lblSubtotal.Font = fuenteTotales
        Me.lblSubtotal.ForeColor = colorLabel
        Me.lblSubtotal.Location = New Point(160, 15)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New Size(190, 25)
        Me.lblSubtotal.Text = "$0.00"
        Me.lblSubtotal.TextAlign = ContentAlignment.MiddleRight

        Me.lblEtiqImpuesto.Font = fuenteTotales
        Me.lblEtiqImpuesto.ForeColor = colorLabel
        Me.lblEtiqImpuesto.Location = New Point(10, 50)
        Me.lblEtiqImpuesto.Name = "lblEtiqImpuesto"
        Me.lblEtiqImpuesto.Size = New Size(150, 25)
        Me.lblEtiqImpuesto.Text = "IVA (15%):"

        Me.lblImpuesto.Font = fuenteTotales
        Me.lblImpuesto.ForeColor = colorLabel
        Me.lblImpuesto.Location = New Point(160, 50)
        Me.lblImpuesto.Name = "lblImpuesto"
        Me.lblImpuesto.Size = New Size(190, 25)
        Me.lblImpuesto.Text = "$0.00"
        Me.lblImpuesto.TextAlign = ContentAlignment.MiddleRight

        Me.lblSepTotal.BackColor = Color.FromArgb(200, 200, 200)
        Me.lblSepTotal.Location = New Point(10, 85)
        Me.lblSepTotal.Name = "lblSepTotal"
        Me.lblSepTotal.Size = New Size(340, 2)

        Me.lblEtiqTotal.Font = fuenteTotalGrande
        Me.lblEtiqTotal.ForeColor = Color.FromArgb(38, 50, 56)
        Me.lblEtiqTotal.Location = New Point(10, 100)
        Me.lblEtiqTotal.Name = "lblEtiqTotal"
        Me.lblEtiqTotal.Size = New Size(120, 40)
        Me.lblEtiqTotal.Text = "TOTAL:"

        Me.lblTotal.Font = fuenteTotalGrande
        Me.lblTotal.ForeColor = Color.FromArgb(76, 175, 80)
        Me.lblTotal.Location = New Point(130, 100)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New Size(220, 40)
        Me.lblTotal.Text = "$0.00"
        Me.lblTotal.TextAlign = ContentAlignment.MiddleRight

        Me.pnlTotales.Controls.AddRange(New Control() {
            lblEtiqSubtotal, lblSubtotal, lblEtiqImpuesto, lblImpuesto,
            lblSepTotal, lblEtiqTotal, lblTotal})

        ' ── Panel de pago ──
        Me.pnlPago.BackColor = Color.White
        Me.pnlPago.BorderStyle = BorderStyle.FixedSingle
        Me.pnlPago.Location = New Point(0, 270)
        Me.pnlPago.Name = "pnlPago"
        Me.pnlPago.Size = New Size(365, 170)

        Me.lblEtiqMetodo.AutoSize = True
        Me.lblEtiqMetodo.Font = fuenteLabel
        Me.lblEtiqMetodo.ForeColor = colorLabel
        Me.lblEtiqMetodo.Location = New Point(10, 10)
        Me.lblEtiqMetodo.Name = "lblEtiqMetodo"
        Me.lblEtiqMetodo.Text = "Método de Pago:"

        Me.cboMetodoPago.Font = fuenteStd
        Me.cboMetodoPago.Location = New Point(10, 33)
        Me.cboMetodoPago.Name = "cboMetodoPago"
        Me.cboMetodoPago.Size = New Size(340, 27)
        Me.cboMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboMetodoPago.Items.AddRange(New Object() {"Efectivo", "Tarjeta", "Transferencia"})
        Me.cboMetodoPago.SelectedIndex = 0

        Me.lblEtiqMontoPagado.AutoSize = True
        Me.lblEtiqMontoPagado.Font = fuenteLabel
        Me.lblEtiqMontoPagado.ForeColor = colorLabel
        Me.lblEtiqMontoPagado.Location = New Point(10, 70)
        Me.lblEtiqMontoPagado.Name = "lblEtiqMontoPagado"
        Me.lblEtiqMontoPagado.Text = "Monto Entregado:"

        Me.txtMontoPagado.Font = New Font("Segoe UI", 14.0F)
        Me.txtMontoPagado.Location = New Point(10, 95)
        Me.txtMontoPagado.Name = "txtMontoPagado"
        Me.txtMontoPagado.Size = New Size(200, 32)
        Me.txtMontoPagado.PlaceholderText = "0.00"
        Me.txtMontoPagado.MaxLength = 15

        Me.lblEtiqCambio.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        Me.lblEtiqCambio.ForeColor = colorLabel
        Me.lblEtiqCambio.Location = New Point(220, 80)
        Me.lblEtiqCambio.Name = "lblEtiqCambio"
        Me.lblEtiqCambio.Size = New Size(130, 20)
        Me.lblEtiqCambio.Text = "Cambio:"

        Me.lblCambio.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Me.lblCambio.ForeColor = Color.FromArgb(33, 150, 243)
        Me.lblCambio.Location = New Point(220, 102)
        Me.lblCambio.Name = "lblCambio"
        Me.lblCambio.Size = New Size(135, 35)
        Me.lblCambio.Text = "$0.00"
        Me.lblCambio.TextAlign = ContentAlignment.MiddleRight

        Me.pnlPago.Controls.AddRange(New Control() {
            lblEtiqMetodo, cboMetodoPago,
            lblEtiqMontoPagado, txtMontoPagado,
            lblEtiqCambio, lblCambio})

        ' ── Botones de acción ──
        Me.btnProcesarVenta.BackColor = Color.FromArgb(76, 175, 80)
        Me.btnProcesarVenta.FlatAppearance.BorderSize = 0
        Me.btnProcesarVenta.FlatStyle = FlatStyle.Flat
        Me.btnProcesarVenta.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        Me.btnProcesarVenta.ForeColor = Color.White
        Me.btnProcesarVenta.Location = New Point(0, 460)
        Me.btnProcesarVenta.Name = "btnProcesarVenta"
        Me.btnProcesarVenta.Size = New Size(365, 55)
        Me.btnProcesarVenta.Text = "PROCESAR VENTA"
        Me.btnProcesarVenta.Cursor = Cursors.Hand

        Me.btnCancelar.BackColor = Color.FromArgb(120, 120, 120)
        Me.btnCancelar.FlatAppearance.BorderSize = 0
        Me.btnCancelar.FlatStyle = FlatStyle.Flat
        Me.btnCancelar.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Me.btnCancelar.ForeColor = Color.White
        Me.btnCancelar.Location = New Point(0, 525)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New Size(365, 38)
        Me.btnCancelar.Text = "Cancelar y Cerrar"
        Me.btnCancelar.Cursor = Cursors.Hand

        Me.pnlDerecha.Controls.AddRange(New Control() {
            pnlCliente, pnlTotales, pnlPago, btnProcesarVenta, btnCancelar})

        ' ══════════════════════════════════════════════════════════════════════
        ' Configuración del Formulario frmVentas
        ' ══════════════════════════════════════════════════════════════════════
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.BackColor = Color.FromArgb(245, 245, 245)
        Me.ClientSize = New Size(1150, 670)
        Me.Controls.Add(Me.pnlIzquierda)
        Me.Controls.Add(Me.pnlDerecha)
        Me.Controls.Add(Me.pnlEncabezado)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmVentas"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Ferree-Lara - Punto de Venta"

        Me.ResumeLayout(False)

    End Sub

    ' ── Declaración de controles ──
    Friend WithEvents pnlEncabezado As Panel
    Friend WithEvents lblTituloPOS As Label
    Friend WithEvents lblCajeroInfo As Label

    Friend WithEvents pnlIzquierda As Panel
    Friend WithEvents lblBuscarProducto As Label
    Friend WithEvents txtBuscarProducto As TextBox
    Friend WithEvents btnBuscarProducto As Button
    Friend WithEvents dgvBusqueda As DataGridView
    Friend WithEvents lblCantidad As Label
    Friend WithEvents nudCantidad As NumericUpDown
    Friend WithEvents btnAgregarCarrito As Button
    Friend WithEvents lblTituloCarrito As Label
    Friend WithEvents dgvCarrito As DataGridView
    Friend WithEvents btnQuitarProducto As Button
    Friend WithEvents btnVaciarCarrito As Button

    Friend WithEvents pnlDerecha As Panel
    Friend WithEvents pnlCliente As Panel
    Friend WithEvents lblSelCliente As Label
    Friend WithEvents cboCliente As ComboBox

    Friend WithEvents pnlTotales As Panel
    Friend WithEvents lblEtiqSubtotal As Label
    Friend WithEvents lblSubtotal As Label
    Friend WithEvents lblEtiqImpuesto As Label
    Friend WithEvents lblImpuesto As Label
    Friend WithEvents lblSepTotal As Label
    Friend WithEvents lblEtiqTotal As Label
    Friend WithEvents lblTotal As Label

    Friend WithEvents pnlPago As Panel
    Friend WithEvents lblEtiqMetodo As Label
    Friend WithEvents cboMetodoPago As ComboBox
    Friend WithEvents lblEtiqMontoPagado As Label
    Friend WithEvents txtMontoPagado As TextBox
    Friend WithEvents lblEtiqCambio As Label
    Friend WithEvents lblCambio As Label

    Friend WithEvents btnProcesarVenta As Button
    Friend WithEvents btnCancelar As Button

End Class
