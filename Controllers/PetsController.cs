using Microsoft.AspNetCore.Mvc;
using test_things.DTOs;
using test_things.Services.Actions;

namespace test_things.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController(PetsService petsService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPets()
        {
            return Ok(petsService.GetAll());
        }
    }

    [Route("api/pets/types")]
    [ApiController]
    public class PetsTypesController(PetsTypesService petsTypesService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPetsTypes()
        {
            return Ok(petsTypesService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetPetType(int id)
        {
            var res = petsTypesService.GetById(id);
            return res.Match<IActionResult>(
                Ok,
                NotFound
            );
        }

        [HttpPost]
        public IActionResult Create([FromBody] NewPetTypeDTO newType)
        {
            var res = petsTypesService.Create(newType);
            return res.Match<IActionResult>(
                type => CreatedAtAction(nameof(GetPetType), new { type.Id }, type),
                BadRequest
            );
        }
    }
}
