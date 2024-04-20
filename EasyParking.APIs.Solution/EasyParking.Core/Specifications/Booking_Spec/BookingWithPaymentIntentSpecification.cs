using EasyParking.Core.Entities.Booking_Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Specifications.Booking_Spec
{
    public class BookingWithPaymentIntentSpecification : BaseSpecification<Booking>
    {
        public BookingWithPaymentIntentSpecification(string IntentId) : base(B=>B.PaymentIntentId==IntentId)
        {
            
        }
    }
}
