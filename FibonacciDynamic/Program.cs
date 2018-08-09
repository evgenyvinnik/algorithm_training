using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciDynamic
{
    class Program
    {
        static void Main(string[] args)
        {
            //forward calc
            int i = 7;
            Console.WriteLine($"Fib {i} == {fib(i)}");
            i = 1;
            Console.WriteLine($"Fib {i} == {fib(i)}");

            i = 2;
            Console.WriteLine($"Fib {i} == {fib(i)}");
        }

        static int fib(int num)
        {
            List<int> fibNums = new List<int>();
            for (int i = 0; i < num; i++)
            {
                if (i <= 1)
                {
                    fibNums.Add(1);
                }
                else
                {
                    fibNums.Add(fibNums[i-1] + fibNums[i-2]);
                }
            }

            return fibNums[num - 1];
        }

    }
}
