using AutoMapper;
using EasyParking.APIs.Dtos;
using EasyParking.APIs.Errors;
using EasyParking.Core.Entities.Identity;
using EasyParking.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyParking.APIs.Controllers
{
	public class AccountsController : ApiBaseController
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly ITokenService tokenService;
		//private readonly IMapper mapper;

		public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
			ITokenService tokenService)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.tokenService = tokenService;
		}

		[HttpPost("login")] //Post: api/accounts/login
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);
			if(user is  null)
				return Unauthorized(new ApiErrorResponse(401));

			var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if(!result.Succeeded)
				return Unauthorized(new ApiErrorResponse(401));

			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName, 
				Email = user.Email,
				Token = await tokenService.CreateTokenAsync(user, userManager)
			});
		}


		[HttpPost("Register")] //Post: api/accounts/Register
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			if (CheckEmailExsist(model.Email).Result.Value)
				return BadRequest(new ApiValidationErrorResponse { Errors = new string[] { "This Email is allready Exsist" } });
			var user = new AppUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.Email.Split('@')[0],
				PhoneNumber = model.PhoneNumber,
				Age = model.Age,
				CarNumber = model.CarNumber,
			};
			var result = await userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
				return BadRequest(new ApiErrorResponse(400));

			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await tokenService.CreateTokenAsync(user, userManager)
			});
		}


		[Authorize]
		[HttpGet("currentuser")] //Get: api/accounts/currentuser
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await userManager.FindByEmailAsync(email);
			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await tokenService.CreateTokenAsync(user, userManager)
			});
		}


		[Authorize]
		[HttpGet("carnumber")]
		public async Task<ActionResult<string>> GetUserCarNumber()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await userManager.FindByEmailAsync(email);
			
			return Ok(user.CarNumber);
		}


		[Authorize]
		[HttpPut("carnumber")]
		public async Task<ActionResult<string>> UpdateUser(string carNumber)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await userManager.FindByEmailAsync(email);
			user.CarNumber = carNumber;
			var Result = await userManager.UpdateAsync(user);
			if(!Result.Succeeded) return BadRequest(new ApiErrorResponse(400));
			return Ok(carNumber);
		}

		[HttpGet]
		public async Task<ActionResult<bool>> CheckEmailExsist(string email)
		{
			return await userManager.FindByEmailAsync(email) is not null;
		}
	}
}
