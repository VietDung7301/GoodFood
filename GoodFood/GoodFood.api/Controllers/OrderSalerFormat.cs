namespace GoodFood.api.Controllers
{
    public class OrderSalerFormat
    { 
        public string order_id { 
            get; set; 
        }
        public string customer_name
        {
            get; set;
        }
        public DateTime order_time
        {
            set; get;
        }
        public int order_type
        {
            set; get;
        }
        public IEnumerable<OrderDishes> Orderdish
        {
            get;set;
        }
        public double totalPrice
        {
            get;set;
        }
    }
}
