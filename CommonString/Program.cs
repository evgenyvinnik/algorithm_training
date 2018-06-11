using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonString
{
    class Program
    {
        static void Main(string[] args)
        {
            //string A = "banana";
            //string B = "baonbab";

            //Console.WriteLine($"{LongestCommonSubsequence(A, B, A.Length, B.Length)}");
            //Console.WriteLine($"{LongestPalindrome(A)}");


            int[] res = roundPricesToMatchTarget(new float[] { 0.7f, 2.80f, 4.90f }, 8);
            foreach(var n in res)
            {
                Console.WriteLine(n);
            }
        }

        static int[] actions;
        float currentError = float.MaxValue;


        static int[] roundPricesToMatchTarget(float[] prices, int target)
        {

            int[] res = new int[prices.Length];
            actions = new int[prices.Length];

            float roundingCeil = digAnswer(prices, 0, true, target, actions, currentError);
            float roundingFloor = digAnswer(prices, 0, false, target,actions, currentError);

            if(roundingCeil < roundingFloor)
            {
                actions[0] = 1;
            }
            else
            {
                actions[0] = -1;
            }

            for(int i = 0; i < prices.Length; i++)
            {
                if(actions[i] == 1)
                {
                    res[i] = (int)Math.Ceiling(prices[i]);
                }
                else
                {
                    res[i] = (int)Math.Floor(prices[i]);
                }
            }

            return res;
        }


        static float digAnswer(float[] prices, int index, bool ceil, int target, int[] actions, float currentError)
        {
            if (ceil)
            {
                target = target - (int)Math.Ceiling(prices[index]);
                currentError += (float)Math.Abs(prices[index] - (int)Math.Ceiling(prices[index]));
                //actions[index] = 1;
            }
            else
            {
                target = target - (int)Math.Floor(prices[index]);
                currentError += (float)Math.Abs(prices[index] - (int)Math.Floor(prices[index]));
                //actions[index] = -1;
            }

            if (index == prices.Length-1)
            {
                if (target == 0)
                {
                    return currentError;
                }
                else return float.MaxValue;
            }
            else if (target <=0)
                return float.MaxValue;

            float roundingCeil = digAnswer(prices, index + 1, true, target, actions, currentError);
            float roundingFloor = digAnswer(prices, index + 1, false, target, actions, currentError);

            if (roundingCeil < roundingFloor)
            {
                actions[index + 1] = 1;
                return roundingCeil;
            }
            else
            {
                actions[index + 1] = -1;
                return roundingFloor;
            }
        }

        //static string LongestCommonSubsequence(string A, string B, int aLength, int bLength)
        //{
        //    //if(aLength == 0 || bLength == 0)
        //    //    return "";

        //    int[,] L = new int[aLength+1,bLength+1];

        //    for (int i = 0; i <= aLength; i++)
        //    {
        //        for (int j = 0; j <= bLength; j++)
        //        {
        //            if (i == 0 || j == 0)
        //                L[i, j] = 0;
        //            else if (A[i - 1] == B[j - 1])
        //                L[i, j] = L[i - 1, j - 1] + 1;
        //            else
        //            {
        //                L[i, j] = Math.Max(L[i - 1, j], L[i, j - 1]);
        //            }
        //        }
        //    }

        //    int index = L[aLength, bLength];
        //    int temp = index;

        //    char[] lsc = new char[index + 1];
        //    lsc[index] = '\0';

        //    int k = aLength, l = bLength;
        //    while (k > 0 && l > 0)
        //    {
        //        if (A[k-1] == B[l-1])
        //        {
        //            lsc[index - 1] = A[k - 1];

        //            k--;
        //            l--;
        //            index--;
        //        }
        //        else if (L[k-1, l] > L[k, l-1])
        //        {
        //            k--;
        //        }
        //        else
        //        {
        //            l--;
        //        }
        //    }

        //    return new string(lsc);
        //}

        //static string LongestPalindrome(string A)
        //{
        //    return "";
        //}
    }
}
