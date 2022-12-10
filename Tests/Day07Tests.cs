using NUnit.Framework;

namespace Advent.Solutions;

public class Day07Tests
{
    [Test]
    public void Example()
    {
        var sut = new Day07();
        var input = @"$ cd /
                    $ ls
                    dir a
                    14848514 b.txt
                    8504156 c.dat
                    dir d
                    $ cd a
                    $ ls
                    dir e
                    29116 f
                    2557 g
                    62596 h.lst
                    $ cd e
                    $ ls
                    584 i
                    $ cd ..
                    $ cd ..
                    $ cd d
                    $ ls
                    4060174 j
                    8033020 d.log
                    5626152 d.ext
                    7214296 k";
        var lines = input.Split("\r\n");

        var result = sut.Solve(lines);
        Assert.AreEqual("95437", result);
    }
}
