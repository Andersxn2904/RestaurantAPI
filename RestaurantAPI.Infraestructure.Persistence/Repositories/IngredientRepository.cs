using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Domain.Entities;
using RestaurantAPI.Infraestructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infraestructure.Persistence.Repositories
{
	public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
	{
		private readonly ApplicationContext _dbContext;

		public IngredientRepository(ApplicationContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
	}
}
