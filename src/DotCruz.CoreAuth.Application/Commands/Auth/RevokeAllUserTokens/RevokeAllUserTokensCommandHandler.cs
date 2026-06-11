using DotCruz.CoreAuth.Domain.Interfaces.Data;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;
using MediatR;

namespace DotCruz.CoreAuth.Application.Commands.Auth.RevokeAllUserTokens;

public class RevokeAllUserTokensCommandHandler : IRequestHandler<RevokeAllUserTokensCommand>
{
    private readonly IRefreshTokenReadRepository _refreshTokenReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RevokeAllUserTokensCommandHandler(
        IRefreshTokenReadRepository refreshTokenReadRepository,
        IUnitOfWork unitOfWork
    )
    {
        _refreshTokenReadRepository = refreshTokenReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RevokeAllUserTokensCommand request, CancellationToken cancellationToken)
    {
        var refreshTokens = await _refreshTokenReadRepository.GetActiveTokensByUserIdAsync(request.UserId, cancellationToken);

        if (refreshTokens == null || !refreshTokens.Any())
            return;

        refreshTokens.ToList().ForEach(rt => rt.Revoke());

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
