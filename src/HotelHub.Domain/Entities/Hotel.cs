using HotelHub.Domain.Abstraction;
using HotelHub.Domain.Enums;

namespace HotelHub.Domain.Entities;

public class Hotel : BaseEntity
{
    private Hotel() { }

    public string Name { get; private set; } = null!;
    public string TaxId { get; private set; } = null!;
    public TaxType TaxType { get; private set; }
    public string City { get; private set; } = null!;
    public string State { get; private set; } = null!;
    public string Country { get; private set; } = null!;

    public List<UserHotel> UserHotelList { get; private set; } = [];

    public static Hotel Create(string name, string taxId, TaxType taxType, string city, string state, string country)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do hotel não pode ser vazio.", nameof(name));

        if (string.IsNullOrWhiteSpace(taxId))
            throw new ArgumentException("O documento fiscal não pode ser vazio.", nameof(taxId));

        return new Hotel
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            TaxId = taxId.Trim(),
            TaxType = taxType,
            City = city.Trim(),
            State = state.Trim(),
            Country = country.Trim()
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
