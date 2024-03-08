using LogisticsManagement.Services.Interface;
using LogisticsManagement.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.UI
{
    public class CustomerMenu
    {
        private readonly IAuthenticationServices _authService;
        private readonly ICustomerServices _customerService;
        public CustomerMenu()
        {

        }

        public CustomerMenu(IAuthenticationServices authService, ICustomerServices customerService)
        {
            _authService = authService;
            _customerService = customerService;
        }

        // Main customer menu
        public void ShowMenu()
        {
            try
            {

                //Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\nHello, {_authService.CurrentUser.UserName}");
                Console.ResetColor();
                Console.WriteLine("\n<<<<<<< CUSTOMER MENU >>>>>>>>");
                Console.WriteLine("\n1. View Products");
                Console.WriteLine("2. View Orders");
                Console.WriteLine("3. Logout");
                Console.WriteLine("4. Exit");

                Console.WriteLine("\nEnter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        // Submenu for product
                        ProductMenu();
                        break;
                    case 2:
                        // Submenu for order
                        OrderMenu();
                        break;
                    case 3:
                        _authService.Logout(_authService.CurrentUser);
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Product

        // Submenu for View Product
        public void ProductMenu()
        {
            try
            {
                //Console.Clear();
                Console.WriteLine(Environment.NewLine);

                // List all products
                if (_customerService.ViewAllProducts())
                {
                    Console.WriteLine("\n1. View Product Details ");
                    Console.WriteLine("2. Back to customer ");

                    Console.WriteLine("\nEnter your choice: ");


                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("\nEnter product id you want to view details of:");
                            int sNo = Convert.ToInt32(Console.ReadLine());
                            if (!Validations.ValidateIntInputChoice(sNo)) ProductMenu();

                            ProductDetailsMenu(sNo);
                            break;
                        case "2":
                            ShowMenu();
                            break;
                        default:
                            Console.WriteLine("Invalid Choice");
                            ShowMenu();
                            break;

                    }

                }
                else
                {
                    ShowMenu();
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Submenu for product details
        public void ProductDetailsMenu(int sNo)
        {
            try
            {
                Console.WriteLine("\n<<<<<<< PRODUCT DETAILS >>>>>>>>");

                // View product details
                _customerService.ViewProductDetails(sNo);

                Console.WriteLine("\n1. Buy Product");
                Console.WriteLine("2. Back to Browse Products");
                Console.WriteLine("3. Back to Customer Menu");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Buy the product
                        BuyProductMenu(sNo);
                        break;
                    case "2":
                        // Back to Browse Products
                        ProductMenu();
                        break;
                    case "3":
                        // Back to Customer Menu
                        ShowMenu();
                        break;
                    default:
                        CommonServices.ErrorMessage("Invalid choice");
                        ShowMenu() ;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        // Submenu for buy product
        public void BuyProductMenu(int productId)
        {
            try
            {

                // When the user selects "Buy Product"
                Console.WriteLine("\n<<<<<<< BUY PRODUCT >>>>>>>>");

                Console.WriteLine("Enter Quantity");
                int quantity = Convert.ToInt32(Console.ReadLine());
                if (!Validations.ValidateIntInputChoice(quantity)) BuyProductMenu(productId);

                if (!_customerService.ValidateQuantity(productId, quantity))
                {
                    CommonServices.ErrorMessage("Enter quantity less than available quantity");
                    BuyProductMenu(productId);
                }

                Console.WriteLine("1. Confirm Purchase");
                Console.WriteLine("2. Back to Product Details");

                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        // Buy Product
                        bool isBuySuccess = _customerService.BuyProduct(_authService.CurrentUser.UserId, productId, quantity);
                        if (isBuySuccess) CommonServices.SuccessMessage("Product purchased successfully");
                        else CommonServices.ErrorMessage("Product purchase failed");
                        ShowMenu();
                        break;
                    case 2:
                        // Back to Product Details
                        ShowMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //ORDER

        // Submenu for View order
        public void OrderMenu()
        {
            try
            {
                if (_customerService.ViewAllOrders(_authService.CurrentUser.UserId))
                {
                    Console.WriteLine("1. View Order Details");
                    Console.WriteLine("2. Back to Customer Menu");

                    Console.Write("Enter your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());


                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("\nEnter order id you want to view details of:");
                            int sNo = Convert.ToInt32(Console.ReadLine());
                            if (!Validations.ValidateIntInputChoice(sNo)) OrderMenu();

                            OrderDetailsMenu(sNo);
                            break;
                        case 2:
                            // Back to Customer Menu
                            ShowMenu();
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }

                }
                else
                {
                    ShowMenu();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Submenu for order details
        public void OrderDetailsMenu(int sNo = 0)
        {
            try
            {
                Console.WriteLine("\n<<<<<<< ORDER DETAILS >>>>>>>>");

                _customerService.ViewOrderDetails(_authService.CurrentUser.UserId, sNo);

                ShowMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
