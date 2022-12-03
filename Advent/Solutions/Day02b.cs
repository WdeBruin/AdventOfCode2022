using Advent.Extensions;

namespace Advent.Solutions;

public class Day02b : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day02.txt");

        // De score voor elke situatie is index 1-3 en dan daarbij opgeteld 3,6,0(A)  0,3,6(B) 6,0,3(C) 
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
       
        // x lose, y draw, z win
        // To win, need index +1 or if at 3 % 3 to get to 0
        // To draw, need same index
        // To lose, need index -1 or if at -1 % 3 to get to 2
        int totalScore = 0;
        foreach (var tip in lines)
        {
            (string opp, Result res) = tip.Split(' ') switch { var a => (a[0], a[1] == "X" ? Result.Lose : a[1] == "Y" ? Result.Draw : Result.Win) };
            var oppIdx = "ABC".IndexOf(opp);
            var myIdx = res == Result.Lose ? oppIdx - 1 : res == Result.Draw ? oppIdx : oppIdx + 1;
            myIdx = (myIdx < 0 || myIdx > 2) ? (myIdx.Mod(3)) : myIdx; // In C#, % is actually remainder, not modulo as expected! Got extension method.
                                                                       // (-1 % 3 = -1, -1 mod 3 = 2)
            string round = $"{opp} {"XYZ"[myIdx]}";
            totalScore += dict[round];
        }

        WriteLine($"Total score: {totalScore}");
    }

    public enum Result
    {
        Lose,
        Draw,
        Win
    }
}
