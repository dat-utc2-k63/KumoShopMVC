using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ShippingDate { get; set; }

    public string? DescOrder { get; set; }

    public int? StatusId { get; set; }

    public int? Quantity { get; set; }

    public string? Phone { get; set; }

    public string? Fullname { get; set; }

    public string? Address { get; set; }

    public string? PaymentMethode { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual StatusShipping? Status { get; set; }

    public virtual User User { get; set; } = null!;
}
