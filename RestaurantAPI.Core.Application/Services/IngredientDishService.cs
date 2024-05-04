using AutoMapper;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.ViewModels.IngredientDish;
using RestaurantAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Core.Application.Services
{
	public class IngredientDishService : GenericService<SaveIngredientDishViewModel, IngredientDishViewModel, IngredientDish>, IIngredientDishService
	{
        public IngredientDishService(IIngredientDishRepository ingredientDishRepository, IMapper mapper) : base(ingredientDishRepository, mapper)
        {
            
        }
    }
}
