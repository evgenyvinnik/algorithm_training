using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    class Program
    {
        static void PrintArray(IEnumerable<int> a)
        {
            foreach (var n in a)
            {
                Console.Write($"{n,6}");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            const int arraySize = 1009;
            int[] A = new int [arraySize];
            Random rnd = new Random();
            for(int i = 0; i < arraySize; i++)
            {
                A[i] = rnd.Next(10000);
            }

           // A = new int[]{3,7,8,5,2,1,9,5,4};

            var B = A;
            PrintArray(B);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
            watch.Stop();
            var elapsedMs = watch.ElapsedTicks;
            QuickSort(ref B);
            Console.WriteLine($"Elapsed ticks {elapsedMs}");
            PrintArray(B);
        }



        static void QuickSort(ref int[] b)
        {
            quickie(ref b, 0, b.Length - 1);
        }

        static void quickie(ref int[] b, int lo, int hi)
        {
            if (lo < hi)
            {
                int p = partition(ref b, lo, hi);
                quickie(ref b, lo, p -1);
                quickie(ref b, p + 1, hi);
            }
        }

        static int partition(ref int[] b, int lo, int hi)
        {
            int pivot = b[hi];
            int i = lo - 1;
            int c;

            for (int j = lo; j <= hi - 1;j++)
            {
                if (b[j] < pivot)
                {
                    i = i + 1;
                    if (i + 1 != j)
                    {
                        c = b[i];
                        b[i] = b[j];
                        b[j] = c;
                    }
                }
            }

            c = b[i+1];
            b[i + 1] = b[hi];
            b[hi] = c;

            return i + 1;
        }
    }
}
