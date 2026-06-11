using Bogus;
using DotCruz.CoreAuth.Application.Commands.Auth.RevokeAllUserTokens;

namespace CommonTestUtilities.Requests.Auth;

public class RevokeAllUserTokensCommandBuilder
{
    public static RevokeAllUserTokensCommand Build(Guid? userId = null)
    {
        return new Faker<RevokeAllUserTokensCommand>()
            .CustomInstantiator(f => new RevokeAllUserTokensCommand(
                userId ?? f.Random.Guid()
            ));
    }
}
