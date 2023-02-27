using System.Text.RegularExpressions;

namespace Application.Helper;

public sealed class ClozeTextHelper : IClozeTextHelper
{
    private const string ClozePattern = @"\[\[[^\[]+\]\]";

    public string StripAnswers(string text)
    {
        return Regex.Replace(text, ClozePattern, "[[]]");
    }

    public int CountClozes(string text)
    {
        return Regex.Matches(text, ClozePattern).Count;
    }
}

public interface IClozeTextHelper
{
    public string StripAnswers(string text);

    public int CountClozes(string text);
}