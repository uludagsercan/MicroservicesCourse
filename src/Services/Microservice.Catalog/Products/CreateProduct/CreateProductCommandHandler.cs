

using Microservice.Catalog.Models;

namespace Microservice.Catalog.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(string Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p=> p.Name).NotEmpty().WithMessage("Product name is required");
            RuleFor(p=> p.ImageFile).NotEmpty().WithMessage("Product image file is required");
            RuleFor(p=> p.Category).NotEmpty().WithMessage("Product categorry is required");
            RuleFor(p=> p.Price).GreaterThan(0).WithMessage("Product price must be greater than zero");
        }
    }
    internal class CreateProductCommandHandler(IDocumentSession session,ILogger<CreateProductCommand> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Product Created Called With {@Command}",request);
            Product product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,

            };
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);


            return new CreateProductResult(product.Id.ToString());
        }
    }
}