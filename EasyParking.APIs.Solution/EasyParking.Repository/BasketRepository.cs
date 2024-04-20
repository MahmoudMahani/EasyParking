using EasyParking.Core.Entities;
using EasyParking.Core.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EasyParking.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase dataBase;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            dataBase = redis.GetDatabase();
        }

        

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await dataBase.KeyDeleteAsync(BasketId); 
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var Basket = await dataBase.StringGetAsync(BasketId);
            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);
            var createOrUpdate = await dataBase.StringSetAsync(Basket.Id, JsonBasket, TimeSpan.FromDays(1));
            if (!createOrUpdate) return null;
            return await GetBasketAsync(Basket.Id);
        }
    }
}
