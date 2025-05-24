using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace edok_kiosk.Utility
{
    internal class WindowManager
    {
        public static T Get<T>() where T : Window => Application.Current.Windows.OfType<T>().First();

        public static void Close<T>() where T : Window => Get<T>().Close();
    }
}
