using FluentValidation;
using WebApiArchutecture.Application.DTOs.SignUpForDto;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateSignUpFor;
namespace WebApiArchutecture.Application.Validators
{
    public class SignUpForTattoCreateDtoValidator : AbstractValidator<CreateSignUpForCommand>
    {
        public SignUpForTattoCreateDtoValidator()
        {
            RuleFor(p => p.NumberOfClient).Matches(@"^\+380\d{9}$").NotEmpty().WithMessage("Номер потрібен , щоб ми вам передзвонили");
            RuleFor(p => p.Sessions).GreaterThanOrEqualTo(1).WithMessage("Не може бути менше одного сеансу");
            RuleFor(p => p.TimeOfSign).NotEmpty();
        }
    }
}
