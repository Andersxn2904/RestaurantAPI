using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Core.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infraestructure.Identity.Seeds
{
	public class DefaultUser
	{
		public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			IdentityUser defaultUser = new();
			defaultUser.UserName = "Coquito1010";
			defaultUser.Email = "darianacabreja@gmail.com";
			defaultUser.EmailConfirmed = true;
			defaultUser.PhoneNumberConfirmed = true;


			if (userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultUser, "Danderi1029@");
					await userManager.AddToRoleAsync(defaultUser, Roles.Waiter.ToString());
					await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());

				}
			}



		}
	}
}
