using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class Contact
{
    public int ContactId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Subject { get; set; }

    public string? DescContact { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreateDate { get; set; }
}
