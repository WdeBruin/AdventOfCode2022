namespace Advent.Solutions;

public class Day04a : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day04.txt");

        int count = 0;
        foreach(var pair in lines)
        {
            var one = pair.Split(',')[0];
            var two = pair.Split(',')[1];

            var oneN = one.Split('-').Select(int.Parse)?.ToArray();
            var twoN = two.Split("-").Select(int.Parse)?.ToArray();

            if ((oneN[0] <= twoN[0] && oneN[1] >= twoN[1]) || (twoN[0] <= oneN[0] && twoN[1] >= oneN[1]))
                count++;
        }                

        WriteLine($"Total: {count}");
    }
}
