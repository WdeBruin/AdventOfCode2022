namespace Advent.Solutions;

public class Day01a : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day01.txt");

        var calories = 0;
        var highestCaloriesElf = 0;
        foreach (var l in lines)
        {
            if (string.IsNullOrWhiteSpace(l))
            {
                highestCaloriesElf = Math.Max(highestCaloriesElf, calories);
                calories = 0;

                continue;
            }

            calories += int.Parse(l);
        }
        
        WriteLine($"Highest calories elf: {highestCaloriesElf}");
    }
}
