using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Model.Response
{
    public class OrderSearchByIdResponse: BaseResponse
    {
        public OrderModel OrderModel { get; set; }
    }
}
