using LogisticsManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository
{
    public interface ICustomerRepository
    {
        List<Inventory> GetAllProducts();

        Inventory GetProductById(int productId);

        int CreateOrder(Order order, List<OrderDetail> orderDetails, ShipmentDetail shipment);

         List<Order> ViewOrders(int userId);

        Order GetOrderbyId(int userId,int orderId);
    }
}
