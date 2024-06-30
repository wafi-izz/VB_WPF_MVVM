Imports Prism.Ioc
Imports Prism.Modularity
Public Class ModuleModule
    Implements IModule
    Public Sub RegisterTypes(containerRegistry As IContainerRegistry) Implements IModule.RegisterTypes
        containerRegistry.RegisterForNavigation(Of Screen1View)
        containerRegistry.RegisterForNavigation(Of Screen2View)
    End Sub
    Public Sub OnInitialized(containerProvider As IContainerProvider) Implements IModule.OnInitialized

    End Sub
End Class