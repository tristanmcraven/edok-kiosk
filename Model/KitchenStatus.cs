using System;
using System.Collections.Generic;

namespace edok_kiosk.Model;

public partial class KitchenStatus
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
