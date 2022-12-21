using NUnit.Framework;

namespace Advent.Solutions;

public class Day09Tests
{
    [Test]
    public void ExamplePart1()
    {
        var sut = new Day09();
        var input = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";
        var lines = input.Split("\r\n");

        var result = sut.Solve1(lines);
        Assert.That(result, Is.EqualTo("13"));
    }

    [Test]
    public void ExamplePart2()
    {
        
    }
}
