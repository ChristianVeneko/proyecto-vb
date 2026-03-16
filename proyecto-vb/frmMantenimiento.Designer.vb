' ═══════════════════════════════════════════════════════════════════════════════
' Formulario: frmMantenimiento (Archivo Designer)
' Descripción: Define la interfaz gráfica del módulo de mantenimiento con
'              un TabControl de dos pestañas: Productos y Clientes.
'              Cada pestaña contiene campos de entrada, botones CRUD y un
'              DataGridView para visualizar los registros.
' ═══════════════════════════════════════════════════════════════════════════════

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMantenimiento
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
        Me.lblTituloModulo = New Label()
        Me.tabControl = New TabControl()
        Me.tabProductos = New TabPage()
        Me.tabClientes = New TabPage()

        ' ════════════════════════════════════════════════════════════════
        ' CONTROLES DE LA PESTAÑA PRODUCTOS
        ' ════════════════════════════════════════════════════════════════
        Me.pnlFormProducto = New Panel()
        Me.lblProdCodigo = New Label()
        Me.txtProdCodigo = New TextBox()
        Me.lblProdNombre = New Label()
        Me.txtProdNombre = New TextBox()
        Me.lblProdDescripcion = New Label()
        Me.txtProdDescripcion = New TextBox()
        Me.lblProdCategoria = New Label()
        Me.cboProdCategoria = New ComboBox()
        Me.lblProdPrecioVenta = New Label()
        Me.txtProdPrecioVenta = New TextBox()
        Me.lblProdPrecioCosto = New Label()
        Me.txtProdPrecioCosto = New TextBox()
        Me.lblProdStock = New Label()
        Me.txtProdStock = New TextBox()
        Me.lblProdStockMin = New Label()
        Me.txtProdStockMin = New TextBox()

        Me.pnlBotonesProducto = New Panel()
        Me.btnProdNuevo = New Button()
        Me.btnProdGuardar = New Button()
        Me.btnProdEditar = New Button()
        Me.btnProdEliminar = New Button()
        Me.btnProdLimpiar = New Button()

        Me.pnlBusquedaProducto = New Panel()
        Me.lblProdBuscar = New Label()
        Me.txtProdBuscar = New TextBox()
        Me.btnProdBuscar = New Button()

        Me.dgvProductos = New DataGridView()

        ' ════════════════════════════════════════════════════════════════
        ' CONTROLES DE LA PESTAÑA CLIENTES
        ' ════════════════════════════════════════════════════════════════
        Me.pnlFormCliente = New Panel()
        Me.lblCliNombre = New Label()
        Me.txtCliNombre = New TextBox()
        Me.lblCliApellido = New Label()
        Me.txtCliApellido = New TextBox()
        Me.lblCliCedula = New Label()
        Me.txtCliCedula = New TextBox()
        Me.lblCliTelefono = New Label()
        Me.txtCliTelefono = New TextBox()
        Me.lblCliCorreo = New Label()
        Me.txtCliCorreo = New TextBox()
        Me.lblCliDireccion = New Label()
        Me.txtCliDireccion = New TextBox()

        Me.pnlBotonesCliente = New Panel()
        Me.btnCliNuevo = New Button()
        Me.btnCliGuardar = New Button()
        Me.btnCliEditar = New Button()
        Me.btnCliEliminar = New Button()
        Me.btnCliLimpiar = New Button()

        Me.pnlBusquedaCliente = New Panel()
        Me.lblCliBuscar = New Label()
        Me.txtCliBuscar = New TextBox()
        Me.btnCliBuscar = New Button()

        Me.dgvClientes = New DataGridView()

        Me.SuspendLayout()
        Me.tabControl.SuspendLayout()
        Me.tabProductos.SuspendLayout()
        Me.tabClientes.SuspendLayout()

        ' ══════════════════════════════════════════════════════════════════════
        ' ENCABEZADO
        ' ══════════════════════════════════════════════════════════════════════
        Me.pnlEncabezado.BackColor = Color.FromArgb(33, 150, 243)
        Me.pnlEncabezado.Dock = DockStyle.Top
        Me.pnlEncabezado.Name = "pnlEncabezado"
        Me.pnlEncabezado.Size = New Size(1050, 50)

        Me.lblTituloModulo.AutoSize = True
        Me.lblTituloModulo.Font = New Font("Segoe UI", 15.0F, FontStyle.Bold)
        Me.lblTituloModulo.ForeColor = Color.White
        Me.lblTituloModulo.Location = New Point(15, 10)
        Me.lblTituloModulo.Name = "lblTituloModulo"
        Me.lblTituloModulo.Text = "Mantenimiento - Catálogo e Inventario"

        Me.pnlEncabezado.Controls.Add(Me.lblTituloModulo)

        ' ══════════════════════════════════════════════════════════════════════
        ' TABCONTROL (Contenedor de pestañas)
        ' ══════════════════════════════════════════════════════════════════════
        Me.tabControl.Dock = DockStyle.Fill
        Me.tabControl.Font = New Font("Segoe UI", 10.0F)
        Me.tabControl.Location = New Point(0, 50)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.Size = New Size(1050, 600)
        Me.tabControl.TabPages.Add(Me.tabProductos)
        Me.tabControl.TabPages.Add(Me.tabClientes)

        Me.tabProductos.Text = "  Productos  "
        Me.tabProductos.BackColor = Color.FromArgb(250, 250, 250)
        Me.tabProductos.Name = "tabProductos"
        Me.tabProductos.Padding = New Padding(10)

        Me.tabClientes.Text = "  Clientes  "
        Me.tabClientes.BackColor = Color.FromArgb(250, 250, 250)
        Me.tabClientes.Name = "tabClientes"
        Me.tabClientes.Padding = New Padding(10)

        ' ══════════════════════════════════════════════════════════════════════
        ' PESTAÑA PRODUCTOS — Campos de entrada
        ' ══════════════════════════════════════════════════════════════════════
        Dim fuenteLabel As New Font("Segoe UI", 9.5F)
        Dim fuenteInput As New Font("Segoe UI", 10.0F)
        Dim colorLabel As Color = Color.FromArgb(64, 64, 64)

        Me.pnlFormProducto.Location = New Point(15, 10)
        Me.pnlFormProducto.Name = "pnlFormProducto"
        Me.pnlFormProducto.Size = New Size(1010, 160)
        Me.pnlFormProducto.BackColor = Color.White
        Me.pnlFormProducto.BorderStyle = BorderStyle.FixedSingle

        ' ── Fila 1 ──
        ConfigurarLabel(lblProdCodigo, "Código:", 15, 12, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtProdCodigo, 15, 35, 150, fuenteInput, 20)

        ConfigurarLabel(lblProdNombre, "Nombre:", 180, 12, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtProdNombre, 180, 35, 250, fuenteInput, 100)

        ConfigurarLabel(lblProdDescripcion, "Descripción:", 445, 12, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtProdDescripcion, 445, 35, 300, fuenteInput, 200)

        ConfigurarLabel(lblProdCategoria, "Categoría:", 760, 12, fuenteLabel, colorLabel)
        Me.cboProdCategoria.Font = fuenteInput
        Me.cboProdCategoria.Location = New Point(760, 35)
        Me.cboProdCategoria.Name = "cboProdCategoria"
        Me.cboProdCategoria.Size = New Size(220, 27)
        Me.cboProdCategoria.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboProdCategoria.Items.AddRange(New Object() {
            "Herramientas Manuales",
            "Herramientas Eléctricas",
            "Materiales de Construcción",
            "Plomería",
            "Eléctricos",
            "Pinturas",
            "Cerrajería",
            "Medición",
            "Tornillería",
            "Otros"})

        ' ── Fila 2 ──
        ConfigurarLabel(lblProdPrecioVenta, "Precio Venta:", 15, 75, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtProdPrecioVenta, 15, 98, 150, fuenteInput, 12)

        ConfigurarLabel(lblProdPrecioCosto, "Precio Costo:", 180, 75, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtProdPrecioCosto, 180, 98, 150, fuenteInput, 12)

        ConfigurarLabel(lblProdStock, "Stock:", 345, 75, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtProdStock, 345, 98, 120, fuenteInput, 8)

        ConfigurarLabel(lblProdStockMin, "Stock Mínimo:", 480, 75, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtProdStockMin, 480, 98, 120, fuenteInput, 8)

        Me.pnlFormProducto.Controls.AddRange(New Control() {
            lblProdCodigo, txtProdCodigo, lblProdNombre, txtProdNombre,
            lblProdDescripcion, txtProdDescripcion, lblProdCategoria, cboProdCategoria,
            lblProdPrecioVenta, txtProdPrecioVenta, lblProdPrecioCosto, txtProdPrecioCosto,
            lblProdStock, txtProdStock, lblProdStockMin, txtProdStockMin})

        ' ── Botones CRUD de Productos ──
        Me.pnlBotonesProducto.Location = New Point(15, 178)
        Me.pnlBotonesProducto.Name = "pnlBotonesProducto"
        Me.pnlBotonesProducto.Size = New Size(1010, 42)

        ConfigurarBotonCRUD(btnProdNuevo, "Nuevo", 0, Color.FromArgb(33, 150, 243))
        ConfigurarBotonCRUD(btnProdGuardar, "Guardar", 130, Color.FromArgb(76, 175, 80))
        ConfigurarBotonCRUD(btnProdEditar, "Editar", 260, Color.FromArgb(255, 152, 0))
        ConfigurarBotonCRUD(btnProdEliminar, "Desactivar", 390, Color.FromArgb(211, 47, 47))
        ConfigurarBotonCRUD(btnProdLimpiar, "Limpiar", 520, Color.FromArgb(120, 120, 120))

        Me.pnlBotonesProducto.Controls.AddRange(New Control() {
            btnProdNuevo, btnProdGuardar, btnProdEditar, btnProdEliminar, btnProdLimpiar})

        ' ── Barra de búsqueda de Productos ──
        Me.pnlBusquedaProducto.Location = New Point(15, 228)
        Me.pnlBusquedaProducto.Name = "pnlBusquedaProducto"
        Me.pnlBusquedaProducto.Size = New Size(1010, 38)

        Me.lblProdBuscar.AutoSize = True
        Me.lblProdBuscar.Font = fuenteLabel
        Me.lblProdBuscar.ForeColor = colorLabel
        Me.lblProdBuscar.Location = New Point(0, 8)
        Me.lblProdBuscar.Text = "Buscar:"

        Me.txtProdBuscar.Font = fuenteInput
        Me.txtProdBuscar.Location = New Point(60, 5)
        Me.txtProdBuscar.Size = New Size(300, 27)
        Me.txtProdBuscar.PlaceholderText = "Código, nombre o categoría..."

        ConfigurarBotonCRUD(btnProdBuscar, "Buscar", 375, Color.FromArgb(33, 150, 243))
        Me.btnProdBuscar.Size = New Size(100, 32)

        Me.pnlBusquedaProducto.Controls.AddRange(New Control() {
            lblProdBuscar, txtProdBuscar, btnProdBuscar})

        ' ── DataGridView de Productos ──
        Me.dgvProductos.Location = New Point(15, 275)
        Me.dgvProductos.Name = "dgvProductos"
        Me.dgvProductos.Size = New Size(1010, 270)
        Me.dgvProductos.AllowUserToAddRows = False
        Me.dgvProductos.AllowUserToDeleteRows = False
        Me.dgvProductos.ReadOnly = True
        Me.dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvProductos.MultiSelect = False
        Me.dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvProductos.BackgroundColor = Color.White
        Me.dgvProductos.BorderStyle = BorderStyle.FixedSingle
        Me.dgvProductos.RowHeadersVisible = False
        Me.dgvProductos.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F)
        Me.dgvProductos.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Me.dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243)
        Me.dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Me.dgvProductos.EnableHeadersVisualStyles = False
        Me.dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)

        ' Agregar controles a la pestaña Productos
        Me.tabProductos.Controls.AddRange(New Control() {
            pnlFormProducto, pnlBotonesProducto, pnlBusquedaProducto, dgvProductos})

        ' ══════════════════════════════════════════════════════════════════════
        ' PESTAÑA CLIENTES — Campos de entrada
        ' ══════════════════════════════════════════════════════════════════════
        Me.pnlFormCliente.Location = New Point(15, 10)
        Me.pnlFormCliente.Name = "pnlFormCliente"
        Me.pnlFormCliente.Size = New Size(1010, 110)
        Me.pnlFormCliente.BackColor = Color.White
        Me.pnlFormCliente.BorderStyle = BorderStyle.FixedSingle

        ' ── Fila 1 ──
        ConfigurarLabel(lblCliCedula, "Cédula/ID:", 15, 12, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtCliCedula, 15, 35, 160, fuenteInput, 20)

        ConfigurarLabel(lblCliNombre, "Nombre:", 190, 12, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtCliNombre, 190, 35, 200, fuenteInput, 50)

        ConfigurarLabel(lblCliApellido, "Apellido:", 405, 12, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtCliApellido, 405, 35, 200, fuenteInput, 50)

        ConfigurarLabel(lblCliTelefono, "Teléfono:", 620, 12, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtCliTelefono, 620, 35, 170, fuenteInput, 20)

        ' ── Fila 2 ──
        ConfigurarLabel(lblCliCorreo, "Correo:", 15, 62, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtCliCorreo, 15, 78, 280, fuenteInput, 100)

        ConfigurarLabel(lblCliDireccion, "Dirección:", 310, 62, fuenteLabel, colorLabel)
        ConfigurarTextBox(txtCliDireccion, 310, 78, 480, fuenteInput, 200)

        Me.pnlFormCliente.Controls.AddRange(New Control() {
            lblCliCedula, txtCliCedula, lblCliNombre, txtCliNombre,
            lblCliApellido, txtCliApellido, lblCliTelefono, txtCliTelefono,
            lblCliCorreo, txtCliCorreo, lblCliDireccion, txtCliDireccion})

        ' ── Botones CRUD de Clientes ──
        Me.pnlBotonesCliente.Location = New Point(15, 128)
        Me.pnlBotonesCliente.Name = "pnlBotonesCliente"
        Me.pnlBotonesCliente.Size = New Size(1010, 42)

        ConfigurarBotonCRUD(btnCliNuevo, "Nuevo", 0, Color.FromArgb(33, 150, 243))
        ConfigurarBotonCRUD(btnCliGuardar, "Guardar", 130, Color.FromArgb(76, 175, 80))
        ConfigurarBotonCRUD(btnCliEditar, "Editar", 260, Color.FromArgb(255, 152, 0))
        ConfigurarBotonCRUD(btnCliEliminar, "Eliminar", 390, Color.FromArgb(211, 47, 47))
        ConfigurarBotonCRUD(btnCliLimpiar, "Limpiar", 520, Color.FromArgb(120, 120, 120))

        Me.pnlBotonesCliente.Controls.AddRange(New Control() {
            btnCliNuevo, btnCliGuardar, btnCliEditar, btnCliEliminar, btnCliLimpiar})

        ' ── Barra de búsqueda de Clientes ──
        Me.pnlBusquedaCliente.Location = New Point(15, 178)
        Me.pnlBusquedaCliente.Name = "pnlBusquedaCliente"
        Me.pnlBusquedaCliente.Size = New Size(1010, 38)

        Me.lblCliBuscar.AutoSize = True
        Me.lblCliBuscar.Font = fuenteLabel
        Me.lblCliBuscar.ForeColor = colorLabel
        Me.lblCliBuscar.Location = New Point(0, 8)
        Me.lblCliBuscar.Text = "Buscar:"

        Me.txtCliBuscar.Font = fuenteInput
        Me.txtCliBuscar.Location = New Point(60, 5)
        Me.txtCliBuscar.Size = New Size(300, 27)
        Me.txtCliBuscar.PlaceholderText = "Cédula, nombre o apellido..."

        ConfigurarBotonCRUD(btnCliBuscar, "Buscar", 375, Color.FromArgb(33, 150, 243))
        Me.btnCliBuscar.Size = New Size(100, 32)

        Me.pnlBusquedaCliente.Controls.AddRange(New Control() {
            lblCliBuscar, txtCliBuscar, btnCliBuscar})

        ' ── DataGridView de Clientes ──
        Me.dgvClientes.Location = New Point(15, 225)
        Me.dgvClientes.Name = "dgvClientes"
        Me.dgvClientes.Size = New Size(1010, 320)
        Me.dgvClientes.AllowUserToAddRows = False
        Me.dgvClientes.AllowUserToDeleteRows = False
        Me.dgvClientes.ReadOnly = True
        Me.dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvClientes.MultiSelect = False
        Me.dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvClientes.BackgroundColor = Color.White
        Me.dgvClientes.BorderStyle = BorderStyle.FixedSingle
        Me.dgvClientes.RowHeadersVisible = False
        Me.dgvClientes.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F)
        Me.dgvClientes.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Me.dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243)
        Me.dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Me.dgvClientes.EnableHeadersVisualStyles = False
        Me.dgvClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)

        ' Agregar controles a la pestaña Clientes
        Me.tabClientes.Controls.AddRange(New Control() {
            pnlFormCliente, pnlBotonesCliente, pnlBusquedaCliente, dgvClientes})

        ' ══════════════════════════════════════════════════════════════════════
        ' Configuración del Formulario frmMantenimiento
        ' ══════════════════════════════════════════════════════════════════════
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(1050, 650)
        Me.Controls.Add(Me.tabControl)
        Me.Controls.Add(Me.pnlEncabezado)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMantenimiento"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Ferree-Lara - Mantenimiento"

        Me.tabClientes.ResumeLayout(False)
        Me.tabProductos.ResumeLayout(False)
        Me.tabControl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    ' ══════════════════════════════════════════════════════════════════════════
    ' Métodos auxiliares para configurar controles de forma repetitiva
    ' ══════════════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Configura las propiedades comunes de un Label.
    ''' </summary>
    Private Sub ConfigurarLabel(lbl As Label, texto As String, x As Integer, y As Integer,
                                 fuente As Font, color As Color)
        lbl.AutoSize = True
        lbl.Font = fuente
        lbl.ForeColor = color
        lbl.Location = New Point(x, y)
        lbl.Text = texto
    End Sub

    ''' <summary>
    ''' Configura las propiedades comunes de un TextBox.
    ''' </summary>
    Private Sub ConfigurarTextBox(txt As TextBox, x As Integer, y As Integer,
                                   ancho As Integer, fuente As Font, maxLen As Integer)
        txt.Font = fuente
        txt.Location = New Point(x, y)
        txt.Size = New Size(ancho, 27)
        txt.MaxLength = maxLen
    End Sub

    ''' <summary>
    ''' Configura las propiedades comunes de un botón CRUD.
    ''' </summary>
    Private Sub ConfigurarBotonCRUD(btn As Button, texto As String,
                                     x As Integer, color As Color)
        btn.BackColor = color
        btn.FlatAppearance.BorderSize = 0
        btn.FlatStyle = FlatStyle.Flat
        btn.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btn.ForeColor = Color.White
        btn.Location = New Point(x, 2)
        btn.Size = New Size(120, 35)
        btn.Text = texto
        btn.Cursor = Cursors.Hand
    End Sub

    ' ══════════════════════════════════════════════════════════════════════════
    ' Declaración de controles
    ' ══════════════════════════════════════════════════════════════════════════

    ' ── Encabezado ──
    Friend WithEvents pnlEncabezado As Panel
    Friend WithEvents lblTituloModulo As Label

    ' ── TabControl ──
    Friend WithEvents tabControl As TabControl
    Friend WithEvents tabProductos As TabPage
    Friend WithEvents tabClientes As TabPage

    ' ── Productos: Formulario ──
    Friend WithEvents pnlFormProducto As Panel
    Friend WithEvents lblProdCodigo As Label
    Friend WithEvents txtProdCodigo As TextBox
    Friend WithEvents lblProdNombre As Label
    Friend WithEvents txtProdNombre As TextBox
    Friend WithEvents lblProdDescripcion As Label
    Friend WithEvents txtProdDescripcion As TextBox
    Friend WithEvents lblProdCategoria As Label
    Friend WithEvents cboProdCategoria As ComboBox
    Friend WithEvents lblProdPrecioVenta As Label
    Friend WithEvents txtProdPrecioVenta As TextBox
    Friend WithEvents lblProdPrecioCosto As Label
    Friend WithEvents txtProdPrecioCosto As TextBox
    Friend WithEvents lblProdStock As Label
    Friend WithEvents txtProdStock As TextBox
    Friend WithEvents lblProdStockMin As Label
    Friend WithEvents txtProdStockMin As TextBox

    ' ── Productos: Botones ──
    Friend WithEvents pnlBotonesProducto As Panel
    Friend WithEvents btnProdNuevo As Button
    Friend WithEvents btnProdGuardar As Button
    Friend WithEvents btnProdEditar As Button
    Friend WithEvents btnProdEliminar As Button
    Friend WithEvents btnProdLimpiar As Button

    ' ── Productos: Búsqueda ──
    Friend WithEvents pnlBusquedaProducto As Panel
    Friend WithEvents lblProdBuscar As Label
    Friend WithEvents txtProdBuscar As TextBox
    Friend WithEvents btnProdBuscar As Button

    ' ── Productos: Grid ──
    Friend WithEvents dgvProductos As DataGridView

    ' ── Clientes: Formulario ──
    Friend WithEvents pnlFormCliente As Panel
    Friend WithEvents lblCliNombre As Label
    Friend WithEvents txtCliNombre As TextBox
    Friend WithEvents lblCliApellido As Label
    Friend WithEvents txtCliApellido As TextBox
    Friend WithEvents lblCliCedula As Label
    Friend WithEvents txtCliCedula As TextBox
    Friend WithEvents lblCliTelefono As Label
    Friend WithEvents txtCliTelefono As TextBox
    Friend WithEvents lblCliCorreo As Label
    Friend WithEvents txtCliCorreo As TextBox
    Friend WithEvents lblCliDireccion As Label
    Friend WithEvents txtCliDireccion As TextBox

    ' ── Clientes: Botones ──
    Friend WithEvents pnlBotonesCliente As Panel
    Friend WithEvents btnCliNuevo As Button
    Friend WithEvents btnCliGuardar As Button
    Friend WithEvents btnCliEditar As Button
    Friend WithEvents btnCliEliminar As Button
    Friend WithEvents btnCliLimpiar As Button

    ' ── Clientes: Búsqueda ──
    Friend WithEvents pnlBusquedaCliente As Panel
    Friend WithEvents lblCliBuscar As Label
    Friend WithEvents txtCliBuscar As TextBox
    Friend WithEvents btnCliBuscar As Button

    ' ── Clientes: Grid ──
    Friend WithEvents dgvClientes As DataGridView

End Class
