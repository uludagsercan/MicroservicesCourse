

namespace Microservice.Catalog.Products.GetProductByCategory
{
    // public record GetProductByCategoryRequest(string CategoryName);
    public record GetProductByCategoryResponse(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{categoryName}", async (string categoryName, ISender sender) =>
            {
                var result =await sender.Send(new GetProductByCategoryQuery(categoryName));
                var response = result.Adapt<List<GetProductByCategoryResponse>>();
                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Category")
            .WithDescription("Get Product By Category");
        }
    }
}