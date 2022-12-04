namespace Advent.Solutions;

public class Day04b : DayBase
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

            for (int i = oneN[0]; i <= oneN[1]; i++)
            {
                if (twoN[0] <= i && twoN[1] >= i)
                {
                    count++;
                    break;
                }
            }
        }                

        WriteLine($"Total: {count}");
    }
}
