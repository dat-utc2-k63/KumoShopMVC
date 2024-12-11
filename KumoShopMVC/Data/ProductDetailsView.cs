using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class ProductDetailsView
{
    public int ProductId { get; set; }

    public string? NameProduct { get; set; }

    public string? DescProduct { get; set; }

    public double? Price { get; set; }

    public string? Brands { get; set; }

    public bool? Gender { get; set; }

    public string? ColorName { get; set; }

    public int? SizeNumber { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsNew { get; set; }

    public bool? IsHot { get; set; }
}
