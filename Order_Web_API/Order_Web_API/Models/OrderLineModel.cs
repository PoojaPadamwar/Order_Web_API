namespace Order_Web_API.Models
{
    public class OrderLineModel
    {  
        public int OrderLineNumber { get; set; }       
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public CustomerModel Customer { get; set; }
        
    }
}