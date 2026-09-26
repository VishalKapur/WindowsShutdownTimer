using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WindowsShutdownTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool manual = true; // True if manual mode, false if auto mode

        private DispatcherTimer timer = new DispatcherTimer();  // Timer for countdown
        private int seconds;    // Seconds left on timer

        private FileSystemWatcher watcher;
        private DispatcherTimer idleTimer = new DispatcherTimer();  // Timer for folder tracking
        private DateTime lastActivityTime;
        private string selectedPath;
        private int idleMinutes = 10;   // How long folder/disk is idle until shutdown
        private bool shutdownPending = false;

        private PerformanceCounter diskWriteCounter = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");
        private float diskWriteThreshold = 5_000_000f;  // Speed to ignore idle activity (Default 5,000,000 Bytes/s = 5 MB/s) 

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);   // Timer counts down each second
            timer.Tick += Timer_Tick;   // Run Timer_Tick() every interval

            idleTimer.Interval = TimeSpan.FromSeconds(1);  // Check every second
            idleTimer.Tick += IdleTimer_Tick;
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if (manual)    // Manual mode
            {
                if (int.TryParse(HoursTextBox.Text, out int hours) && int.TryParse(MinutesTextBox.Text, out int minutes))
                {
                    if (hours >= 0 && minutes >= 0)
                        CreateCountdown(hours, minutes);
                    else
                        MessageBox.Show("Please enter a positive whole number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Please enter a valid number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);    // Shows error if either box does not contain a number
            }
            else    // Auto mode
            {
                if (int.TryParse(ThresholdTextBox.Text, out idleMinutes) && float.TryParse(SpeedThresholdTextBox.Text, out diskWriteThreshold))
                {
                    if (idleMinutes > 0 && diskWriteThreshold > 0)
                    {
                        diskWriteThreshold *= 1000000f; // Convert MB/s to Bytes/s

                        if (selectedPath != null && selectedPath != "")
                        {
                            watcher = new FileSystemWatcher(selectedPath);
                            watcher.IncludeSubdirectories = true;
                            watcher.EnableRaisingEvents = true;
                            lastActivityTime = DateTime.Now;

                            // Calls OnFolderActibity() when file in selected folder (& subfolders) is created, changed, renamed and deleted
                            watcher.Created += OnFolderActivity;
                            watcher.Changed += OnFolderActivity;
                            watcher.Renamed += OnFolderActivity;
                            watcher.Deleted += OnFolderActivity;

                            idleTimer.Start();

                            StatusText.Text = "Status: Monitoring Folder . . .";
                        }
                        else
                            MessageBox.Show("Please select a valid folder", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("Invalid threshold", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Please enter a valid whole number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            if (manual)
            {
                timer.Stop();
                CountdownText.Text = TimeSpan.FromSeconds(0).ToString();
            }
            else
            {
                idleTimer.Stop();
                StatusText.Text = "Status: Cancelled";
                

                if (watcher != null)
                {
                    watcher.EnableRaisingEvents = false;
                    watcher.Dispose();
                }

                if (shutdownPending)
                {
                    Process.Start(new ProcessStartInfo  // Cancel pending shutdown
                    {
                        FileName = "shutdown",
                        Arguments = "/a",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                    shutdownPending = false;
                }
            }
        }

        private void CreateCountdown(int hours, int minutes)
        {
            seconds = (hours * 3600) + (minutes * 60);
            CountdownText.Text = TimeSpan.FromSeconds(seconds).ToString();
            timer.Stop();   // Stops any existing timers
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (seconds > 0)
            {
                seconds--;
                CountdownText.Text = TimeSpan.FromSeconds(seconds).ToString();
            }
            else
            {
                timer.Stop();
                Process.Start(new ProcessStartInfo  // Immediately shutdown device
                {
                    FileName = "shutdown",
                    Arguments = "/s /t 0",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });   
            }
        }

        private void IdleTimer_Tick(object? sender, EventArgs e)
        {
            float currentSpeed = diskWriteCounter.NextValue(); // Current disk write speed in bytes/s

            if (currentSpeed >= diskWriteThreshold) // Ignore disk speed under threshold so that idle activity is not counted
                lastActivityTime = DateTime.Now;

            var idleTime = DateTime.Now - lastActivityTime; // Time since last activity
            StatusText.Text = $"Status: Disk Speed: {(currentSpeed / 1024) / 1024:F2} MB/s. Timer: {idleTime.ToString(@"hh\:mm\:ss")}";

            if (idleTime.TotalMinutes >= idleMinutes)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                idleTimer.Stop();
                Process.Start(new ProcessStartInfo  // Shutdown with 60s delay to to show warning
                {
                    FileName = "shutdown",
                    Arguments = "/s /t 60",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });  
                shutdownPending = true;
                StatusText.Text = "Status: Folder inactive - Shutdown in <60s . . .";
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;

            var combobox = sender as ComboBox;
            var current = combobox.SelectedItem as ComboBoxItem;

            if (current.Content.ToString() == "Automatic Folder Tracking")
            {
                ManualPanel.Visibility = Visibility.Hidden;
                AutoPanel.Visibility = Visibility.Visible;
                manual = false;
            }
            else
            {
                ManualPanel.Visibility = Visibility.Visible;
                AutoPanel.Visibility = Visibility.Hidden;
                manual = true;
            }
        }

        private void Select_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            // Opens file browser and gets selected path
            var dialogue = new Microsoft.Win32.OpenFolderDialog();
            dialogue.ShowDialog();
            selectedPath = dialogue.FolderName;
            PathText.Text = selectedPath;
        }

        private void OnFolderActivity(object sender, FileSystemEventArgs e)
        {
            lastActivityTime = DateTime.Now;
        }
    }
}