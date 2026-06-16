using MediatR;
using System.ComponentModel.DataAnnotations;
using WebApiArchutecture.Application.DTOs.UserDto;

namespace WebApiArchutecture.Application.Features.Auth.Commands.Register
{

    public record RegisterUserCommand(
        [Required]
        string Username,
        [Required]
        string Password,
        [Required]
        string Email,
        [Required]
        string ConfirmPassword,
        [Required]
        string Telegram_Tag,
        [Required]
        string Role = "User"
    ) : IRequest<UserReadDto>;
}
