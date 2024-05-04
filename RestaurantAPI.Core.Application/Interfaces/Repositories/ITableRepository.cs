
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Interfaces.Repositories
{
	public interface ITableRepository : IGenericRepository<Table>
	{
		Task<List<Table>> GetAllWithIncludeAsync(List<string> properties);
		Task<Table> GetByIDWithIncludeAsync(List<string> properties, int ID);
	}
}
