using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int ProductQuantity { get; set; }

    public int WarehouseId { get; set; }

    public string? ProductDescription { get; set; }

    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Warehouse Warehouse { get; set; } = null!;
}
