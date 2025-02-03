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
        public OrderPage(Order order)
        {
            InitializeComponent();
            _order = order;
            InitUI();
        }

        private async void InitUI()
        {
            orderNumber_TextBlock.Text = $"Заказ №{_order.Id}";
            var datetime = _order.CreatedAt;
            var timestamp = TimeOnly.FromDateTime(_order.CreatedAt);
            orderTimestamp_TextBlock.Text = $"Дата и время заказа: {timestamp:HH}:{timestamp:mm}:{timestamp:ss} {datetime:dd}/{datetime:MM}/{datetime:yyyy}";
            orderAddress_TextBlock.Text = $"Адрес доставки: {_order.Address}";
            _cartItems = await ApiClient._Order.GetCartItems(_order);
            var count = (_cartItems.Select(x => (int)x.FoodQuantity)).Sum();
            orderPositionsCount_TextBlock.Text = $"Количество позиций в заказе: {count}";

            _client = await ApiClient._User.GetUserById(_order.UserId);
            clientName_TextBlock.Text = $"Имя: {_client.Surname} {_client.Name}";
            clientPhone_TextBlock.Text = $"Номер телефона: +7{_client.PhoneNumber}";
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
        }

        private void nextStatus_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelOrder_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
