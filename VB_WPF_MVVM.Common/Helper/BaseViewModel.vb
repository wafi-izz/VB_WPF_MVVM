Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Reflection
Imports System.Windows.Controls
Imports Prism
Imports Prism.Events
Imports Prism.Ioc
Imports Prism.Mvvm
Imports Prism.Regions
Imports Prism.Unity

Public Class BaseViewModel(Of T)
    Inherits BaseModel
    Implements INavigationAware, IActiveAware
    Public Event IsActiveChanged As EventHandler Implements IActiveAware.IsActiveChanged
    Private _Lock As Object = New Object
    Property _RegionManager As RegionManager
    Property _PupSubEvent() As IEventAggregator


    Public ReadOnly Property PupSubEvent() As IEventAggregator
        Get
            If _PupSubEvent Is Nothing Then
                Dim container As IContainerProvider = CType(PrismApplication.Current, PrismApplicationBase).Container
                _PupSubEvent = container.Resolve(Of IEventAggregator)
            End If
            Return _PupSubEvent
        End Get
    End Property



    Public Property RegionManager As RegionManager
        Get
            Dim Container As IContainerProvider = CType(PrismApplication.Current, PrismApplicationBase).Container
            _RegionManager = Container.Resolve(Of IRegionManager)
            Return _RegionManager
        End Get
        Set(value As RegionManager)
            _RegionManager = value
        End Set
    End Property
    Public Overridable Sub OnNavigatedTo(navigationContext As NavigationContext) Implements INavigationAware.OnNavigatedTo

    End Sub

    Public Overridable Function IsNavigationTarget(navigationContext As NavigationContext) As Boolean Implements INavigationAware.IsNavigationTarget

    End Function

    Public Overridable Sub OnNavigatedFrom(navigationContext As NavigationContext) Implements INavigationAware.OnNavigatedFrom

    End Sub
    Public Property IsActiveMonth As Boolean Implements IActiveAware.IsActive
    Public Property MainHeadar As String
    Public Property SubHeadar As String
    Public Property Icon As Object
    Public Property ViewState() As T
    Public Property ViewStateMassege() As String
    'Public Property MessageViewModel As MessageDilalogViewModel
    'Public Property MessageCommands As ObservableCollection(Of MessageCommand)



    Private _CloseCommand As RelayCommand
    Public ReadOnly Property CloseCommand As RelayCommand
        Get
            If _CloseCommand Is Nothing Then
                _CloseCommand = New RelayCommand(Sub()
                                                     Close()
                                                 End Sub)
            End If
            Return _CloseCommand
        End Get
    End Property
    Public Async Function CloseAsync() As Task(Of Boolean)

        For Each r As Region In RegionManager.Regions.ToList
            If Close(r) Then
                Return True
            End If
        Next

        Return False

    End Function
    Public Async Sub Close()
        Await CloseAsync()
    End Sub
    Private Function Close(Region As IRegion) As Boolean
        Dim View = Region.Views.FirstOrDefault(Function(x As UserControl)
                                                   Return CType(x, UserControl).DataContext.GetHashCode = Me.GetHashCode
                                               End Function)

        If View IsNot Nothing Then
            Region.Remove(View)
            Return True
        End If

        Return False
    End Function



    Public Property DocumentTypeID As Integer
End Class
