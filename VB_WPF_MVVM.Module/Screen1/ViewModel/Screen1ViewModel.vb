Imports VB_WPF_MVVM.Common
Imports System.Collections.ObjectModel
Imports Prism.Regions
Imports VB_WPF_MVVM.Localization
Imports Microsoft.VisualBasic.ApplicationServices
Public Class Screen1ViewModel
    Inherits BaseViewModel(Of ViewStateEnum)
    Public Enum ViewStateEnum
        Normal
        Err
        Busy
    End Enum
    Public Overrides Async Sub OnNavigatedTo(navigationContext As NavigationContext)
        Try
            'name you header
            Me.MainHeadar = Translations.Screen1

            'if you have parameters you want to pass where from where you came
            If navigationContext.Parameters.ContainsKey(NameOf(Screen1View)) Then
                Dim Para1 = navigationContext.Parameters.Item(NameOf(Screen1View))
            End If
        Catch ex As Exception
            Dim Ms As New Message(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

        ' init you first functions here
    End Sub
End Class
