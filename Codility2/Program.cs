using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Odd(new int[] { 9, 3, 9, 3, 9, 7, 9 }).ToString());
            int[] A = new int[999999];
            for(int i = 0; i < A.Length; i++)
            {
                A[i] = i + 2;
            }
            Console.WriteLine(MissingElement(A).ToString());
            //CyclicRotation(new int[] { }, 5, 6);
            //Console.WriteLine(Program.BinaryGap(328).ToString());
        }

        public static int MissingElement(int[] A)
        {
            if (A == null || A.Length == 0)
                return 1;

            double sum = ( (A.Length + 1.0) * ((A.Length + 1.0) + 1.0) )/ 2.0;

            foreach(int n in A)
            {
                sum -= n;
            }
            return (int)sum;
        }

        //public static int Odd(int []A)
        //{
        //    int res = 0 ;
        //    foreach(int n in A)
        //    {
        //        res = res ^ n;
        //    }
        //    return res;
        //}
        //public static void CyclicRotation(int[] A, int N, int K)
        //{
        //    if (A == null || A.Length == 0)
        //        return A;

        //    int shift = K % N;

        //    for(int i = 0; i < shift; i++)
        //    {
        //        int last = A[A.Length - 1];
        //        for(int j= A.Length-1; j > 0; j--)
        //        {
        //            A[j] = A[j - 1];
        //        }
        //        A[0] = last;
        //    }

        //    foreach(int n in A)
        //    {
        //        Console.Write(n + " ");
        //    }
        //}

        //public static int BinaryGap(int N)
        //{
        //    int max = 0;
        //    string binary = Convert.ToString(N, 2);

        //    int current = 0;
        //    bool started = false;
        //    for (int i = 0; i < binary.Length; i++)
        //    {
        //        if(binary[i] == '1')
        //        {
        //            if(started)
        //            {
        //                started = false;
        //                if (current > max)
        //                {
        //                    max = current;
        //                }

        //                if(i + 1 < binary.Length)
        //                {
        //                    if(binary[i + 1] == '0')
        //                    {
        //                        started = true;
        //                        current = 0;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                started = true;
        //                current = 0;
        //            }
        //        }
        //        else
        //        {
        //            if(started)
        //            {
        //                current++;
        //            }
        //        }
        //    }

        //    return max;
        //}
    }
}
