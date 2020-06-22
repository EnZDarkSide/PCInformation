using System.Windows;

namespace WPF_TEST
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public string Password
        {
            get { return passwordBox.Text; }
        }
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        
    }
}
