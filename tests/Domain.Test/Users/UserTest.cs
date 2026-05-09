using CommonTestUtilities.Entities.Users;
using DotCruz.CoreAuth.Domain.Exceptions.BaseExceptions;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;

namespace Domain.Test.Users
{
    public class UserTest
    {
        [Fact]
        public void Success_On_Create()
        {
            var user = UserBuilder.Build();

            Assert.NotNull(user);
            Assert.NotEmpty(user.Name);
            Assert.NotEmpty(user.Email);
            Assert.NotEmpty(user.PasswordHash);
        }

        [Fact]
        public void Success_On_Update()
        {
            var user = UserBuilder.Build();

            var newData = UserBuilder.Build();

            user.Update(newData.Name, newData.Email, newData.PasswordHash, newData.Type);

            Assert.Equal(newData.Name, user.Name);
            Assert.Equal(newData.Email, user.Email);
            Assert.Equal(newData.PasswordHash, user.PasswordHash);
            Assert.Equal(newData.Type, user.Type);
        }

        [Fact]
        public void Success_On_Update_Only_Name()
        {
            var user = UserBuilder.Build();

            var newData = UserBuilder.Build();

            var originalName = user.Name;
            var originalEmail = user.Email;
            var originalPasswordHash = user.PasswordHash;
            var originalType = user.Type;

            user.Update(newData.Name, null, null, null);

            Assert.Equal(newData.Name, user.Name);
            Assert.NotEqual(originalName, user.Name);
            Assert.Equal(originalEmail, user.Email);
            Assert.Equal(originalPasswordHash, user.PasswordHash);
            Assert.Equal(originalType, user.Type);
        }

        [Fact]
        public void Error_Name_Empty()
        {
            static void Action() => UserBuilder.Build(name: string.Empty);

            var exception = Assert.Throws<ErrorOnValidationException>(Action);

            Assert.Contains(ResourceMessagesException.NAME_EMPTY, exception.GetErrorsMessages());

            Assert.Single(exception.GetErrorsMessages());
        }

        [Fact]
        public void Error_Name_Max_Length()
        {
            static void Action() => UserBuilder.Build(nameLength: 201);

            var exception = Assert.Throws<ErrorOnValidationException>(Action);

            var expectedMessage = string.Format(ResourceMessagesException.NAME_MAX_LENGTH, 200);

            Assert.Contains(expectedMessage, exception.GetErrorsMessages());

            Assert.Single(exception.GetErrorsMessages());
        }

        [Fact]
        public void Error_Email_Empty()
        {
            static void Action() => UserBuilder.Build(email: string.Empty);

            var exception = Assert.Throws<ErrorOnValidationException>(Action);

            Assert.Contains(ResourceMessagesException.EMAIL_EMPTY, exception.GetErrorsMessages());

            Assert.Single(exception.GetErrorsMessages());
        }

        [Fact]
        public void Error_Email_Max_Length()
        {
            static void Action() => UserBuilder.Build(emailLength: 201);

            var exception = Assert.Throws<ErrorOnValidationException>(Action);

            var expectedMessage = string.Format(ResourceMessagesException.EMAIL_MAX_LENGTH, 200);

            Assert.Contains(expectedMessage, exception.GetErrorsMessages());

            Assert.Single(exception.GetErrorsMessages());
        }

        [Fact]
        public void Error_Password_Empty()
        {
            static void Action() => UserBuilder.Build(passwordHashed: string.Empty);

            var exception = Assert.Throws<ErrorOnValidationException>(Action);

            Assert.Contains(ResourceMessagesException.PASSWORD_EMPTY, exception.GetErrorsMessages());

            Assert.Single(exception.GetErrorsMessages());
        }
    }
}
