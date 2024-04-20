using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Entities.Booking_Aggregation
{
    public class BookingItem : BaseEntity
    {
        public BookingItem(string name, string pictureUrl, bool state, int quantity)
        {
            Name = name;
            PictureUrl = pictureUrl;
            State = state;
            Quantity = quantity;
        }

        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public bool State { get; set; }
        public int Quantity { get; set; }
    }
}
