using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class ResourceMapping
{
    public int Id { get; set; }

    public int ResourceId { get; set; }

    public int OrderDetailsId { get; set; }

    public int ManagerId { get; set; }

    public virtual User Manager { get; set; } = null!;

    public virtual OrderDetail OrderDetails { get; set; } = null!;

    public virtual Resource Resource { get; set; } = null!;
}
