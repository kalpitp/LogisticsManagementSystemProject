using LogisticsManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository
{
    public interface IAdminRepository
    {
        List<User> GetAllUsersByType(int userRoleId);
        List<User> GetAllPendingUsersByType(int userRoleId);

        int UpdateSignUpRequest(int userIdToUpdate, int updatedState);

        int AddDriverToResource(Resource resource);

        int DeleteUserById(int userIdToDelete);
    }
}
