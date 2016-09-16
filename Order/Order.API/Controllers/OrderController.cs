using System;
using System.Globalization;
using Order.BusinessLayer.Interface;
using System.Net;
using System.Web.Http;
using Order.Common;
using System.Net.Http;
using Order.Common.Enum;
using Order.Common.Logger;

namespace Order.API.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : BaseContoller
    {
        readonly IOrderManager _OrderManager;
        public OrderController(IOrderManager OrderManager)
        {
            _OrderManager = OrderManager;
        }



        /// <summary>
        /// Get all the Order base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{companyCode}")]
        [Route("companyCode/{companyCode}")]
        public IHttpActionResult GetOrders(string companyCode)
        {
            ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: OrderControllerMethodName: GetOrders :: Custome Input: [{companyCode}]");
            try
            {
                var response = _OrderManager.GetOrders(companyCode);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :: ItemLegth: [{response.Orders.Count}]");
                    return Ok(response.Orders);
                }

                ApplicationLogger.InfoLogger("Response Status: Failuer");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger("Exception: HttpResponseException");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger("Exception: BaseException");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Get all the Order base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="OrderCode"></param>
        /// <returns></returns>
        [Route("companyCode/{companyCode}/OrderCode/{OrderCode}")]
        public IHttpActionResult GetOrderById(string companyCode, string OrderCode)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: OrderControllerMethodName: GetOrderById :: Custome Input: companyCode: [{companyCode}] And OrderCode: [{OrderCode}]");

                var response = _OrderManager.GetOrderById(companyCode, OrderCode);
                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger("Response Status: Success :: And ItemLegth: 1");
                    return Ok(response.OrderModel);
                }

                ApplicationLogger.InfoLogger("Response Status: Failuer");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger("Exception: HttpResponseException");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger("Exception: BaseException");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Get all the Order base on filter criteria
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="OrderName"></param>
        /// <returns></returns>
        [Route("companyCode/{companyCode}/OrderName/{OrderName}")]
        public IHttpActionResult GetOrderByName(string companyCode, string OrderName)
        {
            try
            {
                ApplicationLogger.InfoLogger($"TimeStamp: [{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}] :: Request Uri: [{ Request.RequestUri}] :: OrderControllerMethodName: GetOrderByName :: Custome Input: companyCode: [{companyCode}] And OrderName: [{OrderName}]");

                var response = _OrderManager.GetOrderByName(companyCode, OrderName);

                if (response.Status == ResponseStatus.Success)
                {
                    ApplicationLogger.InfoLogger($"Response Status: Success :: And ItemLegth: [{response.Orders.Count}]");
                    return Ok(response.Orders);
                }

                ApplicationLogger.InfoLogger("Response Status: Failure");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, response.ErrorInfo));
            }
            catch (HttpResponseException exception)
            {
                ApplicationLogger.InfoLogger("Exception: HttpResponseException");
                return ResponseMessage(Request.CreateResponse(exception.Response.StatusCode, Constants.NoDataFoundMessage));
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger("Exception: BaseException");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("AddOrder")]
        public IHttpActionResult AddOrder(Order.Model.OrderModel orderModel)
        {
            return Ok(_OrderManager.AddOrder(orderModel));
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public IHttpActionResult UpdateOrder(Order.Model.OrderModel orderModel)
        {
            return Ok(_OrderManager.UpdateOrder(orderModel));
        }
        [HttpDelete]
        [Route("DeleteOrder/{orderId}")]
        public IHttpActionResult DeleteOrder(string orderId)
        {
            return Ok(_OrderManager.DeleteOrder(orderId));
        }


    }
}
