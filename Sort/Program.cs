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
            const int arraySize = 109;
            int[] A = new int [arraySize];
            Random rnd = new Random();
            for (int i = 0; i < arraySize; i++)
            {
                A[i] = rnd.Next(100);
            }

            // A = new int[]{3,7,8,5,2,1,9,5,4};

            PrintArray(A);
            {
                int[] B = new int[A.Length];
                A.CopyTo(B, 0);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                QuickSort(ref B);
                watch.Stop();
                var elapsedMs = watch.ElapsedTicks;
                Console.WriteLine($"QuickSort Elapsed ticks {elapsedMs}");
                PrintArray(B);
            }
            {
                int[] C = new int[A.Length];
                A.CopyTo(C, 0);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                BubbleSort(ref C);
                watch.Stop();
                var elapsedMs = watch.ElapsedTicks;
                Console.WriteLine($"BubbleSort Elapsed ticks {elapsedMs}");
                PrintArray(C);
            }

            {
                int[] D = new int[A.Length];
                A.CopyTo(D, 0);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                MergeSort(ref D);
                watch.Stop();
                var elapsedMs = watch.ElapsedTicks;
                Console.WriteLine($"MergeSort Elapsed ticks {elapsedMs}");
                PrintArray(D);
            }

            {
                int[] E = new int[A.Length];
                A.CopyTo(E, 0);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                HeapSort(ref E);
                watch.Stop();
                var elapsedMs = watch.ElapsedTicks;
                Console.WriteLine($"HeapSort Elapsed ticks {elapsedMs}");
                PrintArray(E);
            }

            {
                int[] F= new int[A.Length];
                A.CopyTo(F, 0);
                List<int> FList = F.ToList();
                var watch = System.Diagnostics.Stopwatch.StartNew();
                FList.Sort();
                watch.Stop();
                var elapsedMs = watch.ElapsedTicks;
                Console.WriteLine($"Built-in sort Elapsed ticks {elapsedMs}");
                PrintArray(FList);
            }
        }

        static void BubbleSort(ref int[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    if (j != i && b[j] > b[i])
                    {
                        int c = b[i];
                        b[i] = b[j];
                        b[j] = c;
                    }
                }
            }
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
                    if (i != j)
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


        static void MergeSort(ref int[] a)
        {
            int[] b = new int[a.Length];
            a.CopyTo(b, 0);
            TopDownSplitMerge(ref b, 0, a.Length, ref a);
        }

        static void TopDownSplitMerge(ref int[] b, int begin, int end, ref int[] a)
        {
            if (end - begin < 2)
                return;

            int middle = (begin + end) / 2;

            TopDownSplitMerge(ref a, begin, middle, ref b);
            TopDownSplitMerge(ref a, middle, end, ref b);

            TopDownMerge(ref b, begin, middle, end, ref a);
        }

        static void TopDownMerge(ref int[] a, int begin, int middle,  int end, ref int[] b)
        {
            int i = begin;
            int j = middle;

            for (int k = begin; k < end; k++)
            {
                if (i < middle && (j >= end || a[i] <= a[j]))
                {
                    b[k] = a[i];
                    i++;
                }
                else
                {
                    b[k] = a[j];
                    j++;
                }
            }
        }

        static void HeapSort(ref int[] b)
        {
            for (int i = b.Length / 2 - 1; i >= 0; i--)
            {
                heapify(ref b, b.Length, i);
            }

            for (int i = b.Length - 1; i >= 0; i--)
            {
                int c = b[i];
                b[i] = b[0];
                b[0] = c;

                heapify(ref b, i, 0);
            }
        }

        static void heapify(ref int[] b, int n, int i)
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;

            if (l < n && b[l] > b[largest])
                largest = l;

            if (r < n && b[r] > b[largest])
                largest = r;

            if (largest != i)
            {
                int c = b[i];
                b[i] = b[largest];
                b[largest] = c;

                heapify(ref b, n, largest);
            }
        }
    }
}
