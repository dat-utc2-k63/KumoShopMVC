using System;
using System.Collections.Generic;

namespace KumoShopMVC.Data;

public partial class Role
{
    public int RoleId { get; set; }

    public string? NameRole { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
