using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Configurations;

public class UserHotelConfiguration : IEntityTypeConfiguration<UserHotel>
{
    public void Configure(EntityTypeBuilder<UserHotel> builder)
    {
        builder.ToTable("user_hotels");

        builder.HasKey(uh => uh.Id);

        builder.Property(uh => uh.Id).HasColumnName("id");
        builder.Property(uh => uh.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(uh => uh.HotelId).HasColumnName("hotel_id").IsRequired();
        builder.Property(uh => uh.HotelRole).HasColumnName("hotel_role").HasMaxLength(50).HasConversion<string>().IsRequired();
        builder.Property(uh => uh.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(uh => uh.UpdatedAt).HasColumnName("updated_at");
        builder.Property(uh => uh.IsActive).HasColumnName("is_active").IsRequired();

        builder.HasOne(uh => uh.User)
            .WithMany(u => u.UserHotels)
            .HasForeignKey(uh => uh.UserId);

        builder.HasOne(uh => uh.Hotel)
            .WithMany(h => h.UserHotelList)
            .HasForeignKey(uh => uh.HotelId);
    }
}
