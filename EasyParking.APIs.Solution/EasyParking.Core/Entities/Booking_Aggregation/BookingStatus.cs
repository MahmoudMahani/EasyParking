using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Entities.Booking_Aggregation
{
    public enum BookingStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value = "Ok")]
        Ok,
        [EnumMember(Value = "Canceled")]
        Canceled
    }
}
