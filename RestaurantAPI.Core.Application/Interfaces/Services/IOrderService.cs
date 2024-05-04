
using RestaurantAPI.Core.Application.ViewModels.Order;
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Interfaces.Services
{
	public interface IOrderService : IGenericService<SaveOrderViewModel, OrderViewModel, Order>
	{
		Task<List<OrderViewModel>> GetAllViewModelWithInclude();
		Task UpdateOrderDishes(UpdateOrderDishes Model);
		Task<OrderViewModel> GetByIDWithIncludeModel(int ID);
		Task<List<OrderViewModel>> OrderInProcessByTable(int TableID);
	}
}
