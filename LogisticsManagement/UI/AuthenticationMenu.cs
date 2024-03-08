using LogisticsManagement.Services.DTOs;
using LogisticsManagement.Services.Interface;
using LogisticsManagement.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogisticsManagement.UI
{
    public class AuthenticationMenu
    {

        private readonly IAuthenticationServices _authService;
        private readonly IAdminServices _adminService;
        private readonly ICustomerServices _customerService;
        private readonly IDriverServices _driverService;

        public AuthenticationMenu(IAuthenticationServices authService, IAdminServices adminService,ICustomerServices customerService, IDriverServices driverService)
        {
            _authService = authService;
            _adminService = adminService;
            _customerService = customerService;
            _driverService = driverService;

        }

        // For converting password to stars
        static string ReadPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                // remove last character from password
                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                // on any key other than control characters add it to the password
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
            return pass;
        }

        // Login
        public void Login()
        {
            try
            {


                Console.Clear();
                Console.WriteLine("\n============ Sign In ============");
                Console.Write("Enter UserID: ");
                string? userId = Console.ReadLine();

                Console.Write("Enter Password: ");
                string? password = ReadPassword();

                Console.WriteLine("1. Login\n2. Back to Main Menu");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    bool isSuccess = _authService.Login(userId, password);
                    if (isSuccess)
                    {
                        CommonServices.SuccessMessage("Login Successfull");
                        if (_authService.CurrentUser.Role == "admin") new AdminMenu(_authService, _adminService).ShowMenu();
                        else if (_authService.CurrentUser.Role == "manager") new ManagerMenu(_authService).ShowMenu();
                        else if (_authService.CurrentUser.Role == "customer") new CustomerMenu(_authService, _customerService).ShowMenu();
                        else if (_authService.CurrentUser.Role == "driver") new DriverMenu(_authService,_driverService).ShowMenu();
                        else Console.WriteLine("Invalid Role!! No menu to display");

                    }
                    //else
                    //{
                    //    CommonServices.ErrorMessage("Login Failed");
                    //}

                }
                else if (option == 2)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Signup
        public void SignUp()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("\n============ Sign Up ============");
                Console.WriteLine("\nWant to sign Up as?");
                Console.WriteLine("1. Admin \n2. Customer\n3. Warehouse Manager\n4.Driver\n5.Back to Main Menu");
                Console.WriteLine("\nEnter your choice: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Admin");
                        break;
                    case 2:
                        CustomerDetails();
                        break;
                    case 3:
                        ManagerDetails();
                        break;
                    case 4:
                        DriverDetails();
                        break;
                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        UserInfo info = new UserInfo();

        public void CommonDetails()
        {
            try
            {

                Console.Write("Enter Name: ");
                string? name = Console.ReadLine();
                if(!Validations.ValidateName(name))CommonDetails();

                Console.Write("Enter Email: ");
                string? email = Console.ReadLine();
                if(!Validations.validateEmail(email))CommonDetails();

                Console.Write("Enter Phone Number: ");
                string? phoneNumber = Console.ReadLine();
                if(!Validations.validatePhoneNumber(phoneNumber))CommonDetails();

                Console.Write("Enter Password: ");
                string? password = ReadPassword();
                if(!Validations.validatePassword(password))CommonDetails();

                Console.Write("Enter Confirm Password:");
                string? confirmPassword = ReadPassword();



                //if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
                //{
                //    CommonServices.ErrorMessage("Invalid Input");
                //    CommonDetails();
                //}
                if (password != confirmPassword)
                {
                    CommonServices.ErrorMessage("Both Password and comfirm password should be same");
                    CommonDetails();
                }


                info.Name = name;
                info.Email = email;
                info.PhoneNumber = phoneNumber;
                info.Password = password;
                info.ConfirmPassword = confirmPassword;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void CustomerDetails()
        {
            try
            {
                CommonDetails();
                Console.Write("Enter Address: ");
                string? address = Console.ReadLine();

                info.ShippingAddress = address;

                bool isSuccess = _authService.SignUp(info, "customer");

                if (isSuccess)
                {
                    return;
                }
                else
                {
                    CommonServices.ErrorMessage("Sign Up Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ManagerDetails()
        {
            try
            {
                CommonDetails();
                //Console.Write("Enter Warehouse Name: ");
                //string? warehouseName = Console.ReadLine();

                bool isSuccess = _authService.SignUp(info, "manager");

                if (isSuccess)
                {
                    //Login();
                    return;
                }
                else
                {
                    CommonServices.ErrorMessage("Sign Up Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DriverDetails()
        {
            try
            {
                CommonDetails();
                Console.Write("Enter Licence Number: ");
                string? licenceNumber = Console.ReadLine();
                if(!Validations.ValidateLicenseNumber(licenceNumber))DriverDetails();

                Console.Write("Vehicle Licence Type: ");
                string? vehicleType = Console.ReadLine();

                Console.Write("Enter Vehicle Number: ");
                string? vehicleNumber = Console.ReadLine();
                if(!Validations.ValidateVehicleNumber(vehicleNumber))DriverDetails();

                info.LicenseNumber = licenceNumber;
                info.VehicleType = vehicleType;
                info.VehicleNumber = vehicleNumber;

                bool isSuccess = _authService.SignUp(info, "driver");

                if (isSuccess)
                {
                    return;
                }
                else
                {
                    CommonServices.ErrorMessage("Sign Up Failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
