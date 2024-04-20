using EasyParking.Core.Entities.Booking_Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Specifications.Booking_Spec
{
    public class BookingSpecification : BaseSpecification<Booking>
    {
        public BookingSpecification(string Email)
            : base(o=>o.CustomerEmail==Email)
        {
            Includes.Add(o => o.Items);
          
        }
        public BookingSpecification(int id ,string Email)
           : base(o => o.CustomerEmail == Email&& o.Id==id)
        {
            Includes.Add(o => o.Items);

        }
    }
}
