using Microsoft.EntityFrameworkCore;

namespace Taxi.Desktop.Models;

public class TaxiDbContext : DbContext
{
    public DbSet<Person> Drivers { get; set; }
    public DbSet<Car> Cars { get; set; }

    public TaxiDbContext()
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Username=postgres;Password=1234;Database=taxi_db;",
            o => o.MapEnum<CarStatus>("CarStatus"));
    }
}