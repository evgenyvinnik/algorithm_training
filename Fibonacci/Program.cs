using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            //simple
            int i = 7;
            Console.WriteLine($"Fib {i} == {fib(i)}");

            //memoization
            Console.WriteLine($"Fib2 {i} == {fib2(i)}");
        }

        static int fib(int n)
        {
            if (n <= 1)
                return n;
            return fib(n - 1) + fib(n - 2);
        }


        static int fib2(int n)
        {
            Dictionary<int, int> prevNums = new Dictionary<int, int> { { 1, 1 }, { 2, 1}};

            return fib2Helper(n,ref prevNums);
        }

        static int fib2Helper(int n,ref Dictionary<int, int> prevNums)
        {
            if (prevNums.ContainsKey(n))
                return prevNums[n];

            prevNums[n] = fib2Helper(n - 1, ref prevNums) + fib2Helper(n - 2, ref prevNums);

            return prevNums[n];
        }
    }
}
