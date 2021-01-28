using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

namespace DayNight12.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region attributes
        private DispatcherTimer _Timer = new DispatcherTimer();
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setWindowSize();
            startTimer();
        }

        private void startTimer()
        {
            _Timer.Tick += new EventHandler(Timer_Tick);
            _Timer.Interval = new TimeSpan(0, 0, 1);
            _Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            displayTime(DateTime.Now);
            updateTitle();
        }

        private void updateTitle()
        {
            this.Title = $"{txtHour.Text}{txtMinute.Text}{txtSecond.Text} - {txtHalf.Text} {txtDate.Text} - DayNight12 {AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Version}";
        }

        private void displayTime(DateTime dateTime)
        {
            txtHour.Text = $"{TimeHelper.CalculateHour(dateTime.Hour)} :";
            txtMinute.Text = $"{dateTime.Minute} :";
            txtSecond.Text = dateTime.Second.ToString();
            
            string daynight = TimeHelper.IsDay(dateTime.Hour) ? "Day" : "Night";
            DateTime calculatedDate = TimeHelper.CalculateDate(dateTime);
            
            txtHalf.Text = $"{daynight} of {calculatedDate.DayOfWeek}";
            txtDate.Text = calculatedDate.ToString("d");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

            Properties.Settings.Default.Save();
        }

        private void setWindowSize()
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
