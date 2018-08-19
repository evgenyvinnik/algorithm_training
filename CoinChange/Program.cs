using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinChange
{
    class Program
    {
        static void Main(string[] args)
        {
            //int value = 4;

            //List<int> coins = new List<int> {1, 2, 3};

            int value = 166;

            List<int> coins = new List<int> { 5, 37, 8, 39, 33, 17, 22, 32, 13, 7, 10, 35, 40, 2, 43, 49, 46, 19, 41, 1, 12, 11, 28 };


            List<List<int>> solutions = possibilities(value, coins);

            foreach (var solution in solutions)
            {
                solution.Sort();
            }

            List<List<int>> finalsolutions = new List<List<int>>();
            HashSet<string> solutionSet = new HashSet<string>();

            foreach (var solution in solutions)
            {
                var s = string.Join(" ", solution);
                if (!solutionSet.Contains(s))
                {
                    solutionSet.Add(s);
                    finalsolutions.Add(solution);
                }
            }

            /*foreach (var solution in finalsolutions)
            {
                Console.WriteLine($"{string.Join(" ", solution)}");
            }*/
            Console.WriteLine($"possibilities {finalsolutions.Count}");
        }

        static List<List<int>> possibilities(int value, List<int> coins)
        {
            if (value == 0)
            {
                return new List<List<int>>();
            }

            var possible = new List<List<int>>();

            foreach (var coin in coins)
            {
                int diff = value - coin;

                if (diff > 0)
                {
                    var possibility = possibilities(diff, coins);

                    if (possibility.Count != 0)
                    {
                        foreach (var poss in possibility)
                        {
                            poss.Add(coin);
                            possible.Add(poss);
                        }
                    }

                    //possible += possibility;
                }
                else if (diff == 0)
                {
                    List<int> res = new List<int>();
                    res.Add(coin);
                    possible.Add(res);
                }
            }

            return possible;

            /*if (value == 0)
            {
                return 1;
            }

            int possible = 0;

            foreach (var coin in coins)
            {
                int diff = value - coin;

                if (diff >= 0)
                {
                    var possibility = possibilities(diff, coins);

                    possible += possibility;
                }
            }

            return possible;*/
        }
    }
}
