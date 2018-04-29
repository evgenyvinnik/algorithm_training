using System;
using System.Collections.Generic;
using System.Text;

namespace Training
{
    class SelfDividing
    {

        private static bool SelfDividingNumber(int i)
        {
            HashSet<int> digits = new HashSet<int>();
            
            foreach(char c in i.ToString())
            {
                digits.Add(c - '0');
            }

            if (digits.Contains(0))
                return false;

            foreach(int digit in digits)
            {
                if (i % digit != 0)
                    return false;
            }

            return true;
        }

        public static IList<int> SelfDividingNumbers(int left, int right)
        {
            List<int> result = new List<int>();

            for (int i = left; i <= right; ++i)
            {
                if (SelfDividingNumber(i))
                {
                    result.Add(i);
                }
            }

            return result;
        }
    }
}
