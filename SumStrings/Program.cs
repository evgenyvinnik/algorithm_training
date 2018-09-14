using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace SumStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "999";
            string b = "999";

            Console.WriteLine($"sum of {a} and {b} is {stringSum(a, b)}");
        }

        static string stringSum(string a, string b)
        {
            string res = String.Empty;

            int carryOver = 0;

            int aEnd = a.Length - 1;
            int bEnd = b.Length - 1;

            while (aEnd >= 0 || bEnd >= 0 || carryOver > 0)
            {
                if (aEnd >= 0)
                {
                    carryOver += a[aEnd] - '0';
                }

                if (bEnd >= 0)
                {
                    carryOver += b[bEnd] - '0';
                }

                char c = (char) (carryOver % 10 + '0');

                carryOver /= 10;

                res = c + res;

                aEnd--;
                bEnd--;
            }

            return res;
        }
    }
}
