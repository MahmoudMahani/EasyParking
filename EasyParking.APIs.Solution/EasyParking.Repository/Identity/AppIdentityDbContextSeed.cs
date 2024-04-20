using EasyParking.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Repository.Identity
{
	public static class AppIdentityDbContextSeed
	{
		public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
		{
			if(!userManager.Users.Any())
			{
				var User = new AppUser()
				{
					DisplayName = "Eman Ibrahim",
					Age = 22,
					Email = "eman.ibrahim@gmail.com",
					UserName = "eman.ibrahim",
					PhoneNumber = "01159494123",
					CarNumber = "SIA9922"
				};
				await userManager.CreateAsync(User,"P@ssw0rd");
			}
		}
	}
}
