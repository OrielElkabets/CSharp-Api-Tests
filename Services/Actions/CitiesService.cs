using Microsoft.EntityFrameworkCore;
using OneOf;
using test_things.DTOs;
using test_things.Errors;
using test_things.Services.Factories;

namespace test_things.Services.Actions;

public class CitiesService(TestDbContext db, CitiesFactory citiesFactory)
{
    public IEnumerable<CityDTO> GetAll()
    {
        return db.Cities.AsNoTracking().Select(CityDTO.FromEO);
    }

    public OneOf<CityDTO, PropertyNotFoundError> GetById(Ulid id)
    {
        var city = db.Cities.AsNoTracking().FirstOrDefault(c => c.Id == id);
        if (city is null) return new PropertyNotFoundError("City", "ID", id);
        return CityDTO.FromEO(city);
    }

    public OneOf<CityDTO, ExistError> Create(NewCityDTO newCity)
    {
        var res = citiesFactory.Build(newCity);
        if (res.IsT1) return res.AsT1;

        var city = res.AsT0;
        db.Cities.Add(city);
        db.SaveChanges();

        return CityDTO.FromEO(city);
    }
}
