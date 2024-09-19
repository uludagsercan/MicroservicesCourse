

using FluentValidation;
using Microservice.Basket.Data;

namespace Microservice.Basket.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(r=> r.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x=> x.Cart.UserName).NotEmpty().WithMessage("User name is required");
        }
    }
    public class StoreBasketHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            ShoppingCart cart = request.Cart;
            var basketResult = await basketRepository.StoreBasket(cart,cancellationToken);
            return new StoreBasketResult(basketResult.UserName);
        }
    }
}