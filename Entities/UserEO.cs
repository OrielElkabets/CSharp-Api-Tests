namespace test_things.Entities;

public class UserEO
{
    public required Ulid Id { get; set; }
    public required string Name { get; set; }
    public Ulid CityId { get; set; }
    public Ulid? PetId { get; set; }

    public CityEO? City { get; set; }
    public PetEO? Pet { get; set; }
}