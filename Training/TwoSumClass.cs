using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Training
{
    class TwoSumClass
    {
        public int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; ++i)
            {
                for (int j = 0; j < nums.Length; ++j)
                {
                    if (i != j && nums[i] + nums[j] == target)
                        return new int[] {i, j};
                }
            }

            return null;
        }

        public static bool IsPalindromeHelper(string s)
        {
            for (int i = 0; i < s.Length/2; i ++)
            {
                if (s[i] != s[s.Length - i - 1])
                    return false;
            }

            return true;
        }

        public static bool IsPalindrome(string s)
        {
            return IsPalindromeHelper(s.ToLowerInvariant().Where(c => c >= 'a' && c <= 'z' || c >= '0' && c <= '9').Aggregate(string.Empty, (current, c) => current + c));
        }
    }
}
