using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class RatingImage
{
    public int RatingImageId { get; set; }

    public int RatingId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual RatingProduct Rating { get; set; } = null!;
}
