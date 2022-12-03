namespace Advent.Solutions;

public class Day03a : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day03.txt");

        var doublePacked = new List<char>();
        var prio = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        foreach (var sack in lines)
        {
            var compartments = sack.Chunk(sack.Length / 2);
            if(compartments.Count() != 2) throw new Exception("not 2 compartments");
                        
            foreach (var item in compartments.First())
            {
                if (compartments.Last().Contains(item))
                {
                    doublePacked.Add(item);
                    break;
                }
            }
        }

        var score = 0;
        foreach (var item in doublePacked)
        {
            score += prio.IndexOf(item) + 1;
        }

        WriteLine($"Total score: {score}");
    }
}
