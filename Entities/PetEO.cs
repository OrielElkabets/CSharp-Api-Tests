namespace test_things.Entities;

public class PetEO
{
    public required Ulid Id { get; set; }
    public required string Name { get; set; }
    public Ulid TypeId { get; set; }

    public PetTypeEO? Type { get; set; }
    public ICollection<UserEO> Users { get; set; } = [];
}
