using System.ComponentModel.DataAnnotations;

namespace WebApiArchutecture.Application.DTOs.UserDto
{
    public class UserLoginDto
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
