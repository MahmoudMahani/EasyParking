using EasyParking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Repository.Data.Configurations
{
	public class GarageConfigurations : IEntityTypeConfiguration<Garage>
	{
		public void Configure(EntityTypeBuilder<Garage> builder)
		{
			builder.Property(G => G.Name).IsRequired().HasMaxLength(60);
			builder.Property(G => G.State).IsRequired();
			builder.Property(G => G.PictureUrl).IsRequired();
			builder.Property(G => G.LocationUrl).IsRequired();
			builder.Property(G => G.HourPrice).IsRequired().HasColumnType("decimal(18,2)");
		}
	}
}
