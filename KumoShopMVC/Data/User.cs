using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class User
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public string? Avatar { get; set; }

    public bool? Status { get; set; }

    public int RoleId { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? RandomKey { get; set; }

    public string? AboutUs { get; set; }

    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<RatingProduct> RatingProducts { get; set; } = new List<RatingProduct>();

    public virtual Role Role { get; set; } = null!;
}
