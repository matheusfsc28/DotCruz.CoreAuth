using FluentAssertions;
using System.Net;
using Xunit;

namespace WebApi.Test.Auth.RevokeTokens;

public class RevokeTokensTest : DotCruzCoreAuthClassFixture
{
    private readonly string _userIdString;
    private readonly string _refreshToken;

    public RevokeTokensTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdString = factory.GetUserId().ToString();
        _refreshToken = factory.GetRefreshToken();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoPost(method: $"api/auth/revoke-tokens/{_userIdString}", request: new { });

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var refreshCommand = CommonTestUtilities.Requests.Auth.RefreshTokenCommandBuilder.Build(_refreshToken);
        var refreshResponse = await DoPost(method: "api/auth/refresh-token", request: refreshCommand);
        refreshResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Success_No_Tokens()
    {
        var randomUserId = Guid.NewGuid().ToString();

        var response = await DoPost(method: $"api/auth/revoke-tokens/{randomUserId}", request: new { });

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
