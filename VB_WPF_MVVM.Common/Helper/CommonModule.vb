Imports Prism.Ioc
Imports Prism.Modularity
Imports Prism.Regions
Imports Unity
Public Class CommonModule
    Implements IModule
    Private iRegionManager As IRegionManager
    Public Sub New(ByVal iRegionManager As IRegionManager)
        Me.iRegionManager = iRegionManager
    End Sub
    Private _regionManager As IRegionManager
    Private _container As IUnityContainer
    Private Sub IModule_RegisterTypes(containerRegistry As IContainerRegistry) Implements IModule.RegisterTypes
        'Throw New NotImplementedException()

        'containerRegistry.RegisterForNavigation(Of MessageDilalogView)
        'containerRegistry.RegisterForNavigation(Of ReportView)
    End Sub
    Private Sub IModule_OnInitialized(containerProvider As IContainerProvider) Implements IModule.OnInitialized
        'Throw New NotImplementedException()
    End Sub
End Class
