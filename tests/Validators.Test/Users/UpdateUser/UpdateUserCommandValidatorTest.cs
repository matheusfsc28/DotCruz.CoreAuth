using CommonTestUtilities.Requests.Users;
using CommonTestUtilities.Validators.Users;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;

namespace Validators.Test.Users.UpdateUser
{
    public class UpdateUserCommandValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = UpdateUserCommandValidatorBuilder.Build();
            var request = UpdateUserCommandBuilder.Build();

            var result = validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Error_Id_Empty()
        {
            var validator = UpdateUserCommandValidatorBuilder.Build();
            var request = UpdateUserCommandBuilder.Build();
            request = request with { Id = Guid.Empty };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.ID_EMPTY));
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = UpdateUserCommandValidatorBuilder.Build();
            var request = UpdateUserCommandBuilder.Build();
            request = request with { Request = request.Request with { Name = string.Empty } };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.NAME_EMPTY));
        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validator = UpdateUserCommandValidatorBuilder.Build();
            var request = UpdateUserCommandBuilder.Build();
            request = request with { Request = request.Request with { Email = string.Empty } };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = UpdateUserCommandValidatorBuilder.Build();
            var request = UpdateUserCommandBuilder.Build();
            request = request with { Request = request.Request with { Email = "invalid_email" } };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(ResourceMessagesException.EMAIL_INVALID));
        }
    }
}
