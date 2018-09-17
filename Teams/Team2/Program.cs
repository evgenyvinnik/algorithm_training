using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team2
{
    class Program
    {
        static void Main(string[] args)
        {
            //array of numbers
            //second number

            //min and max
            int[] array = new int[] { 1, 2, 3, 4, 5 };
            //int[] array = new int[] { 1, 6, 3, -1, 5 };//14
            int count = 2;

            var tuple = getMinMaxSum(array, (uint)count);
            Console.WriteLine($"{tuple.Item1} {tuple.Item2}");
            //1+2
            //4+5

            //get array
            //sort array
            //get count first elements for min
            //get count last elements for max
            //O(n log(n));
        }

        static Tuple<int, int> getMinMaxSum(int[] array, uint count)
        {
            var list = array.ToList();// space O(N)
            list.Sort(); // operation of O(N log N); -> most expensive operation
            int minSum = 0;
            int maxSum = 0;

            for(int i = 0; i < count; i++)//O(N);
            {
                minSum += list[i];
                maxSum += list[list.Count - 1 - i];
            }
            //int step = Math.Min(count, array.Length);
            /*for(int i = 0; i < count && i < list.Count; i++)
            {
                minSum += list[i];
            }

            for (int i = array.Length-1; i >= 0 && count > 0; i--)
            {
                maxSum += list[i];
                count--;
            }*/

            return new Tuple<int, int>(minSum, maxSum);
        }
    }
}
