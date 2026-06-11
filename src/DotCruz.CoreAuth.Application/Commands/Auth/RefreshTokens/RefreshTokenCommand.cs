using DotCruz.CoreAuth.Application.Commands.Auth.Login;
using MediatR;

namespace DotCruz.CoreAuth.Application.Commands.Auth.RefreshTokens;

public record RefreshTokenCommand(string RefreshToken) : IRequest<ResponseTokensDto>;
