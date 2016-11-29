Imports System.Threading




Public Class Form1
    ' This is a definition of a thread. The thread object is Thread_0 and
    ' it uses the method Thread_0_method() below as its code.
    Dim Thread_0 As New Thread(AddressOf Thread_0_method)
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
