using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WindowsShutdownTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int seconds;    // Seconds left on timer

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(HoursTextBox.Text, out int hours) || !int.TryParse(MinutesTextBox.Text, out int minutes))
                MessageBox.Show("Please enter a valid number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);    // Shows error if either box does not contain a number
            else
                CreateCountdown(hours, minutes);
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            CountdownText.Text = TimeSpan.FromSeconds(0).ToString();
        }

        private void CreateCountdown(int hours, int minutes)
        {
            seconds = (hours * 3600) + (minutes * 60);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            CountdownText.Text = TimeSpan.FromSeconds(seconds).ToString();
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
                Process.Start("shutdown", "/s /t 0");
            }
        }
    }
}