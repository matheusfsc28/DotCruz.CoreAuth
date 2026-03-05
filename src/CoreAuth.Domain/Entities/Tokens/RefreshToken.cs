using CoreAuth.Domain.Entities.Base;
using CoreAuth.Exceptions;
using CoreAuth.Exceptions.BaseExceptions;

namespace CoreAuth.Domain.Entities.Tokens
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public Guid UserId { get; private set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsRevoked => RevokedAt.HasValue;
        public bool IsActive => !IsRevoked && !IsExpired;

        private RefreshToken() { }

        public RefreshToken(string token, DateTime expiresAt, Guid userId)
        {
            Token = token;
            ExpiresAt = expiresAt;
            UserId = userId;

            Validate();
        }

        private void Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Token)) 
                errors.Add(ResourceMessagesException.TOKEN_EMPTY);

            if (IsExpired) 
                errors.Add(ResourceMessagesException.EXPIRATION_DATE_EARLIER_CURRENT_DATE);]

            if (errors.Count > 0)
                throw new ErrorOnValidationException(errors);
        }

        public void Revoke()
        {
            if (IsActive)
                RevokedAt = DateTime.UtcNow;
        }
    }
}
