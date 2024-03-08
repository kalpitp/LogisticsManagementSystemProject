using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class UserDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? ShippingAddress { get; set; }

    public string? LicenseNumber { get; set; }

    public string? VehicleType { get; set; }

    public string? VehicleNumber { get; set; }

    public int? WarehouseId { get; set; }

    public int? IsApproved { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Warehouse? Warehouse { get; set; }
}
