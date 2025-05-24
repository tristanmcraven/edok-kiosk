using edok_kiosk.Misc;
using edok_kiosk.Model;
using edok_kiosk.Pages;
using edok_kiosk.UserControls;
using edok_kiosk.Utility;
using System.Windows;
using System.Windows.Controls;

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
            sort_ComboBox.SelectionChanged -= sort_ComboBox_SelectionChanged;
            sort_ComboBox.SelectedIndex = 0;
            sort_ComboBox.SelectionChanged += sort_ComboBox_SelectionChanged;
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
                var newOrders = await ApiClient._Restaurant.GetOrders(GlobalViewModel.Manager.Id);

                var addedOrders = newOrders.Except(_orders, new OrderComparer()).ToList();
                var removedOrders = _orders.Except(newOrders, new OrderComparer()).ToList();

                AddOrderNotification(addedOrders);

                _orders = newOrders;

                await Task.Delay(3000);
            }
        }

        private async void AddOrderNotification(List<Order> orders)
        {
            var ordersSorted = orders.OrderBy(x => x.Id);
            foreach (var order in ordersSorted)
            {
                var cartItems = await ApiClient._Order.GetCartItems(order);
                switch (sort_ComboBox.SelectedIndex)
                {
                    case 0:
                        orders_ListBox.Items.Insert(0, new OrderUserControl(order, cartItems));
                        break;
                    case 1:
                        if (order.Status == "active")
                            orders_ListBox.Items.Insert(0, new OrderUserControl(order, cartItems));
                        break;
                    case 2:
                        if (order.Status == "finished")
                            orders_ListBox.Items.Insert(0, new OrderUserControl(order, cartItems));
                        break;
                    case 3:
                        if (order.Status == "cancelled")
                            orders_ListBox.Items.Insert(0, new OrderUserControl(order, cartItems));
                        break;
                }
            }
        }

        private void orders_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lb = sender as ListBox;
            var selectedItem = lb.SelectedItem;
            if (selectedItem != null)
            {
                var orderUc = selectedItem as OrderUserControl;
                PageManager.MainFrame.Navigate(new OrderPage(orderUc._order.Id));
            }
        }

        private async void sort_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (sort_ComboBox.SelectedIndex)
            {
                case 0:
                    orders_ListBox.Items.Clear();
                    foreach (var order in _orders)
                    {
                        var cartItems = await ApiClient._Order.GetCartItems(order);
                        orders_ListBox.Items.Insert(0, new OrderUserControl(order, cartItems));
                    }
                    break;
                case 1:
                    orders_ListBox.Items.Clear();
                    foreach (var order in _orders)
                    {
                        if (order.Status == "active")
                        {
                            var cartItems = await ApiClient._Order.GetCartItems(order);
                            orders_ListBox.Items.Insert(0, new OrderUserControl(order, cartItems));
                        }
                    }
                    break;
                case 2:
                    orders_ListBox.Items.Clear();
                    foreach (var order in _orders)
                    {
                        if (order.Status == "finished")
                        {
                            var cartItems = await ApiClient._Order.GetCartItems(order);
                            orders_ListBox.Items.Insert(0, new OrderUserControl(order, cartItems));
                        }
                    }
                    break;
                case 3:
                    orders_ListBox.Items.Clear();
                    foreach (var order in _orders)
                    {
                        if (order.Status == "cancelled")
                        {
                            var cartItems = await ApiClient._Order.GetCartItems(order);
                            orders_ListBox.Items.Insert(0, new OrderUserControl(order, cartItems));
                        }
                    }
                    break;
            }
        }
    }
}
