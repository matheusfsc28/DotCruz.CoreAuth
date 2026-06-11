using DotCruz.CoreAuth.Domain.Exceptions.BaseExceptions;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using DotCruz.CoreAuth.Domain.Interfaces.Data;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Security;
using DotCruz.CoreAuth.Domain.Interfaces.Security.Tokens;
using MediatR;

namespace DotCruz.CoreAuth.Application.Commands.Auth.ActivateAccount;

public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand>
{
    private readonly IActivationTokenReadRepository _activationTokenReadRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateAccountCommandHandler(
        IActivationTokenReadRepository activationTokenReadRepository,
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider,
        IUnitOfWork unitOfWork
    )
    {
        _activationTokenReadRepository = activationTokenReadRepository;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var hashedToken = _tokenProvider.Hash(request.Token);

        var activationToken = await _activationTokenReadRepository.GetActiveByTokenAsync(hashedToken, cancellationToken);

        if (activationToken == null || !activationToken.IsActive || activationToken.User == null)
            throw new ErrorOnValidationException(ResourceMessagesException.TOKEN_INVALID);

        var passwordHash = _passwordHasher.HashPassword(request.Password);

        activationToken.User.Activate(passwordHash);
        activationToken.Use();

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
