using MediatR;
using System;

namespace DotCruz.CoreAuth.Application.Commands.Users.DeactivateUser;

public record DeactivateUserCommand(Guid UserId) : IRequest;
