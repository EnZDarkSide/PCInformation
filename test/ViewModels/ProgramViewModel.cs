using DevExpress.Mvvm;
using System.Windows.Input;
using WPF_TEST.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF_TEST.ViewModels
{
    class ProgramViewModel : ViewModelBase
    {
        // Объект приложений
        public ObservableCollection<App> AppList { get; set; }

        public App SelectedApp { get; set; }

        // Проверка новых приложений
        public ICommand ClickAdd { 
            get
            {
                return new DelegateCommand(() => {
                    AppList.Add(new App("Safari"));
                });
            }
        }

        public ProgramViewModel()
        {
            AppList = new ObservableCollection<App>
            {
                new App("Opera"),
                new App("Chrome"),
            };
        }
    }
}
