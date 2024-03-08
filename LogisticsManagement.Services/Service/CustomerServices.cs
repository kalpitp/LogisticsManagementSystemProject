using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository;
using LogisticsManagement.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Services.Service
{
    public class CustomerServices : ICustomerServices
    {

        private readonly IAuthenticationRepository _authRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerServices(IAuthenticationRepository authRepository, ICustomerRepository customerRepository)
        {
            _authRepository = authRepository;
            _customerRepository = customerRepository;
        }
        public bool ViewAllProducts()
        {
            try
            {
                List<Inventory> products = _customerRepository.GetAllProducts();
                if (products != null && products.Count>0)
                {
                    foreach (Inventory product in products)
                    {
                        Console.WriteLine($"Id: {product.Id} | Name: {product.ProductName} | Price: {product.Price}");
                    }
                    return true;
                }
                else
                {
                    CommonServices.WarningMessage("No products found");
                    return false;

                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
            }
            return false;

        }
        public void ViewProductDetails(int productId)
        {
            try
            {
                Inventory product = _customerRepository.GetProductById(productId);
                if (product != null)
                {
                    Console.WriteLine($"\n Id: {product.Id} \n Name: {product.ProductName} \n Price: {product.Price} \n Available Quantity: {product.ProductQuantity} \n Description: {product.ProductDescription}");
                }
                else
                {
                    CommonServices.WarningMessage("No product found");
                }

            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
            }
        }

        public bool ValidateQuantity(int productId, int quantity)
        {
            Inventory product = _customerRepository.GetProductById(productId);
            if (product != null && product.ProductQuantity >= quantity)
            {
                return true;
            }
            if(product != null && product.ProductQuantity == 0)
            {
                CommonServices.WarningMessage("This product is not available at moment");

            }
            else if (product == null)
            {
                CommonServices.WarningMessage("No product found");
            }
            return false;
        }

        public bool BuyProduct(int userId, int productId, int quantity)
        {
            try
            {
                decimal price = Convert.ToDecimal(_customerRepository.GetProductById(productId).Price);

                decimal totalAmount = quantity * price;

                User user = _authRepository.GetUserById(userId);

                string shipmentAddress = "";
                if (user != null)
                {
                    shipmentAddress = user.UserDetails.First().ShippingAddress;
                    
                }


                    //Create new order
                    Order order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                };

                //create new order detail
                OrderDetail orderDetail = new OrderDetail
                {
                    Order = order,
                    InventoryId = productId,
                    Quantity = quantity,
                    TotalAmount = totalAmount,
                    OrderStatus = "Pending"
                };

                ShipmentDetail shipmentDetail = new ShipmentDetail
                {
                    OrderDetails = orderDetail,
                    Origin = "Gandhinagar",
                    Destination = shipmentAddress,
                    ExpectedArrivalTime = DateTime.Now.AddDays(2),

                };


                List<OrderDetail> orderDetails = new List<OrderDetail>() { orderDetail };

                int result = _customerRepository.CreateOrder(order, orderDetails, shipmentDetail);

                if (result > 0) return true;

            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
            }
            return false;
        }

        public bool ViewAllOrders(int userId)
        {
            try
            {
                List<Order> orders = _customerRepository.ViewOrders(userId);
                if (orders != null && orders.Count>0)
                {
                    foreach (Order order in orders)
                    {
                        Console.WriteLine($"Order Id: {order.Id} | Order Date: {order.OrderDate:D} | Status: {order.OrderDetails.First().OrderStatus}");
                    }
                    return true;
                }
                else
                {
                    CommonServices.WarningMessage("No orders found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
            }
            return false;
        }

        public void ViewOrderDetails(int userId, int orderId)
        {
            try
            {
                Order order = _customerRepository.GetOrderbyId(userId, orderId);
                if(order != null)
                {
                    Console.WriteLine($"\n Order Id: {order.Id} " +
                        $"\n Product Name: {order.OrderDetails.First().Inventory.ProductName} " +
                        $"\n Quantity: {order.OrderDetails.First().Quantity} " +
                        $"\n Total Amount: {order.OrderDetails.First().TotalAmount}" +
                        $"\n Order Date: {order.OrderDate:D} " +
                        $"\n Status: {order.OrderDetails.First().OrderStatus}");
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
            }
        }
    }
}
