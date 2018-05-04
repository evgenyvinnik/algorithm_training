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
            //Console.WriteLine(TapeEquilibrium(new int[] { -1000,1000, 3 }).ToString());
            //Console.WriteLine(TapeEquilibrium(new int[] { 3, 1, 2, 4, 3 }).ToString());
            //Console.WriteLine(FrogRiverOne(5, new int[] { 1, 3, 1, 4, 2, 3,  4 , 5}).ToString());
            Console.WriteLine(MissingInteger(new int[] { 1, 3, 6, 4, 1, 2 }).ToString());
            Console.WriteLine(MissingInteger(new int[] { -1, -3 }).ToString());
            Console.WriteLine(MissingInteger(new int[] { 1, 2, 3 }).ToString());
            Console.WriteLine(MissingInteger(new int[] { -1, 0, 1, 2, 3 }).ToString());
            Console.WriteLine(MissingInteger(new int[] { 0 }).ToString());
            Console.WriteLine(MissingInteger(new int[] { 1 }).ToString());
            Console.WriteLine(MissingInteger(new int[] { 1000001 }).ToString());
            //Console.WriteLine(PermCheck(new int[] { 1, 2 }).ToString());
            //Console.WriteLine(FrogJmp(10, 85, 30).ToString());
            //Console.WriteLine(Odd(new int[] { 9, 3, 9, 3, 9, 7, 9 }).ToString());
            //int[] A = new int[999999];
            //for(int i = 0; i < A.Length; i++)
            //{
            //    A[i] = i + 2;
            //}
            //Console.WriteLine(MissingElement(A).ToString());
            //CyclicRotation(new int[] { }, 5, 6);
            //Console.WriteLine(Program.BinaryGap(328).ToString());
        }

        public static int MissingInteger(int[] A)
        {
            byte[] check = new byte[A.Length];

            int max = 0;

            foreach (var n in A)
            {
                if (n > check.Length)
                    continue;
                if (n > 0)
                {
                    check[n - 1] = 1;
                }

                if (n > max)
                    max = n;
            }

            for(int i = 0; i <check.Length; i++)
            {
                if (check[i] == 0)
                    return i + 1;
            }

            return max + 1;
        }

        //public static int FrogRiverOne(int X, int[] A)
        //{
        //    HashSet<int> steps = new HashSet<int>();
        //    for (int i = 1; i <= X; i++)
        //    {
        //        steps.Add(i);
        //    }

        //    for (int i = 0; i < A.Length; i++)
        //    {
        //        if (steps.Contains(A[i]))
        //            steps.Remove(A[i]);
        //        if (steps.Count == 0)
        //        {
        //            return i;
        //        }
        //    }

        //    return -1;
        //}
        //public static int PermCheck(int[] A)
        //{
        //    byte[] check = new byte[A.Length];

        //    foreach (int n in A)
        //    {
        //        if (n > A.Length)
        //            return 0;

        //        check[n - 1] += 1;

        //        if (check[n - 1] > 2)
        //            return 0;
        //    }

        //    foreach (byte n in check)
        //    {
        //        if (n == 0)
        //            return 0;
        //    }

        //    return 1;
        //}

        //public static int TapeEquilibrium(int[] A)
        //{
        //    int sum = 0;
        //    for (int i = 1; i < A.Length; i++)
        //    {
        //        sum += A[i];
        //    }

        //    int right_sum = A[0];
        //    int min = Math.Abs(sum - right_sum);

        //    for (int i = 1; i < A.Length-1; i++)
        //    {
        //        right_sum += A[i];
        //        sum -= A[i];

        //        if (Math.Abs(sum - right_sum) < min )
        //        {
        //            min = Math.Abs(sum - right_sum);
        //        }
        //    }

        //    return min;
        //}
        //public static int FrogJmp(int X, int Y, int D)
        //{
        //    return (int)(Math.Ceiling((double) (Y - X) / (double) D));
        //}
        //public static int MissingElement(int[] A)
        //{
        //    if (A == null || A.Length == 0)
        //        return 1;

        //    double sum = ( (A.Length + 1.0) * ((A.Length + 1.0) + 1.0) )/ 2.0;

        //    foreach(int n in A)
        //    {
        //        sum -= n;
        //    }
        //    return (int)sum;
        //}

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
