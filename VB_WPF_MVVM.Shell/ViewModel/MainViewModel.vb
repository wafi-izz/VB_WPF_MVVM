Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Threading
Imports VB_WPF_MVVM.Common
Imports VB_WPF_MVVM.Module
Imports Prism.Regions
Public Class MainViewModel
    Inherits BaseViewModel(Of ViewStateEnum)
    Public Enum ViewStateEnum
        Normal
        Err
        Busy
    End Enum
    Public Sub New()
        Me.RegionManager = RegionManager
    End Sub

    Public Overrides Async Sub OnNavigatedTo(navigationContext As NavigationContext)
        MyBase.OnNavigatedTo(navigationContext)
        Try
            RegionManager.RequestNavigate("MainRegion", NameOf(HomeView))
        Catch ex As Exception
            MessageBox.Show(ex.Message, MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub

    Public Sub OnIsOpenChaged()
        'pass
    End Sub

    Private Sub MainViewModel_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
        If e.PropertyName = "HomeView" Then
            'pass
        End If
    End Sub
    Public Property SignOut As New RelayCommand(Sub()
                                                    RegionManager.Regions("MainRegion").RemoveAll()
                                                End Sub)
#Region "Properties"
    'to hide the overlay after opening a window
    Public Property EnableDisable As Boolean = 0
#End Region
#Region "Functions"

    Private Sub AddView(ViewName As String, Parameter As NavigationParameters, Optional MultipleView As String = Nothing)
        Dim View = RegionManager.Regions("MainRegion").Views.FirstOrDefault(Function(a)
                                                                                Return a.GetType.Name = ViewName
                                                                            End Function)
        If ViewName Is Nothing Or MultipleView Then
            RegionManager.Regions("MainRegion").RemoveAll()
            RegionManager.RequestNavigate("MainRegion", ViewName, Parameter)
        Else
            RegionManager.Regions("MainRegion").Activate(View)
        End If
    End Sub
    Public Sub AddView(ViewName)
        Dim View = RegionManager.Regions("MainRegion").Views.FirstOrDefault(Function(c)
                                                                                Return c.GetType.Name = ViewName
                                                                            End Function)
        If View Is Nothing Then
            RegionManager.RequestNavigate("MainRegion", ViewName)
        Else
            RegionManager.Regions("MainRegion").Activate(View)
        End If
        IsOpen = False
    End Sub
#End Region
#Region "Commands"
    Public Property CloseTab As RelayCommand = New RelayCommand(Sub(Tab)
                                                                    Try
                                                                        If TypeOf Tab Is HomeView Then
                                                                            Return
                                                                        End If
                                                                        RegionManager.Regions("MainRegion").Remove(Tab)
                                                                    Catch ex As Exception
                                                                        MessageBox.Show(ex.Message, MessageBoxButton.OK, MessageBoxImage.Information)

                                                                    End Try
                                                                End Sub)
    Public Property CloseAllTabs As RelayCommand = New RelayCommand(Sub(Tab)
                                                                        Try
                                                                            RegionManager.Regions("MainRegion").RemoveAll()
                                                                            RegionManager.RequestNavigate("MainWindowRegion", NameOf(LoginView))
                                                                        Catch ex As Exception
                                                                            MessageBox.Show(ex.Message, MessageBoxButton.OK, MessageBoxImage.Information)
                                                                        End Try
                                                                    End Sub)
    Public Property Screen1ViewCommand As RelayCommand = New RelayCommand(Sub()
                                                                              Try
                                                                                  AddView(NameOf(Screen1View))
                                                                                  EnableDisable = False
                                                                              Catch ex As Exception
                                                                                  MessageBox.Show(ex.Message, MessageBoxButton.OK, MessageBoxImage.Information)
                                                                              End Try
                                                                          End Sub)
    Public Property Screen2ViewCommand As RelayCommand = New RelayCommand(Sub()
                                                                              Try
                                                                                  AddView(NameOf(Screen2View))
                                                                                  EnableDisable = False
                                                                              Catch ex As Exception
                                                                                  MessageBox.Show(ex.Message, MessageBoxButton.OK, MessageBoxImage.Information)
                                                                              End Try
                                                                          End Sub)
#End Region
End Class
