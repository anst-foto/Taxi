using Microsoft.EntityFrameworkCore;

namespace Taxi.Desktop.Models;

public class TaxiDbContext : DbContext
{
    private readonly string _connectionString;
    public DbSet<Person> Drivers { get; set; }
    public DbSet<Car> Cars { get; set; }

    public TaxiDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString,
            o => o.MapEnum<CarStatus>("CarStatus"));
    }
}