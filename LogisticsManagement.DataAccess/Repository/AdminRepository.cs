using LogisticsManagement.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CybageLogisticsContext _context;

        public AdminRepository(CybageLogisticsContext dbContext)
        {
            _context = dbContext;
        }

        public List<User> GetAllUsersByType(int userRoleId)
        {
            // Return all user by roll id
            return _context.Users.Include(u => u.UserDetails).Where(e=>e.RoleId == userRoleId).ToList();

        }
        public List<User> GetAllPendingUsersByType(int userRoleId)
        {

            return _context.Users.Include(u => u.UserDetails).Where(e => e.RoleId == userRoleId && e.UserDetails.Any(ud => ud.IsApproved == 0)).ToList();
        }

        public int UpdateSignUpRequest(int userIdToUpdate, int updatedState)
        {
            try
            {
                UserDetail userDetail = _context.UserDetails.FirstOrDefault(ud => ud.UserId == userIdToUpdate);

                if (userDetail != null)
                {
                    userDetail.IsApproved = updatedState;
                    return _context.SaveChanges();
                }
            }

            catch (Exception)
            {
                Console.WriteLine("An Error occured while updating user");
            }
            return -1;
        }

        public int DeleteUserById(int userIdToDelete)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(u => u.Id == userIdToDelete);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    return _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("");
            }
            return 0;
        }

        public int AddDriverToResource(Resource resource)
        {
            try
            {
                _context.Resources.Add(resource);
                return _context.SaveChanges();

            }catch (Exception)
            {
                Console.WriteLine("An Error occured while adding driver to resource");
            }
            return 0;
        }

    }
}

