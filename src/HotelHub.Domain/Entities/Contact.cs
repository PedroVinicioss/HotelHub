using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Enums;

namespace HotelHub.Domain.Entities;

public class Contact : BaseEntity
{
    private Contact() { }

    public Guid HotelId { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? Cpf { get; private set; }
    public ContactStatus Status { get; private set; }
    public DateTime FirstContactAt { get; private set; }
    public DateTime LastInteractionAt { get; private set; }
    public Guid? MergedIntoContactId { get; private set; }
    public DateTime? AnonymizedAt { get; private set; }

    public Hotel Hotel { get; private set; } = null!;
    public List<ContactChannel> ContactChannels { get; private set; } = [];
    public List<Conversation> Conversations { get; private set; } = [];
    public List<Booking> Bookings { get; private set; } = [];

    public static Contact Create(Guid hotelId, string name, string? phone = null, string? email = null, string? cpf = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do contato não pode ser vazio.", nameof(name));

        var now = DateTime.UtcNow;

        return new Contact
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            Name = name.Trim(),
            Phone = phone?.Trim(),
            Email = email?.Trim().ToLowerInvariant(),
            Cpf = cpf?.Trim(),
            Status = ContactStatus.Active,
            FirstContactAt = now,
            LastInteractionAt = now
        };
    }

    public void MergeInto(Guid targetContactId)
    {
        MergedIntoContactId = targetContactId;
        Status = ContactStatus.Merged;
        Update();
    }

    public void Anonymize()
    {
        Name = "Anonimizado";
        Phone = null;
        Email = null;
        Cpf = null;
        Status = ContactStatus.Anonymized;
        AnonymizedAt = DateTime.UtcNow;
        Update();
    }

    public void UpdateLastInteraction()
    {
        LastInteractionAt = DateTime.UtcNow;
        Update();
    }
}
