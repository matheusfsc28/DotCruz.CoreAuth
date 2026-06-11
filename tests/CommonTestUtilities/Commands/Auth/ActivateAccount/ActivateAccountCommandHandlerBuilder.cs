using CommonTestUtilities.Data;
using CommonTestUtilities.Repositories.Tokens;
using CommonTestUtilities.Security;
using DotCruz.CoreAuth.Application.Commands.Auth.ActivateAccount;
using DotCruz.CoreAuth.Domain.Interfaces.Data;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Security;
using DotCruz.CoreAuth.Domain.Interfaces.Security.Tokens;
using Moq;

namespace CommonTestUtilities.Commands.Auth.ActivateAccount;

public class ActivateAccountCommandHandlerBuilder
{
    private IActivationTokenReadRepository _activationTokenReadRepository = new ActivationTokenReadRepositoryBuilder().Build();
    private IPasswordHasher _passwordHasher = new PasswordHasherBuilder().Build();
    private ITokenProvider _tokenProvider = new Mock<ITokenProvider>().Object;
    private IUnitOfWork _unitOfWork = UnitOfWorkBuilder.Build();

    public ActivateAccountCommandHandlerBuilder SetActivationTokenReadRepository(IActivationTokenReadRepository readRepository)
    {
        _activationTokenReadRepository = readRepository;
        return this;
    }

    public ActivateAccountCommandHandlerBuilder SetPasswordHasher(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
        return this;
    }

    public ActivateAccountCommandHandlerBuilder SetTokenProvider(ITokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
        return this;
    }

    public ActivateAccountCommandHandlerBuilder SetUnitOfWork(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        return this;
    }

    public ActivateAccountCommandHandler Build()
    {
        return new ActivateAccountCommandHandler(
            _activationTokenReadRepository,
            _passwordHasher,
            _tokenProvider,
            _unitOfWork
        );
    }
}
