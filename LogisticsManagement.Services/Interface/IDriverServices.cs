using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Services.Interface
{
    public interface IDriverServices
    {
        public void ViewAssignedOrders(int userId);

        bool UpdateAssignedOrderStatus(int userId,int orderDetailId, string orderStatus);
    }
}
