using HotelHub.Domain.Abstraction.ValueObject;
using HotelHub.Domain.Entities;
using HotelHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        builder.Property(u => u.Password).HasColumnName("password").IsRequired();
        builder.Property(u => u.UserRole).HasColumnName("role").HasMaxLength(50).HasConversion<string>().IsRequired();
        builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(u => u.UpdatedAt).HasColumnName("updated_at");
        builder.Property(u => u.IsActive).HasColumnName("is_active").IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(300)
            .IsRequired()
            .HasConversion(
                email => email.Value,
                value => EmailAddress.Create(value).Email!);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
