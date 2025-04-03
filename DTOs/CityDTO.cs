using test_things.Entities;

namespace test_things.DTOs;

public class NewCityDTO
{
    public required string Name { get; set; }

    public CityEO ToEO()
    {
        return new()
        {
            Name = Name
        };
    }
}

public class CityDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public static CityDTO? FromEO(CityEO? city)
    {
        if (city is null) return null;
        
        return new()
        {
            Id = city.Id,
            Name = city.Name
        };
    }
}