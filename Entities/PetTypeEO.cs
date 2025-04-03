namespace test_things.Entities;

public class PetTypeEO
{
    public int Id { get; set; }
    public required string Value { get; set; }

    public ICollection<PetEO> Pets { get; set; } = [];
}
