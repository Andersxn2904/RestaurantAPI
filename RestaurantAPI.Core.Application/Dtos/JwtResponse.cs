

namespace RestaurantAPI.Core.Application.Dtos
{
	public class JwtResponse
	{
		public bool HasError { get; set; }
		public string? Error { get; set; }
	}
}
