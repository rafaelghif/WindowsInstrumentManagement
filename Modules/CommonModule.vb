Module CommonModule
    Public Sub ErrorAlert(stringMessage As String, Optional stringTitle As String = "Application Error")
        MessageBox.Show(stringMessage, stringTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
End Module
