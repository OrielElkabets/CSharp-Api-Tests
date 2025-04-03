namespace test_things.Entities;

public class CityEO
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<UserEO> Users { get; set; } = [];
}
