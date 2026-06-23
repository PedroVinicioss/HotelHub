using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Configurations;

public class ContactChannelConfiguration : IEntityTypeConfiguration<ContactChannel>
{
    public void Configure(EntityTypeBuilder<ContactChannel> builder)
    {
        builder.ToTable("contact_channels");

        builder.HasKey(cc => cc.Id);

        builder.Property(cc => cc.Id).HasColumnName("id");
        builder.Property(cc => cc.ContactId).HasColumnName("contact_id").IsRequired();
        builder.Property(cc => cc.ChannelId).HasColumnName("channel_id").IsRequired();
        builder.Property(cc => cc.ExternalContactId).HasColumnName("external_contact_id").HasMaxLength(300).IsRequired();
        builder.Property(cc => cc.LastSeenAt).HasColumnName("last_seen_at").IsRequired();
        builder.Property(cc => cc.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(cc => cc.UpdatedAt).HasColumnName("updated_at");
        builder.Property(cc => cc.IsActive).HasColumnName("is_active").IsRequired();

        builder.HasOne(cc => cc.Contact)
            .WithMany(c => c.ContactChannels)
            .HasForeignKey(cc => cc.ContactId);

        builder.HasOne(cc => cc.Channel)
            .WithMany(c => c.ContactChannels)
            .HasForeignKey(cc => cc.ChannelId);

        builder.HasIndex(cc => new { cc.ChannelId, cc.ExternalContactId }).IsUnique();
    }
}
