using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Application.DTOs.ArtistDto;
using WebApiArchutecture.Application.DTOs.SignUpForDto;
using WebApiArchutecture.Application.DTOs.TattooDto;
using WebApiArchutecture.Application.Validators;
using WebApiArchutecture.Infrastructure.UnitOfWork;
using MediatR;
using WebApiArchutecture.Application.Features.Tattoos.Queries.GetTattoos;
using WebApiArchutecture.Application.Features.Tattoos.Queries.GetArtistsWithPrice;
using WebApiArchutecture.Application.DTOs.TattooDto.TattooReadDto;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateArtist;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateTattoo;
using WebApiArchutecture.Application.Features.Tattoos.Queries.GetSignUps;
using WebApiArchutecture.Application.Features.Tattoos.Commands.DeleteSignUp;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateSignUpFor;


namespace WebApiArchutecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TattoosController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public TattoosController(IMediator mediator )
        {
            
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TattooReadDto>>> GetTattoos([FromQuery]PaginationParams Params)
        {
            var query = new GetTattoosQuery(Params);
            var tattoos = await _mediator.Send(query);
            return Ok(tattoos);
        }

        [HttpGet("artists-by-price")]
        public async Task<ActionResult<IEnumerable<ArtistReadDto>>> GetArtistWithPrice([FromQuery] GetArtistsWithPriceQuery query)
        {
            var res = await _mediator.Send(query); 
            return Ok(res);
        }

        [HttpPost("create-artist")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ArtistReadDto>> CreateArtist(CreateArtistCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost("registration")]
        public async Task<ActionResult<SignUpForTattooReadDto>> CreateSignUpFor(CreateSignUpForCommand dto)
        {
           var res = await _mediator.Send(dto);
           return Ok(res);
        }


        [HttpPost("create-tattoo")]
        [Authorize(Policy = "AdminTelegram")]
        public async Task <ActionResult<TattooReadDto>> CreateTattoo(CreateTattooCommand command )
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpGet("ShowSignups")]
        [Authorize(Policy ="AdminTelegram")]
        public async Task<ActionResult<IEnumerable<SignUpForTattooReadDto>>>ShowSignUps([FromQuery] GetSignUpsQuery  query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
        }
        [HttpDelete("Delete-SignUp/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSignUp(int id)
        {
            await _mediator.Send(new DeleteSignUpCommand(id));
            return NoContent();
        }

        
    }
}

