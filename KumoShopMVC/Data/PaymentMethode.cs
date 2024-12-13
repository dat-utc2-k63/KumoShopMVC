using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class PaymentMethode
{
    public int PaymentMethodeId { get; set; }

    public string? NamePaymentMethode { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
