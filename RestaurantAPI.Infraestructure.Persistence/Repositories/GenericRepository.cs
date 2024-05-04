using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Infraestructure.Persistence.Contexts;

namespace RestaurantAPI.Infraestructure.Persistence.Repositories
{
	public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
	{
		private readonly ApplicationContext _dbContext;

        public GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

		public virtual async Task<Entity> AddAsync(Entity entity)
		{
			await _dbContext.Set<Entity>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		public virtual async Task UpdateAsync(Entity entity, int ID)
		{
			//_dbContext.Entry(entity).State = EntityState.Modified;
			//await _dbContext.SaveChangesAsync();
			var entry = await _dbContext.Set<Entity>().FindAsync(ID);
			_dbContext.Entry(entry).CurrentValues.SetValues(entity);
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task DeleteAsync(Entity entity)
		{
			_dbContext.Set<Entity>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task<List<Entity>> GetAllAsync()
		{
			return await _dbContext.Set<Entity>().ToListAsync();
		}

		public virtual async Task<Entity> GetByIdAsync(int ID)
		{
			return await _dbContext.Set<Entity>().FindAsync(ID);
		}
	}
}
