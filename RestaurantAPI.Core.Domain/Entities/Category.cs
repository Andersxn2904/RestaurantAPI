using RestaurantAPI.Core.Domain.Common;

namespace RestaurantAPI.Core.Domain.Entities
{
	public class Category : CommonNameProp
	{
        //Navigation prop
        public ICollection<Dish>? Dishes { get; set; }
    }
}
