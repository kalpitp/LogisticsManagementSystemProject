using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class ShipmentDetail
{
    public int Id { get; set; }

    public int OrderDetailsId { get; set; }

    public string Origin { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public DateTime ExpectedArrivalTime { get; set; }

    public virtual OrderDetail OrderDetails { get; set; } = null!;
}
