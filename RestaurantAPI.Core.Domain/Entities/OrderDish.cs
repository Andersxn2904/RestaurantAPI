
namespace RestaurantAPI.Core.Domain.Entities
{
    public class OrderDish
    {
        public int OrderID { get; set; }
        public int DishID { get; set; }
        public int? Quantity {get; set;}

        //Navigation prop
        public Order Order { get; set; }
        public Dish Dish { get; set; }
    }
}
