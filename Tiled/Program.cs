using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiled
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 1;

            Console.WriteLine($"N {n} methods {calculateTilingMethods(n)}");

            n = 2;

            Console.WriteLine($"N {n} methods {calculateTilingMethods(n)}");


            n = 3;

            Console.WriteLine($"N {n} methods {calculateTilingMethods(n)}");

            n = 4;

            Console.WriteLine($"N {n} methods {calculateTilingMethods(n)}");
        }

        static int calculateTilingMethods(int n)
        {
            List<int> fibNums = new List<int>();
            for (int i = 0; i < n + 1; i++)
            {
                if (i == 0 || i == 1)
                {
                    fibNums.Add(1);
                }
                else
                {
                    fibNums.Add(fibNums[i - 1] + fibNums[i - 2]);
                }
            }

            return fibNums[n];
        }
    }
}
