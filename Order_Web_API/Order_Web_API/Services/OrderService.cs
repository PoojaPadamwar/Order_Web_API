using Order_Web_API.Helper;
using Order_Web_API.IServices;
using Order_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;

namespace Order_Web_API.Services
{
    /// <summary>
    /// This is the main service for Order which contains Business logic
    /// </summary>
    public class OrderService : IOrderService
    {
        public XMLOperation _xmlOperation;
        public OrderService()
        {
            _xmlOperation = new XMLOperation();          
        }
        
        /// <summary>
        /// Get Order by order id from XML file
        /// </summary>
        /// <param name="OrderId">Order Number from url hit</param>
        /// <returns></returns>
        public OrderModel GetOrder(int OrderId)
        {            
            OrderModel order=null;

            XDocument doc = _xmlOperation.GetLodedXMLDocument();

            XElement matchElement = doc.Descendants("OrderModel").Elements("OrderNumber")
                                    .SingleOrDefault(x => x.Value == OrderId.ToString());
            
            if (matchElement != null)
            {
                order=_xmlOperation.GetOrderModelFromXML(matchElement, order);
            }

            return order;
        }
             
    }
}
