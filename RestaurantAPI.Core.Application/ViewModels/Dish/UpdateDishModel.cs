using RestaurantAPI.Core.Application.ViewModels.Ingredient;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace RestaurantAPI.Core.Application.ViewModels.Dish
{
	public class UpdateDishModel
	{

		[JsonIgnore]
		[Key]
		public int ID { get; set; }
		public string? Name { get; set; }
		public int? HowMany { get; set; }
		public int? CategoryID { get; set; }
		public decimal? Price { get; set; }
		public List<int>? IngredientsToDeleteID { get; set; }
		public List<IngredientsDto>? IngredientsToAdd { get; set; }
	}
}
