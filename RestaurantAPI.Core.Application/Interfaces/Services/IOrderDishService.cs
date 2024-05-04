
using RestaurantAPI.Core.Application.ViewModels.OrderDish;
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Interfaces.Services
{
	public interface IOrderDishService : IGenericService<SaveOrderDishViewModel, OrderDishViewModel, OrderDish>
	{
	}
}
