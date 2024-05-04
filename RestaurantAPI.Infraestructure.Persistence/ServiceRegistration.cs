using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Infraestructure.Persistence.Contexts;
using RestaurantAPI.Infraestructure.Persistence.Repositories;

namespace RestaurantAPI.Infraestructure.Persistence
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			#region Contexts
			if (configuration.GetValue<bool>("UseDanderiConnection"))
			{
				services.AddDbContext<ApplicationContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DanderiConnection"),
				m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
			}
			else
			{
				services.AddDbContext<ApplicationContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("Default"),
				m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
			}

			#endregion

			#region Repositories
			services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddTransient<IIngredientRepository, IngredientRepository>();
			services.AddTransient<IIngredientDishRepository, IngredientDishRepository>();
			services.AddTransient<IDishRepository, DishRepository>();
			services.AddTransient<ITableRepository, TableRepository>();
			services.AddTransient<IOrderRepository, OrderRepository>();
			services.AddTransient<IOrderDishRepository, OrderDishRepository>();
			#endregion
		}
	}
}
