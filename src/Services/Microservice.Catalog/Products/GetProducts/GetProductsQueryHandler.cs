

using Microservice.Catalog.Models;

namespace Microservice.Catalog.Products.GetProducts
{
    public record GetProductsResult(string Name, List<string> Category, string Description, string ImageFile, decimal Price, string Id);
    public record GetProductsQuery() : IQuery<List<GetProductsResult>>;
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, List<GetProductsResult>>
    {
        public async Task<List<GetProductsResult>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductQueryHandler.Handle called with {@Query}",request);
            var products = (await session.Query<Product>()
            .ToListAsync())
            .Adapt<List<GetProductsResult>>();
            return products;
        }
    }
}