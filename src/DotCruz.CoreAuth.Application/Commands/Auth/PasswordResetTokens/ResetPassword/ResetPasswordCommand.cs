using MediatR;

namespace DotCruz.CoreAuth.Application.Commands.Auth.PasswordResetTokens.ResetPassword
{
    public record ResetPasswordCommand(string Token, string NewPassword) : IRequest;
}
