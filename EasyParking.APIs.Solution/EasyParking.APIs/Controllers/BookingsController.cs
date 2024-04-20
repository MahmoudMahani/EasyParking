using AutoMapper;
using EasyParking.APIs.Dtos;
using EasyParking.APIs.Errors;
using EasyParking.Core.Entities.Booking_Aggregation;
using EasyParking.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EasyParking.APIs.Controllers
{
    [Authorize]
    public class BookingsController : ApiBaseController
    {
        private readonly IBookingService bookingService;
        private readonly IMapper mapper;

        public BookingsController(IBookingService bookingService, IMapper mapper)
        {
            this.bookingService = bookingService;
            this.mapper = mapper;
        }
        [ProducesResponseType(typeof(Booking), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(BookingDto bookingDto)
        {
            var CustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = mapper.Map<AddressDto, Address>(bookingDto.CustomerAddress);
            var Booking = await bookingService.CreateBookingAsync(CustomerEmail, bookingDto.BasketId, address);
            if (Booking is null) return BadRequest(new ApiErrorResponse(400));
            return Ok(Booking);

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BookingToReturnDto>>> GetBookingsForUser()
        {
            var CustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Bookings = await bookingService.GetBookkingsForUserAsync(CustomerEmail);
            var mappedBooking = mapper.Map<IReadOnlyList<Booking>, IReadOnlyList<BookingToReturnDto>>(Bookings);
            return Ok(mappedBooking);
        }


        [ProducesResponseType(typeof(Booking), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingToReturnDto>> GetBookingForUser(int id)
        {
            var CustomerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Booking = await bookingService.GetBookkingByIdForUserAsync(id, CustomerEmail);
            if (Booking is null) return NotFound(new ApiErrorResponse(404));
            var mappedBooking = mapper.Map<Booking, BookingToReturnDto>(Booking);
            return Ok(mappedBooking);
        }

    }
}
