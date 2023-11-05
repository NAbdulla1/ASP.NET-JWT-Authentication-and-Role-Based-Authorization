using Authentication_and_Authorization.Core.DTOs;
using Authentication_and_Authorization.Core.Models;
using Authentication_and_Authorization.Core.Services;
using Authentication_and_Authorization.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Authentication_and_Authorization.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(
            IUserService userService,
            IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PageDTO<UserDTO>>> GetUsers([FromQuery] IndexDTO indexData, string? searchTerm)
        {
            var userPage = await _userService.GetAll(indexData, searchTerm);

            return Ok(userPage);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetUser(long id)
        {
            var user = await _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Register(UserRegisterDTO user)
        {
            try
            {
                var newUser = await _userService.CreateCustomer(user);

                return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
            }
            catch (UserWithEmailAlreadyExistsException ex)
            {
                ModelState.AddModelError(nameof(user.Email), ex.Message);
                return ValidationProblem();
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AccessToken>> Login(LoginUserDTO loginUserDTO)
        {
            try
            {
                var accessToken = await _authService.LoginUser(loginUserDTO);

                return Ok(accessToken);
            }
            catch (UnauthenticatedUserException ex)
            {
                return Problem(title: ex.Message, statusCode: StatusCodes.Status401Unauthorized);
            }
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Logout([FromHeader(Name = "Authorization")] string bearerToken)
        {
            try
            {
                var tokenExp = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Exp);
                await _authService.Logout(bearerToken, tokenExp);

                return Ok();
            }
            catch
            {
                return Problem("Unable to logout.");
            }
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
