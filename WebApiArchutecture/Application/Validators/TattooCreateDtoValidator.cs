using FluentValidation;
using System.Data;
using WebApiArchutecture.Application.DTOs.TattooDto;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateTattoo;

namespace WebApiArchutecture.Application.Validators
{
    public class TattooCreateDtoValidator : AbstractValidator<CreateTattooCommand>
    {
        public TattooCreateDtoValidator()
        {
            RuleFor(p => p.Price).NotEmpty().GreaterThanOrEqualTo(50).WithMessage("Мінімальна ціна - 50");
            RuleFor(p => p.Size).NotEmpty();
            RuleFor(P => P.Color).NotEmpty();
            RuleFor(p => p.Style).NotEmpty();
            
        }
    }
}
