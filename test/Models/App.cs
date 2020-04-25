using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Threading;
using WPF_TEST.Helpers;

namespace WPF_TEST.Models
{
    class App : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DispatcherTimer timer;
        public ImageSource icon => Icon.ExtractAssociatedIcon(Process.MainModule.FileName).IcoToImageSource();

        public int Id => Process.Id;
        public string Title => Process.MainWindowTitle;
        public string Elapsed { get; set; }
        public Process Process { get; set; }

        public App(Process process)
        {
            Process = process;
            timer = InitTimer();
        }

        private DispatcherTimer InitTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += t_Tick;
            timer.Start();
            return timer;
        }

        private void t_Tick(object sender, EventArgs e)
        {
            Elapsed = UpdateElapsed();
        }

        private string UpdateElapsed()
        {
            TimeSpan elpsd = DateTime.Now - Process.StartTime;
            var elapsedString = Convert.ToString(string.Format("{0:00}:{1:00}:{2:00}",
                elpsd.Hours, elpsd.Minutes, elpsd.Seconds));
            return elapsedString;
        }
    }
}
