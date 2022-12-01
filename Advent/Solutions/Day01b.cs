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
                var lowestTopElf = highestCaloriesElfs.Min();
                if (calories > lowestTopElf)
                {
                    var idx = Array.IndexOf(highestCaloriesElfs, lowestTopElf);
                    highestCaloriesElfs[idx] = calories;
                }
                calories = 0;
                continue;
            }

            calories += int.Parse(l);
        }

        var total = highestCaloriesElfs.Sum();
        WriteLine($"Highest calories elf: {total}");
    }
}
