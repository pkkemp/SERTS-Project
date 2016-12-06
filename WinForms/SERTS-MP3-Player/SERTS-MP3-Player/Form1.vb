Imports System.Threading

Public Class Form1
    ' This is a definition of a thread. The thread object is Thread_0 and
    ' it uses the method Thread_0_method() below as its code.

    ' This defines a delegate subroutine that takes a string as its input
    Delegate Sub ListBoxDelegate(ByVal myStr As String)

    Dim Thread_0 As New Thread(AddressOf Thread_0_method)
    ' This makes an instance of the delegate that points to the address
    ' of the InvokeMethod function. When the delegate is invoked it will
    ' run the InvokeMethod method on the thread that owns the window handle.
    Dim ListBoxDel As New ListBoxDelegate(AddressOf ListBoxDelMethod)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Serial Port initialize
        Try
            SerialPort1.Open()
        Catch
            MessageBox.Show("Failed to open serial port", "Audio Player",
           MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk)
            ' Console.WriteLine("Failed to open serial port") this is old code
        End Try
        'Thread_0 initialize
        Thread_0.IsBackground = True
        Thread_0.Priority = ThreadPriority.Lowest
        Thread_0.Start()
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles TrackLabel.Click
        '  TrackNames.Items.Add("Testing")
    End Sub
    Private Sub Thread_0_method()
        'Thread code goes here
        Dim str As String
        Dim c As Integer
        str = ""

        SerialPort1.Write("R", 0, 1)
        Do
            str = SerialPort1.ReadLine()
            If Not (str = "E") Then TrackNames.Invoke(ListBoxDel, str)
        Loop Until str = "E"
    End Sub
    Public Sub ListBoxDelMethod(ByVal myStr As String)
        TrackNames.Items.Add(myStr)
    End Sub
    Private Sub TrackNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TrackNames.SelectedIndexChanged

    End Sub
End Class
