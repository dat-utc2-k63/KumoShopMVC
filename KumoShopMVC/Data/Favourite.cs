using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class Favourite
{
    public int FavouriteId { get; set; }

    public int UserId { get; set; }

    public string? DescFavourite { get; set; }

    public int? Quantity { get; set; }

    public virtual ICollection<FavouriteItem> FavouriteItems { get; set; } = new List<FavouriteItem>();

    public virtual User User { get; set; } = null!;
}
