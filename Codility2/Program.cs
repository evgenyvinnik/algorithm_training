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
            //Console.WriteLine(canPermutePalindrome("code").ToString());
            //Console.WriteLine(canPermutePalindrome("aab").ToString());
            //Console.WriteLine(NumJewelsInStones("aA", "aAAbbbb").ToString());
            //Console.WriteLine(NumJewelsInStones("z", "ZZ").ToString());

            //Console.WriteLine(Division(11, 345, 17).ToString());
            //Console.WriteLine(Division(4, 22, 5).ToString());
            //Console.WriteLine(Division(6, 12, 2).ToString());
            //Console.WriteLine(Division(1, 4,4).ToString());
            //Console.WriteLine(TapeEquilibrium(new int[] { -1000,1000, 3 }).ToString());
            //Console.WriteLine(TapeEquilibrium(new int[] { 3, 1, 2, 4, 3 }).ToString());
            //Console.WriteLine(FrogRiverOne(5, new int[] { 1, 3, 1, 4, 2, 3,  4 , 5}).ToString());
            //Console.WriteLine(MissingInteger(new int[] { 1, 3, 6, 4, 1, 2 }).ToString());
            //Console.WriteLine(MissingInteger(new int[] { -1, -3 }).ToString());
            //Console.WriteLine(MissingInteger(new int[] { 1, 2, 3 }).ToString());
            //Console.WriteLine(MissingInteger(new int[] { -1, 0, 1, 2, 3 }).ToString());
            //Console.WriteLine(MissingInteger(new int[] { 0 }).ToString());
            //Console.WriteLine(MissingInteger(new int[] { 1 }).ToString());
            //Console.WriteLine(MissingInteger(new int[] { 1000001 }).ToString());
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


            //var res = MaxCounters(5, new[] { 3, 4, 4, 6, 1, 4, 4 });
            //foreach (int n in res)
            //{
            //    Console.WriteLine(n.ToString());
            //}

            //Console.WriteLine(NailingPlanks(new int[] {1,4,5,8 }, new int[] {4,5,9,10 }, new int[] {4,6,7,10,2 }).ToString());

            //int[][] b = new int[][] {new[]{ 1, 1, 0 }, new[] { 1, 0, 1 }, new[] { 0, 0, 0 } };
            //FlipAndInvertImage(b );
            //int[][] a = new int[][] { new[] { 1, 1, 0, 0 }, new[] { 1, 0, 0, 1 }, new[] { 0, 1, 1, 1 }, new[] { 1, 0, 1, 0 } };
            //FlipAndInvertImage(a);
            //ZigzagIterator i = new ZigzagIterator(new int[]{1, 2}, new int[] { 3, 4, 5, 6});
            //while (i.HasNext())
            //    Console.WriteLine( i.Next());
        }

        //public class ZigzagIterator
        //{
        //    //pair? named tuple?
        //    List<IList<int>> lists;
        //    List<int> positions;

        //    int selected;

        //    public ZigzagIterator(IList<int> v1, IList<int> v2)
        //    {
        //        lists = new List<IList<int>> {v1, v2};
        //        positions = new List<int> {0, 0};
        //        selected = 0;
        //    }

        //    public bool HasNext()
        //    {
        //        bool hasNext = false;
        //        int local_selected = selected;
        //        for (int i = 0; i < lists.Count; i++)
        //        {
        //            if (positions[local_selected] < lists[local_selected].Count)
        //            {
        //                hasNext = true;
        //                break;
        //            }
        //            local_selected++;
        //            if (local_selected == lists.Count)
        //                local_selected = 0;
        //        }

        //        return hasNext;
        //    }

        //    public int Next()
        //    {
        //        int value = 0;
        //        bool value_found = false;

        //        for (int i = 0; i < lists.Count; i++)
        //        {
        //            if (positions[selected] < lists[selected].Count)
        //            {
        //                value = lists[selected][positions[selected]];
        //                positions[selected]++;
        //                value_found = true;
        //            }

        //            selected++;
        //            if (selected == lists.Count)
        //                selected = 0;

        //            if (value_found)
        //                break;
        //        }


        //        return value;
        //    }
        //}


        //public static int[][] FlipAndInvertImage(int[][] A)
        //{
        //    for (int j = 0; j < A.GetLength(0); j++)
        //    {
        //        for (int i = 0; i < Math.Round(Math.Ceiling(A[j].GetLength(0) / 2.0)); i++)
        //        {
        //            if (i == A.Length - i - 1)
        //            {
        //                A[j][i] = A[j][i] == 0 ? 1 : 0;
        //                continue;
        //            }

        //            int c = A[j][i];
        //            A[j][i] = A[j][A.Length - i - 1] == 0 ? 1 : 0;
        //            A[j][A.Length - i - 1] = c == 0 ? 1 : 0;
        //        }
        //    }

        //    return A;
        //}

        //public static int[] MaxCounters(int N, int[] A)
        //{
        //    int[] res = new int[N];

        //    int max = 0;
        //    int refresh_at = 0; ;
        //    foreach (var n in A)
        //    {
        //        if (n > N)
        //        {
        //            refresh_at = max;
        //        }
        //        else
        //        {
        //            if(res[n - 1] < refresh_at)
        //            {
        //                res[n - 1] = refresh_at;
        //            }

        //            res[n - 1]++;

        //            if (max < res[n - 1])
        //            {
        //                max = res[n - 1];
        //            }
        //        }
        //    }

        //    for (int i = 0; i < res.Length; i++)
        //    {
        //        if (res[i] < refresh_at)
        //        {
        //            res[i] = refresh_at;
        //        }
        //    }

        //    return res;
        //}

        //public static int NailingPlanks(int[] A, int[] B, int[] C)
        //{
        //    int[] n = new 
        //}

        //public static int Division(int A, int B, int K)
        //{
        //    if (A == B)
        //    {
        //        if (A % K == 0)
        //            return 1;
        //        else
        //            return 0;
        //    }

        //    int a_div = A / K;
        //    int a_mod = A % K;

        //    int new_A = A;
        //    if(a_mod != 0)
        //        new_A += (K - a_mod);

        //    int b_div = B / K;
        //    int b_mod = B % K;

        //    int new_B = b_div * K;

        //    if (new_B == new_A)
        //    {
        //        return 1;
        //    }

        //    int res = (new_B - new_A) / K;
        //    if (res < 0)
        //        return 0;

        //    if (new_A != 0)
        //        res++;

        //    return res;
        //}

        //public static int NumJewelsInStones(string J, string S)
        //{
        //    Dictionary<char, int> frequency = new Dictionary<char, int>();

        //    foreach (char c in S)
        //    {
        //        if (frequency.ContainsKey(c))
        //        {
        //            frequency[c]++;
        //        }
        //        else
        //        {
        //            frequency.Add(c, 1);
        //        }
        //    }

        //    int jewels = 0;
        //    foreach (char c in J)
        //    {
        //        if (frequency.ContainsKey(c))
        //            jewels += frequency[c];
        //    }

        //    return jewels;
        //}

        //static bool canPermutePalindrome(string s)
        //{
        //    Dictionary<char, int> frequency = new Dictionary<char, int>();

        //    int odd = 0;
        //    foreach (char c in s)
        //    {
        //        if (frequency.ContainsKey(c))
        //        {
        //            frequency[c]++;
        //        }
        //        else
        //        {
        //            frequency.Add(c,1);
        //        }
        //    }

        //    foreach (KeyValuePair<char, int> c in frequency)
        //    {
        //        if (c.Value % 2 != 0)
        //        {
        //            odd++;
        //        }
        //    }

        //    if (s.Length % 2 == 0)
        //    {
        //        if (odd != 0)
        //            return false;
        //    }
        //    else
        //    {
        //        if (odd != 1)
        //            return false;
        //    }

        //    return true;
        //}

        //public static int MissingInteger(int[] A)
        //{
        //    byte[] check = new byte[A.Length];

        //    int max = 0;

        //    foreach (var n in A)
        //    {
        //        if (n > check.Length)
        //            continue;
        //        if (n > 0)
        //        {
        //            check[n - 1] = 1;
        //        }

        //        if (n > max)
        //            max = n;
        //    }

        //    for(int i = 0; i <check.Length; i++)
        //    {
        //        if (check[i] == 0)
        //            return i + 1;
        //    }

        //    return max + 1;
        //}

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
