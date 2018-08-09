using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UglyNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 300;
            Console.WriteLine($"Is {i} Ugly? { (IsUgly(i) ? "Yes" : "No" )}");
            i = 7;
            Console.WriteLine($"Is {i} Ugly? { (IsUgly(i) ? "Yes" : "No")}");

            //find nth Ugly number;
            int j = 10;
            Console.WriteLine($"{j}th Ugly number is {NthUglyNumber(j)}");
        }

        static int NthUglyNumber(int n)
        {
            List<int> uglyNumbers = new List<int>();
            List<int> nonUglyNumbers = new List<int>();

            int i = 1;
            while (uglyNumbers.Count < n)
            {
                if (IsUglyMemo(i, ref uglyNumbers, ref nonUglyNumbers))
                {
                    uglyNumbers.Add(i);
                }
                else
                {
                    nonUglyNumbers.Add(i);
                }

                i++;
            }

            return uglyNumbers[n - 1];
        }

        static bool IsUgly(int number)
        {
            while (number % 2 == 0)
            {
                number = number / 2;
            }

            while (number % 3 == 0)
            {
                number = number / 3;
            }

            while (number % 5 == 0)
            {
                number = number / 5;
            }

            if (number == 1)
                return true;

            return false;
        }


        static bool IsUglyMemo(int number, ref List<int> uglyNumbers, ref List<int> nonUglyNumbers)
        {
            if (uglyNumbers.Contains(number))
                return true;
            if (nonUglyNumbers.Contains(number))
                return false;

            while (number % 2 == 0)
            {
                number = number / 2;

                if (uglyNumbers.Contains(number))
                    return true;
                if (nonUglyNumbers.Contains(number))
                    return false;
            }

            while (number % 3 == 0)
            {
                number = number / 3;

                if (uglyNumbers.Contains(number))
                    return true;
                if (nonUglyNumbers.Contains(number))
                    return false;
            }

            while (number % 5 == 0)
            {
                number = number / 5;

                if (uglyNumbers.Contains(number))
                    return true;
                if (nonUglyNumbers.Contains(number))
                    return false;
            }

            if (number == 1)
            {
                return true;
            }

            return false;
        }
    }
}
