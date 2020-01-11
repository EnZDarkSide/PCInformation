using System;
using System.ComponentModel;
using System.Windows.Threading;
using DevExpress.Mvvm;

namespace WPF_TEST.Models
{
    class App : INotifyPropertyChanged
    {
        public string Name { get; private set; }

        public string Elapsed { get; private set; }

        public DispatcherTimer timer { get; private set; }
        private DateTime start = DateTime.Now;

        public event PropertyChangedEventHandler PropertyChanged;

        public App(string name)
        {
            Name = name;
            timer = InitTimer();

        }

        private DispatcherTimer InitTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += t_Tick;
            timer.Start();
            return timer;
        }

        // Обновление строки таймера
        private void t_Tick(object sender, EventArgs e)
        {
            TimeSpan elpsd = DateTime.Now - start;
            Elapsed = Convert.ToString(string.Format("{0:00}:{1:00}:{2:00}",
                elpsd.TotalHours, elpsd.TotalMinutes, elpsd.TotalSeconds));

            // Говорим View, что Elapsed изменился.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Elapsed"));
        }
    }
}
