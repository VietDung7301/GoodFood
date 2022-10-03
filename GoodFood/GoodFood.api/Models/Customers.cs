namespace GoodFood.api.Entities
{
    public class Customers
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerUsername { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerToken { get; set; }

        public Customers() { }
    }
}
