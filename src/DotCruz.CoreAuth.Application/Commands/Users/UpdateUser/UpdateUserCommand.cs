using MediatR;

namespace DotCruz.CoreAuth.Application.Commands.Users.UpdateUser
{
    public record UpdateUserCommand(Guid Id, UpdateUserRequest Request) : IRequest;
}
