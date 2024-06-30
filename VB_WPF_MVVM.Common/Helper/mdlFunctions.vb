Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Public Module mdlFunctions
    Public Enum ActionEnums
        Add
        Edit
        Delete
        Confirm
        UnConfirm
        Close
    End Enum
    Public Enum StateEnums
        Added
        Changed
        Deleted
        UnChanged
    End Enum
    Friend Enum MonitorDpiType
        EffectiveDpi = 0
        AngularDpi = 1
        RawDpi = 2
        [Default] = EffectiveDpi
    End Enum
    Public Enum ProcessDpiAwareness
        DpiUnaware = 0
        SystemDpiAware = 1
        PerMonitorDpiAware = 2
    End Enum
    Public Function IsIn(ByVal Value As Object, ByVal ParamArray SourceValues() As Object) As Boolean
        Return SourceValues.Contains(Value)
    End Function
    Public Function IsNull(ByVal Value As Object, ByVal AlternativeValue As Object)
        If Value Is Nothing OrElse
           Value Is DBNull.Value OrElse
           Value.Equals(String.Empty) Then
            Return AlternativeValue
        Else
            Return Value
        End If
    End Function
    Public Function IsValue(ByVal Value As Object, ByVal Value2 As Object, ByVal AlternativeValue As Object) As Object
        If Value.Equals(Value2) Then
            Return AlternativeValue
        Else
            Return Value
        End If
    End Function
    <System.Runtime.CompilerServices.Extension()>
    Public Function Left(ByVal Value As String, ByVal Length As Integer) As String
        If String.IsNullOrEmpty(Value) Then
            Return Value
        End If
        Return String.Join("", Value.Take(Length))
    End Function
    'RegionManager.RequestNavigate("MessageRegion", "AccaidDialog")
    Public Enum IconTypeEnum
        None
        Bussy
        Critical
        Execlamation
        Information
        Question
        Err
        UseCustomIcon
        Save
    End Enum
    Public Sub SendKey(ByVal key As Key)
        Dim args As New KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
        args.RoutedEvent = Keyboard.KeyDownEvent
        InputManager.Current.ProcessInput(args)
    End Sub
    Public Function GetMD5Code(Code As String) As String
        Dim Bytes() As Byte
        Dim sb As New StringBuilder
        Bytes = Encoding.Default.GetBytes("24-09-2019|" & Code & "|HMS_SYS")
        Bytes = MD5.Create().ComputeHash(Bytes)
        For x As Integer = 0 To Bytes.Length - 1
            sb.Append(Bytes(x).ToString("x2"))
        Next
        Return sb.ToString()
    End Function
End Module
