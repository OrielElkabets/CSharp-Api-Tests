using System.Data;
using Microsoft.AspNetCore.Mvc;
using test_things.DTOs;
using test_things.Services.Actions;

namespace test_things.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UsersService usersService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(usersService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var res = usersService.GetUserById(id);

            return res.Match<IActionResult>(
                Ok,
                NotFound
            );
        }

        [HttpGet("by-city")]
        public IActionResult GetUsersGroupd()
        {
            return Ok(usersService.GroupByCities());
        }

        [HttpGet("by-city/{name}")]
        public IActionResult GetUsersGroupd(string name)
        {
            var res = usersService.GroupByCity(name);
            return res.Match<IActionResult>(
                Ok,
                NotFound
            );
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NewUserDTO newUser)
        {
            var res = usersService.Create(newUser);
            return res.Match<IActionResult>(
                user => CreatedAtAction(nameof(GetUser), new { user.Id }, user),
                BadRequest
            );
        }
    }
}
