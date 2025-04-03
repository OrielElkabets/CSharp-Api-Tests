using test_things.Entities;

namespace test_things.DTOs;

public class NewPetDTO
{
    public required string Name { get; set; }
    public required string Type { get; set; }

    public PetEO ToEO(PetTypeEO type)
    {
        return new()
        {
            Name = Name,
            Type = type
        };
    }
}


public class PetDTO
{
    public required int Id { get; set; }
    public required PetTypeDTO? Type { get; set; }
    public required string Name { get; set; }

    public static PetDTO? FromEO(PetEO? pet)
    {
        if (pet is null) return null;
        
        return new()
        {
            Id = pet.Id,
            Name = pet.Name,
            Type = PetTypeDTO.FromEO(pet.Type)
        };
    }
}