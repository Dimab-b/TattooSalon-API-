using MediatR;
using WebApiArchutecture.Application.DTOs.UserDto;

namespace WebApiArchutecture.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string refreshToken) : IRequest<LoginResponseDto>;
    
    
}
