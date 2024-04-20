using EasyParking.Core;
using EasyParking.Core.Entities;
using EasyParking.Core.Entities.Booking_Aggregation;
using EasyParking.Core.Repositories;
using EasyParking.Core.Services;
using EasyParking.Core.Specifications.Booking_Spec;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IConfiguration configuration ,IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdataPaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSetting:SecretKey"] ;
            var basket = await basketRepository.GetBasketAsync(basketId) ;
            if(basket is null)  return null;
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.BookingCost,
                    Currency ="usd",
                    PaymentMethodTypes= new List<string>() { "card"}
                } ;
                paymentIntent = await service.CreateAsync(options) ;
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClintSecret = paymentIntent.ClientSecret;
            }
            else
            {

                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.BookingCost,
                    
                };
                await service.UpdateAsync(basket.PaymentIntentId,options);
                
            }

            await basketRepository.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<Booking> UpdatePaymentIntentToSucceedOrFailed(string PaymentIntentId, bool Flag)
        {
            var Spec = new BookingWithPaymentIntentSpecification(PaymentIntentId);
            var Booking = await unitOfWork.Repository<Booking>().GetByIdlWithSpecAsync(Spec);
            if (Flag)

                Booking.Status = BookingStatus.Ok;
            else
                Booking.Status = BookingStatus.Pending;

            unitOfWork.Repository<Booking> ().Update(Booking);
            await unitOfWork.Complete();

            return Booking;
            
        }
    }
}
