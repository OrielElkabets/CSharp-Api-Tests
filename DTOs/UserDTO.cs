using test_things.Entities;
using static test_things.DTOs.MappingHelper;

namespace test_things.DTOs;


public class NewUserDTO
{
    public required string Name { get; set; }
    public required int CityId { get; set; }
    public NewPetDTO? Pet { get; set; }
}


public class UserDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required CityDTO City { get; set; }
    public PetDTO? Pet { get; set; }

    public static UserDTO FromEO(UserEO user)
    {
        return new()
        {
            Id = user.Id,
            Name = user.Name,
            City = MapOrThrow(user.City, CityDTO.FromEO),
            Pet = MapOrNull(user.Pet, PetDTO.FromEO),
        };
    }
}


public class UsersGroupdByCity
{
    public required string City { get; init; }
    public required IEnumerable<string> Users { get; init; }

    public static UsersGroupdByCity FromEO(CityEO city)
    {
        return new()
        {
            City = city.Name,
            Users = city.Users.Select(u => u.Name)
        };
    }
}