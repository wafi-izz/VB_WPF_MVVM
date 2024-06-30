Imports System
Imports System.ComponentModel
Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices
'Public MustInherit Class BindableBase
'    Implements INotifyPropertyChanged

'    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
'    Protected Overridable Function SetProperty(Of T)(ByRef storage As T, ByVal value As T, <CallerMemberName> Optional propertyName As String = Nothing) As Boolean
'        If Equals(storage, value) Then
'            Return False
'        End If
'        storage = value
'        RaisePropertyChanged(propertyName)
'        Return True
'    End Function
'    Protected Overridable Function SetProperty(Of T)(ByRef storage As T, ByVal value As T, ByVal onChanged As Action, <CallerMemberName> Optional propertyName As String = Nothing) As Boolean
'        If Equals(storage, value) Then
'            Return False
'        End If
'        storage = value
'        onChanged?.Invoke()
'        RaisePropertyChanged(propertyName)
'        Return True
'    End Function


'    Protected Sub RaisePropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
'        OnPropertyChanged(propertyName)
'        'PropertyChanged?.Invoke(Me,)
'    End Sub
'    Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)

'        Dim handler As PropertyChangedEventHandler = PropertyChangedEvent
'        If handler IsNot Nothing Then
'            handler(Me, New PropertyChangedEventArgs(propertyName))
'        End If
'    End Sub
'    'Protected Overridable Sub OnPropertyChanged(ByVal args As PropertyChangedEventArgs)
'    '    PropertyChangedEvent?.Invoke(Me, args)
'    'End Sub

'End Class
Public MustInherit Class BindableBase
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub OnPropertyChanged(<CallerMemberName> Optional ByVal propertyName As String = Nothing)
        PropertyChangedEvent?.Invoke(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Protected Function SetProperty(Of T)(ByRef storage As T, ByVal value As T, <CallerMemberName> Optional ByVal propertyName As String = Nothing) As Boolean
        If Object.Equals(storage, value) Then
            Return False
        End If
        storage = value
        OnPropertyChanged(propertyName)
        Return True
    End Function
    Protected Sub RaisePropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
        OnPropertyChanged(propertyName)
        'PropertyChanged?.Invoke(Me,)
    End Sub
End Class