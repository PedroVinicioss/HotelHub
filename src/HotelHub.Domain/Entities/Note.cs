using HotelHub.Domain.Abstraction;

namespace HotelHub.Domain.Entities;

public class Note : BaseEntity
{
    private Note() { }

    public Guid ConversationId { get; private set; }
    public Guid UserId { get; private set; }
    public string Content { get; private set; } = null!;

    public Conversation Conversation { get; private set; } = null!;
    public User User { get; private set; } = null!;

    public static Note Create(Guid conversationId, Guid userId, string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("O conteúdo da nota não pode ser vazio.", nameof(content));

        return new Note
        {
            Id = Guid.NewGuid(),
            ConversationId = conversationId,
            UserId = userId,
            Content = content.Trim()
        };
    }
}
