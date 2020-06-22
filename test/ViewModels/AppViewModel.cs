using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_TEST.Helpers;
using WPF_TEST.Models;
using WPF_TEST.Repositories;

namespace WPF_TEST.ViewModels
{
    class AppViewModel : ViewModelBase
    {
        public AppViewModel()
        {
            ReportCommand = new RelayCommand(o => SaveReport());

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            timer.Tick += UpdateProcesses;
            timer.Start();
        }
        public CpuCounter CpuCounter { get; set; } = new CpuCounter();
        public ObservableCollection<App> AppList { get; set; } = new ObservableCollection<App>();
        public App SelectedApp { get; set; }
        private void UpdateProcesses(object sender, EventArgs e)
        {
            var currProcIDList = AppList.Select(p => p.Id).ToList();
            var updatedProcesses = Process.GetProcesses().Where(proc => !string.IsNullOrEmpty(proc.MainWindowTitle));
            var updatedProcIdList = updatedProcesses.Select(p => p.Id).ToList();

            // Добавление новых процессов в список
            AppList.AddRange(updatedProcesses.Where(proc => !currProcIDList.Contains(proc.Id)).Select(proc => new App(proc)));

            // Удаление старых процессов
            foreach (var procId in currProcIDList)
            {
                if (!updatedProcIdList.Contains(procId))
                {
                    AppList.Remove(AppList.First(proc => proc.Id == procId));
                }
            }
        }

        public ICommand ReportCommand { get; private set; }

        public void SaveReport()
        {
            MySqlRepository.SaveReport(new List<App>(AppList));
        }
    }
}
