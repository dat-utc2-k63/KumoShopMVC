using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class ProductColor
{
    public int ProductId { get; set; }

    public int ColorId { get; set; }

    public virtual Color Color { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
