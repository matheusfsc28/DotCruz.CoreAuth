using CommonTestUtilities.Requests.Auth;
using DotCruz.CoreAuth.Application.Commands.Auth.RefreshTokens;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using FluentAssertions;

namespace Validators.Test.Auth.RefreshToken;

public class RefreshTokenCommandValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RefreshTokenCommandValidator();
        var command = RefreshTokenCommandBuilder.Build();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Error_RefreshToken_Empty(string? refreshToken)
    {
        var validator = new RefreshTokenCommandValidator();
        var command = RefreshTokenCommandBuilder.Build(refreshToken);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Contain(ResourceMessagesException.TOKEN_EMPTY);
    }
}
