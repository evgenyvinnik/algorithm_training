using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

namespace SlidingWindowWorking
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<char> alphabet = new HashSet<char> { 'c', 'b', 'a' };
            Console.WriteLine(minSubstring("aaaabbbbbcacccc", alphabet));

            HashSet<char> alphabet2 = new HashSet<char> { 'c', 'b', 'a', 's' };
            Console.WriteLine(minSubstring("acbbsab", alphabet2));
        }

        static string minSubstring(string s, HashSet<char> alphabet)
        {
            Dictionary<char, int> slidingWindow = new Dictionary<char, int>();
            int pointerEnd = 0;
            int pointerBegin = 0;

            while (pointerEnd < s.Length)
            {
                if (alphabet.Contains(s[pointerEnd]))
                {
                    Dictionary<>
                }
            }

            return "";
        }
    }
}
