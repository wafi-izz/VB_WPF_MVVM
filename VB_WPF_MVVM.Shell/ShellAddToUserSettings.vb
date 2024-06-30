Public Class ShellAddToUserSettings
    Public Sub Add(LanguageID As String)
        ShellUserSettings.LanguageID = LanguageID
    End Sub
End Class
