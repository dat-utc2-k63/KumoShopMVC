using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? NameCategory { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
