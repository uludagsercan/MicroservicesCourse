

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Infrastructure.Data.Extensions
{
    public static class DatabaseExtension
    {

        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();
            var services = serviceScope.ServiceProvider;
            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedAsync(dbContext);
        }

        private async static Task SeedAsync(ApplicationDbContext dbContext)
        {
            // Seed data
            await SeedCustomerAsync(dbContext);
            await SeedProductAsync(dbContext);
            await SeedOrderWithItemsAsync(dbContext);

        }
        public async static Task SeedCustomerAsync(ApplicationDbContext dbContext)
        {
            // Seed data
            if(!await dbContext.Customers.AnyAsync())
            {
                await dbContext.Customers.AddRangeAsync(InitialData.Customers);
                await dbContext.SaveChangesAsync();
            }
        }
        public async static Task SeedProductAsync(ApplicationDbContext dbContext)
        {
            // Seed data
            if(!await dbContext.Products.AnyAsync())
            {
                await dbContext.Products.AddRangeAsync(InitialData.Products);
                await dbContext.SaveChangesAsync();
            }
        }
        public async static Task SeedOrderWithItemsAsync(ApplicationDbContext dbContext)
        {
            // Seed data
            if(!await dbContext.Orders.AnyAsync())
            {
                await dbContext.Orders.AddRangeAsync(InitialData.OrdersWithItems);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}