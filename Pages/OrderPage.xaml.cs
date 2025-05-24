using edok_kiosk.Model;
using edok_kiosk.UserControls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace edok_kiosk.Pages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private Order _order;
        private User? _client = null;
        private List<CartItem> _cartItems = new();
        private List<KitchenStatus> _kitchenStatuses = new();
        private KitchenStatus _currentKitchenStatus = default;
        private KitchenStatus _nextKitchenStatus = default;

        private uint _orderId;
        public OrderPage(uint orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            InitUI();
        }

        private async void InitUI()
        {
            await FetchData();

            nextStatus_Button.Visibility = Visibility.Visible;
            cancelOrder_Button.Visibility = Visibility.Visible;

            if (_order.Status == "cancelled")
            {
                nextStatus_Button.Visibility = Visibility.Collapsed;
                cancelOrder_Button.Visibility = Visibility.Collapsed;
            }

            orderStatus_TextBlock.Text = "Статус заказа: ";
            orderStatus_TextBlock.Text += _order.Status switch
            {
                "active" => "Активный",
                "cancelled" => "Отменён",
                "finished" => "Доставлен",
                _ => "Неизвестно"
            };


            orderNumber_TextBlock.Text = $"Заказ №{_order.Id}";
            var datetime = _order.CreatedAt;
            var timestamp = TimeOnly.FromDateTime(_order.CreatedAt);
            orderTimestamp_TextBlock.Text = $"Дата и время заказа: {timestamp:HH:mm:ss} {datetime:dd/MM/yyyy}";
            orderAddress_TextBlock.Text = $"Адрес доставки: {_order.Address}";
            _cartItems = await ApiClient._Order.GetCartItems(_order);
            var count = (_cartItems.Select(x => (int)x.FoodQuantity)).Sum();
            orderPositionsCount_TextBlock.Text = $"Количество позиций в заказе: {count}";

            _client = await ApiClient._User.GetUserById(_order.UserId);
            clientName_TextBlock.Text = $"Имя: {_client.Surname} {_client.Name}";
            clientPhone_TextBlock.Text = $"Номер телефона: {_client.PhoneNumber}";
            clientEmail_TextBlock.Text = $"Электронная почта: ";
            clientEmail_TextBlock.Text += _client.Email != null ? $"{_client.Email}" : "Не указана";
            clientOrderCount_TextBlock.Text = $"Количество заказов: {(await ApiClient._User.GetOrders(_client.Id)).Count()}";

            orderItems_StackPanel.Children.Clear();
            foreach (var item in _cartItems)
            {
                var food = await ApiClient._Food.GetById(item.FoodId);
                orderItems_StackPanel.Children.Add(new FoodUserControl(food, item));
            }
            foodTotal_TextBlock.Text = $"{_order.Total} РУБ.";

            _kitchenStatuses = await ApiClient._KitchenStatus.Get();
            _currentKitchenStatus = _kitchenStatuses.FirstOrDefault(x => x.Id == _order.KitchenStatusId);
            _nextKitchenStatus = _kitchenStatuses.FirstOrDefault(x => x.Id == (_order.KitchenStatusId + 1));
            if (_nextKitchenStatus == null) _nextKitchenStatus = _kitchenStatuses.Last();

            nextStatus_Button.Content = $"Проставить следующий статус ({_currentKitchenStatus.Name}) -> ({_nextKitchenStatus.Name})";

            if (_order.KitchenStatusId == _kitchenStatuses.Last().Id)
            {
                nextStatus_Button.Visibility = Visibility.Collapsed;
                cancelOrder_Button.Visibility= Visibility.Collapsed;
            }

            orderKitchenStatus_TextBlock.Text = $"Внутренний статус заказа: {_currentKitchenStatus.Name}";
            orderTotalPositionsCount_TextBlock.Text = $"Количество блюд: {_cartItems.Count}";
        }

        private async Task FetchData()
        {
            _order = await ApiClient._Order.GetById(_orderId);
        }

        private async void nextStatus_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show($"Вы уверены, что хотите изменить статус с [{_currentKitchenStatus.Name}] на [{_nextKitchenStatus.Name}]?",
                        "Подтверждение",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                await ApiClient._Order.ApplyNextStatus(_order.Id);

                _order = await ApiClient._Order.GetById(_order.Id);
                InitUI();
            }
        }

        private async void cancelOrder_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show($"Вы уверены, что хотите отменить выполнение заказа?",
            "Подтверждение",
            MessageBoxButton.YesNo,
            MessageBoxImage.Asterisk);
            if (result == MessageBoxResult.Yes)
            {
                _order = await ApiClient._Order.CancelOrder(_order.Id);
                InitUI();
            }
        }
    }
}
