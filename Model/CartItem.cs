using System;
using System.Collections.Generic;

namespace edok_kiosk.Model;

public partial class CartItem
{
    public uint Id { get; set; }

    public uint CartId { get; set; }

    public uint FoodId { get; set; }

    public uint FoodQuantity { get; set; }

    public uint UtensilsQuantity { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Food Food { get; set; } = null!;
}
