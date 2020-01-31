using System;
using System.ComponentModel;
using System.Windows.Threading;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using LiveCharts;
using System.Diagnostics;

namespace WPF_TEST.Models
{
    class App : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime start = DateTime.Now;
        public DispatcherTimer timer;

        // Тестовая генерация значений для загрузки процессора приложением
        Random rand = new Random();

        public int Id => Process.Id;
        public string Name => Process.ProcessName;
        public PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public string Elapsed { get; set; }
        public Process Process { get; set; }
        public bool isSelected { get; set; };

        public SeriesCollection ProcessorLoading { get; private set; }

        public App(Process process)
        {
            Process = process;

            timer = InitTimer();

            ProcessorLoading = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<LiveCharts.Defaults.ObservableValue>()
                },
            };

        }

        private DispatcherTimer InitTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += t_Tick;
            timer.Start();
            return timer;
        }

        // Обновление по таймеру
        private void t_Tick(object sender, EventArgs e)
        {
            Elapsed = UpdateElapsed();
            UpdateChartSeries();
        }

        private void UpdateChartSeries()
        {
            // Удаляем значение с левого края, если точек больше 10
            if (ProcessorLoading[0].Values.Count >= 10)
                ProcessorLoading[0].Values.RemoveAt(0);

            var procLoading = cpuCounter.NextValue();

            //Добавление в график значения в конец
            ProcessorLoading[0].Values.Add(new ObservableValue(procLoading));
        }
        
        private string UpdateElapsed()
        {
            TimeSpan elpsd = DateTime.Now - start;
            var elapsedString = Convert.ToString(string.Format("{0:00}:{1:00}:{2:00}",
                elpsd.Hours, elpsd.Minutes, elpsd.Seconds));
            //Оповещаем подписчиков через событие PropertyChanged. Знак вопроса заменяет конструкцию if(PropertyChanged != null)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Elapsed"));
            return elapsedString;
        }
    }
}
