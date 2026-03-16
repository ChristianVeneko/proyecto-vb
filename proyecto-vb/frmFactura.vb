' ═══════════════════════════════════════════════════════════════════════════════
' Formulario: frmFactura (Stub - Se desarrollará en la Fase 3)
' Descripción: Placeholder de la factura. Se completará con la visualización
'              completa de los detalles de la venta procesada.
' ═══════════════════════════════════════════════════════════════════════════════

Public Class frmFactura

    ' Campo público para recibir el ID de la venta desde frmVentas
    Public VentaIdRecibido As Integer = 0

    Private Sub frmFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = $"Ferree-Lara - Factura | Venta #{VentaIdRecibido}"
    End Sub

End Class
