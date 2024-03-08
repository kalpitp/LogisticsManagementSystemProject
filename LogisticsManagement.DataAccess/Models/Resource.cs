using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class Resource
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public bool IsAvailable { get; set; }

    public virtual ICollection<ResourceMapping> ResourceMappings { get; set; } = new List<ResourceMapping>();

    public virtual User User { get; set; } = null!;
}
