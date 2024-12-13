using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public int PaymentMethodeId { get; set; }

    public int UserId { get; set; }

    public string? Quantity { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? FullNameCard { get; set; }

    public string? CardNumber { get; set; }

    public string? CardHolder { get; set; }

    public string? ExpDate { get; set; }

    public string? Cvv { get; set; }

    public virtual PaymentMethode PaymentMethode { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
