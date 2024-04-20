using AutoMapper;
using EasyParking.APIs.Dtos;
using EasyParking.Core.Entities;
using EasyParking.Core.Entities.Booking_Aggregation;
using EasyParking.Core.Entities.Identity;

namespace EasyParking.APIs.Helpers
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
		{
			CreateMap<Pakya, PakyaToReturnDto>()
					 .ForMember(PG => PG.Garage, O => O.MapFrom(P => P.Garage.Name));
					 
			CreateMap<Garage, GarageToReturnDto>();
			CreateMap<AppUser,UserDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
			
			CreateMap<AddressDto,Core.Entities.Booking_Aggregation.Address>();

			CreateMap<Booking, BookingToReturnDto>();
			CreateMap<BookingItem, BookingItemDto>()
				    .ForMember(D=>D.PictureUrl,B=>B.MapFrom<BookingPictureUrlResolver>());
			
        }
    }
}
