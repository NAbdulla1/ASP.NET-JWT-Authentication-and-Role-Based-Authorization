using Authentication_and_Authorization.Data;
using Authentication_and_Authorization.Data.Entities;
using Authentication_and_Authorization.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication_and_Authorization.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly UserContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserController(UserContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([Bind("Email,Password")] User user)
        {
            user.UserType = UserType.Customer;
            _dbContext.users.Add(user);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([Bind("Email,Password")] User loginUser)
        {
            var user = _dbContext.users.Where(u => u.Email == loginUser.Email).FirstOrDefault();
            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Password != loginUser.Password)
            {
                return Unauthorized();
            }

            var nowUtc = DateTime.UtcNow;
            var expirationDuration = TimeSpan.FromMinutes(2);
            var expirationUtc = nowUtc.Add(expirationDuration);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["JwtSecurityToken:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(nowUtc).ToString(), ClaimValueTypes.Integer64),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("UserType", user.UserType.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityToken:Key"]));
            var signingCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSecurityToken:Issuer"],
                audience: _configuration["JwtSecurityToken:Audience"],
                claims: claims,
                expires: expirationUtc,
                signingCredentials: signingCred
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(new AccessToken { Token = tokenString });
        }

        [HttpGet]
        [Authorize]
        public IActionResult TestAuth()
        {
            return Ok(new { msg = "Authenticated" });
        }
    }
}
