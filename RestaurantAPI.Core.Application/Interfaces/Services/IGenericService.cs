using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Core.Application.Interfaces.Services
{
	public interface IGenericService<SaveViewModel, ViewModel, Entity>
		where SaveViewModel : class
		where ViewModel : class
		where Entity : class
	{
		Task Update(SaveViewModel vm, int ID);

		Task<SaveViewModel> Add(SaveViewModel vm);
		Task DeleteByEntityAync(Entity entity);
		
	    Task Delete(int ID);

		Task<SaveViewModel> GetByIdSaveViewModel(int ID);

		Task<List<ViewModel>> GetAllViewModel();
	}
}
