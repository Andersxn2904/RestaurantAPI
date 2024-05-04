
namespace RestaurantAPI.Core.Application.ViewModels.OrderDish
{
    public class SaveOrderDishViewModel
    {
		public int OrderID { get; set; }
		public int DishID { get; set; }
		public int? Quantity { get; set; }
	}
}
