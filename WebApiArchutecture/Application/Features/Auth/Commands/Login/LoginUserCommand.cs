using MediatR;
using System.ComponentModel.DataAnnotations;
using WebApiArchutecture.Application.DTOs.UserDto;

namespace WebApiArchutecture.Application.Features.Auth.Commands.Loggin
{
    public record LoginUserCommand(
        [Required]
         string Password ,
    [Required]
     string Username 
        
        ) : IRequest<LoginResponseDto>;
    
    
}
