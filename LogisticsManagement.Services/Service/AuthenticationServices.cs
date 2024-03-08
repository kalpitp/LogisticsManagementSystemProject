using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository;
using LogisticsManagement.Services.DTOs;
using LogisticsManagement.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogisticsManagement.Services.Service
{

    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IAuthenticationRepository _repository;
        public UserSession? CurrentUser { get; private set; }


        public AuthenticationServices(IAuthenticationRepository repository)
        {
            _repository = repository;
        }


        //Login 
        public bool Login(string userId, string password)
        {
            try
            {
                // for email validation
                string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

                // check if user id or password is empty
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
                {
                    CommonServices.ErrorMessage("User ID or Password can't be empty");
                }
                else if (!Regex.IsMatch(userId, pattern))
                {
                    CommonServices.ErrorMessage("Invalid User ID");

                }
                else
                {
                    User user = _repository.GetUserByUserId(userId);

                    if (user != null && user.Password == GenerateHashPassword(password))
                    {

                        int ApprovedStatus = _repository.GetApprovedStatusById(user.Id);

                        if (ApprovedStatus == 1)
                        {
                            CurrentUser = new UserSession()
                            {
                                UserId = user.Id,
                                UserEmail = user.Email,
                                UserName= user.Name,
                                RoleId = user.RoleId,
                                Role = user.Role.Name
                            };

                            return true;
                        }
                        else if (ApprovedStatus == -1)
                        {
                            CommonServices.ErrorMessage("Your sign up approval request is rejected");
                        }
                        else if (ApprovedStatus == 0)
                        {
                            CommonServices.WarningMessage("Your sign up approval request is Pending");
                        }
                    }
                    else
                    {
                        CommonServices.ErrorMessage("Login Failed");

                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
                return false;
            }
        }


        //SignUp
        public bool SignUp(UserInfo user, string role)
        {
            try
            {
                if (_repository.GetUserByUserId(user.Email) != null)
                {
                    CommonServices.ErrorMessage("Email ID already exists");
                    return false;
                }

                string hashedPassword = GenerateHashPassword(user.Password);


                User newUser = new User()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = hashedPassword,
                    PhoneNumber = user.PhoneNumber,
                    RoleId = _repository.GetRoleIdByName(role),
                };


                UserDetail? newUserDetail = null;
                if (role == "admin")
                {
                    Console.WriteLine("Logic for admin");
                }
                else if (role == "customer")
                {
                    newUserDetail = new UserDetail()
                    {
                        User = newUser,
                        ShippingAddress = user.ShippingAddress,
                        WarehouseId = null,
                        IsApproved = 1,
                    };
                }
                else if (role == "manager")
                {
                    newUserDetail = new UserDetail()
                    {
                        User = newUser,
                        WarehouseId = 1,
                        IsApproved = 0

                    };
                }
                else if (role == "driver")
                {
                    newUserDetail = new UserDetail()
                    {
                        User = newUser,
                        LicenseNumber = user.LicenseNumber,
                        VehicleType = user.VehicleType,
                        VehicleNumber = user.VehicleNumber,
                        WarehouseId = null,
                        IsApproved = 0
                    };
                }
                else
                {
                    CommonServices.ErrorMessage("Invalid role");
                    return false;
                }

                int result = _repository.AddUser(newUser, newUserDetail);
                if (result > 0)
                {
                    CommonServices.SuccessMessage("Sign Up successfull. Please login to continue");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
                return false;
            }
        }

        public void Logout(UserSession currentUser)
        {
            try
            {
                CurrentUser = null;

            }catch(Exception )
            {
                CommonServices.ErrorMessage("Error while logging out");
            }
        }

        public string GenerateHashPassword(string password)
        {
            try
            {
                using(SHA512 sha512 = SHA512.Create())
                {
                    byte[] hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in hashedBytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }catch(Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
                return null;
            }
        }
    }
}
