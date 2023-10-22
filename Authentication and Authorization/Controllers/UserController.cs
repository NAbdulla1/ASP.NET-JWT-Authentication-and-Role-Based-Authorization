using Authentication_and_Authorization.Data;
using Authentication_and_Authorization.Data.Entities;
using Authentication_and_Authorization.Models;
using Authentication_and_Authorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication_and_Authorization.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly UserAccountContext _dbContext;
        private readonly IJsonWebTokenService _jsonWebTokenService;
        private readonly IConfiguration _configuration;

        public UserController(UserAccountContext dbContext, IJsonWebTokenService jsonWebTokenService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _jsonWebTokenService = jsonWebTokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([Bind("Email,Password")] User user)
        {
            if (await _dbContext.Users.Where(u => u.Email == user.Email).FirstOrDefaultAsync() != null)
            {
                ModelState.AddModelError("Email", $"An user already exists with the email '{user.Email}'.");

                return ValidationProblem();
            }

            user.UserType = UserType.Customer;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccessToken>> Login([Bind("Email,Password")] User loginUser)
        {
            var user = await _dbContext.Users.Where(u => u.Email == loginUser.Email).FirstOrDefaultAsync();
            if (user == null || user.Password != loginUser.Password)
            {
                return Unauthorized();
            }

            string tokenString = _jsonWebTokenService.CreateToken(user);

            return Ok(new AccessToken { Token = tokenString });
        }

        [HttpGet]
        [Route("admin-test")]
        [Authorize(Roles = "Admin")]
        public IActionResult TestAdminAuthenticationAndAuthentication()
        {
            return Ok(new { msg = "Authenticated user and Authorized the user as an Admin." });
        }

        [HttpGet]
        [Route("customer-test")]
        [Authorize(Roles = "Customer")]
        public IActionResult TestCustomerAuthenticationAndAuthentication()
        {
            return Ok(new { msg = "Authenticated user and Authorized the user as a Customer." });
        }
    }
}
