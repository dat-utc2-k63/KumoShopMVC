using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class Size
{
    public int SizeId { get; set; }

    public int SizeNumber { get; set; }

    public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
}
