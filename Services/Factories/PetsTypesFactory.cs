using OneOf;
using test_things.DTOs;
using test_things.Entities;
using test_things.Errors;

namespace test_things.Services.Factories;

public class PetsTypesFactory(TestDbContext db)
{
    public OneOf<PetTypeEO, ExistError> Build(NewPetTypeDTO dto)
    {
        var exist = db.PetsTypes.Any(pt => pt.Value == dto.Value);
        if (exist) return new ExistError("PetType", "value", dto.Value);

        return new PetTypeEO
        {
            Value = dto.Value
        };
    }
}