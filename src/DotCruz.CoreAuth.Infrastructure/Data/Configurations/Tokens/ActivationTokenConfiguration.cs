using DotCruz.CoreAuth.Domain.Entities.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotCruz.CoreAuth.Infrastructure.Data.Configurations.Tokens;

public class ActivationTokenConfiguration : IEntityTypeConfiguration<ActivationToken>
{
    public void Configure(EntityTypeBuilder<ActivationToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ExpiresAt).IsRequired();
        builder.Property(x => x.UsedAt).IsRequired(false);
        builder.Property(x => x.UserId).IsRequired();

        builder.HasOne(at => at.User)
            .WithMany()
            .HasForeignKey(at => at.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.IsExpired);
        builder.Ignore(x => x.IsUsed);
        builder.Ignore(x => x.IsActive);
    }
}
