namespace RestaurantAPI.Core.Domain.Entities
{
	public class IngredientDish
	{
        public int DishID { get; set; }
        public int IngredientID { get; set; }

        //Navigation prop
        public Dish Dish { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
