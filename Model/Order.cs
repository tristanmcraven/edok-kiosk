using System;
using System.Collections.Generic;

namespace edok_kiosk.Model;

public partial class Order
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public uint RestaurantId { get; set; }

    public uint CartId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public string Address { get; set; } = null!;

    public uint? Total { get; set; }

    public uint? KitchenStatusId { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual KitchenStatus? KitchenStatus { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
