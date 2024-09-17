


namespace Microservice.Catalog.Products.GetProductById
{
    public record GetProductByIdResponse(string Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public class GetProductByQueryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (string id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(Guid.Parse(id)));
                return Results.Ok(result);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id"); ;
        }
    }
}