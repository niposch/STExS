using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace TestHelper;

public static class FixtureExtensions
{
    public static T FreezeInject<T>(this IFixture fixture)
    {
        var mock = fixture.Freeze<T>();
        fixture.Inject(mock);
        return mock;
    }
    
    public static T InjectInMemoryDbContext<T>(this IFixture fixture) where T : DbContext
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = (T)Activator.CreateInstance(typeof(T), options)!;
        fixture.Inject(context);
        context.Database.EnsureCreated();
        return context;
    }
}