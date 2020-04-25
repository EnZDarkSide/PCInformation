using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;

namespace WPF_TEST.Models
{
    class CpuCounter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public SeriesCollection CpuLoading { get; private set; }

        public DispatcherTimer timer;
        public PerformanceCounter cpuCounter;
        private void UpdateProcLoading(object sender, EventArgs e)
        {
            if (CpuLoading[0].Values.Count >= 10)
                CpuLoading[0].Values.RemoveAt(0);

            var procLoading = cpuCounter.NextValue();
            CpuLoading[0].Values.Add(new ObservableValue(procLoading));
        }

        public CpuCounter()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            CpuLoading = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservableValue>()
                },
            };

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += UpdateProcLoading;
            timer.Start();
        }
    }
}
