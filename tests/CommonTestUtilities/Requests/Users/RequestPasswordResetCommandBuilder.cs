using Bogus;
using DotCruz.CoreAuth.Application.Commands.Auth.PasswordResetTokens.RequestPasswordReset;

namespace CommonTestUtilities.Requests.Users
{
    public class RequestPasswordResetCommandBuilder
    {
        public static RequestPasswordResetCommand Build()
        {
            return new Faker<RequestPasswordResetCommand>()
                .CustomInstantiator(f => new RequestPasswordResetCommand(
                    f.Internet.Email()
                ));
        }
    }
}
