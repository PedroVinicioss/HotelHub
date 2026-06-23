using HotelHub.Domain.Abstraction;

namespace HotelHub.Domain.Entities;

public class Booking : BaseEntity
{
    private Booking() { }

    public Guid HotelId { get; private set; }
    public Guid ContactId { get; private set; }
    public Guid? ConversationId { get; private set; }
    public DateOnly CheckInDate { get; private set; }
    public DateOnly CheckOutDate { get; private set; }
    public decimal Value { get; private set; }
    public DateTime? ClosedAt { get; private set; }

    public Hotel Hotel { get; private set; } = null!;
    public Contact Contact { get; private set; } = null!;
    public Conversation? Conversation { get; private set; }

    public static Booking Create(
        Guid hotelId,
        Guid contactId,
        DateOnly checkInDate,
        DateOnly checkOutDate,
        decimal value,
        Guid? conversationId = null)
    {
        if (checkOutDate <= checkInDate)
            throw new ArgumentException("A data de check-out deve ser posterior ao check-in.");

        if (value < 0)
            throw new ArgumentException("O valor não pode ser negativo.", nameof(value));

        return new Booking
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            ContactId = contactId,
            ConversationId = conversationId,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate,
            Value = value
        };
    }

    public void Close()
    {
        ClosedAt = DateTime.UtcNow;
        Update();
    }
}
