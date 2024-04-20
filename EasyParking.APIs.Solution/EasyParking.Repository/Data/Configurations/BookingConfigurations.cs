using EasyParking.Core.Entities.Booking_Aggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Repository.Data.Configurations
{
    public class BookingConfigurations : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.OwnsOne(B => B.CustomerAddress, CA => CA.WithOwner());
            builder.Property(B => B.Status)
                .HasConversion(
                     BS => BS.ToString(),
                     BS => (BookingStatus)Enum.Parse(typeof(BookingStatus), BS)
                );
            builder.HasMany(B => B.Items)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
