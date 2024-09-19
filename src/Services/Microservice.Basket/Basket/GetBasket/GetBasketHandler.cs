
using Microservice.Basket.Data;

namespace Microservice.Basket.Basket.GetBasket
{
    public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);
    public class GetBasketHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            //TODO: get basket from database
           //var basket = await _repository.GetBasket(request.UserName);
           var basket = await basketRepository.GetBasket(request.UserName,cancellationToken);
           return new GetBasketResult(basket);
        }
    }
}