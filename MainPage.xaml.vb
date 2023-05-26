
Imports Windows.UI.Xaml.Controls
Imports System.Diagnostics
Imports Windows.UI.Xaml
Imports System.Threading.Tasks
Imports Windows.Storage
Imports Windows.Storage.Streams

Public NotInheritable Class MainPage
    Inherits Page
    Private player As New MediaElement()
    Private timer As DispatcherTimer
    Private isStart As Boolean = False

    Public Sub New()
        InitializeComponent()
        InitializeTimer()
    End Sub

    Private Sub InitializeTimer()
        timer = New DispatcherTimer()
        timer.Interval = TimeSpan.FromSeconds(1)
        AddHandler timer.Tick, AddressOf Timer_Tick
        timer.Start()
    End Sub

    Private Async Sub Timer_Tick(sender As Object, e As Object)
        Dim currentTime As DateTime = DateTime.Now

        timeTextBlock.Text = currentTime.ToString("hh:mm tt")

        Dim currentTimeSpan As TimeSpan = currentTime.TimeOfDay
        Dim targetTimeSpan As TimeSpan = tp1.Time

        If currentTimeSpan.Hours = targetTimeSpan.Hours AndAlso currentTimeSpan.Minutes = targetTimeSpan.Minutes AndAlso isStart = False Then
            isStart = True
            Dim musicFileName As String = "Assets\t1.mp3"
            Dim folder As StorageFolder = Windows.ApplicationModel.Package.Current.InstalledLocation
            Dim soundFile As StorageFile = Await folder.GetFileAsync(musicFileName)
            Dim soundStream As IRandomAccessStream = Await soundFile.OpenAsync(FileAccessMode.Read)
            player.SetSource(soundStream, soundFile.ContentType)
            player.Play()
        End If
    End Sub


    Private Async Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim musicFileName As String = "Assets\t1.mp3"
        Dim folder As StorageFolder = Windows.ApplicationModel.Package.Current.InstalledLocation
        Dim soundFile As StorageFile = Await folder.GetFileAsync(musicFileName)
        Dim soundStream As IRandomAccessStream = Await soundFile.OpenAsync(FileAccessMode.Read)

        player.SetSource(soundStream, soundFile.ContentType)
        'player.Play()

    End Sub

    Private Sub btnSetTime_Click(sender As Object, e As RoutedEventArgs) Handles btnSetTime.Click

        Dim selectedTime As TimeSpan = tp1.Time
        Dim formattedTime As String = FormatTime(selectedTime)
        tb2.Text = formattedTime

    End Sub



    Private Function FormatTime(time As TimeSpan) As String

        Dim formattedTime As String = ""
        Dim hour As Integer = time.Hours
        Dim minute As Integer = time.Minutes

        If hour >= 12 Then
            formattedTime = $"0{hour Mod 12}:{minute:D2} PM"
        Else
            If hour = 0 Then
                formattedTime = $"12:{minute:D2} AM"
            Else
                formattedTime = $"0{hour}:{minute:D2} AM"
            End If
        End If

        Return formattedTime
    End Function

End Class