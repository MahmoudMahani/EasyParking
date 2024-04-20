using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Entities.Booking_Aggregation
{
    public class Booking : BaseEntity
    {
        public Booking() { }
        public Booking(string customerEmail,string IntentId, Address customerAddress, ICollection<BookingItem> items)
        {
            CustomerEmail = customerEmail;
            CustomerAddress = customerAddress;
            Items = items;
            PaymentIntentId = IntentId;
        }

        public string CustomerEmail { get; set; }
        public DateTimeOffset Bookingdate { get; set; } = DateTimeOffset.Now;
        public Address  CustomerAddress { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public ICollection<BookingItem> Items { get; set;} = new HashSet<BookingItem>();
         public string PaymentIntentId { get; set; } 
    }
}
