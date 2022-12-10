using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Advent.Extensions;

namespace Advent.Solutions;

public class Day07 : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day07.txt");

        var solution = Solve(lines);
        
        WriteLine($"Answer: {solution}");
    }

    public string Solve(string[] lines)
    {
        var tree = new Collection<Node<FsObject>>();
        Node<FsObject> activeNode = null;

        foreach (var command in lines)
        {
            switch (command)
            {
                case "$ cd /":
                    activeNode = CreateOrGetNode(tree, "/", true, activeNode);
                    break;
                case "ls":
                    break;
                case "$ cd ..":
                    activeNode = activeNode.Parent ?? throw new Exception("Trying cd .. without parent");
                    break;
                case string c when c.StartsWith("cd"):
                    var navigateTo = c.Split(' ').Last();
                    activeNode = tree.First(x => x.Parent == activeNode && x.Value.DirName == navigateTo);
                    break;
                case string c when c.StartsWith("dir"):
                    var dirName = c.Split(' ').Last().Trim();
                    activeNode = CreateOrGetNode(tree, dirName, true, activeNode);
                    break;
                case string c when Regex.IsMatch(c, @"^\d+"):
                    var size = c.Split(' ').First().Trim();
                    var fileName = c.Split(' ').Last().Trim();
                    activeNode = CreateOrGetNode(tree, fileName, false, activeNode, int.Parse(size));
                    break;
                default:
                    throw new Exception("Command not recognized");
            }
        }

        throw new NotImplementedException();
    }

    private Node<FsObject> CreateOrGetNode(Collection<Node<FsObject>> tree, string v, bool isDir, Node<FsObject> parent, int? size = null)
    {
        var node = tree.FirstOrDefault(x => x.Value.DirName == v || x.Value.FileName == v && x.Parent == parent);

        if (node == null && isDir)
        {
            node = new Node<FsObject> { Value = new FsObject { DirName = v, IsDir = true }, Parent = parent };
            tree.Add(node);
        } 
        else if (node == null && !isDir)
        {
            node = new Node<FsObject> { Value = new FsObject { FileName = v, IsDir = false }, Parent = parent };
            tree.Add(node);
        }

        return node ?? throw new Exception("Somehow null node");
    }

    public class Node<T>
    {
        public Node<T>? Parent { get; set; }
        public T Value { get; set; }
    }

    public class FsObject
    {
        public string? FileName { get; set; }
        public string? DirName { get; set; }
        public bool IsDir { get; set; }        
    }
}
