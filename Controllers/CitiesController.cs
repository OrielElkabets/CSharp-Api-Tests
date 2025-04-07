using Microsoft.AspNetCore.Mvc;
using test_things.DTOs;
using test_things.Services.Actions;

namespace test_things.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController(CitiesService citiesService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(citiesService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(Ulid id)
        {
            var res = citiesService.GetById(id);
            return res.Match<IActionResult>(
                Ok,
                NotFound
            );
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NewCityDTO newCity)
        {
            var res = citiesService.Create(newCity);
            return res.Match<IActionResult>(
                city => CreatedAtAction(nameof(GetCity), new { city.Id }, city),
                BadRequest
            );
        }
    }
}