using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PozyczkoPrzypominajka.Models;

namespace PozyczkoPrzypominajkaV2.Data
{
	public static class DBInitialization
	{
		public static void SeedAdminUsers(UserManager<AppUser> userManager, IConfiguration Configuration)
		{
			var adminUserName = Configuration.GetSection("Users").GetSection("AdminUser").GetSection("UserName").Value;
			var password = Configuration.GetSection("Users").GetSection("AdminUser").GetSection("Password").Value;

			if (userManager.FindByNameAsync(adminUserName).Result == null)
			{
				var user = new AppUser
				{
					UserName = adminUserName,
					Email = adminUserName,
				};

				IdentityResult result = userManager.CreateAsync(user, password).Result;

				if (result.Succeeded)
				{
					userManager.AddToRoleAsync(user, "Admin").Wait();
				}
			}
		}
	}
}