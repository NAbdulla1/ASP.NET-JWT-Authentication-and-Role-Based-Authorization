using Authentication_and_Authorization.Core.DTOs;
using Authentication_and_Authorization.Core.Exceptions;
using Authentication_and_Authorization.Core.Models;
using Authentication_and_Authorization.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_and_Authorization.Core.Services
{
    public interface IAuthService
    {
        Task<AccessToken> LoginUser(LoginUserDTO loginUserDTO);
    }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJsonWebTokenService _jsonWebTokenService;

        public AuthService(IUnitOfWork unitOfWork, IJsonWebTokenService jsonWebTokenService)
        {
            _unitOfWork = unitOfWork;
            _jsonWebTokenService = jsonWebTokenService;
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
    }
}
