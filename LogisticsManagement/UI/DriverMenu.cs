using LogisticsManagement.Services.Interface;
using LogisticsManagement.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.UI
{
    public class DriverMenu
    {
        private readonly IAuthenticationServices _authService;
        private readonly IDriverServices _driverService;

        public DriverMenu()
        {
        }

        public DriverMenu(IAuthenticationServices authService, IDriverServices driverService)
        {
            _authService = authService;
            _driverService = driverService;

        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\nHello, {_authService.CurrentUser.UserName}");
            Console.ResetColor();
            Console.WriteLine("\n<<<<<<< DRIVER MENU >>>>>>>");
            Console.WriteLine("1. View Assigned Orders");
            Console.WriteLine("2. Update Order Status");
            Console.WriteLine("3. Logout");
            Console.WriteLine("4. Exit");
            Console.WriteLine("\nEnter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // View Assigned Orders
                    ViewAssignedOrders();
                    break;
                case 2:
                    UpdateOrderStatus();
                    break;
                case 3:
                    _authService.Logout(_authService.CurrentUser);
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }

        public void ViewAssignedOrders()
        {
            // Inside the Driver menu
            Console.WriteLine("\n<<<<<<< VIEW ASSIGNED ORDERS >>>>>>>");
            _driverService.ViewAssignedOrders(_authService.CurrentUser.UserId);

            Console.WriteLine("1. Back to Driver Menu");

            Console.Write("\nEnter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    ShowMenu();
                    break;
            }

        }

        public void UpdateOrderStatus()
        {
            Console.WriteLine("\n<<<<<<< UPDATE ORDER STATUS >>>>>>>");

            _driverService.ViewAssignedOrders(_authService.CurrentUser.UserId);


            Console.Write("\nEnter Order ID: ");
            int orderId = Convert.ToInt32(Console.ReadLine());
            Validations.ValidateIntInputChoice(orderId);


            Console.Write("Select Status: ");
            string orderStatus = Console.ReadLine();

            Console.WriteLine("1. Confirm Update");
            Console.WriteLine("2. Back to Manager Menu");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                bool isUpdated = _driverService.UpdateAssignedOrderStatus(_authService.CurrentUser.UserId, orderId, orderStatus);
                if (isUpdated)
                    CommonServices.SuccessMessage("Status updated successfully");
                else
                    CommonServices.ErrorMessage("Failed to update Status");
            }
            else if (choice == "2")
            {
                ShowMenu();
            }
            else
            {
                Console.WriteLine("Invalid Choice");
                ShowMenu();
            };
        }
    }
}
