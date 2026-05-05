using Bogus;
using DotCruz.CoreAuth.Application.Commands.Auth.PasswordResetTokens.ResetPassword;

namespace CommonTestUtilities.Requests.Users
{
    public class ResetPasswordCommandBuilder
    {
        public static ResetPasswordCommand Build(int passwordLength = 10)
        {
            return new Faker<ResetPasswordCommand>()
                .CustomInstantiator(f => new ResetPasswordCommand(
                    f.Random.Guid().ToString(),
                    f.Internet.Password(passwordLength)
                ));
        }
    }
}
