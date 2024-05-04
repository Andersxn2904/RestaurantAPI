using RestaurantAPI.Core.Domain.Common;

namespace RestaurantAPI.Core.Domain.Entities
{
	public class Dish : CommonNameProp
	{
        //public ICollection<Ingredient>? Ingredients { get; set; }
        public int HowMany { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }

        //Navigation props
        public Category Category { get  ; set; }
        public ICollection<IngredientDish>? IngredientDish { get; set; }
		public ICollection<OrderDish>? OrderDishes { get; set; }
	}
}
