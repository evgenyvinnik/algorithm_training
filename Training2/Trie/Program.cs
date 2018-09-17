using System;
using System.Collections.Generic;

namespace Trie
{
    public class TrieNode
    {
        public char Value { get; set; }
        public List<TrieNode> Children { get; set; }
        public TrieNode Parent {get; set; }
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

    class Program
    {
        static void Main(string[] args)
        {
            var trie = new Trie();
            trie.Insert("s");
            trie.Insert("ss");
            trie.Insert("sss");
            trie.Insert("ssss");
            trie.Insert("sssss");
            Console.WriteLine($"{trie.FindChildrenCount("s")}");
            Console.WriteLine($"{trie.FindChildrenCount("ss")}");
            Console.WriteLine($"{trie.FindChildrenCount("sss")}");
            Console.WriteLine($"{trie.FindChildrenCount("ssss")}");
            Console.WriteLine($"{trie.FindChildrenCount("sssss")}");
            Console.WriteLine($"{trie.FindChildrenCount("ssssss")}");
        }
    }
}
