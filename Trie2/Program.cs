using System;
using System.Collections.Generic;

/**
Design a data structure

void AddWord(String word)
bool SearchWord(String word)

. : any one letter

add
adb
adc

ad. -> return true

a-z
 */

class Trie
{
    public char root;
    public Dictionary<char, Trie> children;// HashSet?

    public Trie(char root)
    {
        this.root = root;
    }
}

class SearchDictionary
{

    Trie searchDictionary;

    public SearchDictionary()
    {
        searchDictionary = new Trie('\0');
    }

    public void AddWord(string word)
    {
        char[] chars = word.ToCharArray();

        Trie p = searchDictionary;

        for (int i = 0; i < chars.Length; i++) // N elements -> length of the word
        {
            if (p.children == null)
            {
                p.children = new Dictionary<char, Trie>();
            }

            if (!p.children.ContainsKey(chars[i]))// Dictionary search operation is of constant time
            {
                p.children.Add(chars[i], new Trie(chars[i]));// Addition into Dictionary is of constant time
            }

            p = p.children[chars[i]];
        }

        // make complexity of O(N)
    }

    public bool SearchWord(string word)
    {
        return SearchWordHelper(word, searchDictionary);
    }

    private bool SearchWordHelper(string word, Trie dictionary)
    {
        if (String.IsNullOrEmpty(word))
        {
            return true;
        }

        char[] chars = word.ToCharArray();

        Trie p = dictionary;

        for (int i = 0; i < chars.Length; i++) // N -> length of the word
        {
            //Console.WriteLine($"char {chars[i]}");
            if (p.children != null)
            {
                if (p.children.ContainsKey(chars[i])) // checking is of constant time
                {
                    //Console.WriteLine($"found a child");
                    p = p.children[chars[i]];

                    if (i == chars.Length - 1)
                    {
                        return true;
                    }
                }
                else if (chars[i] == '.')
                {
                    // found '.'
                    // take every child
                    // branch out SearchWordHelper with child
                    // and the remainder of the word
                    // get one with true
                    // if one exist
                    if (p.children != null)
                    {
                        bool found = false;
                        //Console.WriteLine($"word {word.Substring(i+1, word.Length-i-1)}");
                        foreach (Trie child in p.children.Values)
                        {

                            //Console.WriteLine($"child root {child.root}");

                            found = SearchWordHelper(word.Substring(i + 1, word.Length - i - 1), child);//splitting execution by the number of children (M)
                            if (found)
                            {
                                return true;
                            }
                        }

                        return found;
                    }
                }
                else
                {
                    //Console.WriteLine($"return false everything fails");
                    return false;
                }
            }
        }

        //worst case complexity is of O(N*M);

        return false;
    }
}


class Solution
{
    static void Main(string[] args)
    {
        SearchDictionary dictionary = new SearchDictionary();

        // populate

        dictionary.AddWord("add");
        dictionary.AddWord("adb");
        dictionary.AddWord("adcc");


        // test search
        Console.WriteLine($"{dictionary.SearchWord("ad.")}");// expecting true
        Console.WriteLine($"{dictionary.SearchWord("ad.c")}");// expecting true
        Console.WriteLine($"{dictionary.SearchWord("ad.d")}");// expecting false
        Console.WriteLine($"{dictionary.SearchWord("adb")}");// expecting true
        Console.WriteLine($"{dictionary.SearchWord("adg")}");// expecting false
        Console.WriteLine($"{dictionary.SearchWord("ac.")}");//expecting false        
        Console.WriteLine($"{dictionary.SearchWord("ad..")}");// expecting true
        Console.WriteLine($"{dictionary.SearchWord(".dc.")}");// expecting true
    }
}

