using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Core.Application.Dtos;
using RestaurantAPI.Core.Application.Enums;
using RestaurantAPI.Core.Application.Interfaces.Services;
using RestaurantAPI.Core.Domain.Settings;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantAPI.Infraestructure.Identity.Services
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly JWTSettings _jwtsettings;


		public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOptions<JWTSettings> jwtsettings)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtsettings = jwtsettings.Value;

		}

		public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
		{
			AuthenticationResponse response = new();

			var user = await _userManager.FindByNameAsync(request.Username);
			if (user == null)
			{
				response.HasError = true;
				response.Error = $"There're not Accounts registered with {request.Username}";
				return response;
			}

			var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
			if (!result.Succeeded)
			{
				response.HasError = true;
				response.Error = $"Invalid credentials for {request.Username}";
				return response;
			}




			JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

			var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

			response.Roles = rolesList.ToList();
			response.JWTToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
			var refreshToken = GenerateRefreshToken();
			response.RefreshToken = refreshToken.Token;
			return response;
		}

		public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin)
		{

			Roles role;
			string RoleUser;
			RegisterResponse response = new()
			{
				HasError = false
			};


			if (Enum.TryParse<Roles>(request.RoleID.ToString(), out role))
			{
				RoleUser = role.ToString();
			}
			else
			{
				response.HasError = true;
				response.Error = $"This User role is invalid";
				return response;
			}


			var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
			if (userWithSameUserName != null)
			{
				response.HasError = true;
				response.Error = $"username '{request.UserName}' is already taken.";
				return response;
			}

			var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
			if (userWithSameEmail != null)
			{
				response.HasError = true;
				response.Error = $"Email '{request.Email}' is already registered.";
				return response;
			}

			var user = new IdentityUser
			{
				Email = request.Email,
				UserName = request.UserName,
				EmailConfirmed = true,
				PhoneNumberConfirmed = true,



			};

			var result = await _userManager.CreateAsync(user, request.Password);


			if (result.Succeeded)
			{

				await _userManager.AddToRoleAsync(user, RoleUser);
			}
			else
			{
				response.HasError = true;
				response.Error = $"An error occurred trying to register the user.";
				return response;
			}

			return response;
		}

		public async Task SignOutAsync()
		{
			await _signInManager.SignOutAsync();
		}

		private RefreshToken GenerateRefreshToken()
		{
			return new RefreshToken
			{
				Token = RandomTokenString(),
				Expires = DateTime.UtcNow.AddDays(7),
				Created = DateTime.UtcNow
			};
		}

		private string RandomTokenString()
		{
			using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
			var ramdomBytes = new byte[40];
			rngCryptoServiceProvider.GetBytes(ramdomBytes);

			return BitConverter.ToString(ramdomBytes).Replace("-", "");
		}

		private async Task<JwtSecurityToken> GenerateJWToken(IdentityUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);

			var roleClaims = new List<Claim>();

			foreach (var role in roles)
			{
				roleClaims.Add(new Claim("role", role));
			}

			var claims = new[]
			{
				new Claim("Id", user.Id),
				new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email,user.Email),
				//new Claim("role", "Basic"),
				//new Claim("RoleName", "Basic")

			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.Key));
			var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwtsettings.Issuer,
				audience: _jwtsettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtsettings.DurationInMinutes),
				signingCredentials: signingCredetials);

			return jwtSecurityToken;
		}

		//public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
		//{
		//	ResetPasswordResponse response = new()
		//	{
		//		HasError = false
		//	};




		//	var user = await _userManager.FindByEmailAsync(request.Email);

		//	if (user == null)
		//	{
		//		response.HasError = true;
		//		response.Error = $"No Accounts registered with {request.Email}";
		//		return response;
		//	}

		//	request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
		//	var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

		//	if (!result.Succeeded)
		//	{
		//		response.HasError = true;
		//		response.Error = $"An error occurred while reset password";
		//		return response;
		//	}
		//	else
		//	{
		//		await _emailService.SendAsync(new EmailRequest()
		//		{
		//			To = user.Email,
		//			Body = $"Your password was reset your new password is: <strong>{request.Password}</strong>",
		//			Subject = "New Password"
		//		});
		//	}

		//	return response;
		//}




	}
}
