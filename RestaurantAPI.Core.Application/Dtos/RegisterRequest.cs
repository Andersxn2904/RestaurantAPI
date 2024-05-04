

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace RestaurantAPI.Core.Application.Dtos
{
	public class RegisterRequest
	{
		[Display(Name = "Nombre del Pokémon", Description = "Nombre del Pokémon en la Pokédex")]
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		[JsonIgnore]
		public int RoleID { get; set; }

	}
}
