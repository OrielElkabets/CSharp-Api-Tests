using OneOf;
using test_things.DTOs;
using test_things.Entities;
using test_things.Errors;

namespace test_things.Services.Factories;

public class PetsFactory(TestDbContext db)
{
    public OneOf<PetEO, BadRequestError> Build(NewPetDTO dto)
    {
        dto = Normalize(dto);
        var type = db.PetsTypes.FirstOrDefault(pt => pt.Id == dto.TypeId);
        if (type is null) return new BadRequestError($"TypeId '{dto.TypeId}' is not valid");

        return new PetEO
        {
            Id = Ulid.NewUlid(),
            Name = dto.Name,
            Type = type
        };
    }

    private NewPetDTO Normalize(NewPetDTO dto) {
        dto.Name = dto.Name.ToLower();
        return dto;
    }
}