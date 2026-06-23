using HotelHub.Domain.Abstraction;

namespace HotelHub.Domain.Entities;

public class ContactChannel : BaseEntity
{
    private ContactChannel() { }

    public Guid ContactId { get; private set; }
    public Guid ChannelId { get; private set; }
    public string ExternalContactId { get; private set; } = null!;
    public DateTime LastSeenAt { get; private set; }

    public Contact Contact { get; private set; } = null!;
    public Channel Channel { get; private set; } = null!;

    public static ContactChannel Create(Guid contactId, Guid channelId, string externalContactId)
    {
        if (string.IsNullOrWhiteSpace(externalContactId))
            throw new ArgumentException("O ID externo do contato não pode ser vazio.", nameof(externalContactId));

        return new ContactChannel
        {
            Id = Guid.NewGuid(),
            ContactId = contactId,
            ChannelId = channelId,
            ExternalContactId = externalContactId.Trim(),
            LastSeenAt = DateTime.UtcNow
        };
    }

    public void UpdateLastSeen()
    {
        LastSeenAt = DateTime.UtcNow;
        Update();
    }
}
