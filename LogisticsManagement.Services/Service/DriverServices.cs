using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository;
using LogisticsManagement.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Services.Service
{
    public class DriverServices : IDriverServices
    {
        private readonly IAuthenticationRepository _authRepository;
        private readonly IDriverRepository _driverRepository;


        public DriverServices(IAuthenticationRepository authRepository, IDriverRepository driverRepository)
        {
            _authRepository = authRepository;
            _driverRepository = driverRepository;
        }

        public bool UpdateAssignedOrderStatus(int userId, int orderDetailId, string orderStatus)
        {
            try
            {
                int rowUpdated = _driverRepository.UpdateOrderStatus(userId, orderDetailId, orderStatus);
                if (rowUpdated > 0)
                {
                    return true;
                }
              
            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
            }
            return false;
        }

        public void ViewAssignedOrders(int userId)
        {
            try
            {
                var assignedOrder = _driverRepository.GetAssignedOrder(userId);
                if (assignedOrder != null && assignedOrder.Count > 0) {
                    foreach (var order in assignedOrder)
                    {
                        Console.WriteLine($"\nOrder Details Id: {order.ResourceMappings.First().OrderDetailsId}");
                        Console.WriteLine($"Order Status: {order.ResourceMappings.First().OrderDetails.OrderStatus}");
                        Console.WriteLine($"Order Destination: {order.ResourceMappings.First().OrderDetails.ShipmentDetails.First().Destination}");
                    }
                }
                else
                {
                    CommonServices.WarningMessage("No orders found");
                }

            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
            }
        }

    }
}
