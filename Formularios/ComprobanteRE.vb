Imports System.Drawing.Printing

Public Class ComprobanteRE

    Private Const NombreSeccion As String = "NumericValues"

    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    Dim Path_Archivo_Ini As String

    Dim FUENTE As New Font("Arial", 16)
    Dim FuenteFirma As New Font("Arial", 12)

    Dim Firma As String = ""
    Dim Observ As String = ""
    Dim ObResult As String = ""
    Dim CR As String = ""
    Dim NComp As String = ""
    Dim NRem As String = ""


    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Call GrabarINI(Path_Archivo_Ini, NombreSeccion, "nud1", nudComprobante.Value)
        Call GrabarINI(Path_Archivo_Ini, NombreSeccion, "nud2", nudRemito.Value)

    End Sub

    Private Sub cmdCompRE_Click(sender As Object, e As EventArgs) Handles cmdCompRE.Click

        NRem = ""
        NComp = nudComprobante.Value


        CR = "Comprobante de  
Retiro / Entrega"
        Observ = "OBSERVACIONES: "
        ObResult = txtObservaciones.Text
        Firma = ""
        PrintPreviewDialog.ShowDialog()

    End Sub


    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument.PrintPage

        Dim X As Integer = e.MarginBounds.Left
        Dim Y As Integer = e.MarginBounds.Top

        'Cliente

        'Cabecera
        e.Graphics.DrawImage(PictureBox.Image, X - 50, Y - 50)
        e.Graphics.DrawString(CR, FUENTE, Brushes.Black, X + 220, Y)
        e.Graphics.DrawString("N° " & NComp, FUENTE, Brushes.Black, e.MarginBounds.Right - 100, Y)
        e.Graphics.DrawString("N° " & NRem, FUENTE, Brushes.Black, e.MarginBounds.Right - 100, Y)
        e.Graphics.DrawString("Fecha: ", FUENTE, Brushes.Black, e.MarginBounds.Right - 100, Y + 30)
        e.Graphics.DrawString(DateTimePicker.Text, FUENTE, Brushes.Black, e.MarginBounds.Right - 100, Y + 60)


        e.Graphics.DrawString("-------------------------------------------------------------------------------------------", FUENTE, Brushes.Black, e.MarginBounds.Left - 30, Y + 100)


        'Datos de cliente
        e.Graphics.DrawString("Cliente: " & txtUsuario.Text, FUENTE, Brushes.Black, X - 30, Y + 130)
        e.Graphics.DrawString("Domicilio: " & txtDomicilio.Text, FUENTE, Brushes.Black, X - 30, Y + 160)
        e.Graphics.DrawString("Tel: " & txtTelefono.Text, FUENTE, Brushes.Black, e.MarginBounds.Right - 200, Y + 160)

        'Producto
        e.Graphics.DrawString("PRODUCTO: " & txtProducto.Text, FUENTE, Brushes.Black, X - 30, Y + 220)

        'Observaciones
        e.Graphics.DrawString(Observ & ObResult, FUENTE, Brushes.Black, X - 30, Y + 350)
        e.Graphics.DrawString(Firma, FuenteFirma, Brushes.Black, e.MarginBounds.Right - 100, Y + 420)


        'Negocio

        'Cabecera
        e.Graphics.DrawImage(PictureBox.Image, X - 50, Y + 500)
        e.Graphics.DrawString(CR, FUENTE, Brushes.Black, X + 220, Y + 550)
        e.Graphics.DrawString("N° " & NComp, FUENTE, Brushes.Black, e.MarginBounds.Right - 100, Y + 550)
        e.Graphics.DrawString("N° " & NRem, FUENTE, Brushes.Black, e.MarginBounds.Right - 100, Y + 550)
        e.Graphics.DrawString("Fecha: ", FUENTE, Brushes.Black, e.MarginBounds.Right - 100, Y + 580)
        e.Graphics.DrawString(DateTimePicker.Text, FUENTE, Brushes.Black, e.MarginBounds.Right - 100, Y + 610)


        e.Graphics.DrawString("-------------------------------------------------------------------------------------------", FUENTE, Brushes.Black, e.MarginBounds.Left - 30, Y + 650)


        'Datos de cliente
        e.Graphics.DrawString("Cliente: " & txtUsuario.Text, FUENTE, Brushes.Black, X - 30, Y + 680)
        e.Graphics.DrawString("Domicilio: " & txtDomicilio.Text, FUENTE, Brushes.Black, X - 30, Y + 710)
        e.Graphics.DrawString("Tel: " & txtTelefono.Text, FUENTE, Brushes.Black, e.MarginBounds.Right - 200, Y + 710)

        'Producto
        e.Graphics.DrawString("PRODUCTO: " & txtProducto.Text, FUENTE, Brushes.Black, X - 30, Y + 770)

        'Observaciones

        e.Graphics.DrawString(Observ & ObResult, FUENTE, Brushes.Black, X - 30, Y + 900)
        e.Graphics.DrawString(Firma, FuenteFirma, Brushes.Black, e.MarginBounds.Right - 100, Y + 950)



    End Sub


    Private Sub cmdRemito_Click(sender As Object, e As EventArgs) Handles cmdRemito.Click

        NComp = ""
        NRem = nudRemito.Value

        CR = "Remito"
        Observ = ""
        ObResult = ""
        Firma = "Firma"
        PrintPreviewDialog.ShowDialog()

    End Sub

    Private Sub ComprobanteRE_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DateTimePicker.Text = Date.Today
        Path_Archivo_Ini = Application.StartupPath & "\config.ini"

        nudComprobante.Value = LeerINI(Path_Archivo_Ini, NombreSeccion, "nud1", 0)
        nudRemito.Value = LeerINI(Path_Archivo_Ini, NombreSeccion, "nud2", 0)


    End Sub

    Private Function LeerINI(ByVal sFileName As String, ByVal sSection As String, ByVal sKeyName As String, Optional ByVal sDefault As String = "0") As String

        Dim ret As Integer
        Dim sRetVal As String

        sRetVal = New String(Chr(0), 255)

        ret = GetPrivateProfileString(sSection, sKeyName, sDefault, sRetVal, Len(sRetVal), sFileName)

        If ret = 0 Then
            Return sDefault
        Else
            Return Strings.Left(sRetVal, ret)
        End If

    End Function

    Private Sub GrabarINI(ByVal sFileName As String, ByVal sSection As String, ByVal sKeyName As String, ByVal sValue As String)
        Call WritePrivateProfileString(sSection, sKeyName, sValue, sFileName)
    End Sub

End Class
