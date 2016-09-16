using System;
using System.Collections.Generic;
using Order.DataLayer.Entities;
using Order.Model;

namespace Order.BusinessLayer
{
    class Converter
    {
        public static List<OrderModel> Convert(List<OrderMaster> OrderMasters, string companyCode)
        {
            var OrderModels = new List<OrderModel>();
            foreach (var OrderMaster in OrderMasters)
                OrderModels.Add(Convert(OrderMaster, companyCode));

            return OrderModels;
        }
        public static OrderModel Convert(OrderMaster OrderMaster, string companyCode)
        {
            return new OrderModel()
            {
                IncriptionId = OrderMaster.iinc_id,
                Summary = OrderMaster.summary,
                Specific_Field1 = OrderMaster.specific_field1,
                Specific_Field2 = OrderMaster.specific_field2,
                TaxId = OrderMaster.taxid,
                Time = OrderMaster.ttime
            };
        }
       public static OrderMaster Convert(OrderModel orderModel)
        {
            return new OrderMaster()
            {
                iinc_id = orderModel.IncriptionId,
                specific_field1 = orderModel.Specific_Field1,
                specific_field2 = orderModel.Specific_Field2,
                taxid = orderModel.TaxId,
                summary = orderModel.Summary,
                ttime = orderModel.Time
            };
        }
    }
}
