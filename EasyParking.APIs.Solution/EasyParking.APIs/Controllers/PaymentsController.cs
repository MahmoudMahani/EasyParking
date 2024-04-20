using AutoMapper;
using EasyParking.APIs.Dtos;
using EasyParking.APIs.Errors;
using EasyParking.Core.Entities;
using EasyParking.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EasyParking.APIs.Controllers
{
    [Authorize]
    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;
        private readonly ILogger<PaymentIntent> logger;
        const string _whSercert = "whsec_8976a5ffe85c5f887fb5a671ab04a0eaf36b474bfe19d80a8b5e57613fbc23f0";

        public PaymentsController(IPaymentService paymentService,IMapper mapper, ILogger<PaymentIntent> logger)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntrnt(string basketId)
        {
            var basket = await paymentService.CreateOrUpdataPaymentIntent(basketId);
            if (basket == null) return BadRequest(new ApiErrorResponse(400,"A Problem With Your Basket"));
            var MapBascket = mapper.Map<CustomerBasket,CustomerBasketDto>(basket);
            return Ok(MapBascket);
        }



        [HttpPost("webhook")]
        public async Task<IActionResult> Stripewephook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();   
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _whSercert);
                var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        await paymentService.UpdatePaymentIntentToSucceedOrFailed(paymentIntent.Id,true);
                    logger.LogInformation("Payment is Succeed", paymentIntent.Id);
                        break;
                    case Events.PaymentIntentPaymentFailed:
                        await paymentService.UpdatePaymentIntentToSucceedOrFailed(paymentIntent.Id, false);
                    logger.LogInformation("Payment is Failed", paymentIntent.Id);
                    break;

                }

                return Ok();
        }
    }
}
