using DotCruz.CoreAuth.Exceptions;
using FluentValidation;

namespace DotCruz.CoreAuth.Application.Commands.Auth.PasswordResetTokens.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.TOKEN_EMPTY);

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
                .MinimumLength(8)
                .WithMessage(string.Format(ResourceMessagesException.PASSWORD_MIN_LENGTH, 8));
        }
    }
}
