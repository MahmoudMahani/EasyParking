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
	public class PakyaConfigurations : IEntityTypeConfiguration<Pakya>
	{
		public void Configure(EntityTypeBuilder<Pakya> builder)
		{
			builder.Property(P => P.Name).IsRequired().HasMaxLength(50);
			builder.Property(P => P.State).IsRequired();
			builder.Property(P => P.PictureUrl).IsRequired();

			builder.HasOne(P => P.Garage).WithMany().HasForeignKey(P => P.GarageId);
		}
	}
}
