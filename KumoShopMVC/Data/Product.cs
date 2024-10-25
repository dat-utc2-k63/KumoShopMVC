using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class Product
{
    public int ProductId { get; set; }

    public string? NameProduct { get; set; }

    public string? DescProduct { get; set; }

    public double? Price { get; set; }

    public string? Brands { get; set; }

    public bool? Gender { get; set; }

    public double? Discount { get; set; }

    public bool? Status { get; set; }

    public int CategoryId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? Quantity { get; set; }

    public bool? IsNew { get; set; }

    public bool? IsHot { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<FavouriteItem> FavouriteItems { get; set; } = new List<FavouriteItem>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<RatingProduct> RatingProducts { get; set; } = new List<RatingProduct>();
}
