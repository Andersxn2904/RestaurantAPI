using AutoMapper;
using RestaurantAPI.Core.Application.Interfaces.Repositories;
using RestaurantAPI.Core.Application.Interfaces.Services;

namespace RestaurantAPI.Core.Application.Services
{
	public class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity>
		where SaveViewModel : class
		where ViewModel : class
		where Entity : class
	{
		private readonly IGenericRepository<Entity> _repository;
		private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper) 
        {
			_repository = repository;
			_mapper = mapper;
		}

        public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
		{
			Entity entity = _mapper.Map<Entity>(vm);

			entity = await _repository.AddAsync(entity);

			SaveViewModel entityVm = _mapper.Map<SaveViewModel>(entity);

			return entityVm;
		}

		public virtual async Task Delete(int ID)
		{
			Entity entity = await _repository.GetByIdAsync(ID);
			await _repository.DeleteAsync(entity);
		}

		public async Task DeleteByEntityAync(Entity entity)
		{
			
			await _repository.DeleteAsync(entity);
		}

		public async Task<List<ViewModel>> GetAllViewModel()
		{
			var entityList = await _repository.GetAllAsync();

			return _mapper.Map<List<ViewModel>>(entityList);
		}

		public async Task<SaveViewModel> GetByIdSaveViewModel(int ID)
		{
			Entity entity = await _repository.GetByIdAsync(ID);

			SaveViewModel saveVm = _mapper.Map<SaveViewModel>(entity);

			return saveVm;
		}

		public virtual async Task Update(SaveViewModel vm, int ID)
		{
			Entity entity = _mapper.Map<Entity>(vm);
			await _repository.UpdateAsync(entity, ID);
		}
	}
}
