using LogisticsManagement.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.UI
{
    public class ManagerMenu
    {
        private readonly IAuthenticationServices _authService;

        public ManagerMenu()
        {

        }

        public ManagerMenu(IAuthenticationServices authService)
        {
            _authService = authService;
        }
        // Manager main menu
        public void ShowMenu()
        {
            try
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\nHello, {_authService.CurrentUser.UserName}");
                Console.ResetColor();
                Console.WriteLine("\n<<<<<<< MANAGER MENU >>>>>>>");
                Console.WriteLine("\n1. Manage Inventory");
                Console.WriteLine("2. Assign Driver to Order");
                Console.WriteLine("3. Update Order Status");
                Console.WriteLine("4. View Orders");
                Console.WriteLine("5. Logout");
                Console.WriteLine("6. Exit");
                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Manage Inventory
                        ManageInventoryMenu();
                        break;
                    case "2":
                        // Assign Driver to Order
                        AssignDriver();
                        break;
                    case "3":
                        // Update Order Status
                        UpdateOrderStatus(); // Method to update the status of orders
                        break;
                    case "4":
                        // View Orders
                        Console.WriteLine("1. Order1\n 2. Order2");
                        //ViewOrders(); // Method to view orders
                        break;
                    case "5":
                        // Logout
                        _authService.Logout(_authService.CurrentUser);
                        break;
                    case "6":
                        // Exit
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        //---------------------------------------------------------------------

        // Manage Inventory Menu
        public  void ManageInventoryMenu()
        {
            try
            {


                Console.WriteLine("\n<<<<<<< MANAGE INVENTORY >>>>>>>");
                Console.WriteLine("1. View All Products");
                Console.WriteLine("2. Add New Products");
                Console.WriteLine("3. Update Products");
                Console.WriteLine("4. Delete Products");
                Console.WriteLine("5. Back to Manager Menu");

                Console.WriteLine("\nEnter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Product listing
                        Console.WriteLine("view Products");
                        Console.WriteLine("1. prod1\n prod2");
                        break;
                    case "2":
                        AddNewProduct();
                        break;
                    case "3":
                        Console.WriteLine("Update Product");
                        break;
                    case "4":
                        DeleteProduct();
                        break;
                    case "5":
                        ShowMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Add Product
        public void AddNewProduct()
        {
            try
            {
                Console.Write("Enter Product Name: ");
                string productName = Console.ReadLine();

                Console.Write("Enter Product Quantity: ");
                int productQuantity = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Product Added");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Delete Product
        public void DeleteProduct()
        {
            try
            {


                Console.WriteLine($"\n<<<<<<< DELETE PRODUCT >>>>>>>");
                Console.WriteLine($"1. Delete product");
                Console.WriteLine($"2. Back to Manage Inventory");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Enter ID to Delete
                        Console.WriteLine($"1. Enter product ID to Delete");
                        int id = Int32.Parse(Console.ReadLine());
                        break;
                    case "2":
                        // Back to Manage Inventory
                        ManageInventoryMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //----------------------------------------------------------------------
        public void AssignDriver()
        {
            try
            {
                Console.WriteLine("\n<<<<<<< ASSIGN DRIVER TO ORDER >>>>>>>");
                Console.WriteLine("Orders\n 1. ord1\n 2.ord2");
                Console.WriteLine("\nDriver\n 1. Drive1\n 2.Driver2");

                Console.WriteLine("1. Assign Driver to Order");
                Console.WriteLine("2. Back to Manager Menu");
                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Select Order
                        Console.Write("Enter Order ID: ");
                        int orderId = Int32.Parse(Console.ReadLine());
                        Console.Write("Select Driver ID: ");
                        int driverId = Int32.Parse(Console.ReadLine());


                        Console.WriteLine("1. Confirm Assignment");
                        Console.WriteLine("2. Back to Manager Menu");
                        string choice2 = Console.ReadLine();

                        if (choice2 == "1")
                        {
                            Console.WriteLine("Driver Assigned");
                        }
                        else if (choice2 == "2")
                        {
                            ShowMenu();
                        }
                        else Console.WriteLine("Invalid Choice");
                        break;
                    case "2":
                        //Back to Manager menu
                        ShowMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //----------------------------------------------------------------------

        // Update order status
        public void UpdateOrderStatus()
        {
            try
            {
                Console.WriteLine("\n<<<<<<< UPDATE ORDER STATUS >>>>>>>");

                Console.Write("\nEnter Order ID: ");
                int orderId = Int32.Parse(Console.ReadLine());
                Console.Write("Select Status: ");
                string orderStatus = Console.ReadLine();

                Console.WriteLine("1. Confirm Update");
                Console.WriteLine("2. Back to Manager Menu");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Status Updated");
                }
                else if (choice == "2")
                {
                    ShowMenu();
                }
                else Console.WriteLine("Invalid Choice");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
