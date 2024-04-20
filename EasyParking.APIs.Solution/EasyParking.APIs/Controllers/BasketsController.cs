using AutoMapper;
using EasyParking.APIs.Dtos;
using EasyParking.APIs.Errors;
using EasyParking.Core.Entities;
using EasyParking.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace EasyParking.APIs.Controllers
{

    public class BasketsController : ApiBaseController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketsController(IBasketRepository basketRepository,IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        [HttpGet("{BasketID}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketID)
        {
            var Basket = await basketRepository.GetBasketAsync(BasketID);
            return Basket is null ? new CustomerBasket(BasketID) : Ok(Basket);
        }
        [HttpPost]
        
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basket)
        {
             var MapperCustomerBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
           
            var CreateOrUpdate = await basketRepository.UpdateBasketAsync(MapperCustomerBasket);
            if (CreateOrUpdate == null) return BadRequest(new ApiErrorResponse(400));
            return Ok(CreateOrUpdate);
        }
        [HttpDelete]
        public  async Task<ActionResult<bool>> DeleteBasket (string BasketId)
        {
            return await basketRepository.DeleteBasketAsync(BasketId);
        }
    }
}
