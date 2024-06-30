Imports VB_WPF_MVVM.Common
Imports System.Collections.ObjectModel
Imports Prism.Regions
Imports VB_WPF_MVVM.Localization
Public Class Screen2ViewModel
    Inherits BaseViewModel(Of ViewStateEnum)
    Public Enum ViewStateEnum
        Normal
        Err
        Busy
    End Enum
    Public Overrides Async Sub OnNavigatedTo(navigationContext As NavigationContext)
        Try
            Me.MainHeadar = Translations.Screen2

            ' init you first functions here
        Catch ex As Exception
            Dim Ms As New Message(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub
End Class
