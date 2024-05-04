using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantAPI.Core.Application.ViewModels.Table
{
	public class UpdateTableModel
	{

		[JsonIgnore]
		public int ID {  get; set; }
		public string Description { get; set; }
		public int PersonCapacity { get; set; }
	}
}
