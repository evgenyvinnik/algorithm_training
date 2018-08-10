using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grasshoper
{
    class Program
    {
        static void Main(string[] args)
        {
            int lastStick;
            lastStick = 0;
            Console.WriteLine($"on the stick {lastStick} the grasshoper can jump in {hopsPossible(lastStick)} different ways");
            Console.WriteLine($"on the stick {lastStick} the grasshoper can super jump in {superHopsPossible(lastStick)} different ways");

            lastStick = 1;
            Console.WriteLine($"on the stick {lastStick} the grasshoper can jump in {hopsPossible(lastStick)} different ways");
            Console.WriteLine($"on the stick {lastStick} the grasshoper can super jump in {superHopsPossible(lastStick)} different ways");

            lastStick = 2;
            Console.WriteLine($"on the stick {lastStick} the grasshoper can jump in {hopsPossible(lastStick)} different ways");
            Console.WriteLine($"on the stick {lastStick} the grasshoper can super jump in {superHopsPossible(lastStick)} different ways");

            lastStick = 3;
            Console.WriteLine($"on the stick {lastStick} the grasshoper can jump in {hopsPossible(lastStick)} different ways");
            Console.WriteLine($"on the stick {lastStick} the grasshoper can super jump in {superHopsPossible(lastStick)} different ways");

            lastStick = 4;
            Console.WriteLine($"on the stick {lastStick} the grasshoper can jump in {hopsPossible(lastStick)} different ways");
            Console.WriteLine($"on the stick {lastStick} the grasshoper can super jump in {superHopsPossible(lastStick)} different ways");

            lastStick = 5;
            Console.WriteLine($"on the stick {lastStick} the grasshoper can jump in {hopsPossible(lastStick)} different ways");
            Console.WriteLine($"on the stick {lastStick} the grasshoper can super jump in {superHopsPossible(lastStick)} different ways");
        }

        static int hopsPossible(int lastStick)
        {
            List<int> fibNums = new List<int>();
            for (int i = 0; i < lastStick+1; i++)
            {
                if (i <= 1)
                {
                    fibNums.Add(1);
                }
                else
                {
                    fibNums.Add(fibNums[i - 1] + fibNums[i - 2]);
                }
            }

            return fibNums[lastStick];
        }

        static int superHopsPossible(int lastStick)
        {
            List<int> fibNums = new List<int>();
            for (int i = 0; i < lastStick + 1; i++)
            {
                if (i == 0 || i == 1)
                {
                    fibNums.Add(1);
                }
                else if (i == 2)
                {
                    fibNums.Add(fibNums[i - 1] + fibNums[i - 2]);
                }
                else
                {
                    fibNums.Add(fibNums[i - 1] + fibNums[i - 2] + fibNums[i - 3]);
        }
            }

            return fibNums[lastStick];
        }
    }
}
