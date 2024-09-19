

using FluentValidation;
using Microservice.Basket.Data;

namespace Microservice.Basket.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {   
             RuleFor(p=> p.UserName).NotEmpty().WithMessage("User Name is Required");
        }
    }
    public class DeleteBasketHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var result = await basketRepository.DeleteBasket(request.UserName,cancellationToken);
            return new  DeleteBasketResult(result);
        }
    }
}