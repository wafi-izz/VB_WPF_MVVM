Imports System.Reflection
Imports Almagal.DataBaseManager

Public Class ConnectionManager
    Private Shared _MasterConnection As DataConnectionStringManager
    Public Shared Property MasterConnection() As DataConnectionStringManager
        Get
            _MasterConnection = (If(_MasterConnection, LoadMasterConnection()))
            Return _MasterConnection
        End Get
        Set(value As DataConnectionStringManager)
            _MasterConnection = value
        End Set
    End Property
    Private Shared Function LoadMasterConnection() As DataConnectionStringManager
        Try
            Dim ConnPath As String = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VB_WPF_MVVM_DB.csf") ' New System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath & "Agri-DB.csf" 'IO.Path.Combine(  '(My .Info.DirectoryPath, "Agri-DB.dat")
            Dim _LoadMasterConnection As New DataConnectionStringManager(ConnPath, "VB_WPF_MVVM_DB")
            If _LoadMasterConnection Is Nothing Then
                Throw New ArgumentNullException("Connection Name", "Connection Name VB_WPF_MVVM_DB is not exists")
            End If
            Return _LoadMasterConnection
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
