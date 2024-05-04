using RestaurantAPI.Core.Application.ViewModels.Ingredient;
using System.Text.Json.Serialization;

namespace RestaurantAPI.Core.Application.ViewModels.Dish
{
    public class DishViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int HowMany { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public List<IngredientViewModel> Ingredients { get; set;}

        
		

	}
}
