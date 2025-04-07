using OneOf;
using test_things.DTOs;
using test_things.Entities;
using test_things.Errors;

namespace test_things.Services.Factories;

public class PetsFactory(TestDbContext db)
{
    public OneOf<PetEO, BadRequestError> Build(NewPetDTO dto)
    {
        var type = db.PetsTypes.FirstOrDefault(pt => pt.Id == dto.TypeId);
        if (type is null) return new BadRequestError($"TypeId '{dto.TypeId}' is not valid");

        return new PetEO
        {
            Name = dto.Name,
            Type = type
        };
    }
}