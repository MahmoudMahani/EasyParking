using EasyParking.Core.Entities;
using EasyParking.Core.Entities.Booking_Aggregation;
using EasyParking.Repository.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Repository.Data
{
	public class StoreContext : DbContext
	{
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
        {
            
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfiguration(new GarageConfigurations());
			//modelBuilder.ApplyConfiguration(new PakyaConfigurations());

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		public DbSet<Garage> Garages { get; set; }
        public DbSet<Pakya> Pakyas { get; set; }

		public DbSet<Booking> Bookings {  get; set; }
		public DbSet<BookingItem> BookingItems { get; set; }


    }
}
