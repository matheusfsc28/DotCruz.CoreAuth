using CoreAuth.Exceptions;
using FluentValidation;

namespace CoreAuth.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ResourceMessagesException.ID_EMPTY);

            RuleFor(x => x.Request.Name)
                .NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY)
                .MaximumLength(200).WithMessage(string.Format(ResourceMessagesException.NAME_MAX_LENGTH, 200));

            RuleFor(x => x.Request.Email)
                .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY)
                .MaximumLength(200).WithMessage(string.Format(ResourceMessagesException.EMAIL_MAX_LENGTH, 200))
                .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        }
    }
}
