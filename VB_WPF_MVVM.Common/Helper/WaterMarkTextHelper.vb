Imports System.Windows
Imports System.Windows.Controls

Public Class WaterMarkTextHelper
    Inherits DependencyObject
#Region "Attached Properties"
    Public Shared Function GetIsMonitoring(ByVal obj As DependencyObject) As Boolean
        Return CBool(obj.GetValue(IsMonitoringProperty))
    End Function
    Public Shared Sub SetIsMonitoring(ByVal obj As DependencyObject, ByVal value As Boolean)
        obj.SetValue(IsMonitoringProperty, value)
    End Sub
    Public Shared ReadOnly IsMonitoringProperty As DependencyProperty = DependencyProperty.RegisterAttached("IsMonitoring", GetType(Boolean), GetType(WaterMarkTextHelper), New UIPropertyMetadata(False, AddressOf OnIsMonitoringChanged))

    Public Shared Function GetWatermarkText(ByVal obj As DependencyObject) As String
        Return obj.GetValue(WatermarkTextProperty)
    End Function
    Public Shared Sub SetWatermarkText(ByVal obj As DependencyObject, ByVal value As String)
        obj.SetValue(WatermarkTextProperty, value)
    End Sub
    Public Shared ReadOnly WatermarkTextProperty As DependencyProperty = DependencyProperty.RegisterAttached("WatermarkText", GetType(String), GetType(WaterMarkTextHelper), New UIPropertyMetadata(String.Empty))
    Public Shared Function GetTextLength(ByVal obj As DependencyObject) As Integer
        Return CInt(Fix(obj.GetValue(TextLengthProperty)))
    End Function
    Public Shared Sub SetTextLength(ByVal obj As DependencyObject, ByVal value As Integer)
        obj.SetValue(TextLengthProperty, value)

        If value >= 1 Then
            obj.SetValue(HasTextProperty, True)
        Else
            obj.SetValue(HasTextProperty, False)
        End If
    End Sub
    Public Shared ReadOnly TextLengthProperty As DependencyProperty = DependencyProperty.RegisterAttached("TextLength", GetType(Integer), GetType(WaterMarkTextHelper), New UIPropertyMetadata(0))

    Public Shared ReadOnly IconProperty As DependencyProperty = DependencyProperty.RegisterAttached("Icon", GetType(String), GetType(WaterMarkTextHelper), New UIPropertyMetadata(String.Empty))
    Public Shared Function GetIcon(ByVal obj As DependencyObject) As String
        Return (obj.GetValue(IconProperty))
    End Function
    Public Shared Sub SetIcon(ByVal obj As DependencyObject, ByVal value As String)
        obj.SetValue(IconProperty, value)
    End Sub

#End Region
#Region "Internal DependencyProperty"
    Public Property HasText() As Boolean
        Get
            Return CBool(GetValue(HasTextProperty))
        End Get
        Set(ByVal value As Boolean)
            SetValue(HasTextProperty, value)
        End Set
    End Property
    Private Shared ReadOnly HasTextProperty As DependencyProperty = DependencyProperty.RegisterAttached("HasText", GetType(Boolean), GetType(WaterMarkTextHelper), New FrameworkPropertyMetadata(False))

#End Region
#Region "Implementation"
    Private Shared Sub OnIsMonitoringChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        If TypeOf d Is TextBox Then
            Dim txtBox As TextBox = TryCast(d, TextBox)

            If CBool(e.NewValue) Then
                AddHandler txtBox.TextChanged, AddressOf TextChanged
            Else
                RemoveHandler txtBox.TextChanged, AddressOf TextChanged
            End If
        ElseIf TypeOf d Is PasswordBox Then
            Dim passBox As PasswordBox = TryCast(d, PasswordBox)

            If CBool(e.NewValue) Then
                AddHandler passBox.PasswordChanged, AddressOf PasswordChanged
            Else
                RemoveHandler passBox.PasswordChanged, AddressOf PasswordChanged
            End If
        End If
    End Sub
    Private Shared Sub TextChanged(ByVal sender As Object, ByVal e As TextChangedEventArgs)
        Dim txtBox As TextBox = TryCast(sender, TextBox)
        If txtBox Is Nothing Then
            Return
        End If
        SetTextLength(txtBox, txtBox.Text.Length)
    End Sub
    Private Shared Sub PasswordChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim passBox As PasswordBox = TryCast(sender, PasswordBox)
        If passBox Is Nothing Then
            Return
        End If
        SetTextLength(passBox, passBox.Password.Length)
    End Sub

#End Region
End Class