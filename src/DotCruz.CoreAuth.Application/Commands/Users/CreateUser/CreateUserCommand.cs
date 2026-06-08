using DotCruz.CoreAuth.Domain.Enums.Users;
using MediatR;
using System;

namespace DotCruz.CoreAuth.Application.Commands.Users.CreateUser
{
    public record CreateUserCommand(
        string Name,
        string Email,
        string Password,
        UserType Type,
        Guid? TenantId = null) : IRequest<Guid>;
}