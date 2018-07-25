using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uber2
{
    public class Interval : IEquatable<Interval>
    {
        public Interval(int begin, int end)
        {
            Begin = begin;
            End = end;
        }

        public int Begin { get; private set; }

        public int End { get; private set; }

        public bool Equals(Interval x, Interval y)
        {
            if (x.End == y.End && x.Begin == y.Begin)
                return true;
            return false;
        }

        public bool Equals(Interval other)
        {
            if (this.End == other.End && this.Begin == other.Begin)
                return true;
            return false;
        }

        public int GetHashCode(Interval obj)
        {
            return Begin ^ End;
        }
    }
    class Program
    {
        //Given an array arr[0, n-1], find the index of the element with the minimum value between the two given,valid indices in that array.

        // (ex)
        // arr[] = {-2, 7, -4, 5, 8, 2, 10}
        // index ->  0, 1,  2, 3, 4, 5, 6

        // int Query(2, 4) returns 2
        // int Query(4, 6) returns 5

        // You are allowed to pre-process the array and a build a data structure of your choice. The overall goal is to make Query() function, as fast as possible.

        // Pre-processing time - f(n)
        // Query time - g(n)

        static Dictionary<Interval, int> queryDb = new Dictionary<Interval, int>();

        static void Main(string[] args)
        {
            int[] arr = new[] { -2, 7, -4, 5, 8, 2, 10 };
            PreprocessQueries(arr, ref queryDb);

            Console.WriteLine("Query(2, 4) " + Query(2, 4));
            Console.WriteLine("Query(4, 6) " + Query(4, 6));
        }

        static void PreprocessQueries(int[] arr, ref Dictionary<Interval, int> queryDb)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                for(int j = i; j < arr.Length; j++)
                {
                    queryDb[new Interval(i, j)] = MinIndexCalculate(arr, i, j);
                }
            }
        }

        static int MinIndexCalculate(int[]arr, int begin, int end)
        {
            int minIndex = begin;
            int minValue = arr[begin];

            for(int i = begin + 1; i < end; i++)
            {
                if(arr[i] < minValue)
                {
                    minIndex = i;
                    minValue = arr[i];
                }
            }

            return minIndex;
        }

        static int Query(int begin, int end)
        {
            return MinIndexSearch(begin, end, queryDb);
        }

        static int MinIndexSearch(int begin, int end, Dictionary<Interval, int> queryDb)
        {
            return queryDb[new Interval(begin, end)];
        }
    }
}
