using MediatR;

namespace CoreAuth.Application.Commands.Auth.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<ResponseLoginDto>;
}
