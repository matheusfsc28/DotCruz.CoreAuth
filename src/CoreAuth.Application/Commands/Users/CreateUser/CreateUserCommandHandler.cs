using CoreAuth.Domain.Entities.Users;
using CoreAuth.Domain.Interfaces.Data;
using CoreAuth.Domain.Interfaces.Repositories.Users;
using CoreAuth.Domain.Interfaces.Security;
using CoreAuth.Exceptions;
using CoreAuth.Exceptions.BaseExceptions;
using MediatR;

namespace CoreAuth.Application.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(
            IUserWriteRepository userWriteRepository,
            IUserReadRepository userReadRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _userReadRepository.ExistsActiveUserWithEmailAsync(request.Email, cancellationToken);

            if (emailExists)
                throw new ErrorOnValidationException(ResourceMessagesException.EMAIL_ALREADY_EXISTS);

            var passwordHash = _passwordHasher.HashPassword(request.Password);

            var user = new User(
                request.Name,
                request.Email,
                passwordHash,
                request.Type
            );

            await _userWriteRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return user.Id;
        }
    }
}