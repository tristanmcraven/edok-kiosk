using System;
using System.Collections.Generic;

namespace edok_kiosk.Model;

public partial class Restaurant
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public TimeOnly OpeningTime { get; set; }

    public TimeOnly ClosingTime { get; set; }

    public string LegalInfo { get; set; } = null!;

    public string LegalAddress { get; set; } = null!;

    public string Inn { get; set; } = null!;

    public uint ManagerId { get; set; }

    public bool Pending { get; set; }

    public string? RejectReason { get; set; }

    public bool Disabled { get; set; }

    public string? DisableReason { get; set; }

    public byte[] Image { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual Manager Manager { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<RestaurantCategory> RestaurantCategories { get; set; } = new List<RestaurantCategory>();
}
