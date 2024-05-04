using AutoMapper;
using RestaurantAPI.Core.Application.Exceptions;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.ViewModels.Dish;
using RestaurantAPI.Core.Application.ViewModels.Ingredient;
using RestaurantAPI.Core.Application.ViewModels.IngredientDish;
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Services
{
    public class DishService : GenericService<SaveDishViewModel, DishViewModel, Dish>, IDishService
    {
		#region Settings
		private readonly IIngredientDishService _ingredientDishService;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public DishService(IDishRepository dishRepository, IMapper mapper, IIngredientDishService ingredientDishService) : base(dishRepository, mapper)
        {
            _ingredientDishService = ingredientDishService;
            _dishRepository = dishRepository;
			_mapper = mapper;

		}
		#endregion

		public override async Task<SaveDishViewModel> Add(SaveDishViewModel vm)
		{
			var dish = await base.Add(vm);

			foreach (IngredientsDto ingredient in vm.Ingredients)
            {

                SaveIngredientDishViewModel addIngredientToDish = new()
                {
                    IngredientID = ingredient.IngredientID,
                    DishID = dish.ID
                };

				await _ingredientDishService.Add(addIngredientToDish);
			}
            return dish;
		}

		public async Task UpdateDish(UpdateDishModel model, int ID)
		{

            if(model.IngredientsToDeleteID != null || model.IngredientsToDeleteID.Count != 0)
            {
				foreach (int id in model.IngredientsToDeleteID)
				{
					try
					{

						IngredientDish addIngredientDish = new()
						{
							IngredientID = id,
							DishID = model.ID
						};

						await _ingredientDishService.DeleteByEntityAync(addIngredientDish);
					}
					catch (Exception ex)
					{
						continue;

					}


				}

			}
            if (model.IngredientsToAdd != null || model.IngredientsToAdd.Count != 0)
            {

				foreach (IngredientsDto dto in model.IngredientsToAdd)
				{
                    try
                    {

                        SaveIngredientDishViewModel addIngredientToDish = new()
                        {
                            IngredientID = dto.IngredientID,
                            DishID = model.ID
                        };

						await _ingredientDishService.Add(addIngredientToDish);
					}
                    catch (Exception ex) 
                    {
                        continue;
                           
                    }

					
				}
				

			}

            await base.Update(_mapper.Map<SaveDishViewModel>(model), ID);
			
		}

		public async Task<List<DishViewModel>> GetAllViewModelWithInclude()
		{
			var dishList = await _dishRepository.GetAllWithIncludeAsync(new List<string> { "IngredientDish", "Category"});

			return dishList.Select(dish => new DishViewModel
            {
                ID = dish.ID,
                Name = dish.Name,
                HowMany = dish.HowMany,
                CategoryID = dish.CategoryID,
                CategoryName = dish.Category.Name,
                Price = dish.Price,
                Ingredients = dish.IngredientDish.Where(i => i.DishID == dish.ID).Select(i => new IngredientViewModel
                {
                    ID = i.IngredientID,
                    Name = i.Ingredient.Name
                }).ToList()
            }).ToList();

		}

		public async Task<DishViewModel> GetByIdViewModel(int ID)
		{
            try
            {
                Dish dish = await _dishRepository.GetByIDWithIncludeAsync(new List<string> { "IngredientDish", "Category" }, ID);
                if (dish != null)
                {
                    DishViewModel vm = new()
                    {
                        ID = dish.ID,
                        Name = dish.Name,
                        HowMany = dish.HowMany,
                        CategoryID = dish.CategoryID,
                        CategoryName = dish.Category.Name,
                        Price = dish.Price,
                        Ingredients = dish.IngredientDish.Where(i => i.DishID == dish.ID).Select(i => new IngredientViewModel
                        {
                            ID = i.IngredientID,
                            Name = i.Ingredient.Name
                        }).ToList()
                    };

                    return vm;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionNotFound("This Dish wasn't found" + ex.Message);
            }
		}
	}
}
