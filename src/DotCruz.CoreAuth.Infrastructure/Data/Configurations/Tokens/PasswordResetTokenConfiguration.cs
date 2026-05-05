using DotCruz.CoreAuth.Domain.Entities.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotCruz.CoreAuth.Infrastructure.Data.Configurations.Tokens
{
    public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ExpiresAt).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(pst => pst.User)
                .WithMany(u => u.PasswordResetTokens)
                .HasForeignKey(pst => pst.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(x => x.IsExpired);
        }
    }
}
