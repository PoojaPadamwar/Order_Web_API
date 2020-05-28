using Order_Web_API.IServices;
using Order_Web_API.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using Order_Web_API.Services;
using Order_Web_API.Validators;

namespace Order_Web_API.Controllers
{
    /// <summary>
    /// This is the Order controller and request will come here via api/Order
    /// </summary>
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        private IOrderService _orderService;

        //Injecting IOrderService to OrderController 
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;          
        }
              
        /// <summary>
        /// This is controller action for order by ID
        /// </summary>
        /// <param name="id">Order id to be provided url hit</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [OrderValidator]
        public HttpResponseMessage Get(int id)
        {
            //service call
            OrderModel orderModel = _orderService.GetOrder(id);

            //Order Model validations
            if (orderModel==null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, orderModel);               
            }
                   
        }
    }
}
