Imports Prism.Regions
Imports VB_WPF_MVVM.Localization

Public Class HomeViewModel
    Inherits BaseViewModel(Of ViewStateEnum)
    Public Sub New()

    End Sub
    Public Sub New(ID As String)
        SubHeadar = ID
    End Sub
#Region "Enums"
    Public Enum ViewStateEnum
        Normal
        Err
        Busy
    End Enum
#End Region
#Region "Properties"
#End Region
#Region "Initilization and Navigation"
    Public Overrides Async Sub OnNavigatedTo(navigationContext As NavigationContext)
        Try
            MyBase.OnNavigatedTo(navigationContext)
            Me.MainHeadar = Translations.Dashboard
        Catch ex As Exception
            Dim Ms As New Message(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub
#End Region

End Class
