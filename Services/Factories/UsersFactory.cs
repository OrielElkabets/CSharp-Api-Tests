using OneOf;
using test_things.DTOs;
using test_things.Entities;
using test_things.Errors;

namespace test_things.Services.Factories;

public class UsersFactory(TestDbContext db, PetsFactory petsFactory)
{
    public OneOf<UserEO, BadRequestError> Build(NewUserDTO dto)
    {
        var city = db.Cities.FirstOrDefault(c => c.Id == dto.CityId);
        if (city is null) return new BadRequestError($"CityId '{dto.CityId}' is not valid");

        PetEO? pet = null;
        if (dto.Pet is not null)
        {
            var res = petsFactory.Build(dto.Pet);
            if(res.IsT0) pet = res.AsT0;
            return res.AsT1;
        }

        return new UserEO {
            Name = dto.Name,
            City = city,
            Pet = pet
        };
    }
}
