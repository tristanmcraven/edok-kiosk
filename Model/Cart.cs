using System;
using System.Collections.Generic;

namespace edok_kiosk.Model;

public partial class Cart
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public uint RestaurantId { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
