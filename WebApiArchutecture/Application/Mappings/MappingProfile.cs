using AutoMapper;
using WebApiArchutecture.Application.DTOs.ArtistDto;
using WebApiArchutecture.Application.DTOs.SignUpForDto;
using WebApiArchutecture.Application.DTOs.TattooDto;
using WebApiArchutecture.Application.DTOs.TattooDto.TattooReadDto;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateArtist;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateSignUpFor;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateTattoo;
using WebApiArchutecture.Domain;

namespace WebApiArchutecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
        CreateMap<Tattoo , TattooReadDto>();
        CreateMap<Artist, ArtistReadDto>();
        CreateMap<CreateArtistCommand, Artist>();
        CreateMap<SignUpForTattoo, SignUpForTattooReadDto>();
        CreateMap<User, UserReadDto>();
        CreateMap<CreateSignUpForCommand, SignUpForTattoo>().ForMember(dest => dest.Artist, opt => opt.Ignore()); ;
        CreateMap<CreateTattooCommand, Tattoo>();
        }
    }
}
