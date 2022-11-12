using AutoFixture;

namespace TestHelper;

public static class FixtureExtensions
{
    public static T FreezeInject<T>(this IFixture fixture)
    {
        var mock = fixture.Freeze<T>();
        fixture.Inject(mock);
        return mock;
    }
}