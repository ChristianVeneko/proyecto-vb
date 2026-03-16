' ═══════════════════════════════════════════════════════════════════════════════
' Formulario: frmPrincipal (Archivo Designer)
' Descripción: Define la interfaz gráfica del menú principal de Ferree-Lara.
'              Incluye un panel lateral de navegación, una barra superior con
'              información del usuario, y un área central que muestra la
'              información corporativa (Misión, Visión) y botones de acceso
'              a los módulos del sistema.
' ═══════════════════════════════════════════════════════════════════════════════

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPrincipal
    Inherits System.Windows.Forms.Form

    ' ── Dispose: Libera los recursos del formulario ──
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

    ' ══════════════════════════════════════════════════════════════════════════
    ' Método: InitializeComponent
    ' Descripción: Inicializa y configura todos los controles del menú principal.
    ' ══════════════════════════════════════════════════════════════════════════
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()

        ' ── Declaración de controles ──
        Me.pnlBarraSuperior = New Panel()
        Me.lblBienvenida = New Label()
        Me.lblRolUsuario = New Label()
        Me.lblFechaHora = New Label()

        Me.pnlMenuLateral = New Panel()
        Me.lblLogoMenu = New Label()
        Me.lblSeparadorMenu = New Label()
        Me.btnMantenimiento = New Button()
        Me.btnVentas = New Button()
        Me.btnCerrarSesion = New Button()

        Me.pnlContenido = New Panel()
        Me.lblTituloEmpresa = New Label()
        Me.lblLineaDecorativa = New Label()

        Me.pnlMision = New Panel()
        Me.lblTituloMision = New Label()
        Me.lblTextoMision = New Label()

        Me.pnlVision = New Panel()
        Me.lblTituloVision = New Label()
        Me.lblTextoVision = New Label()

        Me.pnlAccesosRapidos = New Panel()
        Me.lblTituloAccesos = New Label()
        Me.btnAccesoMantenimiento = New Button()
        Me.btnAccesoVentas = New Button()

        Me.lblPieInfo = New Label()

        Me.tmrReloj = New Timer(components)

        Me.pnlBarraSuperior.SuspendLayout()
        Me.pnlMenuLateral.SuspendLayout()
        Me.pnlContenido.SuspendLayout()
        Me.pnlMision.SuspendLayout()
        Me.pnlVision.SuspendLayout()
        Me.pnlAccesosRapidos.SuspendLayout()
        Me.SuspendLayout()

        ' ══════════════════════════════════════════════════════════════════════
        ' BARRA SUPERIOR (Franja horizontal arriba)
        ' ══════════════════════════════════════════════════════════════════════
        Me.pnlBarraSuperior.BackColor = Color.FromArgb(33, 150, 243)
        Me.pnlBarraSuperior.Dock = DockStyle.Top
        Me.pnlBarraSuperior.Name = "pnlBarraSuperior"
        Me.pnlBarraSuperior.Size = New Size(1100, 55)

        ' ── Label: Bienvenida ──
        Me.lblBienvenida.AutoSize = True
        Me.lblBienvenida.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        Me.lblBienvenida.ForeColor = Color.White
        Me.lblBienvenida.Location = New Point(220, 5)
        Me.lblBienvenida.Name = "lblBienvenida"
        Me.lblBienvenida.Text = "Bienvenido/a"

        ' ── Label: Rol del usuario ──
        Me.lblRolUsuario.AutoSize = True
        Me.lblRolUsuario.Font = New Font("Segoe UI", 9.0F, FontStyle.Italic)
        Me.lblRolUsuario.ForeColor = Color.FromArgb(200, 230, 255)
        Me.lblRolUsuario.Location = New Point(220, 32)
        Me.lblRolUsuario.Name = "lblRolUsuario"
        Me.lblRolUsuario.Text = "Rol: -"

        ' ── Label: Fecha y hora ──
        Me.lblFechaHora.Font = New Font("Segoe UI", 10.0F)
        Me.lblFechaHora.ForeColor = Color.White
        Me.lblFechaHora.Location = New Point(870, 15)
        Me.lblFechaHora.Name = "lblFechaHora"
        Me.lblFechaHora.Size = New Size(210, 25)
        Me.lblFechaHora.TextAlign = ContentAlignment.MiddleRight
        Me.lblFechaHora.Text = ""

        Me.pnlBarraSuperior.Controls.Add(Me.lblBienvenida)
        Me.pnlBarraSuperior.Controls.Add(Me.lblRolUsuario)
        Me.pnlBarraSuperior.Controls.Add(Me.lblFechaHora)

        ' ══════════════════════════════════════════════════════════════════════
        ' MENÚ LATERAL (Panel izquierdo de navegación)
        ' ══════════════════════════════════════════════════════════════════════
        Me.pnlMenuLateral.BackColor = Color.FromArgb(38, 50, 56)
        Me.pnlMenuLateral.Dock = DockStyle.Left
        Me.pnlMenuLateral.Name = "pnlMenuLateral"
        Me.pnlMenuLateral.Size = New Size(200, 645)

        ' ── Logo / Nombre de la empresa en el menú ──
        Me.lblLogoMenu.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        Me.lblLogoMenu.ForeColor = Color.FromArgb(33, 150, 243)
        Me.lblLogoMenu.Location = New Point(10, 15)
        Me.lblLogoMenu.Name = "lblLogoMenu"
        Me.lblLogoMenu.Size = New Size(180, 35)
        Me.lblLogoMenu.Text = "FERREE-LARA"
        Me.lblLogoMenu.TextAlign = ContentAlignment.MiddleCenter

        ' ── Separador visual ──
        Me.lblSeparadorMenu.BackColor = Color.FromArgb(55, 71, 79)
        Me.lblSeparadorMenu.Location = New Point(15, 60)
        Me.lblSeparadorMenu.Name = "lblSeparadorMenu"
        Me.lblSeparadorMenu.Size = New Size(170, 1)

        ' ── Botón: Mantenimiento (Catálogo e Inventario) ──
        Me.btnMantenimiento.BackColor = Color.FromArgb(38, 50, 56)
        Me.btnMantenimiento.FlatAppearance.BorderSize = 0
        Me.btnMantenimiento.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 71, 79)
        Me.btnMantenimiento.FlatStyle = FlatStyle.Flat
        Me.btnMantenimiento.Font = New Font("Segoe UI", 10.0F)
        Me.btnMantenimiento.ForeColor = Color.White
        Me.btnMantenimiento.ImageAlign = ContentAlignment.MiddleLeft
        Me.btnMantenimiento.Location = New Point(0, 80)
        Me.btnMantenimiento.Name = "btnMantenimiento"
        Me.btnMantenimiento.Padding = New Padding(15, 0, 0, 0)
        Me.btnMantenimiento.Size = New Size(200, 45)
        Me.btnMantenimiento.Text = "  Mantenimiento"
        Me.btnMantenimiento.TextAlign = ContentAlignment.MiddleLeft
        Me.btnMantenimiento.Cursor = Cursors.Hand

        ' ── Botón: Punto de Venta ──
        Me.btnVentas.BackColor = Color.FromArgb(38, 50, 56)
        Me.btnVentas.FlatAppearance.BorderSize = 0
        Me.btnVentas.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 71, 79)
        Me.btnVentas.FlatStyle = FlatStyle.Flat
        Me.btnVentas.Font = New Font("Segoe UI", 10.0F)
        Me.btnVentas.ForeColor = Color.White
        Me.btnVentas.Location = New Point(0, 130)
        Me.btnVentas.Name = "btnVentas"
        Me.btnVentas.Padding = New Padding(15, 0, 0, 0)
        Me.btnVentas.Size = New Size(200, 45)
        Me.btnVentas.Text = "  Punto de Venta"
        Me.btnVentas.TextAlign = ContentAlignment.MiddleLeft
        Me.btnVentas.Cursor = Cursors.Hand

        ' ── Botón: Cerrar Sesión ──
        Me.btnCerrarSesion.BackColor = Color.FromArgb(211, 47, 47)
        Me.btnCerrarSesion.FlatAppearance.BorderSize = 0
        Me.btnCerrarSesion.FlatStyle = FlatStyle.Flat
        Me.btnCerrarSesion.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Me.btnCerrarSesion.ForeColor = Color.White
        Me.btnCerrarSesion.Location = New Point(10, 555)
        Me.btnCerrarSesion.Name = "btnCerrarSesion"
        Me.btnCerrarSesion.Size = New Size(180, 40)
        Me.btnCerrarSesion.Text = "Cerrar Sesión"
        Me.btnCerrarSesion.Cursor = Cursors.Hand

        Me.pnlMenuLateral.Controls.Add(Me.lblLogoMenu)
        Me.pnlMenuLateral.Controls.Add(Me.lblSeparadorMenu)
        Me.pnlMenuLateral.Controls.Add(Me.btnMantenimiento)
        Me.pnlMenuLateral.Controls.Add(Me.btnVentas)
        Me.pnlMenuLateral.Controls.Add(Me.btnCerrarSesion)

        ' ══════════════════════════════════════════════════════════════════════
        ' PANEL DE CONTENIDO CENTRAL
        ' ══════════════════════════════════════════════════════════════════════
        Me.pnlContenido.AutoScroll = True
        Me.pnlContenido.BackColor = Color.FromArgb(245, 245, 245)
        Me.pnlContenido.Dock = DockStyle.Fill
        Me.pnlContenido.Name = "pnlContenido"
        Me.pnlContenido.Padding = New Padding(30, 20, 30, 20)

        ' ── Título de la empresa ──
        Me.lblTituloEmpresa.AutoSize = True
        Me.lblTituloEmpresa.Font = New Font("Segoe UI", 22.0F, FontStyle.Bold)
        Me.lblTituloEmpresa.ForeColor = Color.FromArgb(38, 50, 56)
        Me.lblTituloEmpresa.Location = New Point(30, 20)
        Me.lblTituloEmpresa.Name = "lblTituloEmpresa"
        Me.lblTituloEmpresa.Text = "FERREE-LARA"

        ' ── Línea decorativa bajo el título ──
        Me.lblLineaDecorativa.BackColor = Color.FromArgb(33, 150, 243)
        Me.lblLineaDecorativa.Location = New Point(30, 65)
        Me.lblLineaDecorativa.Name = "lblLineaDecorativa"
        Me.lblLineaDecorativa.Size = New Size(120, 4)

        ' ──────────────────────────────────────────────────────────────────────
        ' PANEL: MISIÓN
        ' ──────────────────────────────────────────────────────────────────────
        Me.pnlMision.BackColor = Color.White
        Me.pnlMision.Location = New Point(30, 85)
        Me.pnlMision.Name = "pnlMision"
        Me.pnlMision.Size = New Size(400, 170)
        Me.pnlMision.BorderStyle = BorderStyle.None

        Me.lblTituloMision.AutoSize = True
        Me.lblTituloMision.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        Me.lblTituloMision.ForeColor = Color.FromArgb(33, 150, 243)
        Me.lblTituloMision.Location = New Point(15, 12)
        Me.lblTituloMision.Name = "lblTituloMision"
        Me.lblTituloMision.Text = "Nuestra Misión"

        Me.lblTextoMision.Font = New Font("Segoe UI", 9.5F)
        Me.lblTextoMision.ForeColor = Color.FromArgb(80, 80, 80)
        Me.lblTextoMision.Location = New Point(15, 45)
        Me.lblTextoMision.Name = "lblTextoMision"
        Me.lblTextoMision.Size = New Size(370, 115)
        Me.lblTextoMision.Text = "Proveer a nuestros clientes materiales, herramientas y soluciones " &
            "técnicas de la más alta calidad, ofreciendo un servicio ágil, asesoría " &
            "especializada y precios competitivos que contribuyan al éxito de sus " &
            "proyectos de construcción, reparación y mejora del hogar."

        Me.pnlMision.Controls.Add(Me.lblTituloMision)
        Me.pnlMision.Controls.Add(Me.lblTextoMision)

        ' ──────────────────────────────────────────────────────────────────────
        ' PANEL: VISIÓN
        ' ──────────────────────────────────────────────────────────────────────
        Me.pnlVision.BackColor = Color.White
        Me.pnlVision.Location = New Point(450, 85)
        Me.pnlVision.Name = "pnlVision"
        Me.pnlVision.Size = New Size(400, 170)
        Me.pnlVision.BorderStyle = BorderStyle.None

        Me.lblTituloVision.AutoSize = True
        Me.lblTituloVision.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        Me.lblTituloVision.ForeColor = Color.FromArgb(33, 150, 243)
        Me.lblTituloVision.Location = New Point(15, 12)
        Me.lblTituloVision.Name = "lblTituloVision"
        Me.lblTituloVision.Text = "Nuestra Visión"

        Me.lblTextoVision.Font = New Font("Segoe UI", 9.5F)
        Me.lblTextoVision.ForeColor = Color.FromArgb(80, 80, 80)
        Me.lblTextoVision.Location = New Point(15, 45)
        Me.lblTextoVision.Name = "lblTextoVision"
        Me.lblTextoVision.Size = New Size(370, 115)
        Me.lblTextoVision.Text = "Consolidarnos como la ferretería de referencia en la región, " &
            "reconocida por la amplitud de nuestro catálogo, la excelencia en el " &
            "servicio al cliente y la adopción de tecnología que optimice nuestros " &
            "procesos internos, garantizando una experiencia de compra confiable y moderna."

        Me.pnlVision.Controls.Add(Me.lblTituloVision)
        Me.pnlVision.Controls.Add(Me.lblTextoVision)

        ' ──────────────────────────────────────────────────────────────────────
        ' PANEL: ACCESOS RÁPIDOS (Tarjetas de acceso a módulos)
        ' ──────────────────────────────────────────────────────────────────────
        Me.pnlAccesosRapidos.BackColor = Color.FromArgb(245, 245, 245)
        Me.pnlAccesosRapidos.Location = New Point(30, 275)
        Me.pnlAccesosRapidos.Name = "pnlAccesosRapidos"
        Me.pnlAccesosRapidos.Size = New Size(820, 220)

        Me.lblTituloAccesos.AutoSize = True
        Me.lblTituloAccesos.Font = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        Me.lblTituloAccesos.ForeColor = Color.FromArgb(38, 50, 56)
        Me.lblTituloAccesos.Location = New Point(0, 5)
        Me.lblTituloAccesos.Name = "lblTituloAccesos"
        Me.lblTituloAccesos.Text = "Accesos Rápidos"

        ' ── Tarjeta: Mantenimiento ──
        Me.btnAccesoMantenimiento.BackColor = Color.White
        Me.btnAccesoMantenimiento.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220)
        Me.btnAccesoMantenimiento.FlatAppearance.BorderSize = 1
        Me.btnAccesoMantenimiento.FlatAppearance.MouseOverBackColor = Color.FromArgb(227, 242, 253)
        Me.btnAccesoMantenimiento.FlatStyle = FlatStyle.Flat
        Me.btnAccesoMantenimiento.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        Me.btnAccesoMantenimiento.ForeColor = Color.FromArgb(38, 50, 56)
        Me.btnAccesoMantenimiento.Location = New Point(0, 40)
        Me.btnAccesoMantenimiento.Name = "btnAccesoMantenimiento"
        Me.btnAccesoMantenimiento.Size = New Size(250, 160)
        Me.btnAccesoMantenimiento.Text = "MANTENIMIENTO" & vbCrLf & vbCrLf & "Gestión de Productos" & vbCrLf & "y Clientes"
        Me.btnAccesoMantenimiento.TextAlign = ContentAlignment.MiddleCenter
        Me.btnAccesoMantenimiento.Cursor = Cursors.Hand

        ' ── Tarjeta: Punto de Venta ──
        Me.btnAccesoVentas.BackColor = Color.White
        Me.btnAccesoVentas.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220)
        Me.btnAccesoVentas.FlatAppearance.BorderSize = 1
        Me.btnAccesoVentas.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 245, 233)
        Me.btnAccesoVentas.FlatStyle = FlatStyle.Flat
        Me.btnAccesoVentas.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        Me.btnAccesoVentas.ForeColor = Color.FromArgb(38, 50, 56)
        Me.btnAccesoVentas.Location = New Point(280, 40)
        Me.btnAccesoVentas.Name = "btnAccesoVentas"
        Me.btnAccesoVentas.Size = New Size(250, 160)
        Me.btnAccesoVentas.Text = "PUNTO DE VENTA" & vbCrLf & vbCrLf & "Registrar Ventas" & vbCrLf & "y Facturación"
        Me.btnAccesoVentas.TextAlign = ContentAlignment.MiddleCenter
        Me.btnAccesoVentas.Cursor = Cursors.Hand

        Me.pnlAccesosRapidos.Controls.Add(Me.lblTituloAccesos)
        Me.pnlAccesosRapidos.Controls.Add(Me.btnAccesoMantenimiento)
        Me.pnlAccesosRapidos.Controls.Add(Me.btnAccesoVentas)

        ' ── Pie de información ──
        Me.lblPieInfo.Font = New Font("Segoe UI", 8.0F)
        Me.lblPieInfo.ForeColor = Color.Silver
        Me.lblPieInfo.Location = New Point(30, 510)
        Me.lblPieInfo.Name = "lblPieInfo"
        Me.lblPieInfo.Size = New Size(820, 20)
        Me.lblPieInfo.Text = "Ferree-Lara v1.0.0 | Sistema de Gestión de Ferretería | 2026"
        Me.lblPieInfo.TextAlign = ContentAlignment.MiddleCenter

        ' ── Agregar controles al panel de contenido ──
        Me.pnlContenido.Controls.Add(Me.lblTituloEmpresa)
        Me.pnlContenido.Controls.Add(Me.lblLineaDecorativa)
        Me.pnlContenido.Controls.Add(Me.pnlMision)
        Me.pnlContenido.Controls.Add(Me.pnlVision)
        Me.pnlContenido.Controls.Add(Me.pnlAccesosRapidos)
        Me.pnlContenido.Controls.Add(Me.lblPieInfo)

        ' ── Timer: Reloj en la barra superior ──
        Me.tmrReloj.Enabled = True
        Me.tmrReloj.Interval = 1000

        ' ══════════════════════════════════════════════════════════════════════
        ' Configuración del Formulario frmPrincipal
        ' ══════════════════════════════════════════════════════════════════════
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(1100, 650)
        ' Orden de agregado importa: Dock.Fill va último para que funcione bien
        Me.Controls.Add(Me.pnlContenido)
        Me.Controls.Add(Me.pnlMenuLateral)
        Me.Controls.Add(Me.pnlBarraSuperior)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmPrincipal"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Ferree-Lara - Menú Principal"

        Me.pnlBarraSuperior.ResumeLayout(False)
        Me.pnlBarraSuperior.PerformLayout()
        Me.pnlMenuLateral.ResumeLayout(False)
        Me.pnlContenido.ResumeLayout(False)
        Me.pnlContenido.PerformLayout()
        Me.pnlMision.ResumeLayout(False)
        Me.pnlMision.PerformLayout()
        Me.pnlVision.ResumeLayout(False)
        Me.pnlVision.PerformLayout()
        Me.pnlAccesosRapidos.ResumeLayout(False)
        Me.pnlAccesosRapidos.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    ' ── Declaración de controles ──
    Friend WithEvents pnlBarraSuperior As Panel
    Friend WithEvents lblBienvenida As Label
    Friend WithEvents lblRolUsuario As Label
    Friend WithEvents lblFechaHora As Label

    Friend WithEvents pnlMenuLateral As Panel
    Friend WithEvents lblLogoMenu As Label
    Friend WithEvents lblSeparadorMenu As Label
    Friend WithEvents btnMantenimiento As Button
    Friend WithEvents btnVentas As Button
    Friend WithEvents btnCerrarSesion As Button

    Friend WithEvents pnlContenido As Panel
    Friend WithEvents lblTituloEmpresa As Label
    Friend WithEvents lblLineaDecorativa As Label

    Friend WithEvents pnlMision As Panel
    Friend WithEvents lblTituloMision As Label
    Friend WithEvents lblTextoMision As Label

    Friend WithEvents pnlVision As Panel
    Friend WithEvents lblTituloVision As Label
    Friend WithEvents lblTextoVision As Label

    Friend WithEvents pnlAccesosRapidos As Panel
    Friend WithEvents lblTituloAccesos As Label
    Friend WithEvents btnAccesoMantenimiento As Button
    Friend WithEvents btnAccesoVentas As Button

    Friend WithEvents lblPieInfo As Label
    Friend WithEvents tmrReloj As Timer

End Class
