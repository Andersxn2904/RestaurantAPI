using RestaurantAPI.Core.Application.ViewModels.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantAPI.Core.Application.ViewModels.Order
{
	public class UpdateOrderDishes
	{
		[JsonIgnore]
		public int OrderID { get; set; }
		public List<int> DishesToDelete { get; set; }
		public List<DishDto> DishesToAdd { get; set; }
	}
}
