using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Advent.Extensions;
using static Advent.Solutions.Day07;

namespace Advent.Solutions;

public class Day07 : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day07.txt");

        var solution = Solve1(lines);

        WriteLine($"Answer: {solution}");

        var solution2 = Solve2(lines);
        WriteLine($"Answer part 2: {solution2}");
    }

    public string Solve2(string[] lines)
    {
        Node<FsObject> activeNode = GenerateStructureWithDirSizes(lines);

        var totalSpace = 70000000;
        var totalUsed = activeNode.Value.DirSize;
        var available = totalSpace - totalUsed;
        var requiredForUpdate = 30000000;
        var toFree = requiredForUpdate - available;

        RecursiveFindSmallestToFree(activeNode, toFree.Value);
        
        return smallest.ToString();
    }

    public string Solve1(string[] lines)
    {
        Node<FsObject> activeNode = GenerateStructureWithDirSizes(lines);

        // Add up dirs with size <= 100.000
        Func<Node<FsObject>, int> nodeSum = null;
        nodeSum = node =>
        {
            int result = (int)((node.Value.DirSize != null && node.Value.DirSize <= 100000) ? node.Value.DirSize : 0);
            if (node.Children.Any())
            {
                result += node.Children.Where(x => x.Value.DirSize != null).Sum(nodeSum);
            }
            return result;
        };

        var total = nodeSum(activeNode);

        return total.ToString();
    }

    private static Node<FsObject> GenerateStructureWithDirSizes(string[] lines)
    {
        var tree = new Collection<Node<FsObject>>();
        Node<FsObject> activeNode = new Node<FsObject> { Parent = null, Value = new FsObject { DirName = "/", IsDir = true } };

        // Build structure
        foreach (var command in lines)
        {
            switch (command.Trim())
            {
                case "$ cd /":
                    break;
                case "$ ls":
                    break;
                case "$ cd ..":
                    activeNode = activeNode.Parent ?? throw new Exception("Trying cd .. without parent");
                    break;
                case string c when c.StartsWith("$ cd"):
                    var navigateTo = c.Split(' ').Last();
                    var searchChild = activeNode.Children.FirstOrDefault(x => x.Value.IsDir == true && x.Value.DirName == navigateTo);
                    activeNode = searchChild ?? throw new Exception("cd to non existing dir");
                    break;
                case string c when c.StartsWith("dir"):
                    var dirName = c.Split(' ').Last().Trim();

                    var searchDir = activeNode.Children.FirstOrDefault(x => x.Value.IsDir == true && x.Value.DirName == dirName);
                    if (searchDir == null)
                    {
                        var toAdd = new Node<FsObject> { Parent = activeNode, Value = new FsObject { DirName = dirName, IsDir = true } };
                        activeNode.Children.Add(toAdd);
                    }
                    break;
                case string c when Regex.IsMatch(c, @"^\d+"):
                    var size = int.Parse(c.Split(' ').First().Trim());
                    var fileName = c.Split(' ').Last().Trim();

                    var searchFile = activeNode.Children.FirstOrDefault(x => x.Value.IsDir == false && x.Value.FileName == fileName && x.Value.FileSize == size);
                    if (searchFile == null)
                    {
                        var toAdd = new Node<FsObject> { Parent = activeNode, Value = new FsObject { FileName = fileName, IsDir = false, FileSize = size } };
                        activeNode.Children.Add(toAdd);
                    }
                    break;
                default:
                    throw new Exception("Command not recognized");
            }
        }

        // Traverse back to root
        while (activeNode.Parent != null)
            activeNode = activeNode.Parent;

        // Traverse down and determine dirsize for every dir.        
        RecursiveCalculateDirSize(activeNode);
        return activeNode;
    }

    public static void RecursiveCalculateDirSize(Node<FsObject> node)
    {
        if (node.Value.IsDir)
        {
            node.Value.DirSize += node.Children.Where(x => !x.Value.IsDir).Sum(y => y.Value.FileSize);

            foreach (var childDir in node.Children.Where(x => x.Value.IsDir))
            {
                RecursiveCalculateDirSize(childDir);
            }

            node.Value.DirSize += node.Children.Where(x => x.Value.IsDir).Sum(y => y.Value.DirSize);
        }
    }

    public static int smallest = 30000000;
    public static void RecursiveFindSmallestToFree(Node<FsObject> node, int toFree)
    {
        if (node.Value.IsDir)
        {
            if (node.Value.DirSize >= toFree && node.Value.DirSize < smallest)
                smallest = node.Value.DirSize.Value;
                        
            foreach (var childDir in node.Children.Where(x => x.Value.IsDir))
            {
                RecursiveFindSmallestToFree(childDir, toFree);
            }            
        }
    }

    public class Node<T>
    {
        public Node<T>? Parent { get; set; }
        public Collection<Node<T>> Children { get; set; } = new Collection<Node<T>>();
        public T Value { get; set; }
    }

    public class FsObject
    {
        public string? FileName { get; set; }
        public int? FileSize { get; set; }
        public string? DirName { get; set; }
        public bool IsDir { get; set; }

        public int? DirSize { get; set; } = 0;
    }
}
