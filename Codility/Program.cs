using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(solution(4, new []{1,2,3,4,1,1,3}).ToString());
        }

        static int solution(int K, int[] A)
        {
            int res = 0;

            List<int> a = A.ToList();
            bool change = true;
            while (change)
            {
                change = false;
                for (int j = 1; j < a.Count - 1; j++)
                {
                    if (a[j] < K)
                    {
                        int left = int.MinValue;
                        if (a[j - 1] < K && a[j - 1] != 0)
                        {
                            left = a[j] + a[j - 1];
                        }

                        int right = int.MinValue;
                        if (a[j + 1] < K && a[j + 1] != 0)
                        {
                            right = a[j] + a[j + 1];
                        }

                        if (left > a[j] || right > a[j])
                        {
                            if (left > right)
                            {
                                a[j] = left;
                                a[j - 1] = 0;
                            }
                            else
                            {
                                a[j] = right;
                                a[j + 1] = 0;
                            }

                            change = true;
                        }
                    }
                }

                for (int j = a.Count-1; j > 0; j--)
                {
                    if (a[j] == 0)
                    {
                        a.RemoveAt(j);
                    }
                }
            }

            foreach (var val in a)
            {
                if (val >= K)
                    res++;
            }

            return res;
        }
    }
}
