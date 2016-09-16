using System;
using System.Collections.Generic;
using System.Web.Http;
using Order.Common.Logger;
using Order.DataLayer.Entities;
using DenodoAdapter;
using Order.DataLayer.Interfaces;
using System.Linq;

namespace Order.DataLayer
{
    public interface ITest
    { string DomainUri { get; set; } }
    public class DataLayerContext : IDataLayerContext, ITest
    {
        private const string LIKE_OPERATOR = "like";
        private const string OrderNAME_FIELD = "sl01002";
        private const string COMPANYCODE_PLACEHOLDER = "{CompanyCode}";
        private readonly IDenodoContext _denodoContext;
        private readonly string _viewUri;

        public string DomainUri { get; set; }

        public DataLayerContext(params object[] theObjects)
        {
            try
            {
                ApplicationLogger.InfoLogger("DataLayer :: constructor :: Reading configuration for Data Layer");
                ConfigReader configReader = new ConfigReader(false);
                ApplicationLogger.InfoLogger(
                    $"DataLayer :: constructor :: Initializing Denodo Adapter Url:{configReader.BaseUri} Username:{configReader.DenodoUsername} Password:{configReader.DenodoPassword}");
                _denodoContext = new DenodoContext(configReader.BaseUri, configReader.DenodoUsername,
                    configReader.DenodoPassword);

                if (theObjects.Length > 0)
                    _denodoContext = (IDenodoContext)theObjects[0];

                _viewUri = configReader.OrderViewUri;
            }
            catch (Exception exception)
            {
                ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                    exception.InnerException);
                throw;
            }
        }


        public List<OrderMaster> GetOrderByName(string companyCode, string OrderName)
        {
            try
            {
                string companyViewUri = _viewUri.Replace(COMPANYCODE_PLACEHOLDER, companyCode);
                string filter = $"{OrderNAME_FIELD} {LIKE_OPERATOR} '%{OrderName}%'";
                ApplicationLogger.InfoLogger($"DataLayer :: GetOrderByName :: Getting Order by name [{OrderName}] with company code [{companyCode}] from denodo url: {companyViewUri} with filter: {filter}");
                var Orders = _denodoContext.SearchData<OrderMaster>(companyViewUri, filter);
                ApplicationLogger.InfoLogger($"Orders count: {Orders.Count}");
                return Orders;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }
        public List<OrderMaster> GetOrders(string companyCode)
        {
            try
            {
                string companyViewUri = _viewUri.Replace(COMPANYCODE_PLACEHOLDER, companyCode);
                ApplicationLogger.InfoLogger($"DataLayer :: GetOrders :: Getting Orders with company code [{companyCode}] from denodo url: {companyViewUri}");
                var Orders = _denodoContext.GetData<OrderMaster>(companyViewUri);
                ApplicationLogger.InfoLogger($"Orders count: {Orders.Count}");
                return Orders;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }
        public OrderMaster GetOrderById(string companyCode, string OrderCode)
        {
            try
            {
                string companyViewUri = _viewUri.Replace(COMPANYCODE_PLACEHOLDER, companyCode);
                ApplicationLogger.InfoLogger($"DataLayer :: GetOrderById :: Getting Order by Id [{OrderCode}] with company code [{companyCode}] from denodo url: {companyViewUri}");
                var data = _denodoContext.GetData<OrderMaster>(companyViewUri, OrderCode);
                return data;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }
        public bool UpdateOrder(OrderMaster orderMaster)
        {
            string companyViewUri = _viewUri;//.Replace(COMPANYCODE_PLACEHOLDER, companyCode);
            return _denodoContext.Update<OrderMaster>(_viewUri, orderMaster.iinc_id.ToString(), orderMaster);

        }

        public bool DeleteOrder(string orderId)
        {
            string companyViewUri = _viewUri;
            return _denodoContext.Delete(companyViewUri, orderId);

        }

        public bool AddOrder(OrderMaster orderMaster)
        {
            string companyViewUri = _viewUri;
            return _denodoContext.Insert<OrderMaster>(_viewUri, orderMaster);
        }

        private static void LogException(Exception exception)
        {
            if (exception.GetType() == typeof(HttpResponseException))
            {
                HttpResponseException responseException = (HttpResponseException)exception;
                ApplicationLogger.Errorlog(responseException.Response.ReasonPhrase, Category.Database,
                    responseException.Response.Content.ReadAsStringAsync().Result, responseException.InnerException);
                ApplicationLogger.InfoLogger(
                    $"Exception from Denodo Adapter :: {responseException.Response.ReasonPhrase} {Environment.NewLine} {responseException.Response.Content.ReadAsStringAsync().Result}");
                throw responseException;
            }
            ApplicationLogger.Errorlog(exception.Message, Category.Database, exception.StackTrace,
                exception.InnerException);
        }

        public bool AddOrderTroubleShoot(OrderTroubleshoot orderTroubleShoot)
        {
            return _denodoContext.Insert<OrderTroubleshoot>(_viewUri, orderTroubleShoot);
        }
    }
}
