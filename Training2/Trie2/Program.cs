using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TrieNode
{
    public char Value { get; set; }
    public List<TrieNode> Children { get; set; }
    public TrieNode Parent { get; set; }
    public int Depth { get; set; }

    public int ChildrenCount { get; set; }

    public TrieNode(char value, int depth, TrieNode parent)
    {
        Value = value;
        Children = new List<TrieNode>();
        Depth = depth;
        Parent = parent;
        ChildrenCount = 0;
    }

    public bool IsLeaf()
    {
        return Children.Count == 0;
    }

    public TrieNode FindChildNode(char c)
    {
        foreach (var child in Children)
        {
            if (child.Value == c)
                return child;
        }

        return null;
    }

    public void DeleteChildNode(char c)
    {
        for (var i = 0; i < Children.Count; i++)
        {
            if (Children[i].Value == c)
            {
                Children.RemoveAt(i);
                break;
            }
        }
    }
}

public class Trie
{
    readonly TrieNode root;

    public Trie()
    {
        root = new TrieNode('^', 0, null);
    }

    public TrieNode Prefix(string s)
    {
        var currentNode = root;
        var result = currentNode;

        foreach (var c in s)
        {
            currentNode = currentNode.FindChildNode(c);
            if (currentNode == null)
                break;

            result = currentNode;
        }

        return result;
    }

    public bool Search(string s)
    {
        var prefix = Prefix(s);
        return prefix.Depth == s.Length && prefix.FindChildNode('$') != null;
    }

    public void InsertRange(List<string> items)
    {
        for (int i = 0; i < items.Count; i++)
            Insert(items[i]);
    }

    public void Insert(string s)
    {
        var currentNode = root;
        var result = currentNode;

        foreach (var c in s)
        {
            currentNode = currentNode.FindChildNode(c);
            if (currentNode == null)
            {
                result.ChildrenCount--;
                break;
            }

            currentNode.ChildrenCount++;
            result = currentNode;
        }

        //var commonPrefix = Prefix(s);
        var current = result;

        for (var i = current.Depth; i < s.Length; i++)
        {
            var newNode = new TrieNode(s[i], current.Depth + 1, current);
            current.Children.Add(newNode);
            current.ChildrenCount++;
            current = newNode;
        }

        current.Children.Add(new TrieNode('$', current.Depth + 1, current));
        current.ChildrenCount++;
    }

    public int FindChildrenCount(string s)
    {
        var prefix = Prefix(s);
        if (prefix.Depth == s.Length)
            return prefix.ChildrenCount;
        else return 0;
    }

    public void Delete(string s)
    {
        if (Search(s))
        {
            var node = Prefix(s).FindChildNode('$');

            while (node.IsLeaf())
            {
                var parent = node.Parent;
                parent.DeleteChildNode(node.Value);
                parent.ChildrenCount--;
                node = parent;
            }
        }
    }
}

class Solution
{

    /*
     * Complete the contacts function below.
     */
    static int[] contacts(string[][] queries)
    {
        var res = new List<int>();
        var trie = new Trie();
        foreach (var query in queries)
        {
            if (query[0] == "add")
            {
                trie.Insert(query[1]);
            }
            else if (query[0] == "find")
            {
                res.Add(trie.FindChildrenCount(query[1]));
            }
        }

        return res.ToArray();
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int queriesRows = Convert.ToInt32(Console.ReadLine());

        string[][] queries = new string[queriesRows][];

        for (int queriesRowItr = 0; queriesRowItr < queriesRows; queriesRowItr++)
        {
            queries[queriesRowItr] = Console.ReadLine().Split(' ');
        }

        int[] result = contacts(queries);

        textWriter.WriteLine(string.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
