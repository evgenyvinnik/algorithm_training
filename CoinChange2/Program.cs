using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinChange2
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = 3;
            long[] c = new long[] { 3, 2, 1 };
            Console.WriteLine($"you can give change to {n} in {getWays(n, c)} ways");
        }

        static long getWays(long n, long[] c)
        {
            List<long> ways = new List<long>();

            HashSet<long> coins = new HashSet<long>(c);
            Array.Sort(c);
            long max = c.Max();
            long min = c.Min();

            for(int i = 0; i < min; i++)
            {
                ways.Add(1);
            }

            ways.Add(1);

            for (long i = min + 1; i <= n; i++)
            {
                long r = max;

                if (i < r)
                {
                    r = i;
                }

                long val = 0;
                for (long j = 1; j <= r; j++)
                {
                    if (coins.Contains(j))
                    {
                        val += ways[(int)(i - j)];
                    }
                }

                ways.Add(val);
            }

            return ways[(int)n];
        }
    }
}
