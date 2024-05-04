using System.ComponentModel.DataAnnotations;


namespace RestaurantAPI.Core.Application.ViewModels.IngredientDish
{
    public class SaveIngredientDishViewModel
    {
		[Required]
		public int IngredientID { get; set; }


		[Required]
		public int DishID { get; set; }
    }
}
