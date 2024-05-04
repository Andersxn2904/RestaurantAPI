using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Domain.Entities;
using RestaurantAPI.Infraestructure.Persistence.Contexts;

namespace RestaurantAPI.Infraestructure.Persistence.Repositories
{
	public class DishRepository : GenericRepository<Dish>, IDishRepository
	{
		private readonly ApplicationContext _dbContext;

        public DishRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

		public virtual async Task<List<Dish>> GetAllWithIncludeAsync(List<string> properties)
		{
			var query = _dbContext.Set<Dish>().AsQueryable();

			foreach (string property in properties)
			{
				if (property == "IngredientDish")
				{
					query = query.Include(d => d.IngredientDish).ThenInclude(id => id.Ingredient);
				}
				else
				{
					query = query.Include(property);
				}
			}

			return await query.ToListAsync();
		}

		public virtual async Task<Dish> GetByIDWithIncludeAsync(List<string> properties, int ID)
		{
			var query = _dbContext.Set<Dish>().AsQueryable().Where(d => d.ID == ID);

			foreach (string property in properties)
			{
				if (property == "IngredientDish")
				{
					query = query.Include(d => d.IngredientDish).ThenInclude(id => id.Ingredient);
				}
				else
				{
					query = query.Include(property);
				}
			}

			var dish = await query.SingleOrDefaultAsync();

			return dish;
		}
	}
}
