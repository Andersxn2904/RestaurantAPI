using Microsoft.Extensions.DependencyInjection;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.Services;
using System.Reflection;

namespace RestaurantAPI.Core.Application
{
	public static class ServiceRegistration
	{
		public static void AddApplicationLayer(this IServiceCollection services)
		{
			services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddTransient<IIngredientService, IngredientService>();
			services.AddTransient<IIngredientDishService, IngredientDishService>();
			services.AddTransient<IDishService, DishService>();
			services.AddTransient<ITableService, TableService>();
			services.AddTransient<IOrderService, OrderService>();
			services.AddTransient<IOrderDishService, OrderDishService>();
		}
	}
}
