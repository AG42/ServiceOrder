using Order.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataLayer.Interfaces
{
    public interface IDataLayerContext
    {
        List<OrderMaster> GetOrders(string companyCode);
        OrderMaster GetOrderById(string companyCode, string OrderCode);
        List<OrderMaster> GetOrderByName(string companyCode, string OrderName);
        bool UpdateOrder(OrderMaster master);
        bool DeleteOrder(string orderId);
        bool AddOrder(OrderMaster master);
        bool AddOrderTroubleShoot(OrderTroubleshoot orderTroubleShoot);
    }
}
