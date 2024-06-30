Imports System.Globalization
Imports CommonServiceLocator
Imports Prism.Ioc
Imports Prism.Modularity
Imports VB_WPF_MVVM.Module

Class Application
    Protected Overrides Sub RegisterTypes(containerRegistry As IContainerRegistry)
        containerRegistry.RegisterForNavigation(Of MainWindow)
        containerRegistry.RegisterForNavigation(Of MainView)
        containerRegistry.RegisterForNavigation(Of HomeView)
        containerRegistry.RegisterForNavigation(Of LoginView)
    End Sub
    Protected Overrides Sub ConfigureModuleCatalog(moduleCatalog As IModuleCatalog)
        moduleCatalog.AddModule(Of ModuleModule)
    End Sub

    Dim _cultureInfo As CultureInfo
    Protected Overrides Function CreateShell() As Window
        Dim dd = ServiceLocator.Current.GetInstance(Of MainWindow)()
        'If _cultureInfo.TextInfo.IsRightToLeft Then
        '    dd.FlowDirection = FlowDirection.RightToLeft
        'Else
        '    dd.FlowDirection = FlowDirection.LeftToRight
        'End If
        Return dd
    End Function
End Class
