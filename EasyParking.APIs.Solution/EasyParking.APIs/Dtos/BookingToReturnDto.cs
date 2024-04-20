using EasyParking.Core.Entities.Booking_Aggregation;

namespace EasyParking.APIs.Dtos
{
    public class BookingToReturnDto
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public DateTimeOffset Bookingdate { get; set; } 
        public Address CustomerAddress { get; set; }
        public decimal Cost { get; set; }
        public BookingStatus Status { get; set; } 

        public ICollection<BookingItemDto> Items { get; set; } = new HashSet<BookingItemDto>();
        public string PaymentIntentId { get; set; }
    }
}
