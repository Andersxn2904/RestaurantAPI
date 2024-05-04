using RestaurantAPI.Core.Application.ViewModels.Ingredient;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantAPI.Core.Application.ViewModels.Dish
{
    public class SaveDishViewModel
    {
        [JsonIgnore]
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
		[Required]
		public int HowMany { get; set; }
		[Required]
		public int CategoryID { get; set; }
		
		public decimal Price { get; set; }
        public List<IngredientsDto> Ingredients { get; set; }
    }
}
