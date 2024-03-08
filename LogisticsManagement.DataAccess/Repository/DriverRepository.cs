using LogisticsManagement.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly CybageLogisticsContext _dbContext;

        public DriverRepository(CybageLogisticsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Resource> GetAssignedOrder(int userId)
        {
            try
            {
                //var assignedOrderes = _dbContext.Resources
                //       .Where(r => r.UserId == userId)
                //       .Include(r => r.ResourceMappings)
                //           .SelectMany(r => r.ResourceMappings.Select(rm => rm.OrderDetailsId))
                //           .ToList();


                List<Resource> orders = _dbContext.Resources.Where(e=>e.UserId == userId)
                   .Include(o => o.ResourceMappings)
                   .ThenInclude(od=>od.OrderDetails)
                   .ThenInclude(od => od.ShipmentDetails)
                   .ToList();

                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while fetching assigned orders");
            }
            return null;

        }

        public int UpdateOrderStatus(int userId, int orderDetailId, string orderStatus)
        {
            try
            {
                OrderDetail orderDetail = _dbContext.OrderDetails.FirstOrDefault(ud => ud.Id == orderDetailId);

                if (orderDetail != null)
                {
                    orderDetail.OrderStatus = orderStatus;
                    return _dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An Error occured while updating status");
            }
            return 0;
        }
    }
}
