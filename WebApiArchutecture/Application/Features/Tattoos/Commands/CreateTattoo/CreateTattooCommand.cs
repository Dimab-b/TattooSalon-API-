using MediatR;
using System.ComponentModel.DataAnnotations;
using WebApiArchutecture.Application.DTOs.TattooDto.TattooReadDto;
using WebApiArchutecture.Domain;

namespace WebApiArchutecture.Application.Features.Tattoos.Commands.CreateTattoo
{
    public record CreateTattooCommand(
        [Required]
     string Size ,
        [Required]
     Color Color ,
        [Required]
     string Style ,
        [Required]
     decimal Price 
        
        ) : IRequest<TattooReadDto>;
    
    
}
