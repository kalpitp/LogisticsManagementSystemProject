using LogisticsManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository
{
    public interface IAuthenticationRepository
    {

        //For fetching user for login
        User GetUserByUserId(string userId);
        User GetUserById(int userId);

        int AddUser(User user, UserDetail userDetail);

        int GetRoleIdByName(string roleName);
        int GetApprovedStatusById(int userId);
    }
}
