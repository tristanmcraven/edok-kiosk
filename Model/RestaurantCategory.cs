using System;
using System.Collections.Generic;

namespace edok_kiosk.Model;

public partial class RestaurantCategory
{
    public uint Id { get; set; }

    public uint RestaurantId { get; set; }

    public uint CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; } = null!;
}
