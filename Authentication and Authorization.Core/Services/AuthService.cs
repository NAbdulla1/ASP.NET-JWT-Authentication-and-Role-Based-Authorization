using Authentication_and_Authorization.Core.DTOs;
using Authentication_and_Authorization.Core.Exceptions;
using Authentication_and_Authorization.Core.Models;
using Authentication_and_Authorization.Data;
using Authentication_and_Authorization.Data.InMemory.Repositories;

namespace Authentication_and_Authorization.Core.Services
{
    public interface IAuthService
    {
        Task<AccessToken> LoginUser(LoginUserDTO loginUserDTO);
        Task Logout(string bearerToken, string expireAtUtc);
    }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJsonWebTokenService _jsonWebTokenService;
        private readonly IBlockedJWTRepository _blockedJWTRepository;

        public AuthService(
            IUnitOfWork unitOfWork,
            IJsonWebTokenService jsonWebTokenService,
            IBlockedJWTRepository blockedJWTRepository)
        {
            _unitOfWork = unitOfWork;
            _jsonWebTokenService = jsonWebTokenService;
            _blockedJWTRepository = blockedJWTRepository;
        }

        public async Task<AccessToken> LoginUser(LoginUserDTO loginUserDTO)
        {
            var user = await _unitOfWork.Users.GetByEmail(loginUserDTO.Email);

            if (user == null || user.Password != loginUserDTO.Password)
            {
                throw new UnauthenticatedUserException("User email or password is not correct.");
            }

            var token = _jsonWebTokenService.CreateToken(user);

            return new AccessToken { Token = token };
        }

        public async Task Logout(string bearerToken, string expireAtTimestamp)
        {
            if(!int.TryParse(expireAtTimestamp, out int timestamp))
            {
                throw new Exception("Unable to logout.");
            }

            var willExpireAt = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;

            double seconds = willExpireAt.Subtract(DateTime.UtcNow).TotalSeconds;
            if (seconds > 0)
            {
                await _blockedJWTRepository.BlockJWT(bearerToken, seconds);
            }
        }
    }
}
