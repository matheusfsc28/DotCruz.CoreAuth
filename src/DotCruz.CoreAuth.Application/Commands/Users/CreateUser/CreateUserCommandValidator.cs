using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using FluentValidation;

namespace DotCruz.CoreAuth.Application.Commands.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY)
                .MaximumLength(200).WithMessage(string.Format(ResourceMessagesException.NAME_MAX_LENGTH, 200));

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY)
                .MaximumLength(200).WithMessage(string.Format(ResourceMessagesException.EMAIL_MAX_LENGTH, 200))
                .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
                .MinimumLength(8).WithMessage(string.Format(ResourceMessagesException.PASSWORD_MIN_LENGTH, 8));
            
            RuleFor(x => x.Type)
                .IsInEnum();
        }
    }
}