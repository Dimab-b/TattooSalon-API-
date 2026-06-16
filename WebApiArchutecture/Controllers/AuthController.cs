using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Application.Features.Auth.Commands.Loggin;
using WebApiArchutecture.Application.Features.Auth.Commands.Logout;
using WebApiArchutecture.Application.Features.Auth.Commands.RefreshToken;
using WebApiArchutecture.Application.Features.Auth.Commands.Register;
using WebApiArchutecture.Application.Validators;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.UnitOfWork;


namespace WebApiArchutecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
       
      
        private readonly IMediator _mediator;

        public AuthController(  IMediator mediator)
        {
        
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand dto)
        {
            var result = await _mediator.Send(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserCommand dto , CancellationToken canceltoken)
        {
            var token = await _mediator.Send(dto, canceltoken);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = token.RefreshToken.Expires,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("refreshToken", token.RefreshToken.Token, cookieOptions);
            return Ok(token.AccessToken); 
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(CancellationToken cancellationToken)
        {
          
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Refresh token is missing");
            }



            var command = new RefreshTokenCommand(refreshToken);
            var result = await _mediator.Send(command, cancellationToken);

             
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = result.RefreshToken.Expires,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };
                Response.Cookies.Append("refreshToken", result.RefreshToken.Token, cookieOptions);

               
                return Ok(result.AccessToken);
            
          
        }


        [HttpPost("logout")]

        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            var refresh = Request.Cookies["refreshToken"];
            if (refresh != null)
            {
                var command = new LogoutCommand(refresh);
                await _mediator.Send(command, cancellationToken);



                return Ok("Logged out successfully");
            }
            Response.Cookies.Delete("refreshToken");
            return Ok("Logged out succesfully");
        }
    }
}
