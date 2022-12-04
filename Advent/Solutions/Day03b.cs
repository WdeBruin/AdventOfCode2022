namespace Advent.Solutions;

public class Day03b : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day03.txt");

        var badges = new List<char>();        
                
        var chunks = lines.Chunk(3);
        foreach(var groupSacks in chunks)
        {
            foreach (var item in groupSacks[0])
            {
                if (groupSacks[1].Contains(item) && groupSacks[2].Contains(item))
                {
                    badges.Add(item);
                    break;
                }
            }
        }

        var score = 0;
        foreach (var item in badges)
        {
            score += (item % 32) + (char.IsUpper(item) ? 26 : 0); // ASCII table numbers, modulo 32 works for getting index at start 1.
        }

        WriteLine($"Total score: {score}");
    }
}
