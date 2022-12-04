namespace Advent.Extensions;

public static class StringExtensions
{
    public static int ToInt(this string val)
    {
        return int.Parse(val);
    }

    public static int[] ToIntArray(this string val, char splitBy)
    {
        return val.Split(splitBy).ToIntArray();
    }
}