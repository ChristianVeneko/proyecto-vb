' ═══════════════════════════════════════════════════════════════════════════════
' Formulario: frmLogin (Código Behind)
' Descripción: Controla la lógica de autenticación del sistema Ferree-Lara.
'              Valida las credenciales del usuario contra la base de datos,
'              establece la sesión activa y permite el acceso al menú principal.
' Autor: Equipo Ferree-Lara
' Fecha: Marzo 2026
' ═══════════════════════════════════════════════════════════════════════════════

Imports Microsoft.Data.Sqlite

Public Class frmLogin

    ' ── Contador de intentos fallidos de inicio de sesión ──
    Private intentosFallidos As Integer = 0
    Private Const MaxIntentos As Integer = 5

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: Form_Load
    ' Descripción: Se ejecuta al cargar el formulario. Inicializa la base de
    '              datos (crea tablas y datos iniciales si es la primera vez)
    '              y prepara la interfaz para recibir las credenciales.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Inicializar la base de datos (crear tablas si no existen)
            DatabaseHelper.InicializarBaseDatos()

            ' Limpiar los campos y posicionar el cursor en el campo de usuario
            LimpiarCampos()
            txtUsuario.Focus()

        Catch ex As Exception
            MessageBox.Show(
                "Error al inicializar la base de datos:" & vbCrLf & ex.Message,
                "Error Crítico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnIngresar_Click
    ' Descripción: Maneja el clic en el botón "INGRESAR". Ejecuta la validación
    '              de campos vacíos, verifica las credenciales en la base de
    '              datos y, si son correctas, abre el menú principal.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click

        ' ── Paso 1: Verificar que el sistema no esté bloqueado por intentos ──
        If intentosFallidos >= MaxIntentos Then
            MostrarMensaje("Sistema bloqueado por demasiados intentos. Reinicie la aplicación.", Color.Red)
            Return
        End If

        ' ── Paso 2: Validar que los campos no estén vacíos ──
        If Not ValidarCampos() Then
            Return
        End If

        ' ── Paso 3: Obtener los valores ingresados ──
        Dim nombreUsuario As String = txtUsuario.Text.Trim()
        Dim contrasena As String = txtContrasena.Text

        ' ── Paso 4: Intentar autenticar al usuario ──
        Try
            If AutenticarUsuario(nombreUsuario, contrasena) Then
                ' Login exitoso: abrir el menú principal
                MostrarMensaje("Acceso concedido. Bienvenido/a.", Color.Green)
                AbrirMenuPrincipal()
            Else
                ' Login fallido: incrementar contador y mostrar error
                intentosFallidos += 1
                Dim intentosRestantes As Integer = MaxIntentos - intentosFallidos
                MostrarMensaje(
                    $"Credenciales incorrectas. Intentos restantes: {intentosRestantes}",
                    Color.Red)
                txtContrasena.Clear()
                txtContrasena.Focus()
            End If
        Catch ex As Exception
            MostrarMensaje("Error al conectar con la base de datos.", Color.Red)
            MessageBox.Show(
                "Detalle del error:" & vbCrLf & ex.Message,
                "Error de Autenticación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: AutenticarUsuario
    ' Descripción: Consulta la base de datos para verificar si existe un usuario
    '              activo con el nombre de usuario y contraseña proporcionados.
    '              Si la autenticación es exitosa, establece la sesión.
    ' Parámetros:  nombreUsuario - El nombre de usuario ingresado.
    '              contrasena    - La contraseña en texto plano (se encripta
    '                              antes de comparar).
    ' Retorna:     True si las credenciales son válidas, False si no.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Function AutenticarUsuario(nombreUsuario As String, contrasena As String) As Boolean
        ' Encriptar la contraseña ingresada para compararla con la almacenada
        Dim contrasenaHash As String = DatabaseHelper.EncriptarContrasena(contrasena)

        Using conexion As SqliteConnection = DatabaseHelper.ObtenerConexion()
            conexion.Open()

            ' Consulta parametrizada para prevenir inyección SQL
            Dim sql As String = "
                SELECT UsuarioId, NombreUsuario, NombreCompleto, Rol
                FROM Usuarios
                WHERE NombreUsuario = @usuario
                  AND Contrasena    = @contrasena
                  AND Activo        = 1;"

            Using comando As New SqliteCommand(sql, conexion)
                ' Agregar parámetros de forma segura
                comando.Parameters.AddWithValue("@usuario", nombreUsuario)
                comando.Parameters.AddWithValue("@contrasena", contrasenaHash)

                Using lector As SqliteDataReader = comando.ExecuteReader()
                    If lector.Read() Then
                        ' ── Credenciales válidas: establecer la sesión ──
                        SesionActual.IniciarSesion(
                            id:=lector.GetInt32(0),
                            usuario:=lector.GetString(1),
                            nombreComp:=lector.GetString(2),
                            rolUsuario:=lector.GetString(3))
                        Return True
                    End If
                End Using
            End Using
        End Using

        ' Si llegamos aquí, las credenciales no son válidas
        Return False
    End Function

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: ValidarCampos
    ' Descripción: Verifica que los campos de usuario y contraseña no estén
    '              vacíos antes de intentar la autenticación.
    ' Retorna:     True si ambos campos tienen contenido, False si alguno
    '              está vacío.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Function ValidarCampos() As Boolean
        ' Verificar campo de usuario
        If String.IsNullOrWhiteSpace(txtUsuario.Text) Then
            MostrarMensaje("Por favor, ingrese su nombre de usuario.", Color.OrangeRed)
            txtUsuario.Focus()
            Return False
        End If

        ' Verificar campo de contraseña
        If String.IsNullOrWhiteSpace(txtContrasena.Text) Then
            MostrarMensaje("Por favor, ingrese su contraseña.", Color.OrangeRed)
            txtContrasena.Focus()
            Return False
        End If

        Return True
    End Function

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: AbrirMenuPrincipal
    ' Descripción: Oculta el formulario de login y abre el menú principal.
    '              Si el usuario cierra el menú principal, se vuelve a mostrar
    '              el login para permitir un nuevo inicio de sesión.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub AbrirMenuPrincipal()
        Me.Hide()

        ' Crear y mostrar el formulario principal
        Dim formPrincipal As New frmPrincipal()
        formPrincipal.ShowDialog()

        ' Al cerrar el menú principal, limpiar sesión y mostrar login nuevamente
        SesionActual.CerrarSesion()
        LimpiarCampos()
        intentosFallidos = 0
        Me.Show()
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: btnSalir_Click
    ' Descripción: Cierra la aplicación completamente después de confirmar
    '              con el usuario.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Dim resultado As DialogResult = MessageBox.Show(
            "¿Está seguro que desea salir del sistema?",
            "Confirmar Salida",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If resultado = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: chkMostrarContrasena_CheckedChanged
    ' Descripción: Alterna la visibilidad de la contraseña en el campo de texto.
    '              Si el checkbox está marcado, muestra la contraseña en texto
    '              plano; si no, la oculta con el carácter de máscara.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub chkMostrarContrasena_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarContrasena.CheckedChanged
        If chkMostrarContrasena.Checked Then
            txtContrasena.PasswordChar = CChar(vbNullChar) ' Mostrar texto plano
        Else
            txtContrasena.PasswordChar = CChar("●") ' Ocultar con máscara
        End If
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: LimpiarCampos
    ' Descripción: Reinicia los campos del formulario a su estado inicial.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub LimpiarCampos()
        txtUsuario.Clear()
        txtContrasena.Clear()
        chkMostrarContrasena.Checked = False
        lblMensaje.Text = ""
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: MostrarMensaje
    ' Descripción: Muestra un mensaje de estado en el label inferior del
    '              formulario con el color especificado.
    ' Parámetros:  mensaje - El texto del mensaje a mostrar.
    '              color   - El color del texto (rojo para error, verde para éxito).
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub MostrarMensaje(mensaje As String, color As Color)
        lblMensaje.ForeColor = color
        lblMensaje.Text = mensaje
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Evento: txtUsuario_KeyPress / txtContrasena_KeyPress
    ' Descripción: Limpia el mensaje de error cuando el usuario empieza a
    '              escribir nuevamente, para evitar confusión visual.
    ' ═══════════════════════════════════════════════════════════════════════════
    Private Sub txtUsuario_TextChanged(sender As Object, e As EventArgs) Handles txtUsuario.TextChanged
        lblMensaje.Text = ""
    End Sub

    Private Sub txtContrasena_TextChanged(sender As Object, e As EventArgs) Handles txtContrasena.TextChanged
        lblMensaje.Text = ""
    End Sub

End Class
