using test_things.Entities;

namespace test_things.DTOs;

public class NewUserDTO
{
    public required string Name { get; set; }
    public required string City { get; set; }

    public UserEO ToEO(CityEO city)
    {
        return new()
        {
            Name = Name,
            City = city
        };
    }
}


public class UserDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required CityDTO? City { get; set; }

    public static UserDTO FromEO(UserEO user)
    {
        return new()
        {
            Id = user.Id,
            Name = user.Name,
            City = CityDTO.FromEO(user.City),
        };
    }
}
