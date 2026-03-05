namespace CoreAuth.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; protected set; }

        public void Touch() => UpdatedAt = DateTime.UtcNow;
        public void Delete() => DeletedAt = DateTime.UtcNow;
    }
}
