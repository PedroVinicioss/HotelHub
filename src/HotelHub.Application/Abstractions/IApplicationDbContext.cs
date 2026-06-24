using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Hotel> Hotels { get; }
    DbSet<UserHotel> UserHotels { get; }
    DbSet<Contact> Contacts { get; }
    DbSet<Channel> Channels { get; }
    DbSet<ContactChannel> ContactChannels { get; }
    DbSet<Conversation> Conversations { get; }
    DbSet<Message> Messages { get; }
    DbSet<Note> Notes { get; }
    DbSet<Booking> Bookings { get; }
    DbSet<RefreshToken> RefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
