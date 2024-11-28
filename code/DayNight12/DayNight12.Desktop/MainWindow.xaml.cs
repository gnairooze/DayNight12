using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace DayNight12.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region attributes
        private readonly DispatcherTimer _Timer = new();
        private Language.ILanguage _Language;
        #endregion
        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                LogException(ex);
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private static void LogException(Exception ex)
        {
            System.IO.File.WriteAllText("error.log", ex.ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetLanguage();
            SetWindowSize();
            SaveNewSettings();
            StartTimer();
        }

        private void SetLanguage()
        {
            this._Language = DayNight12.Desktop.Language.LanguageHelper.GetLanguageInstance(Properties.Settings.Default.Language);
        }

        private void StartTimer()
        {
            _Timer.Tick += new EventHandler(Timer_Tick);
            _Timer.Interval = new TimeSpan(0, 0, 1);
            _Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DisplayTime(DateTime.Now);
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            this.Title = $"{txtHour.Text}{txtMinute.Text}{txtSecond.Text} - {txtHalf.Text} {txtDate.Text} - DayNight12 {Assembly.GetExecutingAssembly().GetName().Version}";
        }

        private void DisplayTime(DateTime dateTime)
        {
            txtHour.Text = $"{TimeHelper.CalculateHour(dateTime.Hour)} :";
            txtMinute.Text = $"{dateTime.Minute} :";
            txtSecond.Text = dateTime.Second.ToString();
            
            string daynight = TimeHelper.IsDay(dateTime.Hour) ? this._Language.Day : this._Language.Night;
            DateTime calculatedDate = TimeHelper.CalculateDate(dateTime);

            txtHalf.Text = $"{daynight} {calculatedDate.ToString("dddd",new CultureInfo(this._Language.Culture))}";
            
            txtDate.Text = calculatedDate.ToString("yyyy-MM-dd");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveNewSettings();
        }

        private void SaveNewSettings()
        {
            if (WindowState == WindowState.Maximized)
            {
                // Use the RestoreBounds as the current values will be 0, 0 and the size of the screen
                Properties.Settings.Default.Top = RestoreBounds.Top;
                Properties.Settings.Default.Left = RestoreBounds.Left;
                Properties.Settings.Default.Height = RestoreBounds.Height;
                Properties.Settings.Default.Width = RestoreBounds.Width;
                Properties.Settings.Default.Maximized = true;
            }
            else
            {
                Properties.Settings.Default.Top = this.Top;
                Properties.Settings.Default.Left = this.Left;
                Properties.Settings.Default.Height = this.Height;
                Properties.Settings.Default.Width = this.Width;
                Properties.Settings.Default.Maximized = false;
            }

            Properties.Settings.Default.Language = Properties.Settings.Default.Language;

            Properties.Settings.Default.Save();
        }

        private void SetWindowSize()
        {
            this.Top = Properties.Settings.Default.Top;
            this.Left = Properties.Settings.Default.Left;
            this.Height = Properties.Settings.Default.Height;
            this.Width = Properties.Settings.Default.Width;

            if (Properties.Settings.Default.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
    }
}
