using FluentValidation;
using FluentValidation.AspNetCore;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Application.Features.Auth.Commands;
using WebApiArchutecture.Application.Features.Auth.Commands.Register;

namespace WebApiArchutecture.Application.Validators
{
    public class UserRegisterDtoValidator : AbstractValidator<RegisterUserCommand>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).Matches("[0-9]").WithMessage("Пароль повинен містити принаймі одну цифру").Matches("[^a-zA-Z0-9]").WithMessage("Потрібен спеціальний символ (!@#$%^&*)");

            RuleFor(x => x.Telegram_Tag).NotEmpty().Must(x => x.StartsWith("@")).WithMessage("Пароль повинен починатись з символу '@' ");

            RuleFor(x => x.ConfirmPassword).NotEmpty();

            RuleFor(x => x.Username).NotEmpty();

            RuleFor(x => x.Email).NotEmpty().WithMessage("Емейл є обов'язковим").EmailAddress().WithMessage("Введіть коректний емейл").MaximumLength(100).WithMessage("Емейл задовгий");
        }
    }
}
