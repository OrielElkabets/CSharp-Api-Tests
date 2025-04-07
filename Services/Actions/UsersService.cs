using Microsoft.EntityFrameworkCore;
using OneOf;
using test_things.DTOs;
using test_things.Errors;
using test_things.Services.Factories;

namespace test_things.Services.Actions;

public class UsersService(TestDbContext db, UsersFactory usersFactory)
{
    public IEnumerable<UserDTO> GetAll()
    {
        return db.Users
                .AsNoTracking()
                .Include(u => u.City)
                .Include(u => u.Pet)
                .ThenInclude(p => p!.Type)
                .Select(UserDTO.FromEO);
    }

    public OneOf<UserDTO, PropertyNotFoundError> GetUserById(Ulid id)
    {
        var user = db.Users
                .AsNoTracking()
                .Include(u => u.City)
                .Include(u => u.Pet)
                .ThenInclude(p => p!.Type)
                .FirstOrDefault(u => u.Id == id);

        if (user is null) return new PropertyNotFoundError("User", "ID", id);
        return UserDTO.FromEO(user);
    }

    public IEnumerable<UsersGroupdByCity> GroupByCities()
    {
        return db.Cities
            .AsNoTracking()
            .Include(c => c.Users)
            .Select(UsersGroupdByCity.FromEO);
    }

    public OneOf<UsersGroupdByCity, PropertyNotFoundError> GroupByCity(string city)
    {
        var group = db.Cities
            .AsNoTracking()
            .Include(c => c.Users)
            .Select(UsersGroupdByCity.FromEO)
            .FirstOrDefault(g => g.City == city);

        if (group is null) return new PropertyNotFoundError("City", "name", city);
        return group;
    }


    public OneOf<UserDTO, BadRequestError> Create(NewUserDTO newUser)
    {
        var res = usersFactory.Build(newUser);
        if (res.IsT1) return res.AsT1;

        var user = res.AsT0;
        db.Users.Add(user);
        db.SaveChanges();

        return UserDTO.FromEO(user);
    }
}
