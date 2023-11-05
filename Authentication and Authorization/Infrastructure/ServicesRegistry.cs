using Authentication_and_Authorization.Core.Infrastructure;

namespace Authentication_and_Authorization.Infrastructure
{
    public static class ServicesRegistry
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services.RegisterCoreServices();
        }
    }
}
