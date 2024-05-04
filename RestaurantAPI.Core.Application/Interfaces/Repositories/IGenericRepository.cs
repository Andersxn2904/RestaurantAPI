namespace RestaurantAPI.Core.Application.Interfaces.Repositories
{
	public interface IGenericRepository<Entity> where Entity : class
	{
		Task<Entity> AddAsync(Entity entity);
		Task UpdateAsync(Entity entity, int ID);
		Task DeleteAsync(Entity entity);
		Task<Entity> GetByIdAsync(int ID);
		Task<List<Entity>> GetAllAsync();
	}
}
