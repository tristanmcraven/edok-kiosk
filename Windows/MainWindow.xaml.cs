using edok_kiosk.Model;
using edok_kiosk.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Order> _orders = new();
        public MainWindow()
        {
            InitializeComponent();
            _orders.CollectionChanged += _orders_CollectionChanged;
        }

        private void _orders_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in e.NewItems)
            {

            }
        }

        private void logOut_Button_Click(object sender, RoutedEventArgs e)
        {
            LogOut();
        }

        private void exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }

        private void LogOut()
        {
            new AuthWindow().Show();
            this.Close();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame = Frame;
        }
    }
}
