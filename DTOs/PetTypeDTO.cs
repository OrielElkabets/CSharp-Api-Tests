using test_things.Entities;
using test_things.Exceptions;

namespace test_things.DTOs;

public class NewPetTypeDTO
{
    public required string Value { get; set; }
}

public class PetTypeDTO
{
    public required int Id { get; set; }
    public required string Value { get; set; }

    public static PetTypeDTO FromEO(PetTypeEO type)
    {
        return new()
        {
            Id = type.Id,
            Value = type.Value
        };
    }

    public static PetTypeDTO FromEOOrThrow(PetTypeEO? type)
    {
        if (type is null) throw new EntityNotLoadedException(nameof(PetTypeEO));
        return FromEO(type);
    }
}