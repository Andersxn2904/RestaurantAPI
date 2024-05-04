using RestaurantAPI.Core.Domain.Common;

namespace RestaurantAPI.Core.Domain.Entities
{
	public class Order : OtherCommon
	{
        public int TableID { get; set; }
        public decimal Subtotal { get; set; }

        //Navigation prop
        public Table Table { get; set; }
        public ICollection<OrderDish>? OrderDishes { get; set; }
    }
}
