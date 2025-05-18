using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Taxi.Desktop.Models;

public class TaxiDbContextFactory : IDesignTimeDbContextFactory<TaxiDbContext>
{
    public TaxiDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        #if DEBUG
        var connectionString = config.GetConnectionString("TestDb");
        #else
        var connectionString = config.GetConnectionString("ProdDb");
        #endif
        
        return new TaxiDbContext(connectionString!);
    }

    public static TaxiDbContext CreateDbContext() 
        => new TaxiDbContextFactory().CreateDbContext(null);
}