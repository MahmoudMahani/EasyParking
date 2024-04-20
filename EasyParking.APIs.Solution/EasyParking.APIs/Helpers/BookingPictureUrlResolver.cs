using AutoMapper;
using EasyParking.APIs.Dtos;
using EasyParking.Core.Entities.Booking_Aggregation;

namespace EasyParking.APIs.Helpers
{
    public class BookingPictureUrlResolver : IValueResolver<BookingItem, BookingItemDto, string>
    {
        private readonly IConfiguration configuration;

        public BookingPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(BookingItem source, BookingItemDto destination, string destMember, ResolutionContext context)
        {
           if(!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}";

           return string.Empty ;
        }
    }
}
