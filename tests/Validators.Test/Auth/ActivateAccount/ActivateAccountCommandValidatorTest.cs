using CommonTestUtilities.Requests.Auth.ActivateAccount;
using DotCruz.CoreAuth.Application.Commands.Auth.ActivateAccount;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using FluentAssertions;
using Xunit;

namespace Validators.Test.Auth.ActivateAccount;

public class ActivateAccountCommandValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new ActivateAccountCommandValidator();
        var request = ActivateAccountCommandBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Token_Empty()
    {
        var validator = new ActivateAccountCommandValidator();
        var request = ActivateAccountCommandBuilder.Build();
        request = request with { Token = string.Empty };

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains(ResourceMessagesException.TOKEN_EMPTY));
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var validator = new ActivateAccountCommandValidator();
        var request = ActivateAccountCommandBuilder.Build();
        request = request with { Password = string.Empty };

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains(ResourceMessagesException.PASSWORD_EMPTY));
    }

    [Fact]
    public void Error_Password_Min_Length()
    {
        var validator = new ActivateAccountCommandValidator();
        var request = ActivateAccountCommandBuilder.Build(passwordLength: 7);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains(string.Format(ResourceMessagesException.PASSWORD_MIN_LENGTH, 8)));
    }
}
