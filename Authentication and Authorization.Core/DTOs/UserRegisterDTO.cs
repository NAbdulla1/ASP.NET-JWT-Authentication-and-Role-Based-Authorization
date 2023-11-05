using Authentication_and_Authorization.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Authentication_and_Authorization.Core.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        [Compare(nameof(ConfirmPassword))]
        public string Password { get; set; }

        [Required]
        [MaxLength(64)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public User GetUser( UserType userType) {
            return new User
            {
                Email = Email,
                Password = Password,
                UserType = userType
            };
        }
    }
}
