using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class FavouriteItem
{
    public int FavouriteItemId { get; set; }

    public int FavouriteId { get; set; }

    public int ProductId { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Favourite Favourite { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
