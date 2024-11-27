using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public string? Color { get; set; }

    public int? Size { get; set; }

    public string? Image { get; set; }

    public int? Quantity { get; set; }

    public string? NameProduct { get; set; }

    public double? Price { get; set; }

    public double? SubTotal { get; set; }

    public bool? IsRating { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
