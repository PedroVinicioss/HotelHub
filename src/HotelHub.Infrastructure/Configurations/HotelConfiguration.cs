using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("hotels");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Id).HasColumnName("id");
        builder.Property(h => h.Name).HasColumnName("name").HasMaxLength(300).IsRequired();
        builder.Property(h => h.TaxId).HasColumnName("tax_id").HasMaxLength(20).IsRequired();
        builder.Property(h => h.TaxType).HasColumnName("tax_type").HasMaxLength(10).HasConversion<string>().IsRequired();
        builder.Property(h => h.City).HasColumnName("city").HasMaxLength(150).IsRequired();
        builder.Property(h => h.State).HasColumnName("state").HasMaxLength(100).IsRequired();
        builder.Property(h => h.Country).HasColumnName("country").HasMaxLength(100).IsRequired();
        builder.Property(h => h.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(h => h.UpdatedAt).HasColumnName("updated_at");
        builder.Property(h => h.IsActive).HasColumnName("is_active").IsRequired();

        builder.HasMany(h => h.UserHotelList)
            .WithOne(uh => uh.Hotel)
            .HasForeignKey(uh => uh.HotelId);
    }
}
