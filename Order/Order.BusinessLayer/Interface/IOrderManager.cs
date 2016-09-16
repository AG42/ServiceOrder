using Order.Model;
using Order.Model.Requests;
using Order.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.BusinessLayer.Interface
{
    public interface IOrderManager
    {
        OrderSearchByCompanyCodeResponse GetOrders(string companyCode);
        OrderSearchByIdResponse GetOrderById(string companyCode, string OrderCode);
        OrderSearchByNameResponse GetOrderByName(string companyCode, string OrderName);
        bool UpdateOrder(OrderModel orderManager);
        bool DeleteOrder(string orderId);
        bool AddOrder(OrderModel orderManager);
    }
}
