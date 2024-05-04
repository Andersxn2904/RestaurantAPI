using AutoMapper;
using RestaurantAPI.Core.Application.ViewModels.Dish;
using RestaurantAPI.Core.Application.ViewModels.Ingredient;
using RestaurantAPI.Core.Application.ViewModels.IngredientDish;
using RestaurantAPI.Core.Application.ViewModels.Order;
using RestaurantAPI.Core.Application.ViewModels.OrderDish;
using RestaurantAPI.Core.Application.ViewModels.Table;
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Mappings
{
	public class GeneralProfile : Profile
	{
		public GeneralProfile()
		{
			#region Ingredient

			CreateMap<Ingredient, IngredientViewModel>()
				.ReverseMap();

			CreateMap<Ingredient, SaveIngredientViewModel>()
				.ReverseMap();

			#endregion

			#region Dish
			CreateMap<UpdateDishModel, SaveDishViewModel>()

				.ForMember(dest => dest.Ingredients, opt => opt.Ignore())
				.ReverseMap()
				.ForMember(dest => dest.IngredientsToDeleteID, opt => opt.Ignore())
				.ForMember(dest => dest.IngredientsToAdd, opt => opt.Ignore());
				

			CreateMap<Dish, DishViewModel>()
				.ForMember(dest => dest.CategoryName, opt => opt.Ignore())
				.ForMember(dest => dest.Ingredients, opt => opt.Ignore())
				.ReverseMap()
				.ForMember(dest => dest.OrderDishes, opt => opt.Ignore())
				.ForMember(dest => dest.Category, opt => opt.Ignore())
				.ForMember(dest => dest.IngredientDish, opt => opt.Ignore());

			CreateMap<Dish, SaveDishViewModel>()
				.ReverseMap()
				.ForMember(dest => dest.OrderDishes, opt => opt.Ignore())
				.ForMember(dest => dest.Category, opt => opt.Ignore())
				.ForMember(dest => dest.IngredientDish, opt => opt.Ignore());

			#endregion

			#region IngredientDish

			CreateMap<IngredientDish, IngredientViewModel>()
				.ReverseMap();

			CreateMap<IngredientDish, SaveIngredientDishViewModel>()
				.ReverseMap();

			#endregion

			#region Table

			CreateMap<Table, TableViewModel>()
				.ReverseMap()
				.ForMember(dest => dest.Orders, opt => opt.Ignore())
				.ForMember(dest => dest.Status, opt => opt.Ignore());

			CreateMap<Table, SaveTableViewModel>()
				.ReverseMap()
				.ForMember(dest => dest.Orders, opt => opt.Ignore())
				.ForMember(dest => dest.Status, opt => opt.Ignore());

			#endregion

			#region Order

			CreateMap<Order, OrderViewModel>()
				.ForMember(dest => dest.Dishes, opt => opt.Ignore())
				.ReverseMap()
				.ForMember(dest => dest.OrderDishes, opt => opt.Ignore())
				.ForMember(dest => dest.Table, opt => opt.Ignore());

			CreateMap<Order, SaveOrderViewModel>()
				.ForMember(dest => dest.Dishes, opt => opt.Ignore())
				.ReverseMap()
				.ForMember(dest => dest.OrderDishes, opt => opt.Ignore())
				.ForMember(dest => dest.Table, opt => opt.Ignore());

			#endregion

			#region OrderDish

			CreateMap<OrderDish, OrderDishViewModel>()
				.ReverseMap();

			CreateMap<OrderDish, SaveOrderDishViewModel>()
				.ReverseMap();

			#endregion
		}
	}
}
