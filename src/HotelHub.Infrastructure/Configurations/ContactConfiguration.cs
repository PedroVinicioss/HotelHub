using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("contacts");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.HotelId).HasColumnName("hotel_id").IsRequired();
        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        builder.Property(c => c.Phone).HasColumnName("phone").HasMaxLength(30);
        builder.Property(c => c.Email).HasColumnName("email").HasMaxLength(300);
        builder.Property(c => c.Cpf).HasColumnName("cpf").HasMaxLength(14);
        builder.Property(c => c.Status).HasColumnName("status").HasMaxLength(50).HasConversion<string>().IsRequired();
        builder.Property(c => c.FirstContactAt).HasColumnName("first_contact_at").IsRequired();
        builder.Property(c => c.LastInteractionAt).HasColumnName("last_interaction_at").IsRequired();
        builder.Property(c => c.MergedIntoContactId).HasColumnName("merged_into_contact_id");
        builder.Property(c => c.AnonymizedAt).HasColumnName("anonymized_at");
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();

        builder.HasOne(c => c.Hotel)
            .WithMany()
            .HasForeignKey(c => c.HotelId);

        builder.HasOne<Contact>()
            .WithMany()
            .HasForeignKey(c => c.MergedIntoContactId)
            .IsRequired(false);
    }
}
