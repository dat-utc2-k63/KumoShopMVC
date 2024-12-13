using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class ProductColor
{
    public int ProductColorId { get; set; }

    public int? ProductId { get; set; }

    public int? ColorId { get; set; }

    public virtual Color? Color { get; set; }

    public virtual Product? Product { get; set; }
}
