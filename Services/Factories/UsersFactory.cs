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
            if (res.IsT1) return res.AsT1;
            pet = res.AsT0;
        }

        return new UserEO
        {
            Id = Ulid.NewUlid(),
            Name = dto.Name,
            City = city,
            Pet = pet
        };
    }
}
