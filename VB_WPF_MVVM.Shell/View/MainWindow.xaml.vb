Imports System.Windows.Threading
Imports VB_WPF_MVVM.Localization.My.Resources
Imports VB_WPF_MVVM.Module

Public Class MainWindow
    Dim m = 0
    Dim mouse_move = False
    Dim mouse_down = False
    Dim doubleclick = False
    Dim slowmo As DispatcherTimer
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight

        WindowStyle = WindowStyle.SingleBorderWindow
        WindowState = WindowState.Maximized
        WindowStyle = WindowStyle.None
        '
        Dim dispatcherTimer As DispatcherTimer = New DispatcherTimer
        AddHandler dispatcherTimer.Tick, AddressOf Me.checker
        dispatcherTimer.Interval = New TimeSpan(0, 0, 0.1)
        dispatcherTimer.Start()


        slowmo = New DispatcherTimer
        AddHandler slowmo.Tick, AddressOf Me.slow
        slowmo.Interval = New TimeSpan(0, 0, 0.1)



    End Sub
    Private Sub Window_Activated(sender As Object, e As EventArgs)


        If m = 1 Then
            'maxmin_Click()
        End If
    End Sub

    Private Sub checker()
        Try
            If doubleclick = True Then
                doubleclick = False
                mouse_down = False
                Exit Sub
            End If

            If mouse_down Then
                If CalculateDiffrance() = "yes" Then
                    If WindowState = WindowState.Maximized Then
                        WindowState = WindowState.Normal
                        Top = 0
                        Left = MouseXPos - (Me.Width / 2)
                    End If
                    Me.DragMove()
                    mouse_down = False
                    check_size()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub slow()
        Height = Height + 1
        Width = Width + 1
    End Sub


    Public Sub check_size()
        If WindowState = WindowState.Normal Then
        Else
            WindowState = WindowState.Maximized
        End If
    End Sub


    Dim MouseXPos = -1
    Dim MouseYPos = -1
    Private Sub Border_MouseDown()
        Try
            MouseXPos = PointToScreen(Mouse.GetPosition(Application.Current.MainWindow)).X
            MouseYPos = PointToScreen(Mouse.GetPosition(Application.Current.MainWindow)).Y
            mouse_down = True

        Catch ex As Exception
            'MsgBox("a")
        End Try
    End Sub


    Private Sub Border_MouseUp()
        mouse_down = False
    End Sub

    Public Function CalculateDiffrance()
        Dim ThisMouseXPos = PointToScreen(Mouse.GetPosition(Application.Current.MainWindow)).X
        Dim ThisMouseYPos = PointToScreen(Mouse.GetPosition(Application.Current.MainWindow)).Y
        If MouseXPos <> -1 Then
            If Math.Abs(ThisMouseXPos - MouseXPos) > 2 Or Math.Abs(ThisMouseYPos - MouseYPos) > 2 Then
                Return "yes"
            Else
                Return "no"
            End If
        Else
            Return "no"
        End If
    End Function

    Private Sub Border_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            doubleclick = True
            If WindowState = WindowState.Normal Then
                WindowState = WindowState.Maximized
            Else
                WindowState = WindowState.Normal
            End If
        End If
    End Sub

    Private Sub close_Click(sender As Object, e As RoutedEventArgs)
        Dim yesno As New Message("Close this Application ", "Confirmation", MsgBoxStyle.YesNo, MessageBoxImage.Information)
        If yesno.DialogResult = True Then
            Application.Current.Shutdown()
        ElseIf yesno.DialogResult = False Then

        End If
    End Sub

    Private Sub maxmin_Click()
        If WindowState = WindowState.Normal Then
            WindowState = WindowState.Maximized
            'slowmo.Start()
        Else
            WindowState = WindowState.Normal
        End If
    End Sub

    Private Sub minimize_Click(sender As Object, e As RoutedEventArgs)
        m = 1
        WindowState = WindowState.Minimized
    End Sub
End Class
