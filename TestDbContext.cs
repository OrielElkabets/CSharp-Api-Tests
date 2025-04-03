using Microsoft.EntityFrameworkCore;
using test_things.Entities;

namespace test_things;

public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
{
    public required DbSet<UserEO> Users { get; set; }
    public required DbSet<CityEO> Cities { get; set; }
    public required DbSet<PetEO> Pets { get; set; }
    public required DbSet<PetTypeEO> PetsTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEO>(user =>
        {
            user.HasOne(user => user.City)
            .WithMany(city => city.Users)
            .HasForeignKey(user => user.CityId);

            user.HasOne(u => u.Pet)
            .WithMany(p => p.Users)
            .HasForeignKey(u => u.PetId);
        });

        modelBuilder.Entity<PetEO>()
            .HasOne(p => p.Type)
            .WithMany(pt => pt.Pets)
            .HasForeignKey(p => p.TypeId);
    }
}
