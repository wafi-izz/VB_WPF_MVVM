Imports System.Collections.ObjectModel
Imports Prism.Regions
Imports VB_WPF_MVVM.Localization.My.Resources
Imports System.Globalization
Imports System.Threading
Public Class LoginViewModel
    Inherits BaseViewModel(Of ViewStateEnum)
    Public Sub New()
        LangueList = New ObservableCollection(Of LanguageModel)
        LangueList.Add(New LanguageModel() With {.LanguageID = 0, .LanguageName = "عربي", .LanguageAB = "ar"})
        LangueList.Add(New LanguageModel() With {.LanguageID = 1, .LanguageName = "English", .LanguageAB = "en"})
        IsDataLoaded = False
        SelectedLangue = LangueList.FirstOrDefault(Function(x) x.LanguageID = My.Settings.LanguageID)
        Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo(SelectedLangue.LanguageAB)
        Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo(SelectedLangue.LanguageAB)
        IsDataLoaded = True
    End Sub
    Public Enum ViewStateEnum
        Normal
        Busy
        Err
    End Enum
#Region "Properties"
    Public Property LangueList As ObservableCollection(Of LanguageModel)
    Public Property SelectedLangue As LanguageModel
#End Region
#Region "Initilizaton"
    Public Overrides Async Sub OnNavigatedTo(navigationContext As NavigationContext)
        Try
            MyBase.OnNavigatedTo(navigationContext)
            Await GetUserList()
        Catch ex As Exception
            Dim Ms As New Message(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub
#End Region
#Region "Functions"
    Public Sub OnSelectedLangueChanged()
        If IsDataLoaded AndAlso SelectedLangue IsNot Nothing Then
            My.Settings.LanguageID = SelectedLangue.LanguageID
            My.Settings.Save()
            Translation.Culture = New Globalization.CultureInfo(SelectedLangue.LanguageAB)
            CultureInfo.CurrentCulture = New Globalization.CultureInfo(SelectedLangue.LanguageAB)
            Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo(SelectedLangue.LanguageAB)
            Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo(SelectedLangue.LanguageAB)

            RegionManager.Regions("MainWindowRegion").RemoveAll()
            RegionManager.RequestNavigate("MainWindowRegion", NameOf(LoginView))
        End If
    End Sub
    Public Async Function GetUserList() As Task
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub Isvalid(UserNo As Integer, Password As String)
        Try
            If UserNo = Nothing Then
                Throw New ArgumentException("Choose User Name from The List")
            End If
            If Password = "" Then
                Throw New ArgumentException("Insert Your Password")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Async Function LoginAsync(Password As PasswordBox) As Task
        Try
            ' i had to call this twice to work. please let me know if you have a solution
            OnSelectedLangueChanged()
            OnSelectedLangueChanged()

            'you can extent these classes to save you user id ot any data you might need across the system
            Dim UserSettings As AddToUserSettings = New AddToUserSettings
            UserSettings.Add(SelectedLangue.LanguageID)

            Dim ShellAddToUserSettings As ShellAddToUserSettings = New ShellAddToUserSettings
            ShellAddToUserSettings.Add(SelectedLangue.LanguageID)

            Dim APIAddToUserSettings As APIAddToUserSettings = New APIAddToUserSettings
            APIAddToUserSettings.Add(SelectedLangue.LanguageID)

            RegionManager.Regions("MainWindowRegion").RemoveAll()
            RegionManager.RequestNavigate("MainWindowRegion", NameOf(MainView))

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "Commands"
    Public Property LoginCommand As New RelayCommand(Async Sub(Password As PasswordBox)
                                                         Try
                                                             Await LoginAsync(Password)
                                                         Catch ex As Exception
                                                             Dim Ms As New Message(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error)
                                                         End Try
                                                     End Sub)
#End Region
End Class
Public Class LanguageModel
    Public Property LanguageID As Integer
    Public Property LanguageName As String
    Public Property LanguageAB As String
End Class