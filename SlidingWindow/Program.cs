using System;
using System.Collections.Generic;

/*
Write a function that takes an input string and an alphabet and returns the minimum continuous substring which contains every letter of the alphabet at least once.

Example 1:

Input: “acbbsab"
acbbsab
acbbs ab
a cbbsa b

Alphabet: {'a', 'b', ‘c’, ’s'}
Output: "acbbs" or “cbbsa”

Example 2: 

Input: “aaaabbbbbcacccc" -> abcac -> check the result letter->apperance
aaaabbbbbc {a: 4, b:5, c:1} start = 0 end = 10
abbbbbc {a: 1, b:5, c:1} start = 3 end = 10
abbbbbca {a: 2, b:5, c:1} start = 3 end = 11
bca


Alphabet: {a, b, c}
Output: "bca"
*/

class Solution
{
    static void Main(string[] args)
    {

        //Input: “aabcacc"
        //Alphabet: {a, b, c}
        //Output: "bca"

        //first pass
        //a
        //a
        //b
        //c
        //a
        //c -> too short (consider right neighbors)
        //c -> too short

        //second pass
        //aa
        //ab
        //bc
        //ca
        //ac

        //third pass
        //aab
        //abc -> found the substring, end search, the shorted possible
        //bca
        //cac
        //acc

        //n*n

        // ways to make it faster
        // 1. Obvious ways: stop if char not in the alphabet
        // 2. stop early

        // dynamic programming
        // 1. check substring only if new letter appears
        // as we go creating dictionary of letters that have appeared
        // remove letters that have appeared
        //-> this way we aren't recalculating whether substring contains the letter

        // look at original string
        /*HashSet<char> alphabet = new HashSet<char>();
        alphabet.Add('c');
        alphabet.Add('b');
        alphabet.Add('a');
        Console.WriteLine(minSubstring("aaaabbbbbcacccc", alphabet ));*/

        HashSet<char> alphabet2 = new HashSet<char> {'c', 'b', 'a', 's'};
        Console.WriteLine(minSubstring("acbbsab", alphabet2));
    }

    static string minSubstring(string s, HashSet<char> alphabet)
    {
        // check s isn't empty
        // check s contains more letters than alphabet
        Console.WriteLine($" s.Length{ s.Length}, alphabet.Count {alphabet.Count}");

        // currently it is a depth first
        // to make it a breadth first we need to add a stack of searches

        string foundString = null;

        for (int i = 0; i < s.Length - alphabet.Count; i++)
        {
            HashSet<char> alphabetFound = alphabet;

            int res = minSubstringHelper(s, i, alphabet, alphabetFound);

            Console.WriteLine($"res {res}");

            if (res != -1)
            {
                string sFound = s.Substring(i, res - i + 1);

                if (foundString != null && sFound.Length < foundString.Length)
                    foundString = sFound;
                else
                    foundString = sFound;
            }
        }

        return foundString;
    }

    static int minSubstringHelper(string s, int position, HashSet<char> alphabet, HashSet<char> alphabetFound)
    {
        Console.WriteLine($"s {s}, position {position}");
        if (position > s.Length - alphabet.Count)
            return -1;

        if (alphabet.Contains(s[position]))
        {
            alphabetFound.Remove(s[position]);
            Console.WriteLine($"s[position] {s[position]}, alphabetFound.Count {alphabetFound.Count} position {position}");

            if (alphabetFound.Count == 0)
            {
                return position;
            }

            return minSubstringHelper(s, position + 1, alphabet, alphabetFound);
        }

        return -1;
    }
}

