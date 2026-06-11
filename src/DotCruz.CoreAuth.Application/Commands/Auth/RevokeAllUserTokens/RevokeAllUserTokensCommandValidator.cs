using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using FluentValidation;

namespace DotCruz.CoreAuth.Application.Commands.Auth.RevokeAllUserTokens;

public class RevokeAllUserTokensCommandValidator : AbstractValidator<RevokeAllUserTokensCommand>
{
    public RevokeAllUserTokensCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage(ResourceMessagesException.ID_EMPTY);
    }
}
