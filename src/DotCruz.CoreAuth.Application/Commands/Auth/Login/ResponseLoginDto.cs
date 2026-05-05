namespace DotCruz.CoreAuth.Application.Commands.Auth.Login
{
    public record ResponseLoginDto(Guid Id, string Name, string Email, ResponseTokensDto Tokens);
}
