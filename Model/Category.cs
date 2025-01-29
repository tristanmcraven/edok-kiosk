using System;
using System.Collections.Generic;

namespace edok_kiosk.Model;

public partial class Category
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RestaurantCategory> RestaurantCategories { get; set; } = new List<RestaurantCategory>();
}
