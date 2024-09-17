


namespace Microservice.Catalog.Products.CreateProduct
{
    public record CreateProductReqeust(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(string Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductReqeust request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.Id}",response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }
    }
}