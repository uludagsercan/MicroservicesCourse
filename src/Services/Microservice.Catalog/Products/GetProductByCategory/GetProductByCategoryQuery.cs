


using Microservice.Catalog.Models;

namespace Microservice.Catalog.Products.GetProductByCategory
{

    public record GetProductByCategoryQuery(string CategoryName):IQuery<List<GetProductByCategoryResult>>;
    public record GetProductByCategoryResult(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, List<GetProductByCategoryResult>>
    {
        public async Task<List<GetProductByCategoryResult>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var query =await session.Query<Product>()
            .Where(x=> x.Category.Contains(request.CategoryName)).ToListAsync(cancellationToken);
            var result = query.Adapt<List<GetProductByCategoryResult>>();
            return result;
        }
    }
}