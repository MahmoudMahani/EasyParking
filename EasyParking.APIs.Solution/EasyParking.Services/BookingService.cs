using EasyParking.Core;
using EasyParking.Core.Entities;
using EasyParking.Core.Entities.Booking_Aggregation;
using EasyParking.Core.Repositories;
using EasyParking.Core.Services;
using EasyParking.Core.Specifications.Booking_Spec;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPaymentService paymentService;

        public BookingService(IBasketRepository basketRepository , IUnitOfWork unitOfWork,IPaymentService paymentService)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.paymentService = paymentService;
        }
        public async Task<Booking?> CreateBookingAsync(string CustomerEmail, string BasketId, Address CustomerAddress)
        {
            var basket = await basketRepository.GetBasketAsync(BasketId);
            var bookingItems = new List<BookingItem>();
            if (basket?.Items?.Count > 0)
            {
                foreach(var item in basket.Items)
                {
                    var bakya = await unitOfWork.Repository<Pakya>().GetByIdAsync(item.Id);
                    var bookingitem = new BookingItem(bakya.Name,bakya.PictureUrl,bakya.State, item.Quantiy);
                    bookingItems.Add(bookingitem);
                }
            }
            var Spec =new BookingWithPaymentIntentSpecification(basket.PaymentIntentId) ;
            var ExisitBooking = await unitOfWork.Repository<Booking>().GetByIdlWithSpecAsync(Spec);
            if(ExisitBooking is not null)
            {
                unitOfWork.Repository<Booking>().Delete(ExisitBooking);
                await paymentService.CreateOrUpdataPaymentIntent(basket.Id);
            }
            var booking = new  Booking(CustomerEmail,basket.PaymentIntentId ,CustomerAddress, bookingItems);
            await unitOfWork.Repository<Booking>().Add(booking);
            var result = await unitOfWork.Complete();
            if(result <=0) return null;
            return booking;
        }

        public async Task<Booking> GetBookkingByIdForUserAsync(int BookingId, string CustomerEmail)
        {
            var Spec = new BookingSpecification(BookingId,CustomerEmail);
            var Booking = await unitOfWork.Repository<Booking>().GetByIdlWithSpecAsync(Spec); 
            return Booking;
        }

        public async Task<IReadOnlyList<Booking>> GetBookkingsForUserAsync(string CustomerEmail)
        {
            var Spec = new BookingSpecification(CustomerEmail);
            var Bookings = await unitOfWork.Repository<Booking>().GetAllWithSpecAsync(Spec);
            return Bookings;
        }
    }
}
