using OneOf;
using test_things.DTOs;
using test_things.Entities;
using test_things.Errors;

namespace test_things.Services.Factories;

public class CitiesFactory(TestDbContext db)
{
    public OneOf<CityEO, ExistError> Build(NewCityDTO dto)
    {
        dto = Nortmalize(dto);
        var exist = db.Cities.Any(c => c.Name == dto.Name);
        if (exist) return new ExistError("City", "name", dto.Name);
        
        return new CityEO
        {
            Id = Ulid.NewUlid(),
            Name = dto.Name
        };
    }

    private NewCityDTO Nortmalize(NewCityDTO dto) {
        dto.Name = dto.Name.ToLower();
        return dto;
    }
}
