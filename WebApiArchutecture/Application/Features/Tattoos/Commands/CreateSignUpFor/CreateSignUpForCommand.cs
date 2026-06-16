using MediatR;
using System.ComponentModel.DataAnnotations;
using WebApiArchutecture.Application.DTOs.SignUpForDto;

namespace WebApiArchutecture.Application.Features.Tattoos.Commands.CreateSignUpFor
{
    public record CreateSignUpForCommand(
        [Required]
     string NumberOfClient,
        [Required]
     DateTime TimeOfSign,
        [Required]

     int Sessions,
        [Required]
     int ArtistId) : IRequest<SignUpForTattooReadDto>;
    
    
}
