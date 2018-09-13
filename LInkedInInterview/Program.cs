using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LInkedInInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayTest = new [] {-1, 1, 2, -2, -2, 1, 3, -2, 3, -1, -9, 6};
            Console.WriteLine($"max_sum {largestContiguousSubsequence(arrayTest)}");
        }

        static int largestContiguousSubsequence(int[] array)
        {
            var modified_array = new List<int>();
            modified_array.Add(array[0]);

            for (int i = 1; i < array.Length; i++)
            {
                if (Math.Sign(modified_array[modified_array.Count - 1]) == Math.Sign(array[i]))
                {
                    modified_array[modified_array.Count - 1] += array[i];
                }
                else
                {
                    modified_array.Add(array[i]);
                }
            }

            int start = 0;
            if (modified_array[start] < 0)
            {
                start = 1;
            }

            int max_sum = modified_array[start];
            int local_sum = modified_array[start];

            for (int i = start; i < modified_array.Count;)
            {
                if (i + 1 >= modified_array.Count)
                {
                    break;
                }

                if (i + 2 >= modified_array.Count)
                {
                    break;
                }

                if (modified_array[i + 1] + modified_array[i + 2] > 0)
                {
                    local_sum += modified_array[i + 1] + modified_array[i + 2];
                    i += 2;
                }
                else
                {
                    i += 2;
                    local_sum = modified_array[i];
                }

                if (local_sum > max_sum)
                    max_sum = local_sum;
            }

            return max_sum;
        }
    }
}
