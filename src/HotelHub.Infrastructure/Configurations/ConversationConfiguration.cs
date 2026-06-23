using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Configurations;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.ToTable("conversations");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.HotelId).HasColumnName("hotel_id").IsRequired();
        builder.Property(c => c.ContactId).HasColumnName("contact_id").IsRequired();
        builder.Property(c => c.ChannelId).HasColumnName("channel_id").IsRequired();
        builder.Property(c => c.Status).HasColumnName("status").HasMaxLength(50).HasConversion<string>().IsRequired();
        builder.Property(c => c.AssignedUserId).HasColumnName("assigned_user_id");
        builder.Property(c => c.LastMessageAt).HasColumnName("last_message_at").IsRequired();
        builder.Property(c => c.FirstResponseAt).HasColumnName("first_response_at");
        builder.Property(c => c.WindowOpenedAt).HasColumnName("window_opened_at");
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

        builder.HasOne(c => c.Hotel)
            .WithMany()
            .HasForeignKey(c => c.HotelId);

        builder.HasOne(c => c.Contact)
            .WithMany(ct => ct.Conversations)
            .HasForeignKey(c => c.ContactId);

        builder.HasOne(c => c.Channel)
            .WithMany(ch => ch.Conversations)
            .HasForeignKey(c => c.ChannelId);

        builder.HasOne(c => c.AssignedUser)
            .WithMany()
            .HasForeignKey(c => c.AssignedUserId)
            .IsRequired(false);
    }
}
