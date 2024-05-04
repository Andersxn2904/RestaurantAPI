using RestaurantAPI.Core.Application.ViewModels.Category;
using RestaurantAPI.Core.Application.ViewModels.Dish;
using RestaurantAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Core.Application.Interfaces.Services
{
    public interface IDishService : IGenericService<SaveDishViewModel, DishViewModel, Dish>
    {
		Task<List<DishViewModel>> GetAllViewModelWithInclude();
		Task<DishViewModel> GetByIdViewModel(int ID);
		Task UpdateDish(UpdateDishModel model, int ID);
	}
}
