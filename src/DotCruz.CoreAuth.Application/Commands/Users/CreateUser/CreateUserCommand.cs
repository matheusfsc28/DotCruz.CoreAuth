using DotCruz.CoreAuth.Domain.Enums.Users;
using MediatR;

namespace DotCruz.CoreAuth.Application.Commands.Users.CreateUser
{
    public record CreateUserCommand(
        string Name,
        string Email,
        string Password,
        UserType Type) : IRequest<Guid>;
}