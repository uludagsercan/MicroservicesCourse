
namespace Microservice.Basket.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken=default);
        Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasket(string userName,CancellationToken cancellationToken = default);
    }
}