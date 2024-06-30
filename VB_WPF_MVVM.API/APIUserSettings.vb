Module UserSettings
    Public Property LanguageID As Integer

End Module
Public Class APIAddToUserSettings
    Public Sub Add(LanguageID As Integer)
        UserSettings.LanguageID = LanguageID
    End Sub
End Class
