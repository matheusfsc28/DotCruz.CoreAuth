using CommonTestUtilities.Requests.Users;
using DotCruz.CoreAuth.Application.Commands.Auth.PasswordResetTokens.ResetPassword;
using DotCruz.CoreAuth.Exceptions;

namespace Validators.Test.Auth.PasswordResetTokens.ResetPassword
{
    public class ResetPasswordCommandValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new ResetPasswordCommandValidator();
            var command = ResetPasswordCommandBuilder.Build();

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Error_Token_Empty()
        {
            var validator = new ResetPasswordCommandValidator();
            var command = ResetPasswordCommandBuilder.Build() with { Token = string.Empty };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.TOKEN_EMPTY));
        }

        [Fact]
        public void Error_Password_Empty()
        {
            var validator = new ResetPasswordCommandValidator();
            var command = ResetPasswordCommandBuilder.Build() with { NewPassword = string.Empty };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.PASSWORD_EMPTY));
        }

        [Fact]
        public void Error_Password_Min_Length()
        {
            var validator = new ResetPasswordCommandValidator();
            var command = ResetPasswordCommandBuilder.Build(passwordLength: 7);

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(string.Format(ResourceMessagesException.PASSWORD_MIN_LENGTH, 8)));
        }
    }
}
