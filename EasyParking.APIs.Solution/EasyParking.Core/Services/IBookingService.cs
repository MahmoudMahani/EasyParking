using EasyParking.Core.Entities.Booking_Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Services
{
    public interface IBookingService
    {
        Task<Booking?> CreateBookingAsync(string CustomerEmail, string BasketId, Address CustomerAddress);
        Task<Booking> GetBookkingByIdForUserAsync(int BookingId, string CustomerEmail);
        Task<IReadOnlyList<Booking>> GetBookkingsForUserAsync( string CustomerEmail);
    }
}
