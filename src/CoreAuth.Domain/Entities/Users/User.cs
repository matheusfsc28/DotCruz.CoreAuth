using CoreAuth.Domain.Entities.Base;
using CoreAuth.Domain.Enums.Users;
using CoreAuth.Exceptions;
using CoreAuth.Exceptions.BaseExceptions;

namespace CoreAuth.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserType Type { get; private set; }

        private User() { }

        public User(string name, string email, string passwordHash, UserType type)
        {
            Name = name;
            Email = email.ToLowerInvariant();
            PasswordHash = passwordHash;
            Type = type;

            Validate();
        }

        public void Update(string? name, string? email, string? passwordHash, UserType? type)
        {
            Name = name ?? Name;
            Email = email ?? Email;
            PasswordHash = passwordHash ?? PasswordHash;
            Type = type ?? Type;

            Validate();
        }

        private void Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(Name))
                errors.Add(ResourceMessagesException.NAME_EMPTY);

            if (string.IsNullOrEmpty(Email))
                errors.Add(ResourceMessagesException.EMAIL_EMPTY);

            if (string.IsNullOrEmpty(PasswordHash))
                errors.Add(ResourceMessagesException.PASSWORD_EMPTY);

            if (errors.Count > 0)
                throw new ErrorOnValidationException(errors);
        }
    }
}
