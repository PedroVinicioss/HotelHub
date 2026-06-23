using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("id");
        builder.Property(b => b.HotelId).HasColumnName("hotel_id").IsRequired();
        builder.Property(b => b.ContactId).HasColumnName("contact_id").IsRequired();
        builder.Property(b => b.ConversationId).HasColumnName("conversation_id");
        builder.Property(b => b.CheckInDate).HasColumnName("check_in_date").IsRequired();
        builder.Property(b => b.CheckOutDate).HasColumnName("check_out_date").IsRequired();
        builder.Property(b => b.Value).HasColumnName("value").HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(b => b.ClosedAt).HasColumnName("closed_at");
        builder.Property(b => b.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(b => b.UpdatedAt).HasColumnName("updated_at");
        builder.Property(b => b.IsActive).HasColumnName("is_active").IsRequired();

        builder.HasOne(b => b.Hotel)
            .WithMany()
            .HasForeignKey(b => b.HotelId);

        builder.HasOne(b => b.Contact)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.ContactId);

        builder.HasOne(b => b.Conversation)
            .WithOne(c => c.Booking)
            .HasForeignKey<Booking>(b => b.ConversationId)
            .IsRequired(false);
    }
}
