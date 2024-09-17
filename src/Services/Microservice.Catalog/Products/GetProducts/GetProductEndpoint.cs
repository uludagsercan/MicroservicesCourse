


namespace Microservice.Catalog.Products.GetProducts
{

    public record GetProductResponse(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters]GetProductsRequest request,ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<List<GetProductResponse>>();
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}