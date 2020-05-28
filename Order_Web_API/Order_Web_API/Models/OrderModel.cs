using System;
using System.Collections.Generic;

namespace Order_Web_API.Models
{
    public class OrderModel
    {
        public int OrderNumber { get; set; }       
        public DateTime OrderDate { get; set; }
        public ProductModel Product { get; set; }
        public List<OrderLineModel> OrderLines { get; set; }
    }
}