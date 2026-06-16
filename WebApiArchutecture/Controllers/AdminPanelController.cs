using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiArchitecture.Application.Features.AdminPanel.Commands.GenerateReport;
using WebApiArchutecture.Application.DTOs;
using WebApiArchutecture.Application.Features.AdminPanel.Queries.GetAllUsers;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.Repositories.UsersRepository;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPanelController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public AdminPanelController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers([FromQuery] PaginationParams Params)
        {
            var query = new GetArtistWithPriceQuery(Params);
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("generate-report")]
        public IActionResult GenerateReport()
        {
            BackgroundJob.Enqueue<IMediator>(m => m.Send(new GenerateAdminReportCommand()));
            return Accepted(new { Message = "Звіт у процесі генерації. Прийде на пошту протягом хвилини." });
        }
    }
}
