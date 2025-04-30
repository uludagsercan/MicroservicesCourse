
using Microsoft.EntityFrameworkCore;

namespace Microservice.DiscountGrpc.Data
{
    public static class Extensions
    {
        public static  IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DiscountContext>();
                 context.Database.MigrateAsync().Wait();
                return app;
            }
        }
    }
}