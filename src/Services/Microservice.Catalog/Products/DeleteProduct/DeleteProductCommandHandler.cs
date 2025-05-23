

using Microservice.Catalog.Models;

namespace Microservice.Catalog.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSucsess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p=> p.Id).NotEmpty().WithMessage("Product Id is required");
        }

    }
    public class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            session.Delete<Product>(request.Id);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}