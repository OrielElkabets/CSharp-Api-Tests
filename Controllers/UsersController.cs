using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_things.DTOs;
using test_things.Entities;

namespace test_things.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(TestDbContext db) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(db.Users
                .AsNoTracking()
                .Include(u => u.City)
                .Include(u => u.Pet)
                .ThenInclude(p => p!.Type)
                .Select(UserDTO.FromEO));
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = db.Users
                .AsNoTracking()
                .Include(u => u.City)
                .Include(u => u.Pet)
                .ThenInclude(p => p!.Type)
                .FirstOrDefault(u => u.Id == id);

            if (user is null) return NotFound($"User with ID '{id}' not found!");
            return Ok(UserDTO.FromEO(user));
        }

        [HttpGet("by-city")]
        public IActionResult GetUsersGroupd()
        {
            return Ok(
                db.Cities.AsNoTracking().Include(c => c.Users).Select(c => new
                {
                    City = c.Name,
                    Users = c.Users.Select(u => u.Name)
                })
            );
        }

        [HttpGet("by-city/{name}")]
        public IActionResult GetUsersGroupd(string name)
        {
            var city = db.Cities
                .AsNoTracking()
                .Include(c => c.Users)
                .FirstOrDefault(c => c.Name == name);

            if (city is null) return BadRequest($"City '${name}' not exist in the system.");

            return Ok(new
            {
                City = city.Name,
                Users = city.Users.Select(u => u.Name)
            });
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NewUserDTO newUser)
        {
            var city = db.Cities.FirstOrDefault(c => c.Name == newUser.City);
            if (city is null) return BadRequest($"City '{newUser.City}' is not valid");

            PetEO? pet = null;
            if (newUser.Pet is not null)
            {
                var type = db.PetsTypes.FirstOrDefault(pt => pt.Value == newUser.Pet.Type);
                if (type is null) return BadRequest($"Pet Type '{newUser.Pet.Type}' is not valid");
                pet = newUser.Pet.ToEO(type);
            }

            var user = newUser.ToEO(city, pet);
            db.Users.Add(user);
            db.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { user.Id }, UserDTO.FromEO(user));
        }
    }
}
