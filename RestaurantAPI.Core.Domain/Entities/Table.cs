using RestaurantAPI.Core.Domain.Common;

namespace RestaurantAPI.Core.Domain.Entities
{
	public class Table : BaseEntity
	{
		public string? Description { get; set; }
        public int PersonCapacity { get; set; }
        public int StatusID { get; set; }

        //Navigation prop
        public TableStatus Status { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
