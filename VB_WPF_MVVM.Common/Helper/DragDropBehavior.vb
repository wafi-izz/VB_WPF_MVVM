
Imports System.Runtime.CompilerServices
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Interactivity
Imports System.Windows.Media
Imports System.Windows.Shapes
Imports Microsoft.VisualBasic.CompilerServices
Imports Prism.Commands

Public Class DragDropBehavior
    Inherits Behavior(Of TreeView)

    Public Shared CommandProperty As DependencyProperty = DependencyProperty.Register("Command", GetType(DelegateCommand(Of TreeViewDragDropEventArgs)), GetType(DragDropBehavior), New UIPropertyMetadata(Nothing))

    Private tolerance_Renamed As Double

    Private offset_Renamed As Double

    Private isDragging As Boolean

    Private startPosition As Point

    Private _Target As Object

    Private _source As Object
    Public Property AllowedEffects() As DragDropEffects
    Public Property Command() As DelegateCommand(Of TreeViewDragDropEventArgs)
        Get
            Return CType(MyBase.GetValue(CommandProperty), DelegateCommand(Of TreeViewDragDropEventArgs))
        End Get
        Set(value As DelegateCommand(Of TreeViewDragDropEventArgs))
            MyBase.SetValue(CommandProperty, value)
        End Set
    End Property

    Public Property _DragDropEffects() As DragDropEffects

    Public Sub New()
        Me.tolerance_Renamed = 20.0
        Me.offset_Renamed = 5.0
        Me._Target = Nothing
        Me._source = Nothing
    End Sub

    Protected Overrides Sub OnAttached()
        MyBase.OnAttached()
        AddHandler AssociatedObject.MouseLeftButtonDown, AddressOf AssociatedObjectOnMouseLeftButtonDown
        AddHandler AssociatedObject.MouseMove, AddressOf AssociatedObjectOnMouseMove
        AddHandler AssociatedObject.DragOver, AddressOf AssociatedObjectOnDragOver
        AddHandler AssociatedObject.Drop, AddressOf AssociatedObjectOnDrop
        AddHandler AssociatedObject.GiveFeedback, AddressOf AssociatedObjectOnGiveFeedback
    End Sub

    Protected Overrides Sub OnDetaching()
        MyBase.OnDetaching()
        RemoveHandler AssociatedObject.MouseLeftButtonDown, AddressOf AssociatedObjectOnMouseLeftButtonDown
        RemoveHandler AssociatedObject.MouseMove, AddressOf AssociatedObjectOnMouseMove
        RemoveHandler AssociatedObject.DragOver, AddressOf AssociatedObjectOnDragOver
        RemoveHandler AssociatedObject.Drop, AddressOf AssociatedObjectOnDrop
        RemoveHandler AssociatedObject.GiveFeedback, AddressOf AssociatedObjectOnGiveFeedback
    End Sub

    Private Sub AssociatedObjectOnMouseMove(sender As Object, e As MouseEventArgs)
        Dim control As ItemsControl = TryCast(sender, ItemsControl)

        If Not control Is Nothing Then
            Dim Item As FrameworkElement = CType(control.InputHitTest(e.GetPosition(control)), FrameworkElement)

            If Not Item Is Nothing AndAlso
                Not (Item.Name = "Rect" OrElse
                TypeOf Item Is Border OrElse
                TypeOf Item Is Grid OrElse
                TypeOf Item Is Path OrElse
                TypeOf Item Is Rectangle) Then

                Dim Text As TextBlock = TryCast(Item, TextBlock)

                If AssociatedObject.SelectedItem IsNot Nothing Then
                    If AssociatedObject.SelectedItem IsNot Nothing AndAlso e.LeftButton = MouseButtonState.Pressed Then
                        Me.isDragging = True
                        Dim _DragDrop As DragDropEffects = DragDropEffects.None
                        DragDrop.DoDragDrop(AssociatedObject, RuntimeHelpers.GetObjectValue(AssociatedObject.SelectedItem), DragDropEffects.Move)
                        Me.isDragging = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub AssociatedObjectOnGiveFeedback(sender As Object, e As GiveFeedbackEventArgs)
        If _DragDropEffects <> DragDropEffects.Move Then
            Mouse.SetCursor(Cursors.No)
        Else
            e.UseDefaultCursors = True
        End If
        e.Handled = True
    End Sub

    Private Function IsDragStart(position As Point) As Boolean
        Return Math.Abs(position.X - Me.startPosition.X) > SystemParameters.MinimumHorizontalDragDistance OrElse
               Math.Abs(position.Y - Me.startPosition.Y) > SystemParameters.MinimumVerticalDragDistance
    End Function

    Private Sub AssociatedObjectOnMouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        Me.startPosition = e.GetPosition(Nothing)
    End Sub

    Private Sub AssociatedObjectOnDragOver(sender As Object, e As DragEventArgs)
        Dim control As ItemsControl = TryCast(sender, ItemsControl)
        If control IsNot Nothing Then
            Dim MovedNode = e.Source
            Dim TargetNode = CType(control.InputHitTest(e.GetPosition(control)), FrameworkElement).DataContext
            'If TargetNode IsNot Nothing Then
            '    TargetNode.IsExpanded = True
            'End If
            control.InputHitTest(e.GetPosition(control)).Focus()
            Dim scrollViewer As ScrollViewer = GetFirstVisualChild(Of ScrollViewer)(control)

            If scrollViewer Isnot Nothing Then
                Dim tolerance As Double = 60.0
                Dim verticalPos As Double = e.GetPosition(control).Y
                Dim offset As Double = 20.0
                If verticalPos < tolerance Then
                    scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - offset)
                Else
                    If  verticalPos > control.ActualHeight - tolerance Then
                        scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + offset)
                    End If
                End If
                Dim Args As TreeViewDragDropEventArgs = New TreeViewDragDropEventArgs() With {.Source = MovedNode, .Target = TargetNode}
                If TargetNode IsNot Nothing AndAlso Me.Command IsNot Nothing AndAlso Me.Command.CanExecute(Args) Then
                    e.Effects = DragDropEffects.Move
                Else
                    e.Effects = DragDropEffects.None
                End If
                Me._DragDropEffects = e.Effects
                Me._Target = TargetNode
                Me._source = MovedNode
            End If
        End If
    End Sub

    Private Sub AssociatedObjectOnDrop(sender As Object, e As DragEventArgs)
        Try
            Dim control As ItemsControl = TryCast(sender, ItemsControl)
            If control IsNot Nothing Then
                Dim Source = e.Data '.Source.DataContext
                Dim Target = CType(control.InputHitTest(e.GetPosition(control)), FrameworkElement).DataContext
                Dim Args As TreeViewDragDropEventArgs = New TreeViewDragDropEventArgs() With {.Source = Source, .Target = Target}

                If Target IsNot Nothing AndAlso
                    Me.Command IsNot Nothing AndAlso
                    Me.Command.CanExecute(Args) Then
                    Me.Command.Execute(Args)
                End If
            End If
        Catch ex As Exception
            'Throw ex
        End Try
    End Sub

    Public Shared Function GetFirstVisualChild(Of T As DependencyObject)(depObj As DependencyObject) As T
        If depObj IsNot Nothing Then
            Dim num As Integer = VisualTreeHelper.GetChildrenCount(depObj) - 1
            For i As Integer = 0 To num
                Dim child As DependencyObject = VisualTreeHelper.GetChild(depObj, i)
                If child IsNot Nothing AndAlso TypeOf child Is T Then
                    Return CType((CObj(child)), T)
                End If
                Dim childItem As T = GetFirstVisualChild(Of T)(child)
                If childItem IsNot Nothing Then
                    Return childItem
                End If
            Next
        End If
        Return Nothing
    End Function
End Class
Public Class TreeViewDragDropEventArgs
    Inherits EventArgs
    Public Property Source() As Object
    Public Property Target() As Object
End Class