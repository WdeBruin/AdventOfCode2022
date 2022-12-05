using System.Collections;
using System.Text.RegularExpressions;
using Advent.Extensions;

namespace Advent.Solutions;

public class Day05a : DayBase
{
    public override void Run()
    {
        var turn = 0;
        
        var lines = ReadInput("Day05.txt").ToList();
        
        // stacks init
        var stacks = new Stack<string>[9];
        for (var i = 0; i < 9; i++)
        {
            stacks[i] = new Stack<string>();    
        }

        for (var i = 7; i >= 0; i--)
        {
            var chunks = lines[i].SplitInParts(4).ToArray();
            for (var j = 0; j < chunks.Count(); j++)
            {
                if(!string.IsNullOrWhiteSpace(chunks[j]))
                    stacks[j].Push(chunks[j]);
            }
        }

        Log(turn, stacks);
        turn++;

        // movements
        for (var i = 10; i < lines.Count; i++)
        {
           var move = Regex.Split(lines[i], @"\D+").Where(s => s != string.Empty).ToArray().ToIntArray();
           if (move.Length != 3) throw new Exception("Move length not 3");
           move[1]--;
           move[2]--;

           for (var j = 0; j < move[0]; j++)
           {
               stacks[move[2]].Push(stacks[move[1]].Pop());
           }
           
           Log(turn, stacks);
           turn++;
        }
    }

    private static void Log(int turn, Stack<string>[] stacks)
    {
        WriteLine($"Turn {turn}");
        stacks.Print();
        WriteLine(string.Empty);
    }
}
