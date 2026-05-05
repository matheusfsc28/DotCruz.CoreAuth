using CommonTestUtilities.Requests.Users;
using DotCruz.CoreAuth.Application.Commands.Auth.Login;
using DotCruz.CoreAuth.Exceptions;

namespace Validators.Test.Auth.Login
{
    public class LoginCommandValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new LoginCommandValidator();
            var command = LoginCommandBuilder.Build();

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new LoginCommandValidator();
            var command = LoginCommandBuilder.Build() with { Email = string.Empty };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new LoginCommandValidator();
            var command = LoginCommandBuilder.Build() with { Email = "invalid_email" };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.EMAIL_INVALID));
        }

        [Fact]
        public void Error_Password_Empty()
        {
            var validator = new LoginCommandValidator();
            var command = LoginCommandBuilder.Build() with { Password = string.Empty };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.PASSWORD_EMPTY));
        }
    }
}
