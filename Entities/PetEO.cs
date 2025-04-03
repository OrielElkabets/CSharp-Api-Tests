namespace test_things.Entities;

public class PetEO
{
    public int Id { get; set; }
    public int TypeId { get; set; }
    public required string Name { get; set; }

    public PetTypeEO? Type { get; set; }
    public ICollection<UserEO> Users { get; set; } = [];
}
