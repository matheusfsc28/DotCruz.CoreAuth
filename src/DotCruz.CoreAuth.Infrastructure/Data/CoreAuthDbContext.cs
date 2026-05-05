using DotCruz.CoreAuth.Domain.Entities.Base;
using DotCruz.CoreAuth.Domain.Entities.Tokens;
using DotCruz.CoreAuth.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotCruz.CoreAuth.Infrastructure.Data
{
    public class CoreAuthDbContext : DbContext
    {
        public CoreAuthDbContext(DbContextOptions<CoreAuthDbContext> options) : base(options) { }
    
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreAuthDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => e.ClrType != null && typeof(BaseEntity).IsAssignableFrom(e.ClrType)))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var propertyAccess = Expression.Property(parameter, nameof(BaseEntity.DeletedAt));
                var nullValue = Expression.Constant(null, typeof(DateTime?));
                var expression = Expression.Equal(propertyAccess, nullValue);
                var lambda = Expression.Lambda(expression, parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.Touch();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
