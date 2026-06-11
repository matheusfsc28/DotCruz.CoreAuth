using Bogus;
using DotCruz.CoreAuth.Application.Commands.Auth.RefreshTokens;

namespace CommonTestUtilities.Requests.Auth;

public class RefreshTokenCommandBuilder
{
    public static RefreshTokenCommand Build(string? refreshToken = "[DEFAULT]")
    {
        if (refreshToken == "[DEFAULT]")
        {
            return new Faker<RefreshTokenCommand>()
                .CustomInstantiator(f => new RefreshTokenCommand(
                    f.Random.AlphaNumeric(32)
                ));
        }

        return new RefreshTokenCommand(refreshToken!);
    }
}
