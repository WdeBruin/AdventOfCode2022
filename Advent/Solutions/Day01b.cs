using Advent.Extensions;

namespace Advent.Solutions;

public class Day01b : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day01.txt");

        var calories = 0;
        var highestCaloriesElfs = new int[3];
        
        foreach (var l in lines)
        {
            if (string.IsNullOrWhiteSpace(l))
            {
                highestCaloriesElfs.ReplaceLowestIfHigher(calories);
                calories = 0;
                continue;
            }

            calories += l.ToInt();
        }

        var total = highestCaloriesElfs.Sum();
        WriteLine($"Highest calories elf: {total}");
    }
}
