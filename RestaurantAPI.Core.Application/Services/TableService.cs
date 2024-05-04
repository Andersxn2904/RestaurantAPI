using AutoMapper;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Application.ViewModels.Dish;
using RestaurantAPI.Core.Application.ViewModels.Order;
using RestaurantAPI.Core.Application.ViewModels.Table;
using RestaurantAPI.Core.Domain.Entities;

namespace RestaurantAPI.Core.Application.Services
{
	public class TableService : GenericService<SaveTableViewModel, TableViewModel, Table>, ITableService
	{
		#region Settings
		private readonly ITableRepository _tableRepository;
        public TableService(ITableRepository tableRepository, IMapper mapper) : base(tableRepository, mapper)
        {
            _tableRepository = tableRepository;
        }

		public override Task<SaveTableViewModel> Add(SaveTableViewModel vm)
		{
			vm.StatusID = 1;
			return base.Add(vm);
		}
		#endregion

		public async Task<List<TableViewModel>> GetAllViewModelWithInclude()
		{
			var tableList = await _tableRepository.GetAllWithIncludeAsync(new List<string> { "Status" });

			return tableList.Select(table => new TableViewModel
			{
				ID = table.ID,
				Description = table.Description,
				PersonCapacity = table.PersonCapacity,
				StatusID = table.StatusID,
				State = table.Status.Name,

			}).ToList();

		}

		public async Task<TableViewModel> GetByIDWithIncludeModel(int ID)
		{
			var table = await _tableRepository.GetByIDWithIncludeAsync(new List<string> { "Status" }, ID);

			TableViewModel tableVm = new()
			{
				ID = table.ID,
				Description = table.Description,
				PersonCapacity = table.PersonCapacity,
				StatusID = table.StatusID,
				State = table.Status.Name,
			};

			return tableVm;

		}

	}
}
