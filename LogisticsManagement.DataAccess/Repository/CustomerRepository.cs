using LogisticsManagement.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CybageLogisticsContext _dbContext;

        public CustomerRepository(CybageLogisticsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Inventory> GetAllProducts()
        {
            try
            {
                return _dbContext.Inventories.Where(prod => prod.IsActive == true).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured while fetching products");
                return null;
            }
        }

        public Inventory GetProductById(int productId)
        {
            try
            {
                return _dbContext.Inventories.FirstOrDefault(prod => prod.Id == productId);
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured while fetching product");
                return null;
            }
        }

        public int CreateOrder(Order order, List<OrderDetail> orderDetails, ShipmentDetail shipmentDetail)
        {
            try
            {
                _dbContext.Orders.Add(order);
                _dbContext.OrderDetails.AddRange(orderDetails);
                _dbContext.ShipmentDetails.Add(shipmentDetail);
                int result = _dbContext.SaveChanges();

                if (result > 0)
                {
                   return UpdateProductQuantity(orderDetails[0].InventoryId, orderDetails[0].Quantity);
                }

            }
            catch (Exception)
            {
                Console.WriteLine("An error occured while creating order");
            }
            return 0;

        }

        public int UpdateProductQuantity(int productId, int quantity)
        {
            try
            {

                Inventory product = _dbContext.Inventories.FirstOrDefault(prod => prod.Id == productId);
                product.ProductQuantity -= quantity;

                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public List<Order> ViewOrders(int userId)
        {
            try
            {
                var orders = _dbContext.Orders
                    .Where(o => o.UserId == userId)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Inventory)
                    .ToList();

                return orders;

            }catch(Exception)
            {
                Console.WriteLine("An error occured while fetching order");
                return null;
            }
        }

        public Order GetOrderbyId(int userId, int orderId)
        {
            try
            {
                return _dbContext.Orders.Where(ord=>ord.Id == orderId && ord.UserId == userId).Include(o => o.OrderDetails).ThenInclude(od => od.Inventory).FirstOrDefault();
            }catch(Exception)
            {
                Console.WriteLine("An error occured while fetching order");
                return null;
            }
        }
    }
}
