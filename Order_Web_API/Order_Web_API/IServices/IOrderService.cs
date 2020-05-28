using Order_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Web_API.IServices
{
    /// <summary>
    /// Interface for IOrderService Future methods can be added here
    /// </summary>
    public interface IOrderService
    {
        OrderModel GetOrder(int OrderId);
         
    }
}
