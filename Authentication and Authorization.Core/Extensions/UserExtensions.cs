using Authentication_and_Authorization.Core.Models;
using Authentication_and_Authorization.Data.Entities;

namespace Authentication_and_Authorization.Core.Extensions
{
    public static class UserExtensions
    {
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType.ToString()
            };
        }
    }
}
