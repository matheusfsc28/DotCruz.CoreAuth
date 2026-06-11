using DotCruz.CoreAuth.Domain.Exceptions.BaseExceptions;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using DotCruz.CoreAuth.Domain.Interfaces.Data;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Users;
using MediatR;

namespace DotCruz.CoreAuth.Application.Commands.Users.DeactivateUser;

public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand>
{
    private readonly IUserWriteRepository _userWriteRepository;
    private IUnitOfWork _unitOfWork;

    public DeactivateUserCommandHandler(
        IUserWriteRepository userWriteRepository,
        IUnitOfWork unitOfWork
    )
    {
        _userWriteRepository = userWriteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userWriteRepository.GetByIdToUpdate(request.UserId, cancellationToken) 
            ?? throw new NotFoundException(ResourceMessagesException.USER_NOT_FOUND);
        
        user.Delete();

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
