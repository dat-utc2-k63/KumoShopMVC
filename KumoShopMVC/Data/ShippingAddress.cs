using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class ShippingAddress
{
    public int ShippingAddressId { get; set; }

    public int UserId { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Fullname { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual User User { get; set; } = null!;
}
