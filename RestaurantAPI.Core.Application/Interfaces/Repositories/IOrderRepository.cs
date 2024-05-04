
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Interfaces.Repositories
{
	public interface IOrderRepository : IGenericRepository<Order>
	{
		Task<List<Order>> GetAllWithIncludeAsync(List<string> properties);
		Task<Order> GetByIDWithIncludeAsync(List<string> properties, int ID);
	}
}
