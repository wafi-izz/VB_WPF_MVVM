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
            Dim ConnPath As String '= IO.Path.Combine(My.Application.Info.DirectoryPath, "ConnectionStrings.csf")
            Dim _LoadMasterConnection As New Almagal.DataBaseManager.DataConnectionStringManager(ConnPath, "AccaidDB")
            If _LoadMasterConnection Is Nothing Then
                Throw New ArgumentNullException("Connection Name", $"Connection Name SchoolSystemDB is not exists")
            End If

            Return _LoadMasterConnection
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
