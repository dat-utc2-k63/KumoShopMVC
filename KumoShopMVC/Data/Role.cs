using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KumoShopMVC.Data;

public partial class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleId { get; set; }

    public string? NameRole { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
