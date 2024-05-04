using AutoMapper;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.ViewModels.Ingredient;
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Services
{
	public class IngredientService : GenericService<SaveIngredientViewModel, IngredientViewModel, Ingredient>, IIngredientService
	{
        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper) : base(ingredientRepository, mapper)
        {

        }
    }
}
