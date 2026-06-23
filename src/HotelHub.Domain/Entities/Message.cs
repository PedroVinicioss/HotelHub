using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Enums;

namespace HotelHub.Domain.Entities;

public class Message : BaseEntity
{
    private Message() { }

    public Guid ConversationId { get; private set; }
    public MessageDirection Direction { get; private set; }
    public MessageType Type { get; private set; }
    public string? Content { get; private set; }
    public string? MediaUrl { get; private set; }
    public string? MediaType { get; private set; }
    public int? MediaSize { get; private set; }
    public string? ExternalMessageId { get; private set; }
    public MessageStatus Status { get; private set; }
    public DateTime SentAt { get; private set; }
    public Guid? SenderUserId { get; private set; }

    public Conversation Conversation { get; private set; } = null!;
    public User? SenderUser { get; private set; }

    public static Message CreateText(
        Guid conversationId,
        MessageDirection direction,
        string content,
        Guid? senderUserId = null,
        string? externalMessageId = null)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("O conteúdo da mensagem não pode ser vazio.", nameof(content));

        return new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = conversationId,
            Direction = direction,
            Type = MessageType.Text,
            Content = content,
            Status = MessageStatus.Sent,
            SentAt = DateTime.UtcNow,
            SenderUserId = senderUserId,
            ExternalMessageId = externalMessageId
        };
    }

    public static Message CreateMedia(
        Guid conversationId,
        MessageDirection direction,
        MessageType type,
        string mediaUrl,
        string mediaType,
        int? mediaSize = null,
        Guid? senderUserId = null,
        string? externalMessageId = null)
    {
        return new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = conversationId,
            Direction = direction,
            Type = type,
            MediaUrl = mediaUrl,
            MediaType = mediaType,
            MediaSize = mediaSize,
            Status = MessageStatus.Sent,
            SentAt = DateTime.UtcNow,
            SenderUserId = senderUserId,
            ExternalMessageId = externalMessageId
        };
    }

    public void MarkAsDelivered()
    {
        Status = MessageStatus.Delivered;
        Update();
    }

    public void MarkAsRead()
    {
        Status = MessageStatus.Read;
        Update();
    }

    public void MarkAsFailed()
    {
        Status = MessageStatus.Failed;
        Update();
    }
}
