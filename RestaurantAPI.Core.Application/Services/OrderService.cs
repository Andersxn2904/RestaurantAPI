using AutoMapper;
using RestaurantAPI.Core.Application.Enums;
using RestaurantAPI.Core.Application.Exceptions;
using RestaurantAPI.Core.Application.Helpers;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.ViewModels.Dish;
using RestaurantAPI.Core.Application.ViewModels.Order;
using RestaurantAPI.Core.Application.ViewModels.OrderDish;
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Services
{
	public class OrderService : GenericService<SaveOrderViewModel, OrderViewModel, Order>, IOrderService
	{
		#region Settings
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderDishService _orderDishService;
		private readonly IDishRepository _dishRepository;

		public OrderService(IDishRepository dishRepository, IOrderRepository orderRepository, IMapper mapper, IOrderDishService orderDishService) : base(orderRepository, mapper)
		{
			_orderDishService = orderDishService;
			_orderRepository = orderRepository;
			_dishRepository = dishRepository;
		}
		#endregion

		public override async Task<SaveOrderViewModel> Add(SaveOrderViewModel vm)
		{
			vm.State = GetStringValueEnum.GetStringValue(OrderStates.PROCESS);

			List<Dish> dishes = new();

			vm.Subtotal = 0;

			foreach (DishDto DTO in vm.DishesList)
			{
				var dish = await _dishRepository.GetByIdAsync(DTO.DishID);
				dishes.Add(dish);
				vm.Subtotal += (DTO.Quantity * dish.Price);
			}

			var order = await base.Add(vm);

			foreach(DishDto dish in vm.DishesList)
			{
				await _orderDishService.Add(new SaveOrderDishViewModel()
				{
					OrderID = order.ID,
					DishID = dish.DishID,
					Quantity = dish.Quantity,
				});
			}

			return order;
		}

		public async Task<List<OrderViewModel>> GetAllViewModelWithInclude()
		{
			var orderList = await _orderRepository.GetAllWithIncludeAsync(new List<string> { "OrderDishes" });

			return orderList.Select(order => new OrderViewModel
			{
				ID = order.ID,
				State = order.State,
				Subtotal = order.Subtotal,
				TableID = order.TableID,
				Dishes = order.OrderDishes.Where(i => i.OrderID == order.ID).Select(i => new DishJson
				{
					ID = i.Dish.ID,
					Name = i.Dish.Name,
					Quantity = i.Quantity
				}).ToList()
			}).ToList();
		}

		public async Task<OrderViewModel> GetByIDWithIncludeModel(int ID)
		{
			try
			{
				var order = await _orderRepository.GetByIDWithIncludeAsync(new List<string> { "OrderDishes" }, ID);
				if (order != null)
				{
					OrderViewModel orderVm = new()
					{
						ID = order.ID,
						State = order.State,
						Subtotal = order.Subtotal,
						TableID = order.TableID,
						Dishes = order.OrderDishes.Where(i => i.OrderID == order.ID).Select(i => new DishJson
						{
							ID = i.Dish.ID,
							Name = i.Dish.Name,
							Quantity = i.Quantity
						}).ToList()
					};
					return orderVm;
				}
				else
				{
					throw new Exception();
				}
				

				
			}
			catch (Exception ex)
			{
				throw new ExceptionNotFound("This order wasn't found  " + ex.Message);
			}

		}

		public async Task UpdateOrderDishes(UpdateOrderDishes Model)
		{

			if (Model.DishesToDelete.Count != 0 || Model.DishesToDelete != null)
			{
				foreach (int id in Model.DishesToDelete)
				{

					await _orderDishService.DeleteByEntityAync(new OrderDish()
					{
						OrderID = Model.OrderID,
						DishID = id
					});

				}
			}

			if (Model.DishesToAdd.Count != 0 || Model.DishesToAdd != null)
			{
				foreach (DishDto Dto in Model.DishesToAdd)
				{
					await _orderDishService.Add(new SaveOrderDishViewModel()
					{
						OrderID = Model.OrderID,
						DishID = Dto.DishID,
						Quantity = Dto.Quantity,
					});

				}
			}
		}

		public override async Task Delete(int ID)
		{
			try
			{
				base.Delete(ID);
			}
			catch(Exception ex)
			{
				throw new ExceptionNotFound("This order wasn't found  " + ex.Message);

			}

		}

		public async Task<List<OrderViewModel>> OrderInProcessByTable(int TableID)
		{
			var orderList = await _orderRepository.GetAllWithIncludeAsync(new List<string> { "OrderDishes" });

			return orderList.Where(o => o.TableID == TableID && o.State == GetStringValueEnum.GetStringValue(OrderStates.PROCESS)).Select(order => new OrderViewModel
			{
				ID = order.ID,
				State = order.State,
				Subtotal = order.Subtotal,
				TableID = order.TableID,
				Dishes = order.OrderDishes.Where(i => i.OrderID == order.ID).Select(i => new DishJson
				{
					ID = i.Dish.ID,
					Name = i.Dish.Name,
					Quantity = i.Quantity
				}).ToList()
			}).ToList();
		}
	}
}
