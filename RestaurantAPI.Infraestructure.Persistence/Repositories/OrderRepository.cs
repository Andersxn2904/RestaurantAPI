using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Domain.Entities;
using RestaurantAPI.Infraestructure.Persistence.Contexts;

namespace RestaurantAPI.Infraestructure.Persistence.Repositories
{
	public class OrderRepository : GenericRepository<Order>, IOrderRepository
	{
		private readonly ApplicationContext _dbContext;

		public OrderRepository(ApplicationContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public virtual async Task<List<Order>> GetAllWithIncludeAsync(List<string> properties)
		{
			var query = _dbContext.Set<Order>().AsQueryable();

			foreach (string property in properties)
			{
				if (property == "OrderDishes")
				{
					query = query.Include(d => d.OrderDishes).ThenInclude(od => od.Dish)/*.ThenInclude(d => d.IngredientDish)*/;
				}
				else
				{
					query = query.Include(property);
				}
			}

			return await query.ToListAsync();
		}

		public virtual async Task<Order> GetByIDWithIncludeAsync(List<string> properties, int ID)
		{
			var query = _dbContext.Set<Order>().AsQueryable().Where(d => d.ID == ID);

			foreach (string property in properties)
			{
				if (property == "OrderDishes")
				{
					query = query.Include(d => d.OrderDishes).ThenInclude(od => od.Dish)/*.ThenInclude(d => d.IngredientDish)*/;
				}
				else
				{
					query = query.Include(property);
				}
			}

			var order = await query.SingleOrDefaultAsync();

			return order;
		}

	}
}
