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
using System.Collections;
using System.Collections.Specialized;
using edok_kiosk.Misc;
using edok_kiosk.UserControls;

namespace edok_kiosk.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private ObservableCollection<Order> _orders = new();
        private List<Order> _orders = new();
        public MainWindow()
        {
            InitializeComponent();
            //_orders.CollectionChanged += _orders_CollectionChanged;
        }

        //private void _orders_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action == NotifyCollectionChangedAction.Replace)
        //    {
        //        if (!e.OldItems.Equals(e.NewItems))
        //        {
        //            foreach (var item in e.NewItems)
        //            {
        //                orders_ListBox.Items.Add("123");
        //            }
        //        }
        //    }
        //}

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
            UpdateOrdersAsync();
        }

        private async void UpdateOrdersAsync()
        {
            while (true)
            {
                //_orders = new ObservableCollection<Order>(await ApiClient._Restaurant.GetActiveOrders(GlobalViewModel.Restaurant.Id));
                var newOrders = await ApiClient._Restaurant.GetActiveOrders(GlobalViewModel.Restaurant.Id);

                var addedOrders = newOrders.Except(_orders, new OrderComparer()).ToList();
                var removedOrders = _orders.Except(newOrders, new OrderComparer()).ToList();

                AddOrderNotification(addedOrders);

                _orders = newOrders;

                await Task.Delay(3000);
            }
        }

        private async void AddOrderNotification(List<Order> orders)
        {
            foreach (var order in orders)
            {
                var cartItems = await ApiClient._Order.GetCartItems(order);
                orders_ListBox.Items.Add(new OrderUserControl(order, cartItems));
            }
        }
    }
}
