using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApiArchitecture.Application.Exceptions;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchutecture.Application.Features.Tattoos.Commands.DeleteSignUp
{
    public class DeleteSignUpCommandHandler : IRequestHandler<DeleteSignUpCommand , Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteSignUpCommandHandler> _logger;

        public DeleteSignUpCommandHandler(IUnitOfWork uow, ILogger<DeleteSignUpCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteSignUpCommand command , CancellationToken token)
        {
            List<SignUpForTattoo> signs = new List<SignUpForTattoo>();
            var entity = await _uow.SignUps.GetById(command.Id , token);
            signs.Add(entity);

            if (entity == null)
            {
                _logger.LogWarning("Спроба видалення за неправильним Id: {Id}" , command.Id);
                throw new NotFoundException("SignUp", command.Id);
            }
            else
            {
                _uow.SignUps.Delete(signs);
                await _uow.SaveAsync(token);
                _logger.LogInformation("Запис {id} на {Time} успішно видалено" , command.Id , entity.TimeOfSign);
                return Unit.Value;
            }
           

        }
    }
}
