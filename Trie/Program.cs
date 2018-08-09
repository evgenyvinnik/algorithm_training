using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class TrieClass
{
    public char c;
    public bool end;
    public Dictionary<char, TrieClass> children;
    public int words;

    public TrieClass(char c)
    {
        this.c = c;
    }
}

class Solution
{

    static void addTrie(string s, ref TrieClass root)
    {
        char[] chars = s.ToCharArray();

        TrieClass p = root;
        for (int i = 0; i < chars.Length; i++)
        {
            if (p.children == null)
            {
                p.children = new Dictionary<char, TrieClass>();
            }

            if (!p.children.ContainsKey(chars[i]))
            {
                p.children.Add(chars[i], new TrieClass(chars[i]));
            }

            p = p.children[chars[i]];
            p.words++;
        }

        p.end = true;
    }

    static int findTrie(string s, TrieClass root)
    {
        char[] chars = s.ToCharArray();
        TrieClass p = root;

        for (int i = 0; i < chars.Length; i++)
        {
            if (p.children != null)
            {
                if (p.children.ContainsKey(chars[i]))
                {
                    p = p.children[chars[i]];
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        return countWords(p);
    }

    static int countWords(TrieClass root)
    {
        return root.words;
        //int count = 0;

        //if (root.end)
        //{
        //    count++;
        //}

        //if (root.children != null)
        //{
        //    foreach (TrieClass trieChild in root.children.Values)
        //    {
        //        count += countWords(trieChild);
        //    }
        //}

        //return count;
    }

    static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine());

        TrieClass trie = new TrieClass('\0');

        for (int nItr = 0; nItr < n; nItr++)
        {
            string[] opContact = Console.ReadLine().Split(' ');

            string op = opContact[0];

            string contact = opContact[1];

            if (op == "add")
            {
                addTrie(contact, ref trie);
            }
            else if (op == "find")
            {
                Console.WriteLine(findTrie(contact, trie));
            }
        }
    }
}
