

using RestaurantAPI.Core.Application.Dtos;

namespace RestaurantAPI.Core.Application.Interfaces.Services
{
	public interface IAccountService
	{
		Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);

		Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
		//Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);

		Task SignOutAsync();

	}
}
