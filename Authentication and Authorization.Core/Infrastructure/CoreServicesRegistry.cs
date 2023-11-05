using Microsoft.Extensions.DependencyInjection;
using Authentication_and_Authorization.Data.Infrastructure;
using Authentication_and_Authorization.Core.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Authentication_and_Authorization.Core.QueryBuilders;

namespace Authentication_and_Authorization.Core.Infrastructure
{
    public static class CoreServicesRegistry
    {
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
        {
            return services.RegisterUnitOfWork()
                .AddScoped<IJsonWebTokenService, JsonWebTokenService>()
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfigService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IUserQueryBuilder, UserQueryBuilder>()
                .AddScoped<IUserService, UserService>();
        }
    }
}
