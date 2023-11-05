using Microsoft.Extensions.DependencyInjection;
using Authentication_and_Authorization.Data.Infrastructure;
using Authentication_and_Authorization.Core.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Authentication_and_Authorization.Core.QueryBuilders;
using Authentication_and_Authorization.Data.InMemory.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Authentication_and_Authorization.Core.Infrastructure
{
    public static class CoreServicesRegistry
    {
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
        {
            return services.RegisterUnitOfWork()
                .RegisterInMemoryStructures()
                .AddScoped<IJsonWebTokenService, JsonWebTokenService>()
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfigService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IUserQueryBuilder, UserQueryBuilder>()
                .AddScoped<IUserService, UserService>();
        }
    }
}
