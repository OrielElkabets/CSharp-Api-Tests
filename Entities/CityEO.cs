namespace test_things.Entities;

public class CityEO
{
    public required Ulid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<UserEO> Users { get; set; } = [];
}
