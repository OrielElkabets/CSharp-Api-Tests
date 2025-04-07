using Microsoft.EntityFrameworkCore;
using test_things.DTOs;

namespace test_things.Services.Actions;

public class PetsService(TestDbContext db)
{
    public IEnumerable<PetDTO> GetAll()
    {
        return db.Pets
            .AsNoTracking()
            .Include(p => p.Type)
            .Select(PetDTO.FromEO);
    }
}