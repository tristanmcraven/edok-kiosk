using edok_kiosk.Model;
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

namespace edok_kiosk.UserControls
{
    /// <summary>
    /// Interaction logic for FoodUserControl.xaml
    /// </summary>
    public partial class FoodUserControl : UserControl
    {
        private Food _food;
        private CartItem _cartItem;
        public FoodUserControl(Food food, CartItem cartItem)
        {
            InitializeComponent();
            _food = food;
            _cartItem = cartItem;
            InitUI();
        }

        private void InitUI()
        {
            food_Image.Source = ImageHelper.GetImage(_food.Photo);
            foodCount_TextBlock.Text = _cartItem.FoodQuantity.ToString();
            foodPrice_TextBlock.Text = _food.Price.ToString();
        }
    }
}
