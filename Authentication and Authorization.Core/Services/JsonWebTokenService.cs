using Authentication_and_Authorization.Core.Models;
using Authentication_and_Authorization.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication_and_Authorization.Core.Services
{
    public interface IJsonWebTokenService
    {
        string CreateToken(User user);
    }

    public class JsonWebTokenService : IJsonWebTokenService
    {
        private readonly JWTConfig _jwtConfig;

        public JsonWebTokenService(IConfiguration configuration) {
            _jwtConfig = configuration.GetRequiredSection(JWTConfig.SectionName).Get<JWTConfig>();
        }

        public string CreateToken(User user)
        {
            var nowUtc = DateTime.UtcNow;
            var expirationDuration = TimeSpan.FromMinutes(2);
            var expirationUtc = nowUtc.Add(expirationDuration);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _jwtConfig.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(nowUtc).ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            var signingCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: expirationUtc,
                signingCredentials: signingCred
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
