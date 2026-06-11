using MediatR;
using System;

namespace DotCruz.CoreAuth.Application.Commands.Users.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword) : IRequest;
