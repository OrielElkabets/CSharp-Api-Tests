using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_things.DTOs;

namespace test_things.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController(TestDbContext db) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(db.Cities.AsNoTracking().Select(CityDTO.FromEO));
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city = db.Cities.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (city is null) return NotFound($"City with ID '{id}' not found!");
            return Ok(CityDTO.FromEO(city));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NewCityDTO newCity)
        {
            var city = newCity.ToEO();
            db.Cities.Add(city);
            db.SaveChanges();
            return CreatedAtAction(nameof(GetCity), new { city.Id }, CityDTO.FromEO(city));
        }
    }
}
