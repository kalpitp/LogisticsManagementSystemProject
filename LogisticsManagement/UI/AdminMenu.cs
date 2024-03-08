using LogisticsManagement.Services.Interface;
using LogisticsManagement.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.UI
{
    public class AdminMenu
    {
        private readonly IAuthenticationServices _authService;
        private readonly IAdminServices _adminService;


        public AdminMenu()
        {

        }
        public AdminMenu(IAuthenticationServices authService, IAdminServices adminService)
        {
            _authService = authService;
            _adminService = adminService;
        }

        // Main menu for Admin
        public void ShowMenu()
        {
            try
            {


                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\nHello, {_authService.CurrentUser.UserName}");
                Console.ResetColor();
                Console.WriteLine("\n<<<<<<< ADMIN MENU >>>>>>>");
                Console.WriteLine("\n1. Manage Customers");
                Console.WriteLine("2. Manage Warehouse Managers");
                Console.WriteLine("3. Manage Drivers");
                Console.WriteLine("4. Logout");
                Console.WriteLine("5. Exit");
                Console.WriteLine("\nEnter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ManageCustomerMenu();
                        break;
                    case 2:
                        ManageManagerMenu();
                        break;
                    case 3:
                        ManageDriverMenu();
                        break;
                    case 4:
                        _authService.Logout(_authService.CurrentUser);
                        break;
                    case 5:
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

        // Submenu for manage customer
        public void ManageCustomerMenu()
        {
            try
            {
                // Manage Customers submenu
                Console.WriteLine("\n<<<<<<< MANAGE CUSTOMER >>>>>>>");
                Console.WriteLine("1. View All Customers");
                Console.WriteLine("2. Delete Customer");
                Console.WriteLine("3. Back to Admin Menu");

                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // View All Customers
                        CommonViewAllUser("customer");
                        break;
                    case "2":
                        // Delete Customer
                        CommonDeleteUser("customer");
                        break;
                    case "3":
                        // Back to Admin Menu
                        ShowMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        ManageCustomerMenu();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Submenu for manage manager
        public void ManageManagerMenu()
        {
            try
            {
                // Manage Customers submenu
                Console.WriteLine("\n<<<<<<< MANAGE WAREHOUSE MANAGER >>>>>>>");
                Console.WriteLine("1. View All Managers");
                Console.WriteLine("2. Manager Signup Approval");
                Console.WriteLine("3. Delete Manager");
                Console.WriteLine("4. Back to Admin Menu");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // View All Customers
                        CommonViewAllUser("manager");
                        break;
                    case "2":
                        // Manager's approval for signup
                        CommonApproveUser("manager");
                        break;
                    case "3":
                        // Delete Customer
                        CommonDeleteUser("manager");
                        break;
                    case "4":
                        // Back to Admin Menu
                        ShowMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        ManageManagerMenu();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        // Submenu for manage driver
        public void ManageDriverMenu()
        {
            try
            {
                // Manage Driver submenu
                Console.WriteLine("\n<<<<<<< MANAGE Driver >>>>>>>");
                Console.WriteLine("1. View All Drivers");
                Console.WriteLine("2. Driver Signup Approval");
                Console.WriteLine("3. Delete Driver");
                Console.WriteLine("4. Back to Admin Menu");

                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // View All Customers
                        CommonViewAllUser("driver");
                        break;
                    case "2":
                        // Manager's approval for signup
                        CommonApproveUser("driver");
                        break;
                    case "3":
                        // Delete Customer
                        CommonDeleteUser("driver");
                        break;
                    case "4":
                        // Back to Admin Menu
                        ShowMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        ManageDriverMenu();
                        break;
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        //-------------------------------------------------------------------------

        //Menu for listing user
        public void CommonViewAllUser(string userType)
        {
            try
            {

                if (userType == "customer")
                {
                    //View customer
                    _adminService.ViewAllUser(userType, _authService.CurrentUser.RoleId);
                    ManageCustomerMenu();
                }
                else if (userType == "manager")
                {
                    // view manager
                    _adminService.ViewAllUser(userType, _authService.CurrentUser.RoleId);
                    ManageManagerMenu();

                }
                else if (userType == "driver")
                {
                    // view driver
                    _adminService.ViewAllUser(userType, _authService.CurrentUser.RoleId);
                    ManageDriverMenu();

                }
                else
                {
                    Console.WriteLine("Invalid User Type");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        // Menu for approval of manager or driver
        public void CommonApproveUser(string userType)
        {
            try
            {
                Console.WriteLine($"\n<<<<<<< {userType.ToUpper()} SIGNUP APPROVAl >>>>>>>");
                _adminService.ViewAllPendingUser(userType, _authService.CurrentUser.RoleId);

                // Inside the Admin menu

                Console.WriteLine($"1. Approve {userType[0].ToString().ToUpper() + userType.Substring(1)} Signup");
                Console.WriteLine($"2. Reject {userType[0].ToString().ToUpper() + userType.Substring(1)} Signup");
                Console.WriteLine($"3. Back to Admin Menu");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Approve Signup request

                        Console.WriteLine($"\nEnter {userType[0].ToString().ToUpper() + userType.Substring(1)} id you want to approve:");
                        int sNoApprove = Convert.ToInt32(Console.ReadLine());

                        bool result = _adminService.UpdateUserSignUpRequest(sNoApprove, _authService.CurrentUser.RoleId, 1);

                        if (result)
                            CommonServices.SuccessMessage("Status updated successfully");
                        else
                            CommonServices.ErrorMessage("Failed to update Status");

                        if (userType == "manager")
                            ManageManagerMenu();
                        else if (userType == "driver")
                            ManageDriverMenu();


                        break;

                    case "2":
                        // Reject Signup request

                        Console.WriteLine($"\nEnter {userType[0].ToString().ToUpper() + userType.Substring(1)} id you want to reject:");
                        int sNoReject = Convert.ToInt32(Console.ReadLine());

                        //will update status 
                        bool result2 = _adminService.UpdateUserSignUpRequest(sNoReject, _authService.CurrentUser.RoleId, -1);

                        if (result2)
                            CommonServices.SuccessMessage("Status updated successfully");
                        else
                            CommonServices.ErrorMessage("Failed to update Status");

                        if (userType == "manager")
                            ManageCustomerMenu();
                        else if (userType == "driver")
                            ManageDriverMenu();

                        break;

                    case "3":
                        // Back to Admin Menu
                        ShowMenu(); // Method to display the main Admin menu
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


        // Menu for deletion of manager or driver or customer

        public void CommonDeleteUser(string userType)
        {
            try
            {

                Console.WriteLine($"\n<<<<<<< DELETE {userType.ToUpper()} >>>>>>>");

                //list all user
                _adminService.ViewAllUser(userType, _authService.CurrentUser.RoleId);



                Console.WriteLine($"1. Delete {string.Concat(userType[0].ToString().ToUpper(), userType.AsSpan(1))} ");
                Console.WriteLine($"2. Back to Manage {string.Concat(userType[0].ToString().ToUpper(), userType.AsSpan(1))}");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Enter ID to Delete
                        Console.WriteLine($"Enter {userType[0].ToString().ToUpper() + userType.Substring(1)} ID to Delete");
                        int id = Int32.Parse(Console.ReadLine());

                        bool isDeleted = _adminService.DeleteUser(id, _authService.CurrentUser.RoleId);


                        if (isDeleted)
                            CommonServices.SuccessMessage("User deleted successfully");
                        else
                            CommonServices.ErrorMessage("Failed to delete user");

                        if (userType == "customer") ManageCustomerMenu();
                        else if (userType == "manager") ManageManagerMenu();
                        else if (userType == "driver") ManageDriverMenu();

                        break;

                    case "2":
                        // Back to Manage Customers
                        if (userType == "customer") ManageCustomerMenu();
                        else if (userType == "manager") ManageManagerMenu();
                        else if (userType == "driver") ManageDriverMenu();

                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        ShowMenu();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

}
