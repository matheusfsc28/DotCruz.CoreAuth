using CommonTestUtilities.Requests.Users;
using CommonTestUtilities.Validators.Users;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;

namespace Validators.Test.Users.CreateUser
{
    public class CreateUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = CreateUserCommandValidatorBuilder.Build();
            var request = CreateUserCommandBuilder.Build();

            var result = validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = CreateUserCommandValidatorBuilder.Build();
            var request = CreateUserCommandBuilder.Build();
            request = request with { Name = string.Empty };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.NAME_EMPTY));
        }

        [Fact]
        public void Error_Name_Max_Length()
        {
            var validator = CreateUserCommandValidatorBuilder.Build();
            var request = CreateUserCommandBuilder.Build();
            request = request with { Name = new string('a', 201) };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(string.Format(ResourceMessagesException.NAME_MAX_LENGTH, 200)));
        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validator = CreateUserCommandValidatorBuilder.Build();
            var request = CreateUserCommandBuilder.Build();
            request = request with { Email = string.Empty };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Email_Max_Length()
        {
            var validator = CreateUserCommandValidatorBuilder.Build();
            var request = CreateUserCommandBuilder.Build();
            request = request with { Email = new string('a', 195) + "@test.com" };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(string.Format(ResourceMessagesException.EMAIL_MAX_LENGTH, 200)));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = CreateUserCommandValidatorBuilder.Build();
            var request = CreateUserCommandBuilder.Build();
            request = request with { Email = "invalid_email" };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.EMAIL_INVALID));
        }

        [Fact]
        public void Error_Password_Empty()
        {
            var validator = CreateUserCommandValidatorBuilder.Build();
            var request = CreateUserCommandBuilder.Build();
            request = request with { Password = string.Empty };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.PASSWORD_EMPTY));
        }

        [Fact]
        public void Error_Password_Min_Length()
        {
            var validator = CreateUserCommandValidatorBuilder.Build();
            var request = CreateUserCommandBuilder.Build(passwordLength: 7);

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(string.Format(ResourceMessagesException.PASSWORD_MIN_LENGTH, 8)));
        }
    }
}
