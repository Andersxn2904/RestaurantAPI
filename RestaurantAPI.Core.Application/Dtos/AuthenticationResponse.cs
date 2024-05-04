


using System.Text.Json.Serialization;

namespace RestaurantAPI.Core.Application.Dtos
{
	public class AuthenticationResponse
	{
		public string UserName { get; set; }
			
		public bool HasError { get; set; }
		public string? Error { get; set; }

		public List<string> Roles { get; set; }

		public string JWTToken { get; set; }

		[JsonIgnore]
		public string RefreshToken { get; set; }


	}
}
