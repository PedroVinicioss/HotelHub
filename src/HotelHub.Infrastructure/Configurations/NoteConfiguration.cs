using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("notes");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id).HasColumnName("id");
        builder.Property(n => n.ConversationId).HasColumnName("conversation_id").IsRequired();
        builder.Property(n => n.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(n => n.Content).HasColumnName("content").HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(n => n.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(n => n.UpdatedAt).HasColumnName("updated_at");
        builder.Property(n => n.IsActive).HasColumnName("is_active").IsRequired();

        builder.HasOne(n => n.Conversation)
            .WithMany(c => c.Notes)
            .HasForeignKey(n => n.ConversationId);

        builder.HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId);
    }
}
