using edok_kiosk.Model;
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

namespace edok_kiosk.UserControls
{
    /// <summary>
    /// Interaction logic for OrderUserControl.xaml
    /// </summary>
    public partial class OrderUserControl : UserControl
    {
        public Order _order;
        private List<CartItem> _cartItems;
        public OrderUserControl(Order order, List<CartItem> cartItems)
        {
            InitializeComponent();
            _order = order;
            _cartItems = cartItems;
            UpdateUI();
        }

        private void UpdateUI()
        {
            orderNumber_TextBlock.Text = $"Заказ №{_order.Id}";
            var time = TimeOnly.FromDateTime(_order.CreatedAt);
            orderTimestamp_TextBlock.Text = $"Время заказа: {time:HH}:{time:mm}:{time:ss}";
            orderAddress_TextBlock.Text = $"Адрес доставки: {_order.Address}";
            orderPositionsCount_TextBlock.Text = $"Кол-во позиций: {_cartItems.Count}";
            orderTotal_TextBlock.Text = $"Сумма: {_order.Total}";
        }
    }
}
