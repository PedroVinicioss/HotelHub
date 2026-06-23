using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Enums;

namespace HotelHub.Domain.Entities;

public class Channel : BaseEntity
{
    private Channel() { }

    public Guid HotelId { get; private set; }
    public PlatformType PlatformType { get; private set; }
    public string ExternalAccountId { get; private set; } = null!;
    public string AccessTokenEncrypted { get; private set; } = null!;
    public DateTime? TokenExpiresAt { get; private set; }
    public string? WebhookVerifyToken { get; private set; }
    public ChannelStatus Status { get; private set; }

    public Hotel Hotel { get; private set; } = null!;
    public List<ContactChannel> ContactChannels { get; private set; } = [];
    public List<Conversation> Conversations { get; private set; } = [];

    public static Channel Create(
        Guid hotelId,
        PlatformType platformType,
        string externalAccountId,
        string accessTokenEncrypted,
        DateTime? tokenExpiresAt = null,
        string? webhookVerifyToken = null)
    {
        if (string.IsNullOrWhiteSpace(externalAccountId))
            throw new ArgumentException("O ID da conta externa não pode ser vazio.", nameof(externalAccountId));

        return new Channel
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            PlatformType = platformType,
            ExternalAccountId = externalAccountId.Trim(),
            AccessTokenEncrypted = accessTokenEncrypted,
            TokenExpiresAt = tokenExpiresAt,
            WebhookVerifyToken = webhookVerifyToken,
            Status = ChannelStatus.Active
        };
    }

    public void Deactivate()
    {
        Status = ChannelStatus.Inactive;
        Update();
    }

    public void RotateToken(string newEncryptedToken, DateTime? expiresAt)
    {
        AccessTokenEncrypted = newEncryptedToken;
        TokenExpiresAt = expiresAt;
        Update();
    }
}
