using System.ComponentModel.DataAnnotations;

namespace Authentication_and_Authorization.Core.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
    }
}
