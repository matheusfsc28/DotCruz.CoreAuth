using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using FluentValidation;

namespace DotCruz.CoreAuth.Application.Commands.Users.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.PASSWORD_EMPTY);

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
            .MinimumLength(8)
            .WithMessage(string.Format(ResourceMessagesException.PASSWORD_MIN_LENGTH, 8));
    }
}
