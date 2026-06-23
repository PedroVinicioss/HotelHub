namespace HotelHub.Domain.Abstraction;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; protected set; }
    public bool IsActive { get; protected set; } = true;

    public void Update() => UpdatedAt = DateTime.UtcNow;
    public void Delete() => IsActive = false;
}