using DotCruz.CoreAuth.Domain.Exceptions.BaseExceptions;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using DotCruz.CoreAuth.Domain.Interfaces.Data;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Users;
using MediatR;

namespace DotCruz.CoreAuth.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(
            IUserWriteRepository userWriteRepository,
            IUserReadRepository userReadRepository,
            IUnitOfWork unitOfWork)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userWriteRepository.GetByIdToUpdate(request.Id, cancellationToken);

            if (user is null)
                throw new NotFoundException(ResourceMessagesException.USER_NOT_FOUND);

            if (!user.Email.Equals(request.Request.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailExists = await _userReadRepository.ExistsActiveUserWithEmailAsync(request.Request.Email, cancellationToken);
                if (emailExists)
                    throw new ErrorOnValidationException(ResourceMessagesException.EMAIL_ALREADY_EXISTS);
            }

            user.Update(request.Request.Name, request.Request.Email, null, null);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
