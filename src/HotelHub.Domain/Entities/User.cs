using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Abstraction.ValueObject;
using HotelHub.Domain.Enums;

namespace HotelHub.Domain.Entities;

public class User : BaseEntity
{
    private User() 
    { 
        UserHotels = new List<UserHotel>();
    }

    public string Name { get; private set; } = null!;
    public EmailAddress Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public UserRole UserRole { get; private set; } = UserRole.User;

    public List<UserHotel> UserHotels { get; private set; }


    public static User Create(string name, EmailAddress email, string password, UserRole role)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do usuário não pode ser vazio.", nameof(name));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("A senha não pode ser vazia.", nameof(password));

        return new User
        {
            Id = Guid.NewGuid(), 
            Name = name.Trim(),
            Email = email,
            Password = password,
            UserRole = role
        };
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("O nome não pode ser vazio.", nameof(newName));

        Name = newName.Trim();
        Update();
    }
}