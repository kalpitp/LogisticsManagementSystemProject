using LogisticsManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository
{
    public interface IDriverRepository
    {
        List<Resource> GetAssignedOrder(int userId);
        int UpdateOrderStatus(int userId, int orderDetailId, string orderStatus);

    }
}
