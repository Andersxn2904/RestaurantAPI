using RestaurantAPI.Core.Application.ViewModels.Dish;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantAPI.Core.Application.ViewModels.Order
{
	public class SaveOrderViewModel
	{
		[JsonIgnore]
		public int ID { get; set; }
		[JsonIgnore]
		public string? State { get; set; }
		[Required]
		public int TableID { get; set; }

		[JsonIgnore]
		public decimal? Subtotal { get; set; }
		[JsonIgnore]
		public List<DishViewModel>? Dishes { get; set; }
		public List<DishDto>? DishesList { get; set; }

		


	}
}
