using MediatR;
using System.ComponentModel.DataAnnotations;
using WebApiArchutecture.Application.DTOs.ArtistDto;

namespace WebApiArchutecture.Application.Features.Tattoos.Commands.CreateArtist
{
    public record CreateArtistCommand
    ([Required]
     string Name,
        [Required]
     string Surname,
        [Required]
     int Age,
        [Required]
     decimal PriceForSession,
        [Required]
     decimal Experience ) : IRequest<ArtistReadDto>;


}
