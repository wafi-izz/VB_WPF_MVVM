Imports System.ComponentModel.DataAnnotations

<AttributeUsage(AttributeTargets.[Property] Or AttributeTargets.Field, AllowMultiple:=False)>
Public NotInheritable Class NotStoreProcedureParameterAttribute
    Inherits ValidationAttribute
    Public Overrides Function IsValid(
    ByVal value As Object) As Boolean
        Return True
    End Function

    Public Overrides Function FormatErrorMessage(
    ByVal name As String) As String
        Return ""
    End Function
End Class