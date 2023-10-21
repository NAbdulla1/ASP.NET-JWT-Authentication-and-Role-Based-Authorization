using Authentication_and_Authorization.Data;
using Authentication_and_Authorization.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_and_Authorization.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly UserContext _dbContext;

        public UserController(UserContext dbContext)
        {
            _dbContext = dbContext;
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

            return Ok(user);
        }
    }
}
