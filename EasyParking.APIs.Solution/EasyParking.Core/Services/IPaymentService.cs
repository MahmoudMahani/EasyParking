using EasyParking.Core.Entities;
using EasyParking.Core.Entities.Booking_Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Services
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdataPaymentIntent(string basketId);

        Task<Booking> UpdatePaymentIntentToSucceedOrFailed(string PaymentIntentId, bool Flag);
    }
}
