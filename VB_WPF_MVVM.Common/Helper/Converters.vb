Imports System.Globalization
Imports System.Windows.Data

Public Class Converters
End Class
Public Class IsNullConverter
    Implements IValueConverter
    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
        Return value Is Nothing
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New InvalidOperationException("IsNullConverter can only be used OneWay.")
    End Function
End Class

