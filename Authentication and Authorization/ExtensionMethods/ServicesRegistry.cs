using Authentication_and_Authorization.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Authentication_and_Authorization.ExtensionMethods
{
    public static class ServicesRegistry
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            return services
                .AddScoped<IJsonWebTokenService, JsonWebTokenService>()
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfigService>();
        }
    }
}
