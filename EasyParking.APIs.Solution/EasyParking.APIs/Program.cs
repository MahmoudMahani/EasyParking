using EasyParking.APIs.Errors;
using EasyParking.APIs.ExtenTions;
using EasyParking.APIs.Helpers;
using EasyParking.APIs.Middlewares;
using EasyParking.Core.Entities;
using EasyParking.Core.Entities.Identity;
using EasyParking.Core.Repositories;
using EasyParking.Repository;
using EasyParking.Repository.Data;
using EasyParking.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace EasyParking.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Cnfigure Services works with DI
			// Add services to the container.

			builder.Services.AddControllers(); //Add Services ASP Web APIs
			//Databases
			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddScoped<IGenericRepository<Garage>,GenericRepository<Garage>>();
			builder.Services.AddScoped<IGenericRepository<Pakya>, GenericRepository<Pakya>>();
			builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
			{
				var connection = builder.Configuration.GetConnectionString("RedisConnection");
				return ConnectionMultiplexer.Connect(connection);
			});
			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});

			//Extention Services
			builder.Services.AddApplicationServices();
			builder.Services.AddIdentityServices(builder.Configuration);
			builder.Services.AddCors(Options =>
			{
				Options.AddPolicy("MyPolicy", option =>
				{
					option.AllowAnyHeader();
					option.AllowAnyMethod();
					option.WithOrigins(builder.Configuration["FrontBaseUrl"]);
				});
			});
			builder.Services.AddSwaggerServices();
			
			#endregion

			var app = builder.Build();

			//Explicitly
			var scope = app.Services.CreateScope(); //Services Scoped
			var services = scope.ServiceProvider;
			//LoggerFactory
			var LoggerFactory = services.GetRequiredService<ILoggerFactory>(); 
			try
			{
				var dbContext = services.GetRequiredService<StoreContext>(); //Ask CLR to Create Object from Store Context Explicitly
				await dbContext.Database.MigrateAsync(); //Update-database
				await StoreContextSeed.SeedAsync(dbContext);

				var IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
				await IdentityDbContext.Database.MigrateAsync();

				var userManager = services.GetRequiredService<UserManager<AppUser>>();
				await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
			}
			catch (Exception ex)
			{
				var logger = LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an error occured during apply migration");
			}
			#region Cofigure Request into Pipilines
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddleWares();
			}
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseHttpsRedirection();
			app.UseStatusCodePagesWithRedirects("/errors/{0}");
			app.UseCors("MyPolicy");
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();
			#endregion

			app.Run();
		}
	}
}