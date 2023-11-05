using Authentication_and_Authorization.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authentication_and_Authorization.ExtensionMethods
{
    public static class JwtValidatorConfig
    {
        public static void Configure(this JwtBearerOptions options, WebApplicationBuilder builder)
        {
            var jwtConfig = builder.Configuration.GetRequiredSection(JWTConfig.SectionName).Get<JWTConfig>();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = jwtConfig.Audience,
                ValidIssuer = jwtConfig.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
