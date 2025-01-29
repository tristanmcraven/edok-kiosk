using edok_kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edok_kiosk.Utility
{
    public class GlobalViewModel
    {
        public static Manager? Manager { get; set; }
        public static Restaurant? Restaurant { get; set; }
    }
}
