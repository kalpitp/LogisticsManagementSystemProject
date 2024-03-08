using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository;
using LogisticsManagement.Services.DTOs;
using LogisticsManagement.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Services.Service
{
    public class AdminServices : IAdminServices
    {
        private readonly IAuthenticationRepository _authRepository;
        private readonly IAdminRepository _adminRepository;


        public AdminServices(IAuthenticationRepository authRepository, IAdminRepository adminRepository)
        {
            _authRepository = authRepository;
            _adminRepository = adminRepository;
        }

        public void ViewAllUser(string userType, int loggedUserRollId)
        {
            try
            {
                // check if user is admin or not
                if (loggedUserRollId == 1)
                {

                    //return roleid of user
                    int userRollId = _authRepository.GetRoleIdByName(userType);

                    // check if role id is not empty
                    if (userRollId != -1)
                    {

                        List<User> users = _adminRepository.GetAllUsersByType(userRollId);

                        if (users.Count == 0)
                        {
                            CommonServices.ErrorMessage($"No {userType} found!!");
                        }
                        else
                        {
                            Console.WriteLine(Environment.NewLine);

                            foreach (User user in users)
                            {

                                Console.Write($" Id: {user.Id} | Name:{user.Name} | Email: {user.Email} | Phone Number: {user.PhoneNumber}");

                                foreach (var details in user.UserDetails)
                                {
                                    string approveDetail = "";
                                    if (details.IsApproved == -1) approveDetail = "Rejected";
                                    else if (details.IsApproved == 0) approveDetail = "Pending";
                                    else if (details.IsApproved == 1) approveDetail = "Approved";
                                    Console.Write($" | Approved Status: {approveDetail}\n");
                                }
                            }
                            Console.WriteLine(Environment.NewLine);

                        }
                    }
                }
                else
                {
                    CommonServices.ErrorMessage("You are not authorized!!");
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
            }
        }

        public void ViewAllPendingUser(string userType, int loggedUserRollId)
        {
            try
            {
                //return roleid of user
                int userRollId = _authRepository.GetRoleIdByName(userType);

                // check if role id is not empty
                if (userRollId != -1)
                {
                    List<User> users = _adminRepository.GetAllPendingUsersByType(userRollId);

                    if (users.Count == 0)
                    {
                        CommonServices.ErrorMessage($"No {userType} found!!");
                    }
                    else
                    {
                        Console.WriteLine(Environment.NewLine);

                        foreach (User user in users)
                        {

                            Console.Write($" Id: {user.Id} | Name:{user.Name} | Email: {user.Email} | Phone Number: {user.PhoneNumber}");

                            foreach (var details in user.UserDetails)
                            {
                                string pendingName = details.IsApproved == 0 ? "Pending" : "";
                                Console.Write($" | Approved Status: {pendingName}\n");
                            }
                        }
                        Console.WriteLine(Environment.NewLine);

                    }
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
            }
        }

        public bool UpdateUserSignUpRequest(int userIdToUpdate, int loggedUserRollId, int updatedState)
        {
            try
            {
                // check if user is in database
                User user = _authRepository.GetUserById(userIdToUpdate);
                int rowUpdated = 0;
                if (user != null)
                {
                    rowUpdated = _adminRepository.UpdateSignUpRequest(userIdToUpdate, updatedState);
                }

                if (rowUpdated > 0 )
                {
                    //check if user role is driver or not
                    if (user?.RoleId == 4 && updatedState == 1)
                    {
                        Resource resource = new Resource
                        {
                            UserId = user.Id,
                            IsAvailable = true,
                        };

                        int res = _adminRepository.AddDriverToResource(resource);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
            }
            return false;
        }

        public bool DeleteUser(int userIdToDelete, int loggedUserRollId)
        {
            try
            {
                // check if user is in database
                User user = _authRepository.GetUserById(userIdToDelete);

                int rowDeleted = 0;
                if (user != null)
                {
                    rowDeleted = _adminRepository.DeleteUserById(userIdToDelete);
                }

                if (rowDeleted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CommonServices.ErrorMessage(ex.Message);
                return false;
            }
        }
    }
}
