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
        public string Elapsed { get; set; }
        public Process Process { get; set; }

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

        // Обновление строки таймера
        private void t_Tick(object sender, EventArgs e)
        {
            Elapsed = UpdateElapsed();
            UpdateChartSeries();
        }

        private void UpdateChartSeries()
        {
            if (ProcessorLoading[0].Values.Count >= 10)
                ProcessorLoading[0].Values.RemoveAt(0);

            // TODO: Сделать получение информации о загрузке процессора приложением.
            //https://stackoverflow.com/questions/9259772/getting-cpu-usage-of-a-process-in-c-sharp <- TODO: исправить на это
            ProcessorLoading[0].Values.Add(new ObservableValue(rand.Next(0, 100)));
        }

        private string UpdateElapsed()
        {
            TimeSpan elpsd = DateTime.Now - start;
            var elapsedString = Convert.ToString(string.Format("{0:00}:{1:00}:{2:00}",
                elpsd.TotalHours, elpsd.TotalMinutes, elpsd.TotalSeconds));
            //Оповещаем подписчиков через событие PropertyChanged. Знак вопроса заменяет конструкцию if(PropertyChanged != null)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Elapsed"));
            return elapsedString;
        }
    }
}
