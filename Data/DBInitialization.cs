using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PozyczkoPrzypominajka.Models;
using System.Collections.Generic;

namespace PozyczkoPrzypominajkaV2.Data
{
	public static class DBInitialization
	{
		public static void InitRoles(RoleManager<IdentityRole> roleManager)
		{
			if (roleManager.FindByNameAsync("Admin").Result == null)
			{
				var roleAdmin = new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() };
				roleManager.CreateAsync(roleAdmin).Wait();
			}

			if (roleManager.FindByNameAsync("User").Result == null)
			{
				var roleUser = new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() };
				roleManager.CreateAsync(roleUser).Wait();
			}
		}

		public static void SeedAdminUsers(UserManager<AppUser> userManager, IConfiguration Configuration)
		{
			var adminUserName = Configuration.GetSection("Users").GetSection("AdminUser").GetSection("Email").Value;
			var password = Configuration.GetSection("Users").GetSection("AdminUser").GetSection("Password").Value;

			if (userManager.FindByNameAsync(adminUserName).Result == null)
			{
				var user = new AppUser
				{
					UserName = adminUserName,
					Email = adminUserName,
					Imie = "Admin",
					Nazwisko = "Adminowski",
				};

				IdentityResult result = userManager.CreateAsync(user, password).Result;

				if (result.Succeeded)
				{
					userManager.AddToRoleAsync(user, "Admin").Wait();
				}
			}
		}

		public static void SeedUsers(UserManager<AppUser> userManager)
		{
			if (userManager.FindByNameAsync("marcin@inicjalny.com").Result == null)
			{
				var list = new List<AppUser>();

				var user = new AppUser
				{
					UserName = "marcin@inicjalny.com",
					Email = "marcin@inicjalny.com",
					Imie = "Marcin",
					Nazwisko = "Początkowy",
				};
				list.Add(user);

				var user2 = new AppUser
				{
					UserName = "marzenia@inicjalny.com",
					Email = "marzena@inicjalny.com",
					Imie = "Marzena",
					Nazwisko = "Początkowa",
				};
				list.Add(user2);

				var password = "Azerty1-";

				list.ForEach(userItem => {
					var result = userManager.CreateAsync(userItem, password).Result;
					if (result.Succeeded)
					{
						userManager.AddToRoleAsync(userItem, "User").Wait();
					}
				});
			}
		}
	}
}