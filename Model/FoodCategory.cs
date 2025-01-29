using System;
using System.Collections.Generic;

namespace edok_kiosk.Model;

public partial class FoodCategory
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
}
