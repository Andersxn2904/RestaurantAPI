using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Domain.Entities;
using RestaurantAPI.Infraestructure.Persistence.Contexts;

namespace RestaurantAPI.Infraestructure.Persistence.Repositories
{
	public class IngredientDishRepository : GenericRepository<IngredientDish>, IIngredientDishRepository
	{
        private readonly ApplicationContext _dbContext;
        public IngredientDishRepository(ApplicationContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
	}
}
