using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class DbContextOptionsFactory<T>
    where T : DbContext
{
    private readonly string connectionString;

    private readonly bool isDevelopment;
    private readonly DbContextOptionsBuilder<T> optionsBuilder;

    public DbContextOptionsFactory(string connectionString,
        bool isDevelopment)
    {
        optionsBuilder = new DbContextOptionsBuilder<T>();
        this.isDevelopment = isDevelopment;
        this.connectionString = connectionString;
    }

    public DbContextOptions<T> CreateOptions()
    {
        if (isDevelopment)
        {
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
        }

        optionsBuilder.UseSqlServer(connectionString);

        return optionsBuilder.Options;
    }
}