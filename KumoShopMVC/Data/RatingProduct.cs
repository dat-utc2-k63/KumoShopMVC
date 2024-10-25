using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class RatingProduct
{
    public int RatingId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public string? DescRating { get; set; }

    public int? RatePoint { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Fullname { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<RatingImage> RatingImages { get; set; } = new List<RatingImage>();

    public virtual User User { get; set; } = null!;
}
