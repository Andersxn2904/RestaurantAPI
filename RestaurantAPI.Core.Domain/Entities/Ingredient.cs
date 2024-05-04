using RestaurantAPI.Core.Domain.Common;

namespace RestaurantAPI.Core.Domain.Entities
{
	public class Ingredient : CommonNameProp
	{
		//Navigation props
		public ICollection<IngredientDish>? IngredientDish { get; set; }

		//public ICollection<Dish>? Dishes { get; set; }
	}
}
