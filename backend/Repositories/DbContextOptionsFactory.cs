using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class DbContextOptionsFactory<T>
    where T: DbContext
{
    private DbContextOptionsBuilder<T> optionsBuilder;
    
    private bool isDevelopment;
    private string connectionString;
    
    public DbContextOptionsFactory(string connectionString, bool isDevelopment)
    {
        this.optionsBuilder = new DbContextOptionsBuilder<T>();
        this.isDevelopment = isDevelopment;
        this.connectionString = connectionString;
    }

    public DbContextOptions<T> CreateOptions()
    {
        if (isDevelopment)
        {
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite(this.connectionString);
        }
        else
        {
            optionsBuilder.UseSqlServer(this.connectionString);
        }

        return this.optionsBuilder.Options;
    }
}