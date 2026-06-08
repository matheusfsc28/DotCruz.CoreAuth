using CommonTestUtilities.Entities.Users;
using DotCruz.CoreAuth.Domain.Entities.Users;
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

            user.Update(newData.Name, newData.Email, newData.PasswordHash, newData.Type, newData.TenantId);

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

        [Fact]
        public void Error_TenantId_Empty_For_TenantUser()
        {
            static void Action() => new User("Nome", "email@teste.com", "senha-hash", DotCruz.CoreAuth.Domain.Enums.Users.UserType.TenantUser, null);

            var exception = Assert.Throws<ErrorOnValidationException>(Action);

            Assert.Contains(ResourceMessagesException.TENANT_ID_REQUIRED, exception.GetErrorsMessages());

            Assert.Single(exception.GetErrorsMessages());
        }

        [Fact]
        public void Success_On_Create_With_TenantId()
        {
            var tenantId = Guid.NewGuid();
            var user = new User("Nome", "email@teste.com", "senha-hash", DotCruz.CoreAuth.Domain.Enums.Users.UserType.TenantUser, tenantId);

            Assert.NotNull(user);
            Assert.Equal(tenantId, user.TenantId);
        }

        [Fact]
        public void Success_On_Update_To_Global_Clears_TenantId()
        {
            var tenantId = Guid.NewGuid();
            var user = new User("Nome", "email@teste.com", "senha-hash", DotCruz.CoreAuth.Domain.Enums.Users.UserType.TenantUser, tenantId);

            user.Update(null, null, null, DotCruz.CoreAuth.Domain.Enums.Users.UserType.SuperAdmin);

            Assert.Null(user.TenantId);
        }
    }
}
