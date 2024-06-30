Imports Prism.Events

Public Class ActionExecutedEvent
    Inherits PubSubEvent(Of ActionExecutedEventArg)
End Class

Public Class ActionExecutedEventArg
    Public Property Action() As ObjectTypeActions
    Public Property ObjectType() As ObjectTypes
    Public Property ObjectID() As Object
    Public Property vObject() As Object

    Public Enum ObjectTypeActions
        Create
        Update
        Delete
        Recieve
        Fill
    End Enum
    Public Enum ObjectTypes
        Bathes
        Ranks
        Substance
        Retirement
    End Enum


End Class

