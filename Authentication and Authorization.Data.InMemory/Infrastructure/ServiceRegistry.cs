using Authentication_and_Authorization.Data.InMemory.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication_and_Authorization.Data.InMemory.Infrastructure
{
    public static class ServiceRegistry
    {
        public static IServiceCollection RegisterInMemoryStructures(this IServiceCollection services)
        {
            return services.AddScoped<IInMemoryDB, RedisDB>()
                .AddScoped<IBlockedJWTRepository, BlockedJWTRepository>();
        }
    }
}
