using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id).HasColumnName("id");
        builder.Property(m => m.ConversationId).HasColumnName("conversation_id").IsRequired();
        builder.Property(m => m.Direction).HasColumnName("direction").HasMaxLength(20).HasConversion<string>().IsRequired();
        builder.Property(m => m.Type).HasColumnName("type").HasMaxLength(30).HasConversion<string>().IsRequired();
        builder.Property(m => m.Content).HasColumnName("content").HasColumnType("nvarchar(max)");
        builder.Property(m => m.MediaUrl).HasColumnName("media_url").HasMaxLength(2000);
        builder.Property(m => m.MediaType).HasColumnName("media_type").HasMaxLength(100);
        builder.Property(m => m.MediaSize).HasColumnName("media_size");
        builder.Property(m => m.ExternalMessageId).HasColumnName("external_message_id").HasMaxLength(300);
        builder.Property(m => m.Status).HasColumnName("status").HasMaxLength(30).HasConversion<string>().IsRequired();
        builder.Property(m => m.SentAt).HasColumnName("sent_at").IsRequired();
        builder.Property(m => m.SenderUserId).HasColumnName("sender_user_id");
        builder.Property(m => m.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(m => m.UpdatedAt).HasColumnName("updated_at");
        builder.Property(m => m.IsActive).HasColumnName("is_active").IsRequired();

        builder.HasOne(m => m.Conversation)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ConversationId);

        builder.HasOne(m => m.SenderUser)
            .WithMany()
            .HasForeignKey(m => m.SenderUserId)
            .IsRequired(false);
    }
}
