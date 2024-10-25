using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class StatusShipping
{
    public int StatusId { get; set; }

    public string? NameStatus { get; set; }

    public string? DescShipping { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
