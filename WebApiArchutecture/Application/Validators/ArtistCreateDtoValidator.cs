using FluentValidation;
using FluentValidation.AspNetCore;
using WebApiArchutecture.Application.DTOs.ArtistDto;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateArtist;


namespace WebApiArchutecture.Application.Validators
{
    public class ArtistCreateDtoValidator : AbstractValidator<CreateArtistCommand>
    {
        public ArtistCreateDtoValidator() 
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ім'я повинне бути заповнене").MaximumLength(30);
            RuleFor(p => p.Age).NotEmpty().WithMessage("Вік обов'язковий").GreaterThanOrEqualTo(18).WithMessage("Ви повинні бути повнолітнім");
            RuleFor(p => p.Surname).NotEmpty().WithMessage("Прізвище повинне бути заповнене").MaximumLength(50);
            RuleFor(p => p.PriceForSession).NotEmpty().WithMessage("Ціну вказувати обовязково").GreaterThanOrEqualTo(50).WithMessage("Мінімальна ціна - 50");
            RuleFor(p => p.Experience).NotEmpty().GreaterThanOrEqualTo(0);

        }
    }
}
