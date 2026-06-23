using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Configurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.ToTable("channels");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.HotelId).HasColumnName("hotel_id").IsRequired();
        builder.Property(c => c.PlatformType).HasColumnName("platform_type").HasMaxLength(50).HasConversion<string>().IsRequired();
        builder.Property(c => c.ExternalAccountId).HasColumnName("external_account_id").HasMaxLength(300).IsRequired();
        builder.Property(c => c.AccessTokenEncrypted).HasColumnName("access_token_encrypted").HasMaxLength(1000).IsRequired();
        builder.Property(c => c.TokenExpiresAt).HasColumnName("token_expires_at");
        builder.Property(c => c.WebhookVerifyToken).HasColumnName("webhook_verify_token").HasMaxLength(500);
        builder.Property(c => c.Status).HasColumnName("status").HasMaxLength(50).HasConversion<string>().IsRequired();
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

        builder.HasOne(c => c.Hotel)
            .WithMany()
            .HasForeignKey(c => c.HotelId);
    }
}
