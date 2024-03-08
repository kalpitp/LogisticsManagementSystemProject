using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int InventoryId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalAmount { get; set; }

    public string OrderStatus { get; set; } = null!;

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<ResourceMapping> ResourceMappings { get; set; } = new List<ResourceMapping>();

    public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; } = new List<ShipmentDetail>();
}
