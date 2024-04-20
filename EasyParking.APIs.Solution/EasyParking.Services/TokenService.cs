using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyParking.Core.Services;
using EasyParking.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace EasyParking.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration configuration;

		public TokenService(IConfiguration configuration)
        {
			this.configuration = configuration;
		}
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{
			//Private Claims [User-Defined]
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName,user.DisplayName),
				new Claim(ClaimTypes.Email,user.Email)
			};

			var userRoles = await userManager.GetRolesAsync(user);
			foreach (var role in userRoles)
				authClaims.Add(new Claim(ClaimTypes.Role, role));

			//Security Key
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

			//Registered Claims
			var token = new JwtSecurityToken(
				issuer: configuration["JWT:ValidIssuer"],
				audience: configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
