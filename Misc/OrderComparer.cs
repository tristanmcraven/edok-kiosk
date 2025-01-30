using edok_kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edok_kiosk.Misc
{
    public class OrderComparer : IEqualityComparer<Order>
    {
        public bool Equals(Order? x, Order? y)
        {
            if (x == null || y == null) return false;
            return x.Id == y.Id;
        }

        public int GetHashCode(Order obj)
        {
            return obj.Id.GetHashCode();
        }
    }

}
