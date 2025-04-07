using test_things.Entities;
using test_things.Exceptions;

namespace test_things.DTOs;

public class NewPetDTO
{
    public required string Name { get; set; }
    public required int TypeId { get; set; }
}


public class PetDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required PetTypeDTO Type { get; set; }

    public static PetDTO FromEO(PetEO pet)
    {
        return new()
        {
            Id = pet.Id,
            Name = pet.Name,
            Type = PetTypeDTO.FromEOOrThrow(pet.Type)
        };
    }

    public static PetDTO? FromEOOrNull(PetEO? pet)
    {
        if (pet is null) return null;
        return FromEO(pet);
    }
}