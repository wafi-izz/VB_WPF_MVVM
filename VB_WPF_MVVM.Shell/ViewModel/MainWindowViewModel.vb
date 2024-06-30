Imports System.Collections.Specialized
Imports VB_WPF_MVVM.Module
Imports MaterialDesignThemes.Wpf
Imports VB_WPF_MVVM.API
Imports System.Collections.ObjectModel
Imports Prism.Regions
Imports VB_WPF_MVVM.Common
Imports VB_WPF_MVVM.Localization.My.Resources
Imports System.Globalization
Imports System.Threading

Public Class MainWindowViewModel
    Inherits BaseViewModel(Of ViewStateEnum)
    Public Sub New(regionManager As IRegionManager)
        Me.RegionManager = regionManager
        AddHandler Me.RegionManager.Regions.CollectionChanged, AddressOf RegionManager_CollectionChanged

    End Sub
    Public Enum ViewStateEnum
        Normal
        Busy
        Err
    End Enum
#Region "Properties"
    Public Property LanguageID As Integer
    Public Property HudUserName As String = ""
#End Region
#Region "Region Functions"
    Private Async Sub RegionManager_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs)
        Try
            If e.Action = NotifyCollectionChangedAction.Add AndAlso e.NewItems(0).name = "MainWindowRegion" Then
                RegionManager.RequestNavigate("MainWindowRegion", NameOf(LoginView))
                'RegionManager.Regions("MainRegion").RemoveAll()
            End If
            If e.Action = NotifyCollectionChangedAction.Add AndAlso e.NewItems(0).name = "MainRegion" Then
                If ShellUserSettings.LanguageID IsNot Nothing Then
                    LanguageID = ShellUserSettings.LanguageID
                End If
            End If
        Catch ex As Exception
            Dim Ms As New Message(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

#End Region
#Region "Functions"

#End Region
#Region "Commands"


#End Region
End Class