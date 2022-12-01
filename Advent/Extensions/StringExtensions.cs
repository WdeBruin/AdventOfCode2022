namespace Advent.Extensions;

public static class StringExtensions
{
    public static int ToInt(this string val)
    {
        return int.Parse(val);
    }
}