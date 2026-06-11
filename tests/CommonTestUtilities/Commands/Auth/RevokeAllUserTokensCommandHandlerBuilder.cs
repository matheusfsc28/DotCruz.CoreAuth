using CommonTestUtilities.Data;
using CommonTestUtilities.Repositories.Tokens;
using DotCruz.CoreAuth.Application.Commands.Auth.RevokeAllUserTokens;
using DotCruz.CoreAuth.Domain.Interfaces.Data;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;

namespace CommonTestUtilities.Commands.Auth;

public class RevokeAllUserTokensCommandHandlerBuilder
{
    private IRefreshTokenReadRepository _refreshTokenReadRepository = new RefreshTokenReadRepositoryBuilder().Build();
    private IUnitOfWork _unitOfWork = UnitOfWorkBuilder.Build();

    public RevokeAllUserTokensCommandHandlerBuilder SetRefreshTokenReadRepository(IRefreshTokenReadRepository readRepository)
    {
        _refreshTokenReadRepository = readRepository;
        return this;
    }

    public RevokeAllUserTokensCommandHandlerBuilder SetUnitOfWork(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        return this;
    }

    public RevokeAllUserTokensCommandHandler Build()
    {
        return new RevokeAllUserTokensCommandHandler(_refreshTokenReadRepository, _unitOfWork);
    }
}
