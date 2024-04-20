using EasyParking.Core.Entities.Identity;
using EasyParking.Core.Services;
using EasyParking.Repository.Identity;
using EasyParking.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace EasyParking.APIs.ExtenTions
{
	public static class IdentityServicesExtention
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration configuration)
		{

			Services.AddScoped<ITokenService, TokenService>();
			//Services.AddEndpointsApiExplorer();

			Services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;	
				options.Password.RequireNonAlphanumeric = true;
			}).AddEntityFrameworkStores<AppIdentityDbContext>();
			Services.AddAuthentication(options => 
			        { 
						options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
						options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					}).AddJwtBearer(options =>
					  {
						options.TokenValidationParameters = new TokenValidationParameters()
						{
							ValidateIssuer = true,
							ValidIssuer = configuration["JWT:ValidIssuer"],
							ValidateAudience = true,
							ValidAudience = configuration["JWT:ValidAudience"],
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),

						};
					  });

			return Services;
		}
		
	}
}
