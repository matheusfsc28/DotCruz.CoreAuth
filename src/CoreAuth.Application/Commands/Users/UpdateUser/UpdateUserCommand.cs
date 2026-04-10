using MediatR;

namespace CoreAuth.Application.Commands.Users.UpdateUser
{
    public record UpdateUserCommand(Guid Id, UpdateUserRequest Request) : IRequest;
}
