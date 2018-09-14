using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horses
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "nenigehnieighgh";
            horsesCounter(input);

            string input2 = "neigneighh";
            horsesCounter(input2);

            string input3 = "neighneigh";
            horsesCounter(input3);
        }

        static void horsesCounter(string input)
        {
            int max = 0;
            Dictionary<char, int> soundCounters =
                new Dictionary<char, int> {{'n', 0}, {'e', 0}, {'i', 0}, {'g', 0}, {'h', 0}};
            HashSet<char> allowedSet = new HashSet<char> {'n', 'e', 'i', 'g', 'h'};
            bool overlap = false;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (allowedSet.Contains(c))
                {
                    soundCounters[c]++;
                    int res = checkSounds(ref soundCounters, c, ref max, ref overlap);
                    if (res == -1)
                    {
                        Console.WriteLine("Invalid");
                        return;
                    }

                    if (res == 0)
                    {
                        soundCounters = new Dictionary<char, int> { { 'n', 0 }, { 'e', 0 }, { 'i', 0 }, { 'g', 0 }, { 'h', 0 } };
                    }
                }
                else
                {
                    Console.WriteLine("Invalid");
                    return;
                }
            }

            Console.WriteLine($"{max}");
        }

        static int checkSounds(ref Dictionary<char, int> soundCounters, char c, ref int max, ref bool overlap)
        {
            bool allEqual = true;
            bool over = false;

            int localMax = 0;

            if (overlap)
              localMax = max;

            //int currentMax = soundCounters['n'];
            int currentBottom = max;
            int prevCounter = soundCounters['n'];

            foreach (var entry in soundCounters)
            {
                if (entry.Key == 'h' && c=='h')
                {
                    over = true;
                    currentBottom = entry.Value;
                }
                if (entry.Value == prevCounter)
                {

                }
                else if (entry.Value > prevCounter)
                {
                    return -1;
                }
                else
                {
                    allEqual = false;
                    //break;
                }
            }

            if (over)
            {
                //if (currentBottom > max)
                //{
                    if (allEqual)
                    {
                        localMax += currentBottom;
                        soundCounters['n'] -= currentBottom;
                        soundCounters['e'] -= currentBottom;
                        soundCounters['i'] -= currentBottom;
                        soundCounters['g'] -= currentBottom;
                        soundCounters['h'] -= currentBottom;
                        overlap = false;
                    }
                    else
                    {
                        localMax = currentBottom;

                        soundCounters['n'] -= currentBottom;
                        soundCounters['e'] -= currentBottom;
                        soundCounters['i'] -= currentBottom;
                        soundCounters['g'] -= currentBottom;
                        soundCounters['h'] -= currentBottom;

                        overlap = true;
                    }
                //}
                if (localMax > max)
                {
                    max = localMax;
                }
            }

            return 1;
        }
    }
}
