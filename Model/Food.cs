using System;
using System.Collections.Generic;

namespace edok_kiosk.Model;

public partial class Food
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public uint RestaurantId { get; set; }

    public uint FoodCategoryId { get; set; }

    public byte[] Photo { get; set; } = null!;

    public string? Description { get; set; }

    public uint Price { get; set; }

    public bool Disabled { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual FoodCategory FoodCategory { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; } = null!;
}
