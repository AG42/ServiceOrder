using Order.BusinessLayer.Interface;
using Order.DataLayer.Interfaces;
using Order.Model.Response;
using Order.Common.Error;
using Order.Common;
using Order.Common.Logger;
using System;
using Order.Model;
using Order.DataLayer.Entities;

namespace Order.BusinessLayer
{
    public class OrderManager : IOrderManager
    {
        private readonly IDataLayerContext _dataLayerContext = null;

        public OrderManager(IDataLayerContext dataLayerContext)
        {
            // create IOrder instance - Data Layer
            _dataLayerContext = dataLayerContext;
            ApplicationLogger.InfoLogger("Class Name: OrderManager :: Constructor: OrderManager");
        }

        /// <summary>
        /// Get all Order by company code
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public OrderSearchByCompanyCodeResponse GetOrders(string companyCode)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetOrders :: Custome Input: companyCode: [{companyCode}]");
            var response = new OrderSearchByCompanyCodeResponse();
            //  if (!InputValidation.ValidateCompanyCode(companyCode, response))
            // {
            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode Status: Success");
            var result = _dataLayerContext.GetOrders(companyCode);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count}]");
            response.Orders.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger($"Data to Business Model conversion successfull");
            // }

            //ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode Status: Failed");
            return response;
        }

        /// <summary>
        /// Get Order by Order code
        /// </summary>
        /// <param name = "companyCode" ></ param >
        /// < param name="OrderCode"></param>
        /// <returns></returns>
        public OrderSearchByIdResponse GetOrderById(string companyCode, string OrderCode)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetOrderById :: Custome Input: companyCode: [{companyCode}] And OrderCode: [{OrderCode}]");
            var response = new OrderSearchByIdResponse();
            // if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateOrderCode(OrderCode, response))
            //{
            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and OrderCode Status: Success");
            var result = _dataLayerContext.GetOrderById(companyCode, OrderCode);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            //  response.OrderModel = Converter.Convert(result, companyCode);
            ApplicationLogger.InfoLogger($"Data to Business Model conversion successfull");
            //  }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and OrderCode Status: Failed");
            return response;
        }

        public OrderSearchByNameResponse GetOrderByName(string companyCode, string OrderName)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetOrderByName :: Custome Input: companyCode: [{companyCode}] And OrderName: [{OrderName}]");
            var response = new OrderSearchByNameResponse();
            //  if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateOrderName(OrderName, response))
            //{
            var result = _dataLayerContext.GetOrderByName(companyCode, OrderName);
            if (result == null)
            {
                ApplicationLogger.InfoLogger("Error: No Data Found. Data Lenght is 0");
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                return response;
            }

            ApplicationLogger.InfoLogger($"Data Lenght: [{result.Count}]");
            //     response.Orders.AddRange(Converter.Convert(result, companyCode));
            ApplicationLogger.InfoLogger($"Data to Business Model conversion successfull");
            //  }

            return response;
        }

        public bool UpdateOrder(OrderModel orderModel)
        {
            return _dataLayerContext.UpdateOrder(Converter.Convert(orderModel));
        }

        public bool DeleteOrder(string orderId)
        {
            return _dataLayerContext.DeleteOrder(orderId);
        }

        public bool AddOrder(OrderModel orderModel)
        {
            return _dataLayerContext.AddOrder(Converter.Convert(orderModel));
        }
    }
}
