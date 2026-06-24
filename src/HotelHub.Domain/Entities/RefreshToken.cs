using HotelHub.Domain.Abstraction;

namespace HotelHub.Domain.Entities;

public sealed class RefreshToken : BaseEntity
{
    private RefreshToken() { }

    public Guid UserId { get; private set; }
    public string Token { get; private set; } = null!;
    public string? IpAddress { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    public User User { get; private set; } = null!;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsActive => !IsRevoked && !IsExpired;

    public static RefreshToken Create(Guid userId, string token, string? ipAddress, int expiresInDays) =>
        new()
        {
            UserId = userId,
            Token = token,
            IpAddress = ipAddress,
            ExpiresAt = DateTime.UtcNow.AddDays(expiresInDays)
        };

    public void Revoke()
    {
        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
        Update();
    }
}
