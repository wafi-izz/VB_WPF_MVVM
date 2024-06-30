Public Class DataException
    Inherits Exception
    Public Sub New(Message As String)
        MyBase.New(Message)

    End Sub
    Public Sub New(Message As String, ErrorNumber As Integer)
        MyBase.New(Message)
        Me.ErrorNumber = ErrorNumber
    End Sub
    Public Sub New(ErrorNumber As Integer)
        MyBase.New($"Error Number {ErrorNumber} Is Occured.")
        Me.ErrorNumber = ErrorNumber
    End Sub
    Public Property ErrorNumber As Integer
    Public Shadows Property Source As Object
    Public Property Tag As Object
    Public Property TableName As String
End Class