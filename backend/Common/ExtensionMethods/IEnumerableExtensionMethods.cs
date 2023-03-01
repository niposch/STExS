namespace Common.ExtensionMethods;

public static class IEnumerableExtensionMethods
{
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        Random rnd = new Random();
        return source.OrderBy<T, int>((item) => rnd.Next());
    }
}