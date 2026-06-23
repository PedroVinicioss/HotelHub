using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Enums;

namespace HotelHub.Domain.Entities;

public class Conversation : BaseEntity
{
    private Conversation() { }

    public Guid HotelId { get; private set; }
    public Guid ContactId { get; private set; }
    public Guid ChannelId { get; private set; }
    public ConversationStatus Status { get; private set; }
    public Guid? AssignedUserId { get; private set; }
    public DateTime LastMessageAt { get; private set; }
    public DateTime? FirstResponseAt { get; private set; }
    public DateTime? WindowOpenedAt { get; private set; }

    public Hotel Hotel { get; private set; } = null!;
    public Contact Contact { get; private set; } = null!;
    public Channel Channel { get; private set; } = null!;
    public User? AssignedUser { get; private set; }
    public List<Message> Messages { get; private set; } = [];
    public List<Note> Notes { get; private set; } = [];
    public Booking? Booking { get; private set; }

    public static Conversation Create(Guid hotelId, Guid contactId, Guid channelId)
    {
        var now = DateTime.UtcNow;

        return new Conversation
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            ContactId = contactId,
            ChannelId = channelId,
            Status = ConversationStatus.Open,
            LastMessageAt = now,
            WindowOpenedAt = now
        };
    }

    public void AssignTo(Guid userId)
    {
        AssignedUserId = userId;
        FirstResponseAt ??= DateTime.UtcNow;
        Update();
    }

    public void Resolve()
    {
        Status = ConversationStatus.Resolved;
        Update();
    }

    public void Reopen()
    {
        Status = ConversationStatus.Open;
        WindowOpenedAt = DateTime.UtcNow;
        Update();
    }

    public void RegisterMessage()
    {
        LastMessageAt = DateTime.UtcNow;
        Update();
    }
}
