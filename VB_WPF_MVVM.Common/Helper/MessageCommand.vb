Imports Prism.Commands

Public Class MessageCommand
    Inherits DelegateCommand
#Region "Ctors"
    Public Sub New(ByVal Text As String)
        MyBase.New(New Action(Sub()

                              End Sub))
        Me.Text = Text
    End Sub
    Public Sub New(ByVal Text As String, ByVal CommandType As CommandTypeEnum)
        MyBase.New(New Action(Sub()

                              End Sub))
        Me.Text = Text
        Me.CommandType = CommandType
    End Sub

    Public Sub New(ByVal Text As String, ByVal execute As Action)
        MyBase.New(execute)
        Me.Text = Text
    End Sub
    Public Sub New(ByVal Text As String, ByVal execute As Action, ByVal CommandType As CommandTypeEnum)
        MyBase.New(execute)
        Me.Text = Text
        Me.CommandType = CommandType
    End Sub

    Public Sub New(ByVal Text As String, ByVal execute As Action, ByVal canExecute As Func(Of Boolean))
        MyBase.New(execute, canExecute)
        Me.Text = Text
    End Sub
    Public Sub New(ByVal Text As String, ByVal execute As Action, ByVal canExecute As Func(Of Boolean), ByVal CommandType As CommandTypeEnum)
        MyBase.New(execute, canExecute)
        Me.Text = Text
        Me.CommandType = CommandType
    End Sub
#End Region
#Region "Enums"
    Public Enum CommandTypeEnum
        Defualt
        Primary
        Destructive
    End Enum
#End Region
#Region "Fields"
    Private privateText As String
    Public Property Text() As String
        Get
            Return privateText
        End Get
        Set(ByVal value As String)
            privateText = value
        End Set
    End Property
    Private privateCommandType As CommandTypeEnum
    Public Property CommandType() As CommandTypeEnum
        Get
            Return privateCommandType
        End Get
        Set(ByVal value As CommandTypeEnum)
            privateCommandType = value
        End Set
    End Property
#End Region
End Class