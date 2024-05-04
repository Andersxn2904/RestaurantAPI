using RestaurantAPI.Core.Application.ViewModels.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Core.Application.ViewModels.Order
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        public string State { get; set; }
        public int TableID { get; set; }
        public decimal Subtotal { get; set; }
        public List<DishJson> Dishes { get; set; }
    }
}
