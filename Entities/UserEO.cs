namespace test_things.Entities;

public class UserEO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CityId { get; set; }
    public int? PetId { get; set; }

    public CityEO? City { get; set; }
    public PetEO? Pet { get; set; }
}