using Microsoft.EntityFrameworkCore;
using test_things.Entities;

namespace test_things;

public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
{
    public required DbSet<UserEO> Users { get; set; }
    public required DbSet<CityEO> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEO>()
            .HasOne(user => user.City)
            .WithMany(city => city.Users)
            .HasForeignKey(user => user.CityId);
    }
}
