using edok_kiosk.Utility;
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
using System.Windows.Shapes;

namespace edok_kiosk.Windows
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private async void logIn_Button_Click(object sender, RoutedEventArgs e)
        {
            string login = login_TextBox.Text;
            string password = passwordBox.Password;

            await Authenticate(login, password);
        }

        private async Task Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) ||
                string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var manager = await ApiClient._Manager.Authenticate(login, password);
            if (manager == null)
            {
                MessageBox.Show("Неправильное имя пользователя или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            GlobalViewModel.Manager = manager;
            GlobalViewModel.Restaurant = await ApiClient._Manager.GetLatestRestaurantById(manager.Id);
            new MainWindow().Show();

            Close();
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logIn_Button_Click(null, null);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            login_TextBox.Focus();
        }
    }
}
