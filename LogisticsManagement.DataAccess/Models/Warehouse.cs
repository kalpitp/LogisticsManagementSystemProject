using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
