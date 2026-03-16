' ═══════════════════════════════════════════════════════════════════════════════
' Formulario: frmLogin (Archivo Designer)
' Descripción: Define la interfaz gráfica del formulario de inicio de sesión.
'              Incluye campos de usuario y contraseña, botones de ingreso y
'              salida, y elementos visuales de la marca Ferree-Lara.
' ═══════════════════════════════════════════════════════════════════════════════

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
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
    ' Descripción: Inicializa y configura todos los controles visuales del
    '              formulario de login. Generado por el diseñador.
    ' ══════════════════════════════════════════════════════════════════════════
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

        components = New System.ComponentModel.Container()

        ' ── Declaración de controles ──
        Me.pnlContenedor = New System.Windows.Forms.Panel()
        Me.pnlEncabezado = New System.Windows.Forms.Panel()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblSubtitulo = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        Me.lblContrasena = New System.Windows.Forms.Label()
        Me.txtContrasena = New System.Windows.Forms.TextBox()
        Me.btnIngresar = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.lblMensaje = New System.Windows.Forms.Label()
        Me.chkMostrarContrasena = New System.Windows.Forms.CheckBox()
        Me.lblVersion = New System.Windows.Forms.Label()

        Me.pnlContenedor.SuspendLayout()
        Me.pnlEncabezado.SuspendLayout()
        Me.SuspendLayout()

        ' ── Panel Contenedor Principal ──
        Me.pnlContenedor.BackColor = System.Drawing.Color.White
        Me.pnlContenedor.Location = New System.Drawing.Point(85, 30)
        Me.pnlContenedor.Name = "pnlContenedor"
        Me.pnlContenedor.Size = New System.Drawing.Size(380, 420)
        Me.pnlContenedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle

        ' ── Panel Encabezado (Barra superior con color corporativo) ──
        Me.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(33, 150, 243) ' Azul corporativo
        Me.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEncabezado.Name = "pnlEncabezado"
        Me.pnlEncabezado.Size = New System.Drawing.Size(378, 80)

        ' ── Título: Nombre de la empresa ──
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 18.0F, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.White
        Me.lblTitulo.Location = New System.Drawing.Point(75, 10)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Text = "FERREE-LARA"

        ' ── Subtítulo ──
        Me.lblSubtitulo.AutoSize = True
        Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Italic)
        Me.lblSubtitulo.ForeColor = System.Drawing.Color.White
        Me.lblSubtitulo.Location = New System.Drawing.Point(85, 50)
        Me.lblSubtitulo.Name = "lblSubtitulo"
        Me.lblSubtitulo.Text = "Sistema de Gestión de Ferretería"

        ' ── Label: Usuario ──
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.Font = New System.Drawing.Font("Segoe UI", 10.0F)
        Me.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64)
        Me.lblUsuario.Location = New System.Drawing.Point(40, 110)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Text = "Usuario:"

        ' ── TextBox: Usuario ──
        Me.txtUsuario.Font = New System.Drawing.Font("Segoe UI", 11.0F)
        Me.txtUsuario.Location = New System.Drawing.Point(40, 135)
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(300, 27)
        Me.txtUsuario.PlaceholderText = "Ingrese su usuario"
        Me.txtUsuario.MaxLength = 50

        ' ── Label: Contraseña ──
        Me.lblContrasena.AutoSize = True
        Me.lblContrasena.Font = New System.Drawing.Font("Segoe UI", 10.0F)
        Me.lblContrasena.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64)
        Me.lblContrasena.Location = New System.Drawing.Point(40, 180)
        Me.lblContrasena.Name = "lblContrasena"
        Me.lblContrasena.Text = "Contraseña:"

        ' ── TextBox: Contraseña ──
        Me.txtContrasena.Font = New System.Drawing.Font("Segoe UI", 11.0F)
        Me.txtContrasena.Location = New System.Drawing.Point(40, 205)
        Me.txtContrasena.Name = "txtContrasena"
        Me.txtContrasena.Size = New System.Drawing.Size(300, 27)
        Me.txtContrasena.PasswordChar = CChar("●")
        Me.txtContrasena.PlaceholderText = "Ingrese su contraseña"
        Me.txtContrasena.MaxLength = 50

        ' ── CheckBox: Mostrar contraseña ──
        Me.chkMostrarContrasena.AutoSize = True
        Me.chkMostrarContrasena.Font = New System.Drawing.Font("Segoe UI", 8.5F)
        Me.chkMostrarContrasena.ForeColor = System.Drawing.Color.Gray
        Me.chkMostrarContrasena.Location = New System.Drawing.Point(40, 240)
        Me.chkMostrarContrasena.Name = "chkMostrarContrasena"
        Me.chkMostrarContrasena.Text = "Mostrar contraseña"

        ' ── Botón: Ingresar ──
        Me.btnIngresar.BackColor = System.Drawing.Color.FromArgb(33, 150, 243)
        Me.btnIngresar.FlatAppearance.BorderSize = 0
        Me.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIngresar.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
        Me.btnIngresar.ForeColor = System.Drawing.Color.White
        Me.btnIngresar.Location = New System.Drawing.Point(40, 280)
        Me.btnIngresar.Name = "btnIngresar"
        Me.btnIngresar.Size = New System.Drawing.Size(300, 40)
        Me.btnIngresar.Text = "INGRESAR"
        Me.btnIngresar.Cursor = System.Windows.Forms.Cursors.Hand

        ' ── Botón: Salir ──
        Me.btnSalir.BackColor = System.Drawing.Color.FromArgb(211, 47, 47)
        Me.btnSalir.FlatAppearance.BorderSize = 0
        Me.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalir.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
        Me.btnSalir.ForeColor = System.Drawing.Color.White
        Me.btnSalir.Location = New System.Drawing.Point(40, 330)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(300, 35)
        Me.btnSalir.Text = "SALIR"
        Me.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand

        ' ── Label: Mensaje de error/estado ──
        Me.lblMensaje.Font = New System.Drawing.Font("Segoe UI", 9.0F)
        Me.lblMensaje.ForeColor = System.Drawing.Color.Red
        Me.lblMensaje.Location = New System.Drawing.Point(40, 375)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(300, 20)
        Me.lblMensaje.Text = ""
        Me.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        ' ── Label: Versión ──
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 7.5F)
        Me.lblVersion.ForeColor = System.Drawing.Color.Silver
        Me.lblVersion.Location = New System.Drawing.Point(140, 395)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Text = "v1.0.0 - Ferree-Lara 2026"

        ' ── Agregar controles al panel de encabezado ──
        Me.pnlEncabezado.Controls.Add(Me.lblTitulo)
        Me.pnlEncabezado.Controls.Add(Me.lblSubtitulo)

        ' ── Agregar controles al panel contenedor ──
        Me.pnlContenedor.Controls.Add(Me.pnlEncabezado)
        Me.pnlContenedor.Controls.Add(Me.lblUsuario)
        Me.pnlContenedor.Controls.Add(Me.txtUsuario)
        Me.pnlContenedor.Controls.Add(Me.lblContrasena)
        Me.pnlContenedor.Controls.Add(Me.txtContrasena)
        Me.pnlContenedor.Controls.Add(Me.chkMostrarContrasena)
        Me.pnlContenedor.Controls.Add(Me.btnIngresar)
        Me.pnlContenedor.Controls.Add(Me.btnSalir)
        Me.pnlContenedor.Controls.Add(Me.lblMensaje)
        Me.pnlContenedor.Controls.Add(Me.lblVersion)

        ' ══════════════════════════════════════════════════════════════════════
        ' Configuración del Formulario frmLogin
        ' ══════════════════════════════════════════════════════════════════════
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(236, 239, 241) ' Gris claro de fondo
        Me.ClientSize = New System.Drawing.Size(550, 480)
        Me.Controls.Add(Me.pnlContenedor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ferree-Lara - Inicio de Sesión"
        Me.AcceptButton = Me.btnIngresar
        Me.CancelButton = Me.btnSalir

        Me.pnlContenedor.ResumeLayout(False)
        Me.pnlContenedor.PerformLayout()
        Me.pnlEncabezado.ResumeLayout(False)
        Me.pnlEncabezado.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    ' ── Declaración de controles (campos del formulario) ──
    Friend WithEvents pnlContenedor As System.Windows.Forms.Panel
    Friend WithEvents pnlEncabezado As System.Windows.Forms.Panel
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblSubtitulo As System.Windows.Forms.Label
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents lblContrasena As System.Windows.Forms.Label
    Friend WithEvents txtContrasena As System.Windows.Forms.TextBox
    Friend WithEvents btnIngresar As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents chkMostrarContrasena As System.Windows.Forms.CheckBox
    Friend WithEvents lblVersion As System.Windows.Forms.Label

End Class
