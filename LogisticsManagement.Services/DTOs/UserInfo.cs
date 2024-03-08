using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Services.DTOs
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }


        public string PhoneNumber { get; set; }

        public int UserId { get; set; }

        public string? ShippingAddress { get; set; }

        public string? LicenseNumber { get; set; }

        public string? VehicleType { get; set; }

        public string? VehicleNumber { get; set; }

        public int? WarehouseId { get; set; }

        public int IsApproved { get; set; }
    }
}
