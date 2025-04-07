using Microsoft.EntityFrameworkCore;
using OneOf;
using test_things.DTOs;
using test_things.Errors;
using test_things.Services.Factories;

namespace test_things.Services.Actions;

public class PetsTypesService(TestDbContext db, PetsTypesFactory petsTypesFactory)
{
    public IEnumerable<PetTypeDTO> GetAll()
    {
        return db.PetsTypes.AsNoTracking().Select(PetTypeDTO.FromEO);
    }

    public OneOf<PetTypeDTO, PropertyNotFoundError> GetById(Ulid id)
    {
        var type = db.PetsTypes.AsNoTracking().FirstOrDefault(pt => pt.Id == id);
        if (type is null) return new PropertyNotFoundError("PetType", "ID", id);
        return PetTypeDTO.FromEO(type);
    }

    public OneOf<PetTypeDTO, ExistError> Create(NewPetTypeDTO newType)
    {
        var res = petsTypesFactory.Build(newType);
        if (res.IsT1) return res.AsT1;

        var type = res.AsT0;
        db.PetsTypes.Add(type);
        db.SaveChanges();

        return PetTypeDTO.FromEO(type);
    }
}