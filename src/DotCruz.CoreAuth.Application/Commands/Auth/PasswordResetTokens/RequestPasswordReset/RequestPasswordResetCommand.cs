using MediatR;

namespace DotCruz.CoreAuth.Application.Commands.Auth.PasswordResetTokens.RequestPasswordReset
{
    public record RequestPasswordResetCommand(string Email) : IRequest;
}
