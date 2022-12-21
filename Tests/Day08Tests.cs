using NUnit.Framework;

namespace Advent.Solutions;

public class Day08Tests
{
    [Test]
    public void ExamplePart1()
    {
        var sut = new Day08();
        var input = @"30373
25512
65332
33549
35390";
        var lines = input.Split("\r\n");

        var result = sut.Solve1(lines);
        Assert.That(result, Is.EqualTo("21"));
    }

    [Test]
    public void ExamplePart2()
    {
        var sut = new Day08();
        var input = @"30373
25512
65332
33549
35390";
        var lines = input.Split("\r\n");

        var result = sut.Solve2(lines);
        Assert.That(result, Is.EqualTo("8"));
    }
}
