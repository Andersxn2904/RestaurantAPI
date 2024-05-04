using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantAPI.Core.Application.ViewModels.Ingredient
{
	public class SaveIngredientViewModel
	{
		[Key]
		[JsonIgnore]
		public int ID { get; set; }

		[Required]
		public string Name { get; set; }

    }
}
