using CoreAuth.Application.Commands.Users.CreateUser;

namespace CommonTestUtilities.Validators.Users
{
    public class CreateUserCommandValidatorBuilder
    {
        public static CreateUserValidator Build() => new CreateUserValidator();
    }
}
