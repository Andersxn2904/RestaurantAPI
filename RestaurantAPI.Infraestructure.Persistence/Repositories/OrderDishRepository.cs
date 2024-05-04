
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Domain.Entities;
using RestaurantAPI.Infraestructure.Persistence.Contexts;

namespace RestaurantAPI.Infraestructure.Persistence.Repositories
{
	public class OrderDishRepository : GenericRepository<OrderDish>, IOrderDishRepository
	{
		private readonly ApplicationContext _dbContext;

        public OrderDishRepository(ApplicationContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
    }
}
