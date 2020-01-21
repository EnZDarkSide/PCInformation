using DevExpress.Mvvm;
using System.Windows.Input;
using WPF_TEST.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Windows.Threading;
using System.Linq;
using System.Diagnostics;

namespace WPF_TEST.ViewModels
{
    class AppViewModel : ViewModelBase
    {
        // Объект приложений
        public ObservableCollection<App> AppList { get; set; } = new ObservableCollection<App>();
        public App SelectedApp { get; set; }

        public AppViewModel()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            timer.Tick += UpdateProcesses;
            timer.Start();
        }

        private void UpdateProcesses(object sender, EventArgs e)
        {
            var processIDs = AppList.Select(p => p.Id).ToList();
            var newProcesses = Process.GetProcesses();

            foreach (var p in newProcesses)
            {
                if (!processIDs.Contains(p.Id)) // появился новый процесс
                {
                    AppList.Add(new App(p));
                }
            }

            var newProcIds = newProcesses.Select(p => p.Id).ToList();
            foreach (var p in processIDs)
            {
                if(!newProcIds.Contains(p))
                {
                    AppList.Remove(AppList.First(proc => proc.Id == p));
                }
            }
        }
    }
}
