using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Enums;

namespace HotelHub.Domain.Entities;

public class UserHotel : BaseEntity
{
    private UserHotel() { }

    public Guid UserId { get; private set; }
    public Guid HotelId { get; private set; }
    public HotelRole HotelRole { get; private set; }

    public User User { get; private set; } = null!;
    public Hotel Hotel { get; private set; } = null!;

    public static UserHotel Create(Guid userId, Guid hotelId, HotelRole role)
    {
        return new UserHotel
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            HotelId = hotelId,
            HotelRole = role
        };
    }

    public void ChangeRole(HotelRole newRole)
    {
        HotelRole = newRole;
        Update();
    }
}
