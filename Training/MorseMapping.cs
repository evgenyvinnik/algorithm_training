using System;
using System.Collections.Generic;
using System.Text;

namespace Training
{
    class MorseMapping
    {
        public int UniqueMorseRepresentations(string[] words)
        {
            string[] morseAbc = {".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.."};

            HashSet<string> morseSet = new HashSet<string>();

            foreach (string word in words)
            {
                string morseWord = string.Empty;

                foreach (char letter in word)
                {
                    morseWord += morseAbc[letter - 'a'];
                }

                morseSet.Add(morseWord);
            }

            return morseSet.Count;
        }
    }
}
