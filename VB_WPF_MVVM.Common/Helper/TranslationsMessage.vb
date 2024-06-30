Imports Newtonsoft.Json
Public Class TranslationsMessage
    Public Shared Function ConvertToMessage(Source As String) As TranslationsMessage
        Dim obj As TranslationsMessage
        obj = JsonConvert.DeserializeObject(Of TranslationsMessage)(Source)
        Return obj
    End Function
    Public Shared Function ConvertToMessage(Source As String, ParamArray vParamArray() As Object) As TranslationsMessage
        Dim obj As TranslationsMessage
        obj = JsonConvert.DeserializeObject(Of TranslationsMessage)(Source)
        obj.Title = String.Format(obj.Title, vParamArray)
        obj.Message = String.Format(obj.Message, vParamArray)
        Return obj
    End Function
    Public Property Title As String
    Public Property Message As String
End Class
