


using Microservice.Catalog.Exeptions;
using Microservice.Catalog.Models;

namespace Microservice.Catalog.Products.UpdateProduct
{
    public record UpdateProductCommand(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p=> p.Id).NotEmpty().WithMessage("Product Id is required");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required")
            .Length(2,150).WithMessage("Product name must be between 2 and 150 character");
            RuleFor(p => p.ImageFile).NotEmpty().WithMessage("Product image file is required");
            RuleFor(p => p.Category).NotEmpty().WithMessage("Product categorry is required");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Product price must be greater than zero");
        }
    }
    public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product =await session.LoadAsync<Product>(Guid.Parse(request.Id));
            if (product is null) throw new ProductNotFoundExeption();
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}