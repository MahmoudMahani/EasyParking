using EasyParking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EasyParking.Repository.Data
{
	public static class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext dbContext)
		{
			if(!dbContext.Garages.Any())
			{
				var garagesData = File.ReadAllText("../EasyParking.Repository/Data/DataSeed/garages.json");
				var garages = JsonSerializer.Deserialize<List<Garage>>(garagesData);
				if (garages?.Count() > 0)
				{
					foreach (var garage in garages)
						await dbContext.Garages.AddAsync(garage);

					await dbContext.SaveChangesAsync();
				}
			}
			
			if(!dbContext.Pakyas.Any())
			{
				var pakyasData = File.ReadAllText("../EasyParking.Repository/Data/DataSeed/pakyas.json");
				var pakyas = JsonSerializer.Deserialize<List<Pakya>>(pakyasData);
				if (pakyas?.Count() > 0)
				{
					foreach (var pakya in pakyas)
						await dbContext.Pakyas.AddAsync(pakya);

					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
