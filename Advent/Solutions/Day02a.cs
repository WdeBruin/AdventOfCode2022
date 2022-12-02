namespace Advent.Solutions;

public class Day02a : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day02.txt");

        var dict = new Dictionary<string, int>
        {
            { "A X", 1+3 },
            { "A Y", 2+6 },
            { "A Z", 3+0 },

            { "B X", 1+0 },
            { "B Y", 2+3 },
            { "B Z", 3+6 },

            { "C X", 1+6 },
            { "C Y", 2+0 },
            { "C Z", 3+3 },
        };
        // De score voor elke situatie is index 1-3 en dan daarbij opgeteld 3,6,0(A)  0,3,6(B) 6,0,3(C)
        
        int totalScore = 0;
        foreach (var round in lines)
        {
            totalScore += dict[round];           
        }

        WriteLine($"Total score: {totalScore}");
    }
}
