
using RestaurantAPI.Core.Domain.Common;

namespace RestaurantAPI.Core.Domain.Entities
{
	public class TableStatus : CommonNameProp
	{
        // Navigation prop
        public ICollection<Table>? Tables { get; set; }
    }
}
