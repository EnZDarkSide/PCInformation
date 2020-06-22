using System;
using System.Collections.Generic;
using System.Linq;
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
using WPF_TEST;

namespace test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthWindow authWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        // У каждого окна есть метод OnClosing, который вызывается после нажатия, к примеру, крестика
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            do
            {
                //Будем вызывать окно авторизации, пока пароль не будет верным
                authWindow = new AuthWindow();
                //Если окно закрыто на кнопку OK
                if (authWindow.ShowDialog() == true)
                    if (authWindow.Password == "root")
                    {
                        //Используем изначальную версию закрытия окна, как если бы всего этого переопределения не было
                        base.OnClosing(e);
                    }
                    else
                    {
                        MessageBox.Show("Введен неверный пароль");
                        e.Cancel = true;
                    }
                else
                {
                    //Отменяем закрытие окна
                    e.Cancel = true;
                    break;
                }
            } while (authWindow.Password != "root");
        }

    }
}
