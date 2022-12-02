namespace Advent.Solutions;

public class Day02b : DayBase
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
            var targetResult = moves[1];

            string me = "";

            switch(targetResult)
            {
                case "X":
                    me = opp == "A" ? "Z" : opp == "B" ? "X" : "Y";
                break;
                case "Y":
                    me = opp == "A" ? "X" : opp == "B" ? "Y" : "Z";
                    break;
                case "Z":
                    me = opp == "A" ? "Y" : opp == "B" ? "Z" : "X";
                    break;
            }

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
