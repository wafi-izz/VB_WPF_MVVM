Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Interactivity
Public Class BindableSelectedItemBehavior
    Inherits Behavior(Of TreeView)
#Region "SelectedItem Property"
    Public Property SelectedItem() As Object
        Get
            Return CObj(GetValue(SelectedItemProperty))
        End Get
        Set(ByVal value As Object)
            SetValue(SelectedItemProperty, value)
        End Set
    End Property
    Public Shared ReadOnly SelectedItemProperty As DependencyProperty = DependencyProperty.Register("SelectedItem", GetType(Object), GetType(BindableSelectedItemBehavior), New UIPropertyMetadata(Nothing, AddressOf OnSelectedItemChanged))
    Private Shared Sub OnSelectedItemChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim item = TryCast(e.NewValue, TreeViewItem)
        If item IsNot Nothing Then
            item.SetValue(TreeViewItem.IsSelectedProperty, True)
        End If
    End Sub
#End Region
    Protected Overrides Sub OnAttached()
        MyBase.OnAttached()

        AddHandler Me.AssociatedObject.SelectedItemChanged, AddressOf OnTreeViewSelectedItemChanged
    End Sub

    Protected Overrides Sub OnDetaching()
        MyBase.OnDetaching()

        If Me.AssociatedObject IsNot Nothing Then
            RemoveHandler Me.AssociatedObject.SelectedItemChanged, AddressOf OnTreeViewSelectedItemChanged
        End If
    End Sub

    Private Sub OnTreeViewSelectedItemChanged(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of Object))
        Me.SelectedItem = e.NewValue
    End Sub
End Class
