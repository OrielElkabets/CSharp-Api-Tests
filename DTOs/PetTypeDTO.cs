using test_things.Entities;

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
}