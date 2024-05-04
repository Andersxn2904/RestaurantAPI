using RestaurantAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Core.Application.Interfaces.Repositories
{
	public interface IDishRepository : IGenericRepository<Dish>
	{
		Task<List<Dish>> GetAllWithIncludeAsync(List<string> properties);
		Task<Dish> GetByIDWithIncludeAsync(List<string> properties, int ID);
	}
}
