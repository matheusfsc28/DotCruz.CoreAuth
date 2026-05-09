using CommonTestUtilities.Requests.Users;
using DotCruz.CoreAuth.Application.Commands.Auth.PasswordResetTokens.RequestPasswordReset;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;

namespace Validators.Test.Auth.PasswordResetTokens.RequestPasswordReset
{
    public class RequestPasswordResetCommandValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RequestPasswordResetCommandValidator();
            var command = RequestPasswordResetCommandBuilder.Build();

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new RequestPasswordResetCommandValidator();
            var command = new RequestPasswordResetCommand(string.Empty);

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RequestPasswordResetCommandValidator();
            var command = new RequestPasswordResetCommand("invalid-email");

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.EMAIL_INVALID));
        }
    }
}
