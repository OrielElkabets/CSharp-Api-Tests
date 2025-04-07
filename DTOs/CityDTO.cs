using test_things.Entities;

namespace test_things.DTOs;

public class NewCityDTO
{
    public required string Name { get; set; }
}

public class CityDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public static CityDTO FromEO(CityEO city)
    {
        return new()
        {
            Id = city.Id,
            Name = city.Name
        };
    }
}