Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Reflection
Imports System.Windows.Controls

Public Class BaseModel
    Inherits BindableBase
    'Implements INotifyPropertyChanged
    Implements INotifyDataErrorInfo

    'Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event ErrorsChanged As EventHandler(Of DataErrorsChangedEventArgs) Implements INotifyDataErrorInfo.ErrorsChanged
    Private _Lock As Object = New Object
    Public Sub NotifyPropertyChanged(ByVal info As String)
        Try
            If Me.InitializingData = False AndAlso (
                      Not info.Equals(NameOf(InitializingData)) AndAlso
                      Not info.Equals(NameOf(IsDataChanged)) AndAlso
                      Not info.Equals(NameOf(IsDataLoading))) Then
                Me.IsDataChanged = True
            End If
            If IsDataLoading = False Then
                RaisePropertyChanged(info)
                'RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
            End If
            ValidateProperty(info)
        Catch ex As Exception
            Dim i = ex
            Throw ex
        End Try
    End Sub
    'Property _RegionManager As RegionManager
    'Public Property RegionManager As RegionManager
    '    Get
    '        Dim Container As IContainerProvider = CType(PrismApplication.Current, PrismApplicationBase).Container
    '        _RegionManager = Container.Resolve(Of IRegionManager)
    '        Return _RegionManager
    '    End Get
    '    Set(value As RegionManager)
    '        _RegionManager = value
    '    End Set
    'End Property
    Public Property IsDataChanged As Boolean
    Public Property IsDataLoaded As Boolean
    Private _IsDataLoading As Boolean
    Public Property IsDataLoading As Boolean
        Get
            Return _IsDataLoading
        End Get
        Protected Friend Set(value As Boolean)
            _IsDataLoading = value
        End Set
    End Property
    Public Property InitializingData As Boolean
    Public Sub BeginInitializeData()
        Me.InitializingData = True
    End Sub
    Public Sub EndInitializeData()
        Me.InitializingData = False
    End Sub
    Public Errors As New Dictionary(Of String, List(Of String))
    Public ReadOnly Property HasErrors As Boolean Implements INotifyDataErrorInfo.HasErrors
        Get
            Return Errors.Any(Function(e) e.Value IsNot Nothing AndAlso e.Value.Count > 0)
        End Get
    End Property
    Public Overridable Function GetErrors(propertyName As String) As IEnumerable Implements INotifyDataErrorInfo.GetErrors
        If Not String.IsNullOrEmpty(propertyName) Then
            If Errors.ContainsKey(propertyName) AndAlso
                Errors(propertyName) IsNot Nothing AndAlso
                Errors(propertyName).Count > 0 Then
                Return Errors(propertyName).ToList
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function
    Public Sub OnErrorsChanged(propertyName As String)
        RaiseEvent ErrorsChanged(Me, New DataErrorsChangedEventArgs(propertyName))
    End Sub
    Public Sub ValidateProperty(propertyName As String)
        SyncLock _Lock
            If propertyName = "Icon" Then
                Dim m = ""
            End If

            Dim propertyInfo As System.Reflection.PropertyInfo = Me.GetType().GetProperty(propertyName)
            If propertyInfo Is Nothing Then
                Return
            End If
            Dim results = New List(Of DataAnnotations.ValidationResult)()

            If propertyInfo.CanWrite Then
                Dim Value As Object = Nothing
                Try
                    Value = propertyInfo.GetValue(Me, Nothing)
                Catch ex As Exception

                End Try
                Dim result = Validator.TryValidateProperty(Value,
                                                           New ValidationContext(Me, Nothing, Nothing) With {.MemberName = propertyName},
                                                           results)
                If Errors.ContainsKey(propertyName) Then
                    Errors.Remove(propertyName)
                End If
                If result = False Then
                    Dim validationResults = results.Select(Function(e) e.ErrorMessage).ToList
                    Errors.Add(propertyName, validationResults)
                Else
                    Dim cv = CustomeValidation(propertyName)
                    If cv IsNot Nothing AndAlso cv.Count > 0 Then
                        Errors.Add(propertyName, cv)
                    End If
                End If
                OnErrorsChanged(propertyName)
            End If
        End SyncLock
    End Sub
    Public Overridable Function CustomeValidation(columnName As String) As List(Of String)
        Return Nothing
    End Function
End Class
