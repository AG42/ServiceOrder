using System.Collections.Generic;

namespace Order.Model.Response
{
    public class OrderSearchByCompanyCodeResponse: BaseResponse 
    {
        private List<OrderModel> _Orders = new List<OrderModel>();
        public List<OrderModel> Orders { get { return _Orders; } }
    }
}
