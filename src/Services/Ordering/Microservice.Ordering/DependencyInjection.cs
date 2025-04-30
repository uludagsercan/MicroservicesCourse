

namespace Microservice.Ordering
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrderingServices(this IServiceCollection services)
        {
            return services;
        }
        public static WebApplication UseApiServices(this WebApplication app)
        {
       
            return app;
        }
    }
}