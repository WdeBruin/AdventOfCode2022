using System.Text.RegularExpressions;
using Advent.Extensions;

namespace Advent.Solutions;

public class Day06 : DayBase
{
    public override void Run()
    {
        var line = ReadInput("Day06.txt")[0];

        var unique = 0;
        var lastChars = new List<char>();
        for (int i = 0; i < line.Length; i++)
        {
            if(lastChars.Count == 14)
                lastChars.RemoveAt(0);
            lastChars.Add(line[i]);

            if (lastChars.Distinct().Count() == 14) // only difference for b and a is 14 instead of 4
            {
                WriteLine($"Answer: {i + 1}");
                break;
            }
        }
    }
}
