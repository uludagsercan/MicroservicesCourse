

using Microservice.Catalog.Exeptions;
using Microservice.Catalog.Models;

namespace Microservice.Catalog.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product =await session.LoadAsync<Product>(request.Id);
            if(product is null) throw new ProductNotFoundExeption();
            return product.Adapt<GetProductByIdResult>();
        }
    }
}