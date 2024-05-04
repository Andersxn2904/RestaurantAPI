
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantAPI.Core.Application.ViewModels.Table
{
	public class SaveTableViewModel
	{
		[Key]
		[JsonIgnore]
		public int ID { get; set; }

		[Required]
		public string Description { get; set; }
		public int? PersonCapacity { get; set; }

		[JsonIgnore]
		public int? StatusID { get; set; }
	}
}
