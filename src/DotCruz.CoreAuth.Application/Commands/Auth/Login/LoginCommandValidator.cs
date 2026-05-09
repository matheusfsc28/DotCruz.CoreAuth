using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using FluentValidation;

namespace DotCruz.CoreAuth.Application.Commands.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY)
                .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY);
        }
    }
}
