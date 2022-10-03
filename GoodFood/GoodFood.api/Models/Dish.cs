namespace GoodFood.api.Models
{
    public class Dish
    {

        public Guid DishId { get; set; }
        public Guid SalerId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
