using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApiArchutecture.Application.Features.Tattoos.Commands.DeleteSignUp
{
    public record DeleteSignUpCommand(int Id) : IRequest<Unit>;
    
    
}
