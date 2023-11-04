using Microsoft.Extensions.DependencyInjection;

namespace Authentication_and_Authorization.Data.Infrastructure
{
    public static class DataServicesRegistry
    {
        public static IServiceCollection RegisterUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
