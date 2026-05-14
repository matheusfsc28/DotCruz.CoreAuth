using DotCruz.CoreAuth.Application.Interfaces.Services;
using DotCruz.CoreAuth.Domain.Entities.Users;
using DotCruz.CoreAuth.Domain.Exceptions.BaseExceptions;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using DotCruz.CoreAuth.Domain.Interfaces.Data;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Users;
using DotCruz.CoreAuth.Domain.Interfaces.Security;
using MediatR;

namespace DotCruz.CoreAuth.Application.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;

        public CreateUserCommandHandler(
            IUserWriteRepository userWriteRepository,
            IUserReadRepository userReadRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IEmailService emailService)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
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

            await _emailService.SendWelcomeEmailAsync(user.Email, user.Name, cancellationToken);

            return user.Id;
        }
    }
}
