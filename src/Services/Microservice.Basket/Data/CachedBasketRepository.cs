


using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Microservice.Basket.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await basketRepository.DeleteBasket(userName, cancellationToken);
            await cache.RemoveAsync(userName,cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
               return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            var basket = await basketRepository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName,JsonSerializer.Serialize(basket));
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreBasket(shoppingCart, cancellationToken);
            await cache.SetStringAsync(shoppingCart.UserName,JsonSerializer.Serialize(shoppingCart),cancellationToken);
            return shoppingCart;
        }
    }
}