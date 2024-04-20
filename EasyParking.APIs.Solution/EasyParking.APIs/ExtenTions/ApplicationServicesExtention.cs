using EasyParking.APIs.Helpers;
using EasyParking.Core.Repositories;
using EasyParking.Repository.Identity;
using EasyParking.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyParking.APIs.Errors;
using EasyParking.Core;
using EasyParking.Core.Services;
using EasyParking.Services;
using Stripe;

namespace EasyParking.APIs.ExtenTions
{
	public static class ApplicationServicesExtention
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
		{
			Services.AddScoped<IPaymentService, PaymentService>();
			Services.AddScoped<IBookingService,BookingService>();
			Services.AddScoped<IUnitOfWork, UnitOfWork>();
			//Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
			Services.AddAutoMapper(typeof(MappingProfiles));
			Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
														 .SelectMany(p => p.Value.Errors)
														 .Select(E => E.ErrorMessage).ToArray();

					var ValidationErrorResponse = new ApiValidationErrorResponse()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(ValidationErrorResponse);
				};
			});

			return Services;
		}
	}
}
