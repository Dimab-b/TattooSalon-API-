using MediatR;

namespace WebApiArchutecture.Application.Features.Auth.Commands.Logout
{
    public record LogoutCommand(string refreshtoken) : IRequest;
    
    
}
