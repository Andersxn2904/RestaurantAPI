using RestaurantAPI.Core.Application.ViewModels.Table;
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Interfaces.Services
{
	public interface ITableService : IGenericService<SaveTableViewModel, TableViewModel, Table>
	{
		Task<List<TableViewModel>> GetAllViewModelWithInclude();
		Task<TableViewModel> GetByIDWithIncludeModel(int ID);
	}
}
