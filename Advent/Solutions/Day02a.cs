namespace Advent.Solutions;

public class Day02a : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day02.txt");

        int totalScore = 0;
        foreach (var round in lines)
        {
            int score = 0;
            var moves = round.Split(' ');

            var opp = moves[0];
            var me = moves[1];

            switch(opp)
            {
                case "A":
                    switch(me)
                    {
                        case "X":
                            score += 1 + 3;
                            break;
                        case "Y":
                            score += 2 + 6;
                            break;
                        case "Z":
                            score += 3;
                            break;
                    }
                break;
                case "B":
                    switch(me)
                    {
                        case "X":
                            score += 1;
                            break;
                        case "Y":
                            score += 2 + 3;
                            break;
                        case "Z":
                            score += 3 + 6;
                            break;
                    }
                break;
                case "C":
                    switch(me)
                    {
                        case "X":
                            score += 1 + 6;
                            break; 
                        case "Y":
                            score += 2;
                        break;
                        case "Z":
                            score += 3 + 3;
                        break;
                    }
                break;
            }

            totalScore += score;
        }

        WriteLine($"Total score: {totalScore}");
    }
}
