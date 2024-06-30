Public Class VB_WPF_MVVMObjectModel
    Public Property ID As Integer?
    Public Property ParentID As Integer?
    Public Property ObjectNo As Integer
    Public Property ObjectKey As Integer
    Public Property ObjectName As String
    Public Property ObjectDescription As String
    Public Property StatusID As Integer?
    Public Property Path As String
End Class
Public Class VB_WPF_MVVMCountModel
    Public Property ObjectKey As Integer
    Public Property VB_WPF_MVVMName As String
    Public Property VB_WPF_MVVMCount As Integer
End Class