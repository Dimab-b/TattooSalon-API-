using WebApiArchutecture.Application.Features.Auth;
using  WebApiArchutecture.Application.Features.Auth;

namespace WebApiArchutecture.Application.DTOs.UserDto
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; } 
    }
}
