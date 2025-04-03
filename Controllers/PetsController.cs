using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_things.DTOs;

namespace test_things.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController(TestDbContext db) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPets()
        {
            var res = db.Pets.AsNoTracking().Include(p => p.Type).Select(PetDTO.FromEO);
            return Ok(res);
        }
    }

    [Route("api/pets/types")]
    [ApiController]
    public class PetsTypesController(TestDbContext db) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPetsTypes()
        {
            var res = db.PetsTypes.AsNoTracking().Select(PetTypeDTO.FromEO);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetPetType(int id)
        {
            var type = db.PetsTypes.AsNoTracking().FirstOrDefault(pt => pt.Id == id);
            if (type is null) return NotFound($"PetType with ID '{id}' not found!");
            return Ok(PetTypeDTO.FromEO(type));
        }

        [HttpPost]
        public IActionResult Create([FromBody] NewPetTypeDTO newType)
        {
            var type = newType.ToEO();
            db.PetsTypes.Add(type);
            db.SaveChanges();
            return CreatedAtAction(nameof(GetPetType), new { type.Id }, PetTypeDTO.FromEO(type));
        }
    }
}
