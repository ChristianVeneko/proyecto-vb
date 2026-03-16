' ═══════════════════════════════════════════════════════════════════════════════
' Módulo: SesionActual
' Descripción: Módulo global que almacena la información del usuario que ha
'              iniciado sesión en el sistema. Permite a todos los formularios
'              acceder al Id, nombre y rol del usuario activo.
' ═══════════════════════════════════════════════════════════════════════════════

''' <summary>
''' Almacena los datos de la sesión activa del usuario autenticado.
''' Se establece tras un login exitoso y se consulta desde cualquier formulario.
''' </summary>
Module SesionActual

    ' ── Propiedades de la sesión ──
    Public UsuarioId As Integer = 0
    Public NombreUsuario As String = ""
    Public NombreCompleto As String = ""
    Public Rol As String = ""

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: IniciarSesion
    ' Descripción: Establece las variables de sesión con los datos del usuario
    '              que acaba de autenticarse exitosamente.
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub IniciarSesion(id As Integer, usuario As String, nombreComp As String, rolUsuario As String)
        UsuarioId = id
        NombreUsuario = usuario
        NombreCompleto = nombreComp
        Rol = rolUsuario
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: CerrarSesion
    ' Descripción: Limpia todas las variables de sesión (al cerrar sesión o
    '              al salir de la aplicación).
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Sub CerrarSesion()
        UsuarioId = 0
        NombreUsuario = ""
        NombreCompleto = ""
        Rol = ""
    End Sub

    ' ═══════════════════════════════════════════════════════════════════════════
    ' Método: EsAdministrador
    ' Descripción: Verifica si el usuario actual tiene rol de Administrador.
    ' Retorna:     True si el rol es "Administrador", False en caso contrario.
    ' ═══════════════════════════════════════════════════════════════════════════
    Public Function EsAdministrador() As Boolean
        Return Rol = "Administrador"
    End Function

End Module
