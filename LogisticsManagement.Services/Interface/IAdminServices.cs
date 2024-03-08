using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Services.Interface
{
    public interface IAdminServices
    {
        void ViewAllUser(string userType,int loggedUserRollId);

        void ViewAllPendingUser(string userType, int loggedUserRollId);

        bool UpdateUserSignUpRequest(int userIdToUpdate, int loggedUserRollId, int updatedState);

        bool DeleteUser(int userIdToDelete, int loggedUserRollId);
    }
}
