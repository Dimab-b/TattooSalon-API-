using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiArchutecture.Infrastructure.UnitOfWork;

namespace WebApiArchitecture.Application.Features.Tattoos.Commands.DeleteOldSignUps
{
    public class DeleteOldSignUpsCommandHandler : IRequestHandler<DeleteOldSignUpsCommand>
    {
        private readonly IUnitOfWork _uow;
       

        public DeleteOldSignUpsCommandHandler(IUnitOfWork uow) { _uow = uow; }

        public async Task Handle(DeleteOldSignUpsCommand command , CancellationToken token)
        {
            var oldSignUps = await _uow.SignUps.GetQueryable().Where(x => x.TimeOfSign.AddDays(14) < DateTime.UtcNow).ToListAsync(token);
            if (oldSignUps == null) { return; }

            _uow.SignUps.Delete(oldSignUps);
            await _uow.SaveAsync(token);
        }
    }
}
