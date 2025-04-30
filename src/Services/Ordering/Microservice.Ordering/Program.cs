using Microservice.Application;
using Microservice.Infrastructure;
using Microservice.Infrastructure.Data.Extensions;
using Microservice.Ordering;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOrderingServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();
app.UseApiServices();
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}
app.UseApiServices();
app.Run();
