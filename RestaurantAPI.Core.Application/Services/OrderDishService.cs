
using AutoMapper;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.ViewModels.OrderDish;
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Services
{
	public class OrderDishService : GenericService<SaveOrderDishViewModel, OrderDishViewModel, OrderDish>, IOrderDishService
	{
        public OrderDishService(IOrderDishRepository orderDishRepository, IMapper mapper) : base(orderDishRepository, mapper)
        {
            
        }
    }
}
