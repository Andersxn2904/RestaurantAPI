using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Core.Application.Enums;


namespace RestaurantAPI.Infraestructure.Identity.Seeds
{
	public class DefaultRoles
	{
		public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			await roleManager.CreateAsync(new IdentityRole(Roles.Waiter.ToString()));
			await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
			await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));


		}
	}
}
