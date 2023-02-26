using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestHelper;

public static class FixtureExtensions
{
    public static Mock<T> FreezeInject<T>(this IFixture fixture)
        where T : class
    {
        var mock = fixture.Freeze<Mock<T>>();
        fixture.Inject(mock.Object);
        return mock;
    }

    public static T InjectInMemoryDbContext<T>(this IFixture fixture) where T : DbContext
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;
        var context = (T)Activator.CreateInstance(typeof(T), options)!;
        fixture.Inject(context);
        context.Database.EnsureCreated();
        return context;
    }
}