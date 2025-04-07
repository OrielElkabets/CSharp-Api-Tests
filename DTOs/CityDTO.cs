using test_things.Entities;
using test_things.Exceptions;

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

    public static CityDTO FromEoOrThrow(CityEO? city)
    {
        if (city is null) throw new EntityNotLoadedException(nameof(CityEO));
        return FromEO(city);
    }
}