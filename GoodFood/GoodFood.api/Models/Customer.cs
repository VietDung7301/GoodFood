namespace GoodFood.api.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerUsername { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerToken { get; set; }

    }
}
