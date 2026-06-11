using CommonTestUtilities.Requests.Auth;
using DotCruz.CoreAuth.Application.Commands.Auth.RevokeAllUserTokens;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using FluentAssertions;

namespace Validators.Test.Auth.RevokeAllUserTokens;

public class RevokeAllUserTokensCommandValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RevokeAllUserTokensCommandValidator();
        var command = RevokeAllUserTokensCommandBuilder.Build();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_UserId_Empty()
    {
        var validator = new RevokeAllUserTokensCommandValidator();
        var command = RevokeAllUserTokensCommandBuilder.Build(Guid.Empty);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Contain(ResourceMessagesException.ID_EMPTY);
    }
}
