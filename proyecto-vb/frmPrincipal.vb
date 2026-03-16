' ═══════════════════════════════════════════════════════════════════════════════
' Formulario: frmPrincipal (Código Behind)
' Descripción: Menú principal del sistema Ferree-Lara. Muestra la información
'              corporativa (Misión, Visión), datos del usuario autenticado,
'              reloj en tiempo real y proporciona navegación a los módulos
'              de Mantenimiento y Punto de Venta.
' Autor: Equipo Ferree-Lara
' Fecha: Marzo 2026
' ═══════════════════════════════════════════════════════════════════════════════

Public Class frmPrincipal

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: Form_Load
    ' Descripción: Se ejecuta al cargar el formulario. Configura la información
    '              del usuario en la barra superior y aplica restricciones
    '              según el rol del usuario autenticado.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub frmPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Mostrar información del usuario autenticado en la barra superior
        lblBienvenida.Text = $"Bienvenido/a, {SesionActual.NombreCompleto}"
        lblRolUsuario.Text = $"Rol: {SesionActual.Rol}"

        ' Actualizar el título de la ventana con datos de la sesión
        Me.Text = $"Ferree-Lara - Menú Principal | {SesionActual.NombreCompleto}"

        ' Mostrar la fecha y hora actual
        ActualizarReloj()

        ' Aplicar permisos según el rol del usuario
        ConfigurarPermisosPorRol()
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: ConfigurarPermisosPorRol
    ' Descripción: Habilita o deshabilita módulos según el rol del usuario.
    '              - Administrador: acceso completo a todos los módulos.
    '              - Cajero: solo acceso al punto de venta.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub ConfigurarPermisosPorRol()
        If Not SesionActual.EsAdministrador() Then
            ' El Cajero NO tiene acceso al módulo de mantenimiento
            btnMantenimiento.Enabled = False
            btnMantenimiento.BackColor = Color.FromArgb(60, 60, 60)
            btnAccesoMantenimiento.Enabled = False
            btnAccesoMantenimiento.BackColor = Color.FromArgb(230, 230, 230)
            btnAccesoMantenimiento.ForeColor = Color.Gray
            btnAccesoMantenimiento.Text = "MANTENIMIENTO" & vbCrLf & vbCrLf & "(Solo Administrador)"
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: tmrReloj_Tick
    ' Descripción: Actualiza la fecha y hora en la barra superior cada segundo.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub tmrReloj_Tick(sender As Object, e As EventArgs) Handles tmrReloj.Tick
        ActualizarReloj()
    End Sub

    ''' <summary>
    ''' Muestra la fecha y hora actual en formato legible.
    ''' </summary>
    Private Sub ActualizarReloj()
        lblFechaHora.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy HH:mm:ss")
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' EVENTOS DE NAVEGACIÓN — Menú lateral y tarjetas de acceso rápido
    ' ═══════════════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Abre el módulo de Mantenimiento (Catálogo de Productos y Clientes).
    ''' Accesible desde el botón del menú lateral y la tarjeta de acceso rápido.
    ''' </summary>
    Private Sub btnMantenimiento_Click(sender As Object, e As EventArgs) Handles btnMantenimiento.Click
        AbrirMantenimiento()
    End Sub

    Private Sub btnAccesoMantenimiento_Click(sender As Object, e As EventArgs) Handles btnAccesoMantenimiento.Click
        AbrirMantenimiento()
    End Sub

    Private Sub AbrirMantenimiento()
        Dim formMantenimiento As New frmMantenimiento()
        formMantenimiento.ShowDialog()
    End Sub

    ''' <summary>
    ''' Abre el módulo de Punto de Venta (POS).
    ''' Accesible desde el botón del menú lateral y la tarjeta de acceso rápido.
    ''' </summary>
    Private Sub btnVentas_Click(sender As Object, e As EventArgs) Handles btnVentas.Click
        AbrirPuntoDeVenta()
    End Sub

    Private Sub btnAccesoVentas_Click(sender As Object, e As EventArgs) Handles btnAccesoVentas.Click
        AbrirPuntoDeVenta()
    End Sub

    Private Sub AbrirPuntoDeVenta()
        Dim formVentas As New frmVentas()
        formVentas.ShowDialog()
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnCerrarSesion_Click
    ' Descripción: Cierra la sesión actual y regresa al formulario de login.
    '              Solicita confirmación antes de cerrar.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Dim resultado As DialogResult = MessageBox.Show(
            "¿Está seguro que desea cerrar la sesión?",
            "Confirmar Cierre de Sesión",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If resultado = DialogResult.Yes Then
            Me.Close() ' Cierra este formulario y regresa al frmLogin
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: FormClosing
    ' Descripción: Intercepta el cierre del formulario (botón X) para confirmar
    '              si el usuario realmente desea cerrar sesión.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub frmPrincipal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Solo pedir confirmación si el cierre viene de la X del formulario
        If e.CloseReason = CloseReason.UserClosing Then
            ' No solicitar doble confirmación si ya se confirmó desde el botón
            ' (el botón ya cierra con Me.Close, que también dispara FormClosing)
        End If
    End Sub

End Class
