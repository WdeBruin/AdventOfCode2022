using System.Text.RegularExpressions;
using Advent.Extensions;

namespace Advent.Solutions;

public class Day06 : DayBase
{
    public override void Run()
    {
        var line = ReadInput("Day06.txt")[0];
        var length = 4;

        for (int i = length; i < line.Length; i++)
        {
            var part = line[(i - length)..i];
            if (part.Distinct().Count() != length) continue;
            WriteLine($"Answer: {i}");
            break;
        }
    }
}
