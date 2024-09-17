

using Marten.Pagination;
using Microservice.Catalog.Models;

namespace Microservice.Catalog.Products.GetProducts
{

    public record GetProductsResult(string Name, List<string> Category, string Description, string ImageFile, decimal Price, string Id);
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<List<GetProductsResult>>;
    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, List<GetProductsResult>>
    {
        public async Task<List<GetProductsResult>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
            .ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);
            return products.ToList().Adapt<List<GetProductsResult>>();
        }
    }
}