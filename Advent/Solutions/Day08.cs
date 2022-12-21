namespace Advent.Solutions;

public class Day08 : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day08.txt");

        var solution = Solve1(lines);

        WriteLine($"Answer part 1: {solution}");

        var solution2 = Solve2(lines);
        WriteLine($"Answer part 2: {solution2}");
    }

    public string Solve2(string[] lines)
    {
        var width = lines[0].Length;
        var height = lines.Length;

        int maxScenicScore = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int visibleLeft = 0;
                int visibleRight = 0;
                int visibleUp = 0;
                int visibleDown = 0;

                // Look left
                for (int i = x-1; i >= 0; i--)
                {
                    visibleLeft++;
                    if (lines[y][i] >= lines[y][x])
                        break;
                }

                // Look right
                for (int i = x+1; i < width; i++)
                {
                    visibleRight++;
                    if (lines[y][i] >= lines[y][x])
                        break;
                }

                // Look up
                for (int i = y-1; i >= 0; i--)
                {
                    visibleUp++;
                    if (lines[i][x] >= lines[y][x])
                        break;
                }

                // Look down
                for (int i = y+1; i < height; i++)
                {
                    visibleDown++;
                    if (lines[i][x] >= lines[y][x])
                        break;
                }
                
                // Calc score
                var scenicScore = visibleLeft * visibleRight * visibleUp * visibleDown;
                maxScenicScore = Math.Max(maxScenicScore, scenicScore);
            }
        }

        return maxScenicScore.ToString();
    }

    public string Solve1(string[] lines)
    {
        var width = lines[0].Length;
        var height = lines.Length;

        int totalVisible = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Is edge?
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    totalVisible++;                    
                }
                // Is visible from left?
                else if (!lines[y][..x].Any(z => z >= lines[y][x]))
                {
                    totalVisible++;                 
                }               
                // Is visible from right?
                else if (!lines[y][(x+1)..].Any(z => z >= lines[y][x]))
                {
                    totalVisible++;
                }
                // Is visible from up?
                else if (!lines[0..y].Any(z => z[x] >= lines[y][x]))
                {
                    totalVisible++;
                }
                // Is visible from down?
                else if (!lines[(y+1)..].Any(z => z[x] >= lines[y][x]))
                {
                    totalVisible++;
                }
            }
        }

        return totalVisible.ToString();
    }   
}
