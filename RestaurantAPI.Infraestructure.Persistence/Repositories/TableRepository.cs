using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Domain.Entities;
using RestaurantAPI.Infraestructure.Persistence.Contexts;

namespace RestaurantAPI.Infraestructure.Persistence.Repositories
{
	public class TableRepository : GenericRepository<Table>, ITableRepository
	{
        private readonly ApplicationContext _dbContext;
        public TableRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

		public virtual async Task<List<Table>> GetAllWithIncludeAsync(List<string> properties)
		{
			var query = _dbContext.Set<Table>().AsQueryable();

			foreach (string property in properties)
			{
				query = query.Include(property);
			}

			return await query.ToListAsync();
		}
		public virtual async Task<Table> GetByIDWithIncludeAsync(List<string> properties, int ID)
		{
			var query = _dbContext.Set<Table>().AsQueryable().Where(d => d.ID == ID);

			foreach (string property in properties)
			{
				query = query.Include(property);
			}

			var table = await query.SingleOrDefaultAsync();

			return table;
		}
	}
}
