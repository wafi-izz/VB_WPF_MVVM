Imports System.Windows.Threading
Imports Microsoft.VisualBasic
Imports VB_WPF_MVVM.Localization
Public Class Message

    Dim slowmo As DispatcherTimer
    Private Sub Window_Activated(sender As Object, e As EventArgs)


        slowmo = New DispatcherTimer
        AddHandler slowmo.Tick, AddressOf Me.slow
        slowmo.Interval = New TimeSpan(0, 0, 0.2)
        slowmo.Start()
    End Sub


    Dim e As Double = 0
    Private Message As String
    Private Title As String
    Private MsgStyle As MsgBoxStyle
    Private MessageBoxImage As MessageBoxImage

    Public Sub New(ParamMessage As String, ParamTitle As String, ParamMsgStyle As MsgBoxStyle, MessageBoxImage As MessageBoxImage)
        InitializeComponent()
        Me.Message = ParamMessage

        If MessageBoxImage = MessageBoxImage.Information And ParamMsgStyle <> MsgBoxStyle.YesNo Then
            Me.Title = Translations.Success
        ElseIf MessageBoxImage = MessageBoxImage.Error And ParamMsgStyle <> MsgBoxStyle.YesNo Then
            Me.Title = Translations._Error
        Else
            Me.Title = ParamTitle
        End If
        If ParamMsgStyle = MsgBoxStyle.YesNo Then
            Me.Title = Translations.ConfirmChoice
        End If
        ' ParamTitle
        Height = Height + ((Message.Length / 30) * 18)
        Me.MsgStyle = ParamMsgStyle
        Me.MessageBoxImage = MessageBoxImage
        '
        Messagelbl.Text = Message
        Titlelbl.Text = Title
        If MsgStyle = MsgBoxStyle.YesNo Then
            ChoiceAction.Visibility = Visibility.Visible
            IconText.Foreground = New BrushConverter().ConvertFrom("#FF2778BB")
            IconText.Kind = MaterialDesignThemes.Wpf.PackIconKind.QuestionMark
            IconText.FontSize = 50
            IconText.FontWeight = FontWeights.ExtraBold
        Else
            MessageAction.Visibility = Visibility.Visible
            If MessageBoxImage = MessageBoxImage.Information Then
                IconText.Foreground = New BrushConverter().ConvertFrom("#FF3CAEA3")
                IconText.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check
            ElseIf MessageBoxImage = MessageBoxImage.Warning Then
                IconText.Foreground = New BrushConverter().ConvertFrom("#FFF6D55C")
                IconText.Kind = MaterialDesignThemes.Wpf.PackIconKind.AlertCircle
            ElseIf MessageBoxImage = MessageBoxImage.Error Then
                IconText.Foreground = New BrushConverter().ConvertFrom("#FFE0144C")
                IconText.Kind = MaterialDesignThemes.Wpf.PackIconKind.Alert
            Else
                IconText.Foreground = New BrushConverter().ConvertFrom("#FFEA047E")
                IconText.Kind = MaterialDesignThemes.Wpf.PackIconKind.AlertDecagram
            End If
        End If
        ShowDialog()
    End Sub

    Public Sub slow()
        e += 0.01
        'poss.Text = e
        If e > 1 Then
            slowmo.Stop()
        End If
        box.Opacity = e
        IconText.Opacity = e
        Titlelbl.Opacity = e
    End Sub







    Private Sub closeWindow_MouseUp()
        Me.Close()
    End Sub



    Private Sub no_Click(sender As Object, e As RoutedEventArgs)
        DialogResult = False
        Me.Close()
    End Sub

    Private Sub yes_Click(sender As Object, e As RoutedEventArgs)
        DialogResult = True
        Me.Close()
    End Sub

    Private Sub Window_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = 13 Or e.Key = 6 Then
            If MsgStyle <> MessageBoxButton.YesNo Then
                closeWindow_MouseUp()
            End If
        End If
    End Sub
End Class

