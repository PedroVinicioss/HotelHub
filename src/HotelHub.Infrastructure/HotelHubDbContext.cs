using HotelHub.Application.Abstractions;
using HotelHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Infrastructure;

public class HotelHubDbContext : DbContext, IApplicationDbContext
{
    public HotelHubDbContext(DbContextOptions<HotelHubDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<UserHotel> UserHotels => Set<UserHotel>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Channel> Channels => Set<Channel>();
    public DbSet<ContactChannel> ContactChannels => Set<ContactChannel>();
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelHubDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
