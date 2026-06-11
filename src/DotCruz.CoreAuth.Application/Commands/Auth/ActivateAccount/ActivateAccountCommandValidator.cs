using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using FluentValidation;

namespace DotCruz.CoreAuth.Application.Commands.Auth.ActivateAccount;

public class ActivateAccountCommandValidator : AbstractValidator<ActivateAccountCommand>
{
    public ActivateAccountCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage(ResourceMessagesException.TOKEN_EMPTY);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
            .MinimumLength(8).WithMessage(string.Format(ResourceMessagesException.PASSWORD_MIN_LENGTH, 8));
    }
}
